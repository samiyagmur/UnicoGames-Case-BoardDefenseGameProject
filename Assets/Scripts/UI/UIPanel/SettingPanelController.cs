using Managers;
using System;
using Type;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [Serializable]
    internal struct SettingPanelAssignments
    {
        [SerializeField]
        private Button exit;

        [SerializeField]
        private Button vibrationStatus;

        [SerializeField]
        private Image vibrationButtonImage;

        public Button Exit { get => exit; }
        public Button VibrationStatus { get => vibrationStatus; }
        public Image VibrationButtonImage { get => vibrationButtonImage; set => vibrationButtonImage = value; }
    }

    public class SettingPanelController : MonoBehaviour
    {
        [SerializeField]
        private UIManager manager;

        [SerializeField]
        private SettingPanelAssignments settingPanelAssignments;

        private bool _isVirating;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            InitButton();
        }

        private void InitButton()
        {
            settingPanelAssignments.Exit.onClick.
                AddListener(delegate { ArangePanelStatus(UIPanelType.Setting); });

            settingPanelAssignments.VibrationStatus.onClick.
              AddListener(delegate { ArangeVibrationStatus(); });
        }

        private void ArangePanelStatus(UIPanelType uIPanelType)
        {
            manager.ChangePanelStatusOnSetting(uIPanelType);
        }

        private void ArangeVibrationStatus()
        {
            if (settingPanelAssignments.VibrationButtonImage.color == Color.white)
            {
                settingPanelAssignments.VibrationButtonImage.color = Color.gray;
            }
            else
            {
                settingPanelAssignments.VibrationButtonImage.color = Color.white;
            }

            manager.ChangeVibrationStatus(!(settingPanelAssignments.VibrationButtonImage.color == Color.white));
        }
    }
}