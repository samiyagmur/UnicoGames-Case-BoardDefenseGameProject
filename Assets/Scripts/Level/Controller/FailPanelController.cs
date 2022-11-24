using Managers;
using System;
using TMPro;
using Type;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [Serializable]
    internal struct FailPanelAssignments
    {
        [SerializeField]
        private TextMeshProUGUI lastDiamondScore;

        [SerializeField]
        private TextMeshProUGUI lastGoldScore;

        [SerializeField]
        private Button tryAgainButton;

        public TextMeshProUGUI LastDiamondScore { get => lastDiamondScore; }
        public TextMeshProUGUI LastGoldScore { get => lastGoldScore; }
        public Button TryAgainButton { get => tryAgainButton; }
    }

    public class FailPanelController : MonoBehaviour
    {
        [SerializeField]
        private UIManager manager;

        [SerializeField]
        private FailPanelAssignments failPanelAssignments;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            InitButton();
        }

        internal void SetGoldScore(int value)
        {
            failPanelAssignments.LastDiamondScore.text = value.ToString();
        }

        internal void SetDiamondScore(int value)
        {
            failPanelAssignments.LastGoldScore.text = value.ToString();
        }

        private void InitButton()
        {
            failPanelAssignments.TryAgainButton.onClick
                 .AddListener(delegate { ArangePanelStatus(UIPanelType.FailPanel); });
        }

        private void ArangePanelStatus(UIPanelType uIPanelType)
        {
            manager.ChangePanelStatusOnPressTryAgain(uIPanelType);
        }
    }
}