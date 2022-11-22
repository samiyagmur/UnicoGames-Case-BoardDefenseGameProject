using Interfaces;
using Signals;
using System.Collections;
using Type;
using UnityEngine;

namespace Controller
{
    public class BulletPhysicController : MonoBehaviour,IDemeger
    {
        [SerializeField]
        private BulletType bulletType;

        [SerializeField]
        private PoolObjectType poolObjectType;
        private int defenderPower;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicController enemyPhysicController))
            {
                PushToPool(poolObjectType, transform.parent.gameObject);

                Debug.Log(other.gameObject.name);
            }
            if (other.TryGetComponent(out WeaponAtackController weaponAtackController))
            {
              defenderPower=  weaponAtackController.GetPowerToDefender();

                
            }
        }

        public int GetDamage()
        {
           return defenderPower * (int)bulletType;
        }

        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleaseObjectFromPool(poolObjectType, obj);
        }
    }
}