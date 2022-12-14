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
    public class EnemySpawnController : MonoBehaviour, IPullObject
    {
        private Dictionary<EnemyType, EnemyCharacterData> characterData;

        [SerializeField]
        private List<Vector3> spawnPoints = new List<Vector3>();

        [SerializeField]
        private List<EnemyType> willSpawnEnemyList = new List<EnemyType>();

        private EnemySpawnData _enemyspawndata;

        private int _spawnedCount;

        private bool _isSpawn = false;
        [SerializeField]
        private List<GridElements> _levelGridElementList;

        internal void SetData(Dictionary<EnemyType, EnemyCharacterData> enemySpawnData)
        {
            this.characterData = enemySpawnData;
        }

        internal void StartSpawn()
        {
            _isSpawn = true;
            LoadSpawnList();
        }

        private void LoadSpawnList()
        {
            for (int i = 0; i < characterData.Count; i++)
            {
                _enemyspawndata = characterData[(EnemyType)i].enemySpawnData;

                for (int j = 0; j < _enemyspawndata.numberOFEnemy; j++)
                {
                    willSpawnEnemyList.Add((EnemyType)i);
                }
            }

            _spawnedCount = willSpawnEnemyList.Count;

            StartLoopForSpawn();
        }

        private async void StartLoopForSpawn()
        {
            while (willSpawnEnemyList.Count != 0 && _isSpawn)
            {
                await Task.Delay(500);
                ArrangePersentage();
            }
        }

        private void ArrangePersentage()
        {
            if (willSpawnEnemyList.Count <= 0) return;

            int selectRandomEnemy = Random.Range(0, characterData.Count);

            int percentage = Random.Range(0, 100);

            if (!willSpawnEnemyList.Contains((EnemyType)selectRandomEnemy)) return;

            if (percentage < characterData[(EnemyType)selectRandomEnemy].enemySpawnData.percentOfSpawn)
            {
                SpawnEnemy((EnemyType)selectRandomEnemy);

                willSpawnEnemyList.Remove((EnemyType)selectRandomEnemy);

                willSpawnEnemyList.TrimExcess();
            }
        }

        internal void SetSpawnPoint(List<GridElements> levelGridElementList)
        {
            _levelGridElementList = levelGridElementList;
            float _totalGridHeigh = levelGridElementList[0].TotalHeight;
            float _totalGridWeight = levelGridElementList[0].TotalWeight;

            float scaleX = levelGridElementList[0].Scale.x;
            float scaleY = levelGridElementList[0].Scale.z;

            for (int i = 0; i < levelGridElementList.Count; i++)//29 30 31 32
            {
                if (i >= (((_totalGridHeigh / scaleX) - 1) * (_totalGridWeight / scaleY)))
                {
                    spawnPoints.Add(new Vector3(levelGridElementList[i].GridElement.transform.position.x,
                                                levelGridElementList[i].GridElement.transform.position.y +
                                                levelGridElementList[i].GridElement.transform.localScale.y / 2 + 0.305f,
                                                levelGridElementList[i].GridElement.transform.position.z));
                }
            }
        }

        private void SpawnEnemy(EnemyType type)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Count);

            GameObject willSpawnEnemy = PullFromPool((PoolObjectType)(int)type);
            if(willSpawnEnemy!=null)
            willSpawnEnemy.transform.position = spawnPoints[randomSpawnPoint];
        }

        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolObjectType);
        }

        public int GetEnemyTotalCountOnBoard()
        {
            return _spawnedCount;
        }

        internal void Reset()
        {
            spawnPoints.Clear();
            spawnPoints.TrimExcess();
            willSpawnEnemyList.Clear();
            willSpawnEnemyList.TrimExcess();

            _isSpawn = false;
        }
    }
}