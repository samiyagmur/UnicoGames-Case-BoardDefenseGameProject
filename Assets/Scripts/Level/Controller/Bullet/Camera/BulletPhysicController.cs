using Interfaces;
using Manager;
using Signals;
using Type;
using UnityEngine;

namespace Controller
{
    public class BulletPhysicController : MonoBehaviour, IDemager
    {
        [SerializeField]
        private BulletManager bulletManager;

        private int defenderPower;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicController enemyPhysicController))
            {
                PushToPool((PoolObjectType)(int)bulletManager.GetBulutPoolType(), transform.parent.gameObject);
            }
            if (other.TryGetComponent(out WeaponAtackController weaponAtackController))
            {
                transform.parent.rotation = weaponAtackController.gameObject.transform.rotation;

                defenderPower = weaponAtackController.GetPowerToDefender();
            }
        }

        public int GetDamage()
        {
            return defenderPower * (int)bulletManager.GetBulutPoolType();
        }

        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleaseObjectFromPool(poolObjectType, obj);
        }
    }
}