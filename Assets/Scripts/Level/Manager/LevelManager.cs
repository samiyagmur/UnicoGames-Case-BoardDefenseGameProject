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

        private int _levelID;

        private const string _dataPath = "Data/Cd_LevelData";

        private LevelData GetData() => Resources.Load<Cd_LevelData>(_dataPath).LevelData[_levelID];

        private void OnEnable()
        {
            SubscribeEvents();
            Init();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize += OnLevelInitilize;
            CoreGameSignals.Instance.onFail += OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel; 
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize -= OnLevelInitilize;
            CoreGameSignals.Instance.onFail -= OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        }
        private void OnDisable() => UnsubscribeEvents();
        private void Init()
        {
            
            CoreGameSignals.Instance.onGetLevelData?.Invoke(GetData());

            CoreGameSignals.Instance.onLevelInitilize(_levelID);

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
            CoreGameSignals.Instance.onNextLevel?.Invoke();
        }
        private void OnNextLevel()
        {
            _levelID++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        }
        private void OnClearActiveLevel()
        {
            clearActiveLevelCommand.ClearActiveLevel(levelholder);
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onGetLevelData?.Invoke(GetData());
            
        }

    }
}