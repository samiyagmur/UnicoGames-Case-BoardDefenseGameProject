using System.Collections;
using UnityEngine;
using Controller;
using Type;
using Data.ValueObject;
using Data.UnityObject;
using System;
using Signals;

namespace Manager
{
    public class DefanderManager : MonoBehaviour
    {
        [SerializeField]
        public DefenderMovementController movementController;

        [SerializeField]
        public WeaponAtackController atackController;
        [SerializeField]
        private DefanderType _defanderType;

        private bool _ısStartAttack;

        private void OnLevelInitilize(LevelData leveldata)
        {
            Init(leveldata);
        }

        private void Init(LevelData leveldata)
        {
            atackController.SetData(leveldata.DefanderData.DefanderCharacterData[_defanderType]);
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData += OnLevelInitilize;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData -= OnLevelInitilize;

        }

        private void OnDisable() => UnsubscribeEvents();
        internal void WhenHitEnemy(GameObject gameObject)
        {

            movementController.AddDeathList(gameObject);
            _ısStartAttack = true;
        }

        internal void WhenEnterDetectArea()
        {
            atackController.StartAtack( _ısStartAttack);
        }

    }
}