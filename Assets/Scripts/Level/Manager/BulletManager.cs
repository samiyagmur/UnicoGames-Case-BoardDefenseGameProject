using Signals;
using System;
using System.Collections;
using Type;
using UnityEngine;

namespace Manager
{
    public class BulletManager : MonoBehaviour
    {


        [SerializeField]
        private BulletType bulletType;

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

        private void OnReset()
        {
            PushToPool((PoolObjectType)(int)bulletType, gameObject);
        }
        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolObjectType, obj);
        }

        public BulletType GetBulutPoolType()
        {
            return bulletType;
        }
    }
}