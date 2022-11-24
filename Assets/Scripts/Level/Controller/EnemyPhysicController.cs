using Interfaces;
using Manager;
using Signals;
using Type;
using UnityEngine;

namespace Controller
{
    public class EnemyPhysicController : MonoBehaviour, IDemager, IPushObject
    {
        [SerializeField]
        private EnemyManager enemyManager;

        private PoolObjectType poolObject;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDemager damager))
            {
                enemyManager.WhenHitBullet(damager.GetDamage());
            }
            if (other.TryGetComponent(out DefenderPhysicController defanderPhysicController))
            {
                enemyManager.WhenHitDefender();
            }
            if (other.CompareTag("Portal"))
            {
                PushToPool(poolObject, transform.parent.gameObject);
            }
        }

        public int GetDamage()
        {
            return enemyManager.GetDamageWhenHitDefender();
        }

        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolObjectType, obj);
        }
    }
}