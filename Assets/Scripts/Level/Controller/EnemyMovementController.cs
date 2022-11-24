using Data.ValueObject;
using System;
using System.Collections;
using UnityEngine;

namespace Controller
{
    public class EnemyMovementController : MonoBehaviour
    {
        [SerializeField]
        private new  Rigidbody rigidbody;

        private Vector3 velocity;

        private EnemyCharacterData _enemyCharacterData;

        private bool IsMoving;

        internal void SetData(EnemyCharacterData enemyCharacterData)
        {
            _enemyCharacterData = enemyCharacterData;
        }

        private void FixedUpdate()
        {
            if (true)
            {
               //MoveForward(); 
            }
        }
        private void MoveForward()
        {
            velocity = rigidbody.velocity;
            rigidbody.velocity=new Vector3 (velocity.x, velocity.y,-_enemyCharacterData.Speed);
        }


        //internal void StartToMoveForward()
        //{
        //    IsMoving=true;
        //}

        internal void StopToMoveForward()
        {
            IsMoving = false;
        }
    }
}