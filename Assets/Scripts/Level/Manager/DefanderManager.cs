using Controller;
using Data.ValueObject;
using Signals;
using Type;
using UnityEngine;

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

        private LevelData _levelData;
        private RoteteStatus _roteteStatus;

        private void Start()
        {
            _levelData = CoreGameSignals.Instance.onGetLevelDataWhenSpawn?.Invoke();
            Init(_levelData);
        }

        private void Init(LevelData leveldata)
        {
            atackController.SetData(leveldata.DefanderData.DefanderCharacterData[_defanderType]);
            _roteteStatus = _levelData.DefanderData.DefanderCharacterData[_defanderType].roteteStatus;
            defenderDetectController.SetData(_levelData.DefanderData.DefanderCharacterData[_defanderType]);
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

        internal void WhenEnemyEnterDetectArea(GameObject gameObject)
        {
            movementController.AddDeathList(gameObject);

            if (_roteteStatus == RoteteStatus.Static) return;

            movementController.StartFollowAsDefenderType();
        }

        internal void WhenEnemyExitDetectArea(GameObject enemy)
        {
            movementController.RemoveDeathList(enemy);
        }

        internal void WhenEnterDetectArea()
        {
            if (movementController.GetTarger() == null) return;

            atackController.StartAtack(movementController.GetTarger());
        }

        internal void WhenExitDetectArea()
        {
            atackController.StopAtack();
        }

        private void OnReset()
        {
            atackController.ResetAtack();

            PushToPool((PoolObjectType)(int)_defanderType, gameObject);

            if (_roteteStatus == RoteteStatus.Static) return;

            movementController.StopFollow();
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