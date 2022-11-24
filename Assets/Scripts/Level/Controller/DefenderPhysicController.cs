using Interfaces;
using Manager;
using System.Collections;
using UnityEngine;

namespace Controller
{
    public class DefenderPhysicController : MonoBehaviour
    {
        [SerializeField]
        public DefanderManager defenderMeneger;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GridElement"))
            {
               defenderMeneger.WhenDropOnGridElement();
            }
            
        }
    }
}