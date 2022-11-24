using Data.ValueObject;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Type;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Controller
{
    [Serializable]
    internal struct CharacterPanelAssignments
    {
        [SerializeField]
        private Button gimliButton;

        [SerializeField]
        private Button aragornButton;

        [SerializeField]
        private Button legolasButton;

        [SerializeField]
        private List<TextMeshProUGUI> charAmount;

        [SerializeField]
        private List<TextMeshProUGUI> charPrice;

        public Button GimliButton { get => gimliButton; set => gimliButton = value; }
        public Button AragornButton { get => aragornButton; set => aragornButton = value; }
        public Button LegolasButton { get => legolasButton; set => legolasButton = value; }
        public List<TextMeshProUGUI> CharAmount { get => charAmount; set => charAmount = value; }
        public List<TextMeshProUGUI> CharPrice { get => charPrice; set => charPrice = value; }
    }
    public class CharacterPanelController : MonoBehaviour
    {

        [SerializeField]
        private UIManager manager;

        [SerializeField]
        private CharacterPanelAssignments _characterPanelAssignments;

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
            _characterPanelAssignments.GimliButton.onClick.
                AddListener(delegate { ClickCharacterButton(DefanderType.Gimli); });
            _characterPanelAssignments.AragornButton.onClick.
                AddListener(delegate { ClickCharacterButton(DefanderType.Aragorn); });
            _characterPanelAssignments.LegolasButton.onClick.
                AddListener(delegate { ClickCharacterButton(DefanderType.Legolas); });
        }
        private void ClickCharacterButton(DefanderType charType)
        {

            int charAmount = Convert.ToInt32(_characterPanelAssignments.CharAmount[(int)charType-3].text);

            int charPrice  = Convert.ToInt32(_characterPanelAssignments.CharPrice[(int)charType-3].text);

            if (charPrice > 0 && charAmount > 0)
            {
                manager.OnBuyNewChar(charPrice);
            }

            if (charAmount>0) 
            {
                charAmount--;

                manager.HasClickCharacterButton(charType);

                _characterPanelAssignments.CharAmount[(int)charType - 3].text = charAmount.ToString();
            }

            

        }

        internal void InitCharCount(DefanderData defenderCount)
        {
            for (int i = 0; i < defenderCount.DefanderCharacterData.Count; i++)
            {
               
                _characterPanelAssignments.CharAmount[i].text
                    = defenderCount.DefanderCharacterData[(DefanderType)i+3].defenderSpawnData.TotalCountOfDefender.ToString();

                _characterPanelAssignments.CharPrice[i].text
                    = defenderCount.DefanderCharacterData[(DefanderType)i+3].Price.ToString();
            }
        }
    }
}