using Interfaces;
using Manager;
using System.Collections;
using Type;
using UnityEngine;

namespace Controller
{
    public class EnemyPhysicController : MonoBehaviour,IDemeger
    {
        [SerializeField]
        private EnemyManager enemyManager;


        

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDemeger damager))
            {
                Debug.Log(damager.GetDamage());
                enemyManager.WhenHitBullet(damager.GetDamage());
            }
            if (other.TryGetComponent(out DefenderPhysicController defanderPhysicController))
            {
                enemyManager.WhenHitDefender();
            }


            //if (other.CompareTag("Portal"))
            //{
            //    enemyManager.WhenSpawnOnBoard();
            //}
        }

        public int GetDamage()
        {
            return enemyManager.GetDamageWhenHitDefender();
        }


    }
}