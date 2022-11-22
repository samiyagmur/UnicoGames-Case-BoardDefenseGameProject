using Data.ValueObject;
using Interfaces;
using Manager;
using Signals;
using System;
using System.Collections;
using TMPro;
using Type;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controller
{
    public class EnemyHealtController : MonoBehaviour,IPushObject
    {
        private EnemyCharacterData _enemyCharacterData;

        [SerializeField]
        private EnemyManager enemyManager;

        [SerializeField]
        private TextMeshPro healt;

        

        internal void SetData(EnemyCharacterData enemyCharacterData)
        {
            _enemyCharacterData = enemyCharacterData;
            UpdateHealtText(_enemyCharacterData.Healt);
        }

        internal void DecreaseHealt(int damage, EnemyType _enemyType)
        {
            UpdateHealt(-damage, _enemyType);
        }

        private void UpdateHealt(int damage, EnemyType _enemyType)
        {
            _enemyCharacterData.Healt +=damage;
        
            UpdateHealtText(_enemyCharacterData.Healt);

            IsDeadEnemy(_enemyCharacterData.Healt, _enemyType);
        }

        private void IsDeadEnemy(int healt, EnemyType _enemyType)
        {
            if (healt<=0)
            {   
               
                PushToPool((PoolObjectType)(int)_enemyType, transform.parent.gameObject);

                enemyManager.IsDeadEnemy(true);

                int percent= Random.Range(0,100);

                if (percent<_enemyCharacterData.PercentOfDropGem)
                {
                    enemyManager.IsGemDropFromEnemy(true);
                }
            }
        }

        private void UpdateHealtText(int EnemyHealt)
        {
            healt.text = EnemyHealt.ToString();
        }

        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolObjectType, obj);
        }
    }
}