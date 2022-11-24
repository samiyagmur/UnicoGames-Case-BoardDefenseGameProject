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
        private new Rigidbody rigidbody;

        private GameObject _targetEnemy;



        #endregion Private Variables

        #endregion Self Variabels
        internal void StartFollowAsDefenderType()
        {
            Debug.Log("ss");
            FollowEnemy(true);
            
        }

        internal void StopFollow()
        {
            FollowEnemy(false);
        }


        internal void AddDeathList(GameObject enemy)
        {
            Debug.Log(enemy.transform.name);
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
    
            while (true)
            {
                await Task.Delay(1);

                RotateToEnemy();
            }
        }

        public void RotateToEnemy()
        {
            if (_enemyDeadList.Count <= 0) return;

          //  if (_enemyDeadList.Last.Value == null) return;
         
            _targetEnemy = _enemyDeadList.Last.Value;

            Debug.Log(_targetEnemy.activeInHierarchy);

            if (!_targetEnemy.activeInHierarchy)
            {
                if (!_enemyDeadList.Contains(_enemyDeadList.Last.Value)) return;

                _enemyDeadList.Remove(_enemyDeadList.Last.Value);

                _targetEnemy = _enemyDeadList.Last.Value;
            }
                
             



            if (_targetEnemy.transform.position == Vector3.zero) return;

            Vector3 oldPos=new Vector3(transform.position.x,0, transform.position.z);

            Vector3 _shotPositon = new Vector3(_targetEnemy.transform.position.x, 0, _targetEnemy.transform.position.z);
            Vector3  _relativePos = _shotPositon - oldPos;
            Quaternion _rotation = Quaternion.LookRotation(_relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotation,0.1f);

        }

    
        public GameObject GetTarger()
        {
          
            return _enemyDeadList.First.Value;
        }


      


    }
}