using Command;
using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Interfaces;
using Manager;
using Signals;
using System;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private Transform levelholder;

        [SerializeField]
        private LevelLoaderCommand levelLoaderCommand;

        [SerializeField]
        private ClearActiveLevelCommand clearActiveLevelCommand;
        [SerializeField]
        private int _levelID;

        private ISaver _saver;
        private ILoader _loader;
        private LevelData _LevelData;

        private void Awake()
        {
            _saver = new SaveLoadManager();
            _loader = new SaveLoadManager();
            gameLoad();

            _LevelData = GetData();
        }

        private void gameLoad()
        {
            Load();
        }

        private const string _dataPath = "Data/Cd_LevelData";

        private LevelData GetData() => Resources.Load<Cd_LevelData>(_dataPath).LevelData[_levelID];

        private void OnEnable()
        {
            SubscribeEvents();

        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize += OnLevelInitilize;
            CoreGameSignals.Instance.onFail += OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onGetLevelDataWhenSpawn += OnGetLevelDataWhenSpawn;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize -= OnLevelInitilize;
            CoreGameSignals.Instance.onFail -= OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onGetLevelDataWhenSpawn -= OnGetLevelDataWhenSpawn;
        }
        private void OnDisable() => UnsubscribeEvents();

        private void Start()
        {
            Init();
        }
        private void Init()
        {
            CoreGameSignals.Instance.onLevelInitilize(_levelID);

            CoreGameSignals.Instance.onGetLevelData?.Invoke(GetData());


        }

        private void OnLevelInitilize(int levelID)
        {
            levelLoaderCommand.InsitializeLevel(levelID, levelholder);
  
        }

        private void OnFail()
        {

            CoreGameSignals.Instance.onReset?.Invoke();
        }

        private void OnLevelSuccessfull()
        {
      
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();


        }

        private void OnClearActiveLevel()
        {
   
            clearActiveLevelCommand.ClearActiveLevel(levelholder);
            CoreGameSignals.Instance.onReset?.Invoke();



        }
        private void OnNextLevel()
        {
  
            _levelID++;
            CoreGameSignals.Instance.onGetLevelData?.Invoke(GetData());
            CoreGameSignals.Instance.onLevelInitilize(_levelID);
            CoreGameSignals.Instance.onPlay?.Invoke();
            Save();
        }

        private LevelData OnGetLevelDataWhenSpawn()
        {
            return GetData();
        }
        private void Save()
        {
            _saver.UpdateSave(_LevelData);
        }

        private void Load()
        {
            _LevelData = _loader.UpdateLoad<LevelData>();

            OnLevelSuccessfull(); ;

        }




    }
}