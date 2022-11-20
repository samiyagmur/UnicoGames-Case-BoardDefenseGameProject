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

        private void Awake()
        {
           
        }
        private LevelData GetData() => Resources.Load<Cd_LevelData>(_dataPath).LevelData[_levelID];

        private void OnEnable()
        {
            SubscribeEvents();
            Init();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize += OnLevelInitilize;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onFail += OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel; 
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize -= OnLevelInitilize;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onFail -= OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        }
        private void OnDisable() => UnsubscribeEvents();
        private void Init()
        {
            CoreGameSignals.Instance.onLevelInitilize?.Invoke(_levelID);
            InitSpawnPosition();
        }

        private void InitSpawnPosition()
        {
            
        }

        private void OnLevelInitilize(int _levelID)
        {
            levelLoaderCommand.InsitializeLevel(_levelID, levelholder);
        }
        private void OnPlay()
        {
         
        }
        private void OnFail()
        {
            
        }

        private void OnLevelSuccessfull()
        {
            
        }
        private void OnNextLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        }
        private void OnClearActiveLevel()
        {
            //clearActiveLevelCommand.ClearActiveLevel(levelholder);
            CoreGameSignals.Instance.onLevelInitilize?.Invoke(_levelID);
            CoreGameSignals.Instance.onReset?.Invoke();
        }
        private void OnReset()
        {
  

        }
       

        
    }
}