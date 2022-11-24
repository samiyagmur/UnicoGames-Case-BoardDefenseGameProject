using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Type;
using UnityEngine;

namespace Controller
{


    public class DefenderMovementController : MonoBehaviour
    {

        #region Self Variabels

        #region Private Variables

        [ShowInInspector]
        private LinkedList<GameObject> _enemyDeadList = new LinkedList<GameObject>();

        [SerializeField]
        private  GameObject defender;

        private GameObject _targetEnemy;



        #endregion Private Variables

        #endregion Self Variabels
        internal void StartFollowAsDefenderType()
        {
            FollowEnemy(true);
            
        }

        internal void StopFollow()
        {
            FollowEnemy(false);
        }


        internal void AddDeathList(GameObject enemy)
        {
            _enemyDeadList.AddLast(enemy);
        }
        public void RemoveDeathList(GameObject enemy)
        {
         
            if (!_enemyDeadList.Contains(enemy)) return;

            if (_enemyDeadList.Count <= 0) return;

            _enemyDeadList.Remove(enemy);

        }
        public async  void FollowEnemy(bool _isFollow)
        {
    
            while (_isFollow)
            {
                await Task.Delay(1);

                RotateToEnemy();
            }
        }

        public void RotateToEnemy()
        {
            if (_enemyDeadList.Count <= 0) return;

            _targetEnemy = _enemyDeadList.Last.Value;

            if (!_targetEnemy.activeInHierarchy)
            {
                if (!_enemyDeadList.Contains(_enemyDeadList.Last.Value)) return;

                _enemyDeadList.Remove(_enemyDeadList.Last.Value);
                if (_enemyDeadList.Count <= 0) return;
                _targetEnemy = _enemyDeadList.Last.Value;
            }
                
             



            if (_targetEnemy.transform.position == Vector3.zero) return;

            Vector3 oldPos=new Vector3(defender.transform.position.x,0, defender.transform.position.z+0.4f);

            Vector3 _shotPositon = new Vector3(_targetEnemy.transform.position.x, 0, _targetEnemy.transform.position.z);
            Vector3  _relativePos = _shotPositon - oldPos;
            Quaternion _rotation = Quaternion.LookRotation(_relativePos);
            defender.transform.localRotation = Quaternion.Lerp(defender.transform.localRotation, _rotation , 0.1f);

        }

    
        public GameObject GetTarger()
        {
          
            return _enemyDeadList.Last.Value;
        }


      


    }
}