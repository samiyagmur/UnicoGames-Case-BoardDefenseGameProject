using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Manager
{
    public class PortalManager : MonoBehaviour
    {
        private int _numberOfPasses;
        private LevelData _levelData;

        private void OnLevelInitilize(LevelData levelData)
        {
            _levelData = levelData;
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData += OnLevelInitilize;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData -= OnLevelInitilize;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable() => UnsubscribeEvents();

        internal void WhenEnterPortal()
        {
            _numberOfPasses++;
            if (_numberOfPasses == _levelData.FailAmount) CoreGameSignals.Instance.onFail?.Invoke();

            EnemySignals.Instance.onPassEnemyFromPortal?.Invoke();
        }

        private void OnReset()
        {
            _numberOfPasses = 0;
        }
    }
}