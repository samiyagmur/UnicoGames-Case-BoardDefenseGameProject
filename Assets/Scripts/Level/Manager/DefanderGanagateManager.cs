using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using Type;
using UnityEngine;

namespace Manager
{
    public class DefanderGanagateManager : MonoBehaviour
    {
        [SerializeField]
        public DefanderSpawnController spawnController;

        private void OnLevelInitilize(LevelData levelData)
        {
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
            SelectSignals.Instance.onSelectedGrid += OnSelectedGrid;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData -= OnLevelInitilize;
            CoreGameSignals.Instance.onClickCharacterButton -= OnClickCharacterButton;
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onInputTouch -= OnInputTouch;
            InputSignals.Instance.onDragMouse -= OnDragMause;
            SelectSignals.Instance.onSelectedGrid -= OnSelectedGrid;
        }

        private void OnDisable() => UnsubscribeEvents();


        private void OnClickCharacterButton(DefanderType value)
        {
            spawnController.SetSpawnDefenderType(value);
        }

        private void OnDragMause(RaycastHit hitObj)
        {
            spawnController.SetSpawnPoint(hitObj);
        }
        private void OnInputTouch()
        {
            spawnController.ChosePlantPoint();
        }

        private void OnSelectedGrid(List<GridElements> gridElements)
        {
            spawnController.SelectableGridElement(gridElements);
        }

        private void OnReset()
        {
            
        }

    }
}