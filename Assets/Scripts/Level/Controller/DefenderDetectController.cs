using Manager;
using System.Collections;
using Type;
using UnityEngine;

namespace Controller
{
    public class DefenderDetectController : MonoBehaviour
    {
        [SerializeField]
        private DefanderManager defanderManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicController enemyPhysicController))
            {

                defanderManager.WhenHitEnemy(other.gameObject);
                defanderManager.WhenEnterDetectArea();
            }
        }


    }
}