using Data.ValueObject;
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
        public SphereCollider detectColliderDynamicRotate;

        [SerializeField]
        public BoxCollider detectColliderStaticRotate;

        private DefanderCharacterData _defanderCharacterData;
        private RoteteStatus _roteteStatus;

        internal void SetData(DefanderCharacterData defanderCharacterData)
        {
            _defanderCharacterData = defanderCharacterData;

            SelectRotateMod(_defanderCharacterData.roteteStatus, _defanderCharacterData.Range);

        }

        private void SelectRotateMod(RoteteStatus roteteStatus, float range)
        {
            _roteteStatus = roteteStatus;
            switch (roteteStatus)
            {
                case RoteteStatus.Static:
                    detectColliderDynamicRotate.enabled = false;
                    detectColliderStaticRotate.enabled = true;
                  
                    break;
                case RoteteStatus.Dynamic:
                    detectColliderDynamicRotate.enabled = true;
                    detectColliderStaticRotate.enabled = false;

                    break;
                default:
                    break;
            }
        }
        internal void OpenDetectPyhsic(float range)
        {
            detectColliderStaticRotate.size = new Vector3(2, 1, 16);
            detectColliderDynamicRotate.radius = 16;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicController enemyPhysicController))
            {
                defanderManager.WhenEnemyEnterDetectArea(other.gameObject);

                defanderManager.WhenEnterDetectArea();
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicController enemyPhysicController))
            {
                 defanderManager.WhenEnemyExitDetectArea(other.gameObject);

                 defanderManager.WhenExitDetectArea();
            }
        }

     
       
    }
}