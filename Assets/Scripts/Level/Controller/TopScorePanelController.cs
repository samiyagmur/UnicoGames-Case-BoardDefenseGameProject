using Data.ValueObject;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Type;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [Serializable]
    internal struct TopScorePanelAssignments
    {
        [SerializeField]
        private Button exit;
        public Button Exit { get => exit; }
    }

    public class TopScorePanelController : MonoBehaviour
    {
        [SerializeField]
        private UIManager manager;

        [SerializeField]
        private List<TextMeshProUGUI> levelSuccessful=new List<TextMeshProUGUI>(5);

        [SerializeField]
        private TopScorePanelAssignments topScorePanelAssignments;


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
            topScorePanelAssignments.Exit.onClick.
                AddListener(delegate { ArangePanelStatus(UIPanelType.TopScore); });
        }

        private void ArangePanelStatus(UIPanelType uIPanelType)
        {
            manager.ChangePanelStatusOnTopScore(uIPanelType);
        }

        internal void SetTopScore(List<RankedScore> value)
        {
            
            for (int i = 0; i < value.Count; i++)
            {
               // levelSuccessful[i].text = $"{i + 1}. Name{value[i].name} Score= {value[i].score}";
            }
            
        }
    }
}