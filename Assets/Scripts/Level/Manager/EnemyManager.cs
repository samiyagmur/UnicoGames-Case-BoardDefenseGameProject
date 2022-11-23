using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using Type;
using UnityEngine;

namespace Manager
{
    public class EnemyManager : MonoBehaviour 
    {
        [SerializeField]
        private EnemyHealtController enemyHealtController;
        [SerializeField]
        private EnemyMovementController enemyMovementController;

        [SerializeField]
        private EnemyType _enemyType;

        private LevelData _levelData;
        private int deadAmountOnPortal;
        private int _spawnedEnemyCount;

        private void OnLevelInitilize( LevelData levelData)
        {
            _levelData = levelData;
            Init(levelData);
        }

        private void Init(LevelData levelData)
        {
          
            enemyHealtController.SetData(levelData.EnemyData.enemies[_enemyType]);
            enemyMovementController.SetData(levelData.EnemyData.enemies[_enemyType]);
       
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData += OnLevelInitilize;
            
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData -= OnLevelInitilize;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();

            EnemySignals.Instance.onDeadEnemy?.Invoke();

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

    }
}