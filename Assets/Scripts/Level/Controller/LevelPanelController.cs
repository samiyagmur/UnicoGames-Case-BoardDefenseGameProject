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
    internal struct LevelPanelAssignments
    {
        [SerializeField]
        private TextMeshProUGUI currentLevel;
        [SerializeField]
        private TextMeshProUGUI nextLevel;
        [SerializeField]
        private TextMeshProUGUI gold;
        [SerializeField]
        private TextMeshProUGUI diamond;
        [SerializeField]
        private Button exit;
        [SerializeField]
        private Button restart;

        public TextMeshProUGUI CurrentLevel { get => currentLevel;  }
        public TextMeshProUGUI NextLevel { get => nextLevel; }
        public TextMeshProUGUI Gold { get => gold; }
        public TextMeshProUGUI Diamond { get => diamond;}
        public Button Exit { get => exit; }
        public Button Restart { get => restart;}
    }

    public class LevelPanelController : MonoBehaviour
    {
        [SerializeField]
        private UIManager manager;

        [SerializeField]
        private LevelPanelAssignments startPanelassignments;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            InitButton();
        }

        internal void InitGoldScore(int value)
        {
            startPanelassignments.Gold.text = value.ToString();
        }

        internal void InitDiamondScore(int value)
        {
            startPanelassignments.Diamond.text = value.ToString();
        }
        internal void InitLevelID(int levelID)
        {

            startPanelassignments.CurrentLevel.text= (levelID + 1).ToString();
            startPanelassignments.NextLevel.text = (levelID+2).ToString();
        }
        private void InitButton()
        {
            startPanelassignments.Exit.onClick.
                AddListener(delegate { ArangePanelStatusAsExit(UIPanelType.StartPanel); });
            startPanelassignments.Restart.onClick.
                AddListener(delegate { ArangePanelStatusAsRestart(); });
        }

        private void ArangePanelStatusAsRestart()
        {
            manager.ChangePanelOnRestart();
        }

        private void ArangePanelStatusAsExit(UIPanelType uIPanelType)
        {
            manager.ChangePanelStatusOnExit(uIPanelType);
        }

       
    }

}