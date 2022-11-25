using Command;
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

        private int _levelID;

        private ISaver _saver;
        private ILoader _loader;

        private LevelSaveData _levelSaveData;

        private void Awake()
        {
            _saver = new SaveLoadManager();
            _loader = new SaveLoadManager();
            _levelSaveData = GetData().levelSave;
        }

        private const string _dataPath = "Data/Cd_LevelData";

        private Cd_LevelData GetData() => Resources.Load<Cd_LevelData>(_dataPath);

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize += OnLevelInitilize;
            CoreGameSignals.Instance.onFail += OnFail;
            CoreGameSignals.Instance.rePlay += RePlay;
            CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onGetLevelDataWhenSpawn += OnGetLevelDataWhenSpawn;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize -= OnLevelInitilize;
            CoreGameSignals.Instance.onFail -= OnFail;
            CoreGameSignals.Instance.rePlay -= RePlay;
            CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onGetLevelDataWhenSpawn -= OnGetLevelDataWhenSpawn;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void Start()
        {
            Load();

            Init();
        }

        private void Init()
        {
            CoreGameSignals.Instance.onLevelInitilize(_levelID);

           
        }

        private void OnLevelInitilize(int levelID)
        {
            levelLoaderCommand.InsitializeLevel(levelID, levelholder);

            CoreGameSignals.Instance.onGetLevelData?.Invoke(GetData().LevelData[_levelID]);
        }

        private void OnFail()
        {
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        private void RePlay()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitilize(_levelID);
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

            GetData().levelSave.SetLevelID(_levelID);
            CoreGameSignals.Instance.onLevelInitilize(_levelID);
            CoreGameSignals.Instance.onPlay?.Invoke();
            Save();
        }

        private LevelData OnGetLevelDataWhenSpawn()
        {
            return GetData().LevelData[_levelID];
        }

        private void Save()
        {
            //_saver.UpdateSave(GetData().levelSave._levelID);
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void Load()
        {
           // _levelID = _loader.UpdateLoad<int>();
        }
    }
}