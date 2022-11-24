using Data.ValueObject;
using Interfaces;
using Signals;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Type;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controller
{
    public class DefanderSpawnController : MonoBehaviour, IPullObject
    {
      
        [ShowInInspector]
        private Stack<GameObject> spawnDefenderList = new Stack<GameObject>();

        private SerializedDictionary<DefanderType, DefanderCharacterData> _defanderCharacterData;

        private DefanderType _defanderType;

        private GameObject _setSpawnDefender;

        private RaycastHit _hitObj;


        internal void SetData(SerializedDictionary<DefanderType, DefanderCharacterData> defanderCharacterData)
        {
            _defanderCharacterData = defanderCharacterData;
        }

        internal void SetPlantPoint(RaycastHit hitObj)
        {
            _hitObj = hitObj;

            if (spawnDefenderList.Count == 0) return;

            if (_setSpawnDefender == null) return;

            if (hitObj.transform.CompareTag("GridElement"))
            {
                spawnDefenderList.Peek().transform.position = new Vector3(hitObj.transform.position.x,
                                                          hitObj.point.y + spawnDefenderList.Peek().transform.localScale.y + 1f,
                                                          hitObj.transform.position.z);
            }
            else
            {
                spawnDefenderList.Peek().transform.position = new Vector3(hitObj.point.x
                                                         , hitObj.point.y + spawnDefenderList.Peek().transform.localScale.y + 1f
                                                         , hitObj.point.z);
            }
        }

        internal void Plant()
        {
            if (_hitObj.transform.CompareTag("GridElement"))
            {
                if (spawnDefenderList.Count == 0) return;

                spawnDefenderList.Peek().transform.position += new Vector3(0, 0, 0);

                spawnDefenderList.Pop();
            }
        }

        internal void SetSpawnDefenderType(DefanderType defanderType)
        {
            _defanderType = defanderType;

            _setSpawnDefender = PullFromPool((PoolObjectType)(int)defanderType);

            spawnDefenderList.Push(_setSpawnDefender);
        }

        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolObjectType);
        }
    }
}