using Command;
using Controller;
using Data.UnityObject;
using Data.ValueObject;
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
            Debug.Log("OnLevelInitilize");
        }

        private void OnFail()
        {
            Debug.Log("OnFail");
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        private void OnLevelSuccessfull()
        {
            Debug.Log("OnLevelSuccessfull");
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();


        }

        private void OnClearActiveLevel()
        {
            Debug.Log("OnClearActiveLevel");
            clearActiveLevelCommand.ClearActiveLevel(levelholder);
            CoreGameSignals.Instance.onReset?.Invoke();



        }
        private void OnNextLevel()
        {
            Debug.Log("OnNextLevel");
            _levelID++;
            CoreGameSignals.Instance.onGetLevelData?.Invoke(GetData());
            CoreGameSignals.Instance.onLevelInitilize(_levelID);
            CoreGameSignals.Instance.onPlay?.Invoke();

        }

        private LevelData OnGetLevelDataWhenSpawn()
        {
            return GetData();
        }


    }
}