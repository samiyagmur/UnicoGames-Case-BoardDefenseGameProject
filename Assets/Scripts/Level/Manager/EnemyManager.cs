using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Interfaces;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using Type;
using UnityEngine;

namespace Manager
{
    public class EnemyManager : MonoBehaviour,IPushObject
    {
        [SerializeField]
        private EnemyHealtController enemyHealtController;
        [SerializeField]
        private EnemyMovementController enemyMovementController;

        [SerializeField]
        private EnemyType _enemyType;

        private LevelData _levelData;



        private void Start()
        {
            _levelData = CoreGameSignals.Instance.onGetLevelDataWhenSpawn?.Invoke();

            Init(_levelData);
        }

        private void OnGetLevelData( LevelData levelData)
        {
            _levelData = levelData;
            Init(_levelData);
            Debug.Log(_levelData.EnemyData.enemies[_enemyType].Healt);
        }

        private void Init(LevelData levelData)
        {
          
            enemyHealtController.SetData(levelData.EnemyData.enemies[_enemyType]);
            enemyMovementController.SetData(levelData.EnemyData.enemies[_enemyType]);
       
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData += OnGetLevelData;
            CoreGameSignals.Instance.onReset+=OnReset;

            
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData -= OnGetLevelData;
            CoreGameSignals.Instance.onReset -= OnReset;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();

        }
        internal void WhenHitDefender()
        {
            enemyMovementController.StopToMoveForward();
        }

        internal void WhenHitBullet(int damager)
        {
            enemyHealtController.DecreaseHealt(damager,_enemyType);
        }
        internal void IsDeadEnemy(bool IsDead)
        {
            EnemySignals.Instance.onEnemyDeadFromDefander?.Invoke();

            ScoreSignals.Instance.onUpdateGold(_levelData.EnemyData.enemies[_enemyType].EarnedGold);
            
            //reset/partical/animator
        }

        internal void IsGemDropFromEnemy(bool IsDropGem)
        {
            ScoreSignals.Instance.onUpdateGem(_levelData.EnemyData.enemies[_enemyType].EarnGem);
        }

        public int GetDamageWhenHitDefender()
        {
            return _levelData.EnemyData.enemies[_enemyType].Damage;
        }

        private void OnReset()
        {
            PushToPool((PoolObjectType)(int)_enemyType, gameObject);
        }
        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolObjectType, obj);
        }
    }
}