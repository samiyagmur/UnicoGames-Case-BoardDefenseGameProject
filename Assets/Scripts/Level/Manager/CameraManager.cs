using Cinemachine;
using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using System;

using System.Collections;
using UnityEngine;

namespace Manager
{
    public class CameraManager : MonoBehaviour
    {

        [SerializeField]
        private CameraSettingController cameraSettingController;

        [SerializeField] 
        private CinemachineStateDrivenCamera stateDrivenCamera;

        [SerializeField]
        private CameraStateController cameraStateController;
        

        private const string _dataPath = "Data/Cd_CameraData";

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            cameraSettingController.SetData(GetData().cameraSettingData, stateDrivenCamera);
        }

        private CameraData GetData() => Resources.Load<Cd_CameraData>(_dataPath).cameraData;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onFail += OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onFail -= OnFail;
            CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onReset -= OnReset;

        }

        private void OnDisable() => UnsubscribeEvents();
        private void OnPlay() => cameraStateController.WhenPressPlay();
        private void OnFail() => cameraStateController.WhenCharacterFail();
        private void OnLevelSuccessfull() => cameraStateController.WhenCharacterLevelComplated();
        private void OnNextLevel() => cameraStateController.WhenEnterNextLevel();
        private void OnReset() => cameraStateController.WhenPressReset();




    }
}