using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnController spawnController;

        private int _levelID;

        private const string _dataPath = "Data/Cd_LevelData";

        private void Start()
        {
            Init();

          
        }

        private void Init()
        {
      
            spawnController.SetData(GetData().enemies);
        }
        private EnemyData GetData() => Resources.Load<Cd_LevelData>(_dataPath).LevelData[_levelID].EnemyData;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            EnemySignals.Instance.onLevelInitilize += OnLevelInitilize;
            CoreGameSignals.Instance.onLevelInitilize+=OnLevelInit;
        }

        private void UnsubscribeEvents()
        {
            EnemySignals.Instance.onLevelInitilize -= OnLevelInitilize;
            CoreGameSignals.Instance.onLevelInitilize -= OnLevelInit;

        }

        private void OnDisable() => UnsubscribeEvents();


        private void OnLevelInitilize(List<GridElements> levelGridElementList)
        {
            spawnController.SetSpawnPoint(levelGridElementList);
        }

        private void OnLevelInit(int levelId)
        {
            _levelID = levelId;
        }

    }
}