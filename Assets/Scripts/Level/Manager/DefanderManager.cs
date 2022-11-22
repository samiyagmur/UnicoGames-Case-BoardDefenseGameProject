using System.Collections;
using UnityEngine;
using Controller;
using Type;
using Data.ValueObject;
using Data.UnityObject;
using System;

namespace Manager
{
    public class DefanderManager : MonoBehaviour
    {
        [SerializeField]
        public DefenderMovementController movementController;

        [SerializeField]
        public WeaponAtackController atackController;

        private bool _ısStartAttack;


        public const string _dataPath = "Data/Cd_LevelData";
        private int _levelID;
        private DefanderType _defanderType;

        public bool IsStartAttack { get; private set; }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
       
            atackController.SetData(GetData().DefanderCharacterData);
        }

        private DefanderData GetData() => Resources.Load<Cd_LevelData>(_dataPath).LevelData[_levelID].DefanderData;

        internal void WhenHitEnemy(GameObject gameObject)
        {

            movementController.AddDeathList(gameObject);
            _ısStartAttack = true;
        }

        internal void WhenEnterDetectArea(DefanderType defanderType)
        {
            _defanderType = defanderType;
            atackController.StartAtack( defanderType, _ısStartAttack);
        }

    }
}