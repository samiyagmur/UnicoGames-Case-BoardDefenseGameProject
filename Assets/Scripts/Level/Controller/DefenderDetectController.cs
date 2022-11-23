using Manager;
using System;
using System.Collections;
using Type;
using UnityEngine;

namespace Controller
{
    public class DefenderDetectController : MonoBehaviour
    {
        [SerializeField]
        private DefanderManager defanderManager;


        [SerializeField]
        public BoxCollider detectCollider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicController enemyPhysicController))
            {
                defanderManager.WhenHitEnemy(other.gameObject);

                defanderManager.WhenEnterDetectArea();
            }
        }

        internal void OpenDetectPyhsic(float range)
        {
            detectCollider.size = new Vector3(2, 1, 16);
        }
    }
}