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
        private int _diamondScore;
        private int _goldScore;

        private void Awake()
        {
            GetData();
            InitSave();

        }

        private void InitSave()
        {
            _saver = new SaveLoadManager();
            _loader = new SaveLoadManager();
            Load();
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
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        }

        private void UnsubscribeEvents()
        {

            ScoreSignals.Instance.onUpdateGold -= OnUpdateGold;
            ScoreSignals.Instance.onUpdateGem -= OnUpdateGem;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnUpdateGold(int takenGold)
        {
            _goldScore += takenGold;

            ScoreSignals.Instance.onInitLastGoldScore?.Invoke(_goldScore);
        }

        private void OnUpdateGem(int takenDiamond)
        {
            _diamondScore += takenDiamond;

            ScoreSignals.Instance.onInitLastDiamondScore?.Invoke(_diamondScore);
        }

        private async void OnNextLevel()
        {
            await Task.Delay(100);
            Save();
        }

        private void OnLevelSuccessfull()
        {
            UISignals.Instance.onSetTopScore?.Invoke(_scoreData.RankedScore);
           _scoreData.LastDiamondScore = _diamondScore ;
           _scoreData.LastGoldScore=_goldScore;
            Save();
        }

        private void OnReset()
        {
            _goldScore = 0;
            _diamondScore = 0;
        }

        private void Save()
        {
            _saver.UpdateSave(_scoreData);
            
        }
        
        private void Load()
        {
            _scoreData = _loader.UpdateLoad<ScoreData>();

            OnLevelSuccessfull(); ;

        }

    }
}