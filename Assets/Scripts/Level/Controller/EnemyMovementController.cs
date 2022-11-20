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

        private void Start()
        {
           
        }

        private void Init()
        {
            
        }
        private void Update()
        {
            MoveForward();
        }
        private void MoveForward()
        {
            velocity = rigidbody.velocity;
            rigidbody.velocity=new Vector3 (velocity.x, velocity.y,-5);
        }
    }
}