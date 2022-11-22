using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class DefenderMovementController : MonoBehaviour
    {

        #region Self Variabels

        #region Private Variables

        [SerializeField]
        private List<GameObject> _enemyList = new List<GameObject>();

        private GameObject _botTarget;


        #endregion Private Variables

        #endregion Self Variabels


        public void AddDeathList(GameObject enemy)
        {
            _enemyList.Add(enemy);
           
            // _botTarget = _enemyList.Peek();
        }

        public void RemoveDeathList(GameObject gameObject)
        {
            //Debug.Log(gameObject);
            //if (!_deadList.Contains(gameObject)) return;

            //if (_deadList.Count <= 0) return;
            //_deadList.Dequeue();

            //_botTarget = _deadList.Peek();
        }

        public void RoteteToEnemy()
        {
            //if (_deadList.Count <= 0) return;

          
        }


        //internal void RoteteToEnemy()
        //{
            
        //}
        
        public GameObject GetTarger()
        {
            return _enemyList[0];
        }

    }
}