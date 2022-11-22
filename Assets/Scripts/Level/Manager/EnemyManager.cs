using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using System;
using System.Collections;
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

        private int _levelID;

        private const string _dataPath = "Data/Cd_LevelData";

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            enemyHealtController.SetData(GetData());
            enemyMovementController.SetData(GetData());
       
        }

        private EnemyCharacterData GetData() => Resources.Load<Cd_LevelData>(_dataPath).LevelData[_levelID].EnemyData.enemies[_enemyType];
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize += OnLevelInitilize;
            
        }


        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize -= OnLevelInitilize;

        }

        private void OnDisable() => UnsubscribeEvents();


        internal void WhenExitOnBoard()
        {
            //  enemyMovementController.StopToMoveForward();
        }

        internal void WhenSpawnOnBoard()
        {
            // enemyMovementController.StartToMoveForward();
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
          
            ScoreSignals.Instance.onUpdateGold(GetData().EarnedGold);

            //reset/partical/animator
        }

        internal void IsGemDropFromEnemy(bool IsDropGem)
        {
            ScoreSignals.Instance.onUpdateGem(GetData().EarnGem);
        }

        private void OnLevelInitilize(int levelID)
        {
            _levelID=levelID;
        }


        public int GetDamageWhenHitDefender()
        {
            return GetData().Damage;
        }
    }
}