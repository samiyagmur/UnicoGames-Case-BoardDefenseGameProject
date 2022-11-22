using Data.ValueObject;
using Interfaces;
using Manager;
using Signals;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Type;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controller
{
    public class DefanderSpawnController : MonoBehaviour, IPullObject
    {
        private SerializedDictionary<DefanderType, DefanderCharacterData> _defanderCharacterData;

        private GameObject _setSpawnDefender;

        [ShowInInspector]
        private Stack<GameObject> spawnDefenderList = new Stack<GameObject>();


        [SerializeField]
        private List<GameObject> selectableGridElementList;

        private Vector3 plantPoint;


        private RaycastHit _hitObj;

        internal void SetData(SerializedDictionary<DefanderType, DefanderCharacterData> defanderCharacterData)
        {
            _defanderCharacterData = defanderCharacterData;
        }
        internal void SelectableGridElement(List<GridElements> gridElements)
        {
            for (int i = 0; i < gridElements.Count; i++)
            {
                if (gridElements[i].gridElementStatus == GridElementStatus.Selectable)
                {
                    selectableGridElementList.Add(gridElements[i]._gridElement);
                }
            }
        }


        internal void SetSpawnPoint(RaycastHit hitObj)
        {
            _hitObj = hitObj;

            if (spawnDefenderList.Count == 0) return;

           

            if (_setSpawnDefender == null) return;

            if (hitObj.transform.CompareTag("GridElement") )
            {
                
                
                spawnDefenderList.Peek().transform.position = new Vector3(hitObj.transform.position.x, 
                                                          hitObj.point.y + spawnDefenderList.Peek().transform.localScale.y, 
                                                          hitObj.transform.position.z);

            }
            else
            {
                spawnDefenderList.Peek().transform.position = new Vector3(hitObj.point.x 
                                                         ,hitObj.point.y + spawnDefenderList.Peek().transform.localScale.y
                                                         ,hitObj.point.z);
            }

        }
        internal void ChosePlantPoint()
        {

            if (selectableGridElementList.Contains(_hitObj.transform.gameObject))
            {
                if (spawnDefenderList.Count == 0) return;

                spawnDefenderList.Peek().transform.position += new Vector3(0, 0, 0);

                selectableGridElementList.Remove(_hitObj.transform.gameObject);

                spawnDefenderList.Pop();

            }
        }
        internal void SetSpawnDefenderType(DefanderType value)
        {
            _setSpawnDefender = PullFromPool((PoolObjectType)(int)value);


            spawnDefenderList.Push(_setSpawnDefender);

        }
        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolObjectType);
        }

    
    }
}