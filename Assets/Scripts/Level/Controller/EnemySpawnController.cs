using Data.ValueObject;
using Interfaces;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Type;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controller
{
    public class EnemySpawnController : MonoBehaviour,IPullObject
    {
        private Dictionary<EnemyType, EnemyCharacterData> _characterData;
        [SerializeField]
        private List<Vector3> spawnPoints=new List<Vector3>();

        [SerializeField]
        private List<EnemyType> willSpawnEnemy = new List<EnemyType>();

        private EnemySpawnData _enemyspawndata;

        internal void SetData(Dictionary<EnemyType, EnemyCharacterData> enemySpawnData)
        {
            _characterData = enemySpawnData;
        }

        private void Start()
        {
            StackSpawnedEnemyAsType();
        }
        private async void StackSpawnedEnemyAsType()
        {
            for (int i = 0; i < _characterData.Count; i++)
            {
                 _enemyspawndata=_characterData[(EnemyType)i].enemySpawnData;

                for (int j = 0;j < _enemyspawndata.numberOFEnemy; j++)
                {
                   

                    willSpawnEnemy.Add((EnemyType)i);

                    int persentace=Random.Range(0, _enemyspawndata.numberOFEnemy);

                    if (persentace < _enemyspawndata.percentOfSpawn)
                    {
                        await Task.Delay(4000);
                        SpawnEnemy((EnemyType)i);

                        willSpawnEnemy.RemoveAt(willSpawnEnemy.Count-1);

                        willSpawnEnemy.TrimExcess();
                    }
              
                }

            }
        }



        internal void SetSpawnPoint(List<GridElement> levelGridElementList)
        {   
            float gridElementScaleYAxis= levelGridElementList[0].gridElement.transform.localScale.y;

            for (int i =0 ; i < levelGridElementList.Count ; i++)
            {
                if (levelGridElementList[i]._height== levelGridElementList.Count-1)
                {   
                    spawnPoints.Add(new Vector3(levelGridElementList[i]._width, gridElementScaleYAxis/2+0.1f, levelGridElementList[i]._height));
                }
            }
        }

        private  void SpawnEnemy(EnemyType type)
        {   
            

            int randomSpawnPoint = Random.Range(0,spawnPoints.Count+1);

            GameObject willSpawnEnemy = PullFromPool((PoolObjectType)(int)type);

            willSpawnEnemy.transform.position = spawnPoints[randomSpawnPoint];

        }

        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolObjectType);
        }

    }
}