using Data.UnityObject;
using Data.ValueObject;
using Interfaces;
using Manager;
using Signals;
using Sirenix.OdinInspector;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private ScoreData _scoreData;

        private ISaver _saver;
        private ILoader _loader;
        private string _dataPath = "Data/Cd_ScoreData";
        private int _diamondScore=0;
        private int _goldScore=0;

        private void Awake()
        {
            GetData();
            InitSave();

        }

        private void InitSave()
        {
            _saver = new SaveLoadManager();
            _loader = new SaveLoadManager();

        }
        private void Start()
        {
            InitData();
        }
        private void InitData()
        {
            _diamondScore = _scoreData.LastDiamondScore;
            _goldScore = _scoreData.LastGoldScore;
            ScoreSignals.Instance.onInitLastDiamondScore?.Invoke(_diamondScore);
            ScoreSignals.Instance.onInitLastGoldScore?.Invoke(_goldScore);
        }

        public void GetData()
        {
            _scoreData = Resources.Load<Cd_ScoreData>(_dataPath).ScoreData;
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {

            ScoreSignals.Instance.onUpdateGold += OnUpdateGold;
            ScoreSignals.Instance.onUpdateGem += OnUpdateGem;
           // CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {

            ScoreSignals.Instance.onUpdateGold -= OnUpdateGold;
            ScoreSignals.Instance.onUpdateGem -= OnUpdateGem;
            //CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnUpdateGold(int takenGold)
        {

            if (_goldScore > 0)
            {
                _goldScore += takenGold;
                ScoreSignals.Instance.onInitLastGoldScore?.Invoke(_goldScore);
            }
            else
            {
                _goldScore = 0;
            }

  
        }

        private void OnUpdateGem(int takenDiamond)
        {
            if (_diamondScore > 0)
            {
                _diamondScore += takenDiamond;
                ScoreSignals.Instance.onInitLastDiamondScore?.Invoke(_diamondScore);
            }
            else
            {
                _diamondScore = 0;
            }
            
        
        }

        private void Save()
        {
           // _saver.UpdateSave(_scoreData);
            
        }
        
        private void Load()
        {
            //_scoreData = _loader.UpdateLoad<ScoreData>();

           

        }

    }
}