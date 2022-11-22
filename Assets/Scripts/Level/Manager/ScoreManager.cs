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
        private ScoreData _scoreData;

        //private ISaver _saver;
        //private ILoader _loader;
        private string _dataPath = "Data/Cd_ScoreData";
        private void Awake()
        {
            GetData();
            // SetInstance();
            //InitData(); 
     
        }

        private void Start()
        {
            InitData();
        }
        private void InitData()
        {   
            ScoreSignals.Instance.onInitLastDiamondScore?.Invoke(_scoreData.LastDiamondScore);
            ScoreSignals.Instance.onInitLastGoldScore?.Invoke(_scoreData.LastGoldScore);

        }

        public void GetData()
        {
            _scoreData = Resources.Load<Cd_ScoreData>(_dataPath).ScoreData;
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
           // ScoreSignals.Instance.onScoreMultiply += OnScoreMultiply;
            ScoreSignals.Instance.onUpdateGold += OnUpdateGold;
            ScoreSignals.Instance.onUpdateGem += OnUpdateGem;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
        }

        private void UnsubscribeEvents()
        {
           // ScoreSignals.Instance.onScoreMultiply -= OnScoreMultiply;
            ScoreSignals.Instance.onUpdateGold -= OnUpdateGold;
            ScoreSignals.Instance.onUpdateGem -= OnUpdateGem;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
        }


        private void OnDisable() => UnsubscribeEvents();

        private void OnUpdateGold(int takenGold)
        {
            _scoreData.LastGoldScore += takenGold;

            ScoreSignals.Instance.onInitLastGoldScore?.Invoke(_scoreData.LastGoldScore);
        }

        private void OnUpdateGem(int takenDiamond)
        {
            _scoreData.LastDiamondScore += takenDiamond;

            ScoreSignals.Instance.onInitLastDiamondScore?.Invoke(_scoreData.LastDiamondScore);
        }

        //private void OnScoreMultiply(int score)
        //{
        //    _currentScore *= score;
        //}

        private void OnLevelSuccessfull()
        {
            UISignals.Instance.onSetTopScore?.Invoke(_scoreData.RankedScore);
        }

        private void OnReset()
        {
           
        }
        //[Button]//ForTesting
        //private void Save()
        //{
        //    _saver.UpdateSave(_scoreData);
        //}
        //[Button]//ForTesting
        //private void Load()
        //{
        //    _scoreData= _loader.UpdateLoad<ScoreData>();

        //    OnLevelSuccessfull(); ;

        //}

    }
}