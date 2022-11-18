using Managers;
using System;
using System.Collections;
using TMPro;
using Type;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [Serializable]
    internal struct StartPanelassignments
    {
        [SerializeField]
        private TextMeshProUGUI gameName;
        [SerializeField]
        private Button startButton;
        [SerializeField]
        private Button scorePanelButton;
        [SerializeField]
        private Button settingPanelButton;

        public TextMeshProUGUI GameName { get => gameName;}
        public Button StartButton { get => startButton; }
        public Button ScorePanelButton { get => scorePanelButton; }
        public Button SettingPanelButton { get => settingPanelButton; }
    }



    public class StartPanelController : MonoBehaviour
    {
        [SerializeField]
        private UIManager manager;

        [SerializeField]
        private StartPanelassignments startPanelassignments;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            InitText();
            InitButton();
        }

        private void InitText()
        {
            startPanelassignments.GameName.text = "Name Of Game";
        }

        private void InitButton()
        {
            startPanelassignments.StartButton.onClick.
                AddListener(delegate { ArangeStartPanelStatus(); });
            startPanelassignments.ScorePanelButton.onClick.
                AddListener(delegate { ArangePanelStatus(UIPanelType.TopScore); });
            startPanelassignments.SettingPanelButton.onClick.
                AddListener(delegate { ArangePanelStatus(UIPanelType.Setting); });
        }

        private void ArangeStartPanelStatus()
        {
            manager.ChangePanelStatusOnPlay();
        }

        private void ArangePanelStatus(UIPanelType uIPanelType)
        {
            manager.ChangePanelStatusOnStartAsSetting(uIPanelType);
        }
    }
}