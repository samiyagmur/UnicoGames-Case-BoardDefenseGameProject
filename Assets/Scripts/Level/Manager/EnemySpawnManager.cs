using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using System;
using System.Collections.Generic;
using Type;
using UnityEngine;

namespace Manager
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnController spawnController;

        private int _deadCountFromDefender;
        private int _totalSpawnedEnemy;
        private LevelData _levelData;
        private int _passEnemyCountFromPortal;
        private int _totalDeat;

        private void OnGetLevelData( LevelData levelData)
        {
            _levelData = levelData;
            Init(levelData);
         
        }

        private void Init(LevelData levelData)
        {
            spawnController.SetData(levelData.EnemyData.enemies);

         
           
        }

        private void OnPlay()
        {

            spawnController.StartSpawn();
            _totalSpawnedEnemy = spawnController.GetEnemyTotalCountOnBoard();
       
           
        }


        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            EnemySignals.Instance.onLevelInit += OnLevelInit;
            CoreGameSignals.Instance.onGetLevelData+=OnGetLevelData;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
            EnemySignals.Instance.onPassEnemyFromPortal += OnGetSpawnEnemyCount;
            EnemySignals.Instance.onEnemyDeadFromDefander += OnGetSpawnEnemyCount;
            

        }

        private void UnsubscribeEvents()
        {
            EnemySignals.Instance.onEnemyDeadFromDefander -= OnGetSpawnEnemyCount;
            EnemySignals.Instance.onPassEnemyFromPortal -= OnGetSpawnEnemyCount;
            EnemySignals.Instance.onLevelInit -= OnLevelInit;
            CoreGameSignals.Instance.onGetLevelData -= OnGetLevelData;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;

        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnLevelInit(List<GridElements> levelGridElementList)
        {
            spawnController.SetSpawnPoint(levelGridElementList);
        }


        private void OnGetSpawnEnemyCount()
        {
            _totalSpawnedEnemy--; 

            if (_totalSpawnedEnemy==0)
            {
                CoreGameSignals.Instance.onLevelSuccessfull?.Invoke();
            }
        }
        private void OnReset()
        {
            _deadCountFromDefender = 0;

            spawnController.Reset();
        }


    }
}