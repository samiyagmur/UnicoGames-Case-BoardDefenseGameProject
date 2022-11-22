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



        public const string _dataPath= "Data/Cd_LevelData";
        private int _levelID;

        public bool IsStartAttack { get; private set; }

        private void Start()
        {
            Init();
        }


        private void Init()
        {
            spawnController.SetData(GetData().DefanderCharacterData);
            InitDefenderCount();
        }
        private void InitDefenderCount()
        {
            CoreGameSignals.Instance.onInitDefenderInfo?.Invoke(GetData());
        }

        private DefanderData GetData() => Resources.Load<Cd_LevelData>(_dataPath).LevelData[_levelID].DefanderData;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize += OnLevelInitilize;
            CoreGameSignals.Instance.onClickCharacterButton += OnClickCharacterButton;
            InputSignals.Instance.onInputTouch += OnInputTouch;
            InputSignals.Instance.onDragMouse += OnDragMause;
            InputSignals.Instance.onInputReleased += OnInputReleased;
            SelectSignals.Instance.onSelectedGrid += onSelectedGrid;

        }


        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize -= OnLevelInitilize;
            CoreGameSignals.Instance.onClickCharacterButton -= OnClickCharacterButton;
            InputSignals.Instance.onInputTouch -= OnInputTouch;
            InputSignals.Instance.onDragMouse += OnDragMause;
            InputSignals.Instance.onInputReleased -= OnInputReleased;
            SelectSignals.Instance.onSelectedGrid += onSelectedGrid;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnLevelInitilize(int levelID)
        {
            _levelID = levelID;
        }

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
        private void OnInputReleased()
        {
           // spawnController.PlantDefender();
        }

        private void onSelectedGrid(List<GridElements> gridElements)
        {
            spawnController.SelectableGridElement(gridElements);
        }

    }
}