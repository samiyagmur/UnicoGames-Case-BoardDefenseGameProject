using Controller;
using Data.ValueObject;
using Signals;
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
        private CharacterPanelController characterPanelController;

        private int _lastGoldScore;
        private int _lastDiamondScore;
        private int _levelID;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            // CoreGameSignals.Instance.onScoreReNew += OnScoreReNew;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            ScoreSignals.Instance.onInitLastGoldScore += OnInitLastGoldScore;
            ScoreSignals.Instance.onInitLastDiamondScore += OnInitLastDiamondScore;
            CoreGameSignals.Instance.onFail += OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
            CoreGameSignals.Instance.onLevelInitilize += OnLevelInitilize;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onInitDefenderInfo += OnInitDefenderInfo;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            ScoreSignals.Instance.onInitLastGoldScore -= OnInitLastGoldScore;
            ScoreSignals.Instance.onInitLastDiamondScore -= OnInitLastDiamondScore;
            CoreGameSignals.Instance.onFail -= OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
            CoreGameSignals.Instance.onLevelInitilize -= OnLevelInitilize;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onInitDefenderInfo -= OnInitDefenderInfo;
        }

        internal void OnBuyNewChar(int charPrice)
        {
            ScoreSignals.Instance.onUpdateGold(-charPrice);
        }

        internal void HasClickCharacterButton(DefanderType characterType)
        {
            CoreGameSignals.Instance.onClickCharacterButton?.Invoke(characterType);
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnOpenPanel(UIPanelType panelType) => uIPanelController.ChangePanel(panelType, true);

        internal void OnClosePanel(UIPanelType panelType) => uIPanelController.ChangePanel(panelType, false);

        private void OnLevelInitilize(int levelID)
        {
            _levelID = levelID;
            levelPanelController.InitLevelID(levelID);
        }

        internal void ChangePanelStatusOnPlay()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        private void OnPlay()
        {
            OnClosePanel(UIPanelType.StartPanel);
            OnOpenPanel(UIPanelType.LevelPanel);
            OnOpenPanel(UIPanelType.CharPanel);
        }

        public void ChangePanelStatusOnStartAsSetting(UIPanelType uIPanelType)
        {
            OnOpenPanel(uIPanelType);
            OnClosePanel(UIPanelType.StartPanel);
        }

        internal void ChangePanelOnRestart()
        {
            CoreGameSignals.Instance.rePlay?.Invoke();
        }

        internal void ChangePanelStatusOnExit(UIPanelType uIPanelType)
        {
            OnOpenPanel(uIPanelType);
            OnClosePanel(UIPanelType.LevelPanel);
            OnClosePanel(UIPanelType.CharPanel);
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
            OnOpenPanel(UIPanelType.CharPanel);
            CoreGameSignals.Instance.onNextLevel?.Invoke();
        }

        internal void ChangePanelStatusOnSetting(UIPanelType uIPanelType)
        {
            OnClosePanel(uIPanelType);
            OnOpenPanel(UIPanelType.StartPanel);
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

        private void OnInitDefenderInfo(DefanderData defenderCount)
        {
            characterPanelController.InitCharCount(defenderCount);
        }
    }
}