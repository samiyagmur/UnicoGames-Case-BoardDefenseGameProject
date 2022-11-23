using Data.ValueObject;
using Interfaces;
using Signals;
using System.Collections.Generic;
using System.Threading.Tasks;
using Type;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controller
{
    public class EnemySpawnController : MonoBehaviour,IPullObject
    {
        private Dictionary<EnemyType, EnemyCharacterData> characterData;

        [SerializeField]
        private List<Vector3> spawnPoints=new List<Vector3>();

        [SerializeField]
        private List<EnemyType> willSpawnEnemyList = new List<EnemyType>();


        private EnemySpawnData _enemyspawndata;
        private int _spawnedCount;

        internal  void SetData(Dictionary<EnemyType, EnemyCharacterData> enemySpawnData)
        {
            this.characterData = enemySpawnData;

            StackSpawnedEnemyAsType();
        }

        private  async void StackSpawnedEnemyAsType()
        {

            
            for (int i=0; i < characterData.Count ;i++)
            {
                _enemyspawndata =characterData[(EnemyType)i].enemySpawnData;

                for (int j = 0 ; j < _enemyspawndata.numberOFEnemy; j++)
                {
                   willSpawnEnemyList.Add((EnemyType)i);//prometreus//permetreus//prometreus
                }
                
            }
            _spawnedCount = willSpawnEnemyList.Count;
            while (willSpawnEnemyList.Count!=0)
            {
                await Task.Delay(500);
                ArangePosibltyOFWillSpawnObject();
            }
        }


        private  void ArangePosibltyOFWillSpawnObject()
        {

            if (willSpawnEnemyList.Count <= 0) return;

            int selectRandomEnemy = Random.Range(0, characterData.Count);

            int percentage = Random.Range(0, 100);

            if (!willSpawnEnemyList.Contains((EnemyType)selectRandomEnemy))return ;

            if (percentage < characterData[(EnemyType)selectRandomEnemy].enemySpawnData.percentOfSpawn)
            {
                SpawnEnemy((EnemyType)selectRandomEnemy);

                willSpawnEnemyList.Remove((EnemyType)selectRandomEnemy);
 
                willSpawnEnemyList.TrimExcess();
            }

            
        }

        internal void SetSpawnPoint(List<GridElements> levelGridElementList)
        {
          
            float gridElementScaleYAxis = levelGridElementList[0]._gridElement.transform.localScale.y;

            float _totalGridHeigh  = levelGridElementList[0].TotalHeight;
            float _totalGridWeight = levelGridElementList[0].TotalWeight;

            float scaleX = levelGridElementList[0].Scale.x;
            float scaleY = levelGridElementList[0].Scale.z;

            for (int i = 0; i < levelGridElementList.Count; i++)//29 30 31 32
            {
                if (i >= (((_totalGridHeigh/ scaleX) - 1) * (_totalGridWeight/ scaleY)))
                {
                    spawnPoints.Add(new Vector3(levelGridElementList[i]._gridElement.transform.position.x,
                                                levelGridElementList[i]._gridElement.transform.position.y + 
                                                levelGridElementList[i]._gridElement.transform.localScale.y/2 +0.31f, 
                                                levelGridElementList[i]._gridElement.transform.position.z));
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

        public int SpawnObjectCount()
        {
            return _spawnedCount;
        }


    }
}