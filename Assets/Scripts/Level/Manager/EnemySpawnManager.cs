using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnController spawnController;

        private int _deadCount;
        private int _totalSpawnedEnemy;
        private LevelData _levelData;

        private void OnGetLevelData( LevelData levelData)
        {
            _levelData = levelData;
            Init(levelData);
        }

        private void Init(LevelData levelData)
        {
            spawnController.SetData(levelData.EnemyData.enemies);

            _totalSpawnedEnemy = spawnController.SpawnObjectCount();
        }
      
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            EnemySignals.Instance.onLevelInit += onLevelInit;
            CoreGameSignals.Instance.onGetLevelData+=OnGetLevelData;
            CoreGameSignals.Instance.onReset += OnReset;
            EnemySignals.Instance.onDeadEnemy += OnDeadEnemy;
            EnemySignals.Instance.onEnemyDeadFromDefander += OnGetSpawnEnemyCount;
            

        }

        private void UnsubscribeEvents()
        {
            EnemySignals.Instance.onEnemyDeadFromDefander -= OnGetSpawnEnemyCount;
            EnemySignals.Instance.onDeadEnemy -= OnDeadEnemy;
            EnemySignals.Instance.onLevelInit -= onLevelInit;
            CoreGameSignals.Instance.onGetLevelData -= OnGetLevelData;
            CoreGameSignals.Instance.onReset -= OnReset;

        }

        private void OnDisable() => UnsubscribeEvents();

        private void onLevelInit(List<GridElements> levelGridElementList)
        {
            spawnController.SetSpawnPoint(levelGridElementList);
        }


        private void OnDeadEnemy()
        {
            _totalSpawnedEnemy--;

            if (_totalSpawnedEnemy == 0)
            {
                if (_deadCount > ( _totalSpawnedEnemy - _levelData.FailAmount))
                {
                    CoreGameSignals.Instance.onLevelSuccessfull?.Invoke();
                }
            }
        }

        private void OnGetSpawnEnemyCount()
        {
            _deadCount++;
        }

        private void OnReset()
        {
            _deadCount=0;
            _totalSpawnedEnemy=0;
        }


    }
}