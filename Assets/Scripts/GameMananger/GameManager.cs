using Signals;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onUpdateVibrationStatus += OnUpdateVibrationStatus;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onUpdateVibrationStatus -= OnUpdateVibrationStatus;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnUpdateVibrationStatus(bool vibrationStatus)
        {
        }
    }
}