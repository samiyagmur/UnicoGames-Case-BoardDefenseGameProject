using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
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

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData -= OnLevelInitilize;
        }
        private void OnDisable() => UnsubscribeEvents();

        internal void WhenEnterPortal()
        { 
            _numberOfPasses++;
            if (_numberOfPasses == _levelData.FailAmount) CoreGameSignals.Instance.onFail?.Invoke();
        }

    }
}