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
        private float _timer;
        private int count;

        internal  void SetData(Dictionary<EnemyType, EnemyCharacterData> enemySpawnData)
        {
            _characterData = enemySpawnData;

            StackSpawnedEnemyAsType();
        }

        private  async void StackSpawnedEnemyAsType()
        {

            
            for (int i=0; i < _characterData.Count ;i++)
            {
                _enemyspawndata =_characterData[(EnemyType)i].enemySpawnData;

                for (int j = 0 ; j < _enemyspawndata.numberOFEnemy; j++)
                {
                   willSpawnEnemy.Add((EnemyType)i);//prometreus//permetreus//prometreus
                 
                }
                
            }

            while (willSpawnEnemy.Count!=0)
            {
                await Task.Delay(500);
                ArangePosibltyOFWillSpawnObject();
            }
        }


        private  void ArangePosibltyOFWillSpawnObject()
        {   

            if (willSpawnEnemy.Count <= 0) return;



            int selectRandomEnemy = Random.Range(0, _characterData.Count);

            int persentace = Random.Range(0, 100);

            Debug.Log(persentace);

            if (!willSpawnEnemy.Contains((EnemyType)selectRandomEnemy))return;

            if (persentace < _characterData[(EnemyType)selectRandomEnemy].enemySpawnData.percentOfSpawn)
            {
             
                Debug.Log((EnemyType)selectRandomEnemy);
          
                SpawnEnemy((EnemyType)selectRandomEnemy);
      
                willSpawnEnemy.Remove((EnemyType)selectRandomEnemy);
 
                willSpawnEnemy.TrimExcess();
            }
        }

        internal void SetSpawnPoint(List<GridElements> levelGridElementList)
        {
           
            float gridElementScaleYAxis = levelGridElementList[0]._gridElement.transform.localScale.y;

            int _totalGridHeigh  = levelGridElementList[0].TotalHeight;
            int _totalGridWeight = levelGridElementList[0].TotalWeight;

            for (int i = 0; i < levelGridElementList.Count; i++)//29 30 31 32
            {
                if (i >= ((_totalGridHeigh - 1) * _totalGridWeight))
                {
                    spawnPoints.Add(new Vector3(levelGridElementList[i].Width,
                            gridElementScaleYAxis+ 0.1f, levelGridElementList[i].Height));
                }
            }
        }


        private void SpawnEnemy(EnemyType type)
        {
  

            int randomSpawnPoint = Random.Range(0, spawnPoints.Count);

            GameObject willSpawnEnemy = PullFromPool((PoolObjectType)(int)type);


            willSpawnEnemy.transform.position = spawnPoints[randomSpawnPoint];

        }

        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolObjectType);
        }

    }
}