using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Type;
using UnityEngine;

namespace Controller
{


    public class DefenderMovementController : MonoBehaviour
    {

        #region Self Variabels

        #region Private Variables

        [SerializeField]
        private List<GameObject> _enemyDeadList = new List<GameObject>();

        private GameObject _botTarget;
        private DefanderType _defanderType;


        #endregion Private Variables

        #endregion Self Variabels


        internal void AddDeathList(GameObject enemy, DefanderType defanderType)
        {
            _defanderType = defanderType;
            _enemyDeadList.Add(enemy);
        }
        public void RemoveDeathList(GameObject gameObject)
        {
            //Debug.Log(gameObject);
            //if (!_deadList.Contains(gameObject)) return;

            //if (_deadList.Count <= 0) return;
            //_deadList.Dequeue();

            //_botTarget = _deadList.Peek();
        }
        public GameObject GetTarger()
        {
            return _enemyDeadList[0];
        }
       
        private void Update()
        {
            RotateToEnemy();
        }

        public void RotateToEnemy()
        {
            if (_deadList.Count <= 0) return;






        }




        //internal void RoteteToEnemy()
        //{

        //}



    }
}