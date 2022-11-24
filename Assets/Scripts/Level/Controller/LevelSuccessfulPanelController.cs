using Managers;
using System;
using TMPro;
using Type;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [Serializable]
    internal struct LevelSuccessfulPanelAssignments
    {
        [SerializeField]
        private TextMeshProUGUI levelSuccessful;
        [SerializeField]
        private Button onNext;
        public TextMeshProUGUI LevelSuccessful { get => levelSuccessful; }
        public Button OnNext { get => onNext; }
    }

    public class LevelSuccessfulPanelController : MonoBehaviour
    {
        [SerializeField]
        private UIManager manager;

        [SerializeField]
        private LevelSuccessfulPanelAssignments levelSuccessfulPanelAssignments;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            InitButton();
        }

        internal void SetCurrenLevel(int levelID)
        {
            levelSuccessfulPanelAssignments.LevelSuccessful.text =$"Level {levelID+1} Complated" ;
        }

        private void InitButton()
        {
            levelSuccessfulPanelAssignments.OnNext.onClick.
                AddListener(delegate { ArangePanelStatus(UIPanelType.LevelSuccesful); });
        }

        private void ArangePanelStatus(UIPanelType uIPanelType)
        {
            manager.ChangePanelStatusOnPressNextLevel(uIPanelType);
        }

    }
}