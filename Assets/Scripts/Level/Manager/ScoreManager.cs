using Data.UnityObject;
using Data.ValueObject;
using Interfaces;
using Manager;
using Signals;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField]
        private int _currentScore;

        private ScoreData _scoreData;

        private ISaver _saver;
        private ILoader _loader;
        private string _dataPath = "Data/Cd_ScoreData";
        private void Awake()
        {
            GetData();
            SetInstance();
            InitData(); 


        }

        private void SetInstance()
        {
            _saver  = new SaveLoadManager();
            _loader = new SaveLoadManager();
        }

        private void InitData()
        {   
            CoreGameSignals.Instance.onInitLastDiamondScore?.Invoke(_scoreData.LastDiamondScore);
            CoreGameSignals.Instance.onInitLastGoldScore?.Invoke(_scoreData.LastGoldScore);

        }

        public void GetData()
        {
            _scoreData = Resources.Load<Cd_ScoreData>(_dataPath).ScoreData;
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onScoreMultiply += OnScoreMultiply;
            ScoreSignals.Instance.onScoreTaken += OnScoreTaken;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onScoreMultiply -= OnScoreMultiply;
            ScoreSignals.Instance.onScoreTaken -= OnScoreTaken;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnScoreTaken()
        {
            _currentScore++;

            CoreGameSignals.Instance.onScoreUpdate?.Invoke(_currentScore);
        }

        private void OnScoreMultiply(int score)
        {
            _currentScore *= score;
        }

        private void OnLevelSuccessfull()
        {
            UISignals.Instance.onSetTopScore?.Invoke(_scoreData.RankedScore);
        }

        private void OnReset()
        {
            _currentScore = 0;
        }
        [Button]//ForTesting
        private void Save()
        {
            _saver.UpdateSave(_scoreData);
        }
        [Button]//ForTesting
        private void Load()
        {
            _scoreData= _loader.UpdateLoad<ScoreData>();

            OnLevelSuccessfull(); ;

        }

    }
}