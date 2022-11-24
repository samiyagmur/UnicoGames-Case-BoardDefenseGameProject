using Cinemachine;
using Controller;
using Signals;
using UnityEngine;

namespace Manager
{
    public class CameraManager : MonoBehaviour
    {


        [SerializeField]
        private CinemachineStateDrivenCamera stateDrivenCamera;

        [SerializeField]
        private CameraStateController cameraStateController;

  
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