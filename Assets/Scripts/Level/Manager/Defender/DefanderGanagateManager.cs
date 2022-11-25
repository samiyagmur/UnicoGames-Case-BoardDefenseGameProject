using Controller;
using Data.ValueObject;
using Signals;
using Type;
using UnityEngine;

namespace Manager
{
    public class DefanderGanagateManager : MonoBehaviour
    {
        [SerializeField]
        public DefanderSpawnController spawnController;

        private LevelData _levelData;

        private void OnLevelInitilize(LevelData levelData)
        {
            _levelData = levelData;
            Init(levelData);
        }

        private void Init(LevelData levelData)
        {
            spawnController.SetData(levelData.DefanderData.DefanderCharacterData);
            InitDefenderCount(levelData);
        }

        private void InitDefenderCount(LevelData levelData)
        {
            CoreGameSignals.Instance.onInitDefenderInfo?.Invoke(levelData.DefanderData);
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData += OnLevelInitilize;
            CoreGameSignals.Instance.onClickCharacterButton += OnClickCharacterButton;
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onInputTouch += OnInputTouch;
            InputSignals.Instance.onDragMouse += OnDragMause;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData -= OnLevelInitilize;
            CoreGameSignals.Instance.onClickCharacterButton -= OnClickCharacterButton;
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onInputTouch -= OnInputTouch;
            InputSignals.Instance.onDragMouse -= OnDragMause;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnClickCharacterButton(DefanderType value)
        {
            spawnController.SetSpawnDefenderType(value);
        }

        private void OnDragMause(RaycastHit hitObj)
        {
            spawnController.SetPlantPoint(hitObj);
        }

        private void OnInputTouch()
        {
            spawnController.Plant();
        }

        private void OnReset()
        {
            InitDefenderCount(_levelData);
           
        }
    }
}