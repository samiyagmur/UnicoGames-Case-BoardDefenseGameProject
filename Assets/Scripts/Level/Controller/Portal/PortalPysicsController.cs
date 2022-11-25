using Manager;
using UnityEngine;

namespace Controller
{
    public class PortalPysicsController : MonoBehaviour
    {
        [SerializeField]
        private PortalManager portalManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicController enemyPhysic))
            {
                portalManager.WhenEnterPortal();
            }
        }
    }
}