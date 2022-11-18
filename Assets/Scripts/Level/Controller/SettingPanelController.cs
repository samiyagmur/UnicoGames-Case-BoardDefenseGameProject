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
    internal struct SettingPanelAssignments
    {
        [SerializeField]
        private TextMeshProUGUI musicVolumeAmount;
        [SerializeField]
        private Slider musicVolumeAmountSlider;
        [SerializeField]
        private Button exit;
        [SerializeField]
        private Button soundStatus;
        [SerializeField]
        private Button vibrationStatus;
        [SerializeField]
        private Image soundButtonImage;
        [SerializeField]
        private Image vibrationButtonImage;


        public TextMeshProUGUI MusicVolumeAmount { get => musicVolumeAmount; }
        public Button Exit { get => exit; }
        public Button SoundStatus { get => soundStatus;}
        public Button VibrationStatus { get => vibrationStatus;}
        public Slider MusicVolumeAmountSlider { get => musicVolumeAmountSlider; }
        public Image SoundButtonImage { get => soundButtonImage; set => soundButtonImage = value; }
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
            settingPanelAssignments.SoundStatus.onClick.
                AddListener(delegate { ArangeSoundStatus(); });
            settingPanelAssignments.VibrationStatus.onClick.
              AddListener(delegate { ArangeVibrationStatus(); });
            settingPanelAssignments.MusicVolumeAmountSlider.onValueChanged.
                AddListener(delegate { ArangeSoundVolume(settingPanelAssignments.MusicVolumeAmountSlider.value);});
        }

        private void ArangePanelStatus(UIPanelType uIPanelType)
        {
            manager.ChangePanelStatusOnSetting(uIPanelType);
        }

        public void ArangeSoundVolume(float value)
        {
            Debug.Log(value);
            settingPanelAssignments.MusicVolumeAmount.text = Mathf.Floor(value * 100).ToString();
            manager.ArangementSoundVolume(value);
         
        }

        private void ArangeSoundStatus()
        {
            if (settingPanelAssignments.SoundButtonImage.color == Color.white)
            {
                settingPanelAssignments.MusicVolumeAmountSlider.gameObject.SetActive(false);
                settingPanelAssignments.SoundButtonImage.color = Color.gray;
            }
            else
            {
                settingPanelAssignments.MusicVolumeAmountSlider.gameObject.SetActive(true);
                settingPanelAssignments.SoundButtonImage.color = Color.white;
            }
               

            manager.ChangeSoundStatus(!(settingPanelAssignments.SoundButtonImage.color == Color.white));
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
