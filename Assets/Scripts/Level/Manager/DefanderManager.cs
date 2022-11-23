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
        public DefenderDetectController defenderDetectController;

        [SerializeField]
        private DefanderType _defanderType;

        private bool _ısStartAttack;
        private LevelData _levelData;

        private void Start()
        {
            _levelData = CoreGameSignals.Instance.onGetLevelDataWhenSpawn?.Invoke();
            Init(_levelData);
        }
        private void Init(LevelData leveldata)
        {
           
            atackController.SetData(leveldata.DefanderData.DefanderCharacterData[_defanderType]);
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {

            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {

            CoreGameSignals.Instance.onReset -= OnReset;
        }


        private void OnDisable() => UnsubscribeEvents();
        internal void WhenHitEnemy(GameObject gameObject)
        {
            
            movementController.AddDeathList(gameObject);
            _ısStartAttack = true;
        }

        internal void WhenEnterDetectArea()
        {
            atackController.StartAtack( _ısStartAttack, movementController.GetTarger());
        }


        private void OnReset()
        {
            atackController.ResetAtack();
            PushToPool((PoolObjectType)(int)_defanderType, gameObject);
        }
        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolObjectType, obj);
        }

        internal void WhenDropOnGridElement()
        {
            defenderDetectController.OpenDetectPyhsic(_levelData.DefanderData.DefanderCharacterData[_defanderType].Range);
        }


    }
}