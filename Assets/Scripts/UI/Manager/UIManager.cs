using Controller;
using Data.ValueObject;
using Signals;
using System;
using System.Collections.Generic;
using TMPro;
using Type;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField]
        private UIPanelController uIPanelController;

        [SerializeField]
        private LevelPanelController levelPanelController;

        [SerializeField]
        private FailPanelController failPanelController;

        [SerializeField]
        private LevelSuccessfulPanelController levelSuccessfulPanel;

        [SerializeField]
        private TopScorePanelController topScorePanel;

        private int _lastGoldScore;
        private int _lastDiamondScore;
        private int _levelID;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
           // CoreGameSignals.Instance.onScoreReNew += OnScoreReNew;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetTopScore += OnSetTopScore;
            CoreGameSignals.Instance.onInitLastDiamondScore += OnInitLastDiamondScore;
            CoreGameSignals.Instance.onInitLastGoldScore += OnInitLastGoldScore;
            CoreGameSignals.Instance.onFail += OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
            CoreGameSignals.Instance.onLevelInitilize += OnLevelInitilize;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetTopScore -= OnSetTopScore;
            CoreGameSignals.Instance.onInitLastGoldScore -= OnInitLastGoldScore;
            CoreGameSignals.Instance.onInitLastDiamondScore -= OnInitLastDiamondScore;
            CoreGameSignals.Instance.onFail -= OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
            CoreGameSignals.Instance.onLevelInitilize -= OnLevelInitilize;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }



        private void OnDisable() => UnsubscribeEvents();


        private void OnOpenPanel(UIPanelType panelType) => uIPanelController.ChangePanel(panelType, true);

        internal void OnClosePanel(UIPanelType panelType) => uIPanelController.ChangePanel(panelType, false);

 
       
        private void OnLevelInitilize(int levelID)
        {
            _levelID = levelID;
            levelPanelController.InitLevelID(levelID);
            OnOpenPanel(UIPanelType.StartPanel);
        }
        internal void ChangePanelStatusOnPlay()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }
        private void OnPlay()
        {
            OnClosePanel(UIPanelType.StartPanel);
        }

        public void ChangePanelStatusOnStartAsSetting(UIPanelType uIPanelType)
        {
            OnOpenPanel(uIPanelType);
            OnClosePanel(UIPanelType.StartPanel);
        }
        internal void ChangePanelOnRestart()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        }

        internal void ChangePanelStatusOnExit(UIPanelType uIPanelType)
        {
            OnOpenPanel(uIPanelType);
            OnClosePanel(UIPanelType.LevelPanel);
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        }
       
        private void OnFail()
        {
            failPanelController.SetDiamondScore(_lastDiamondScore);
            failPanelController.SetGoldScore(_lastGoldScore);
            OnOpenPanel(UIPanelType.FailPanel);
        }

        internal void ChangePanelStatusOnPressTryAgain(UIPanelType uIPanelType)
        {
             OnClosePanel(uIPanelType);
             OnOpenPanel(UIPanelType.StartPanel);
        }
        private void OnLevelSuccessfull()
        {
            levelSuccessfulPanel.SetCurrenLevel(_levelID);
            OnOpenPanel(UIPanelType.LevelSuccesful);
        }
        internal void ChangePanelStatusOnPressNextLevel(UIPanelType uIPanelType)
        {
            OnClosePanel(uIPanelType);
            OnOpenPanel(UIPanelType.LevelPanel);
            CoreGameSignals.Instance.onNextLevel?.Invoke();
        }
        private void OnReset()
        {
            levelPanelController.InitDiamondScore(0);
            levelPanelController.InitGoldScore(0);
        }

        internal void ChangePanelStatusOnTopScore(UIPanelType uIPanelType)
        {
            OnClosePanel(uIPanelType);
            OnOpenPanel(UIPanelType.StartPanel);
        }

        internal void ChangePanelStatusOnSetting(UIPanelType uIPanelType)
        {
            OnClosePanel(uIPanelType);
            OnOpenPanel(UIPanelType.StartPanel);
        }

       
        private void OnSetTopScore(List<RankedScore> value)
        {
            topScorePanel.SetTopScore(value);
        }
        private void OnInitLastDiamondScore(int value)
        {
            _lastDiamondScore = value;
            levelPanelController.InitDiamondScore(value);
        }

        internal void OnInitLastGoldScore(int value)
        {
            _lastGoldScore = value;
            levelPanelController.InitGoldScore(value);
        }


        internal void ChangeVibrationStatus(bool vibrationStatus)
        {
            CoreGameSignals.Instance.onUpdateVibrationStatus?.Invoke(vibrationStatus);
        }



    }
}