using System;
using System.Collections;
using UnityEngine;

namespace Controller
{
    public class SelectedObjectMeshController : MonoBehaviour
    {
        [SerializeField]
        private Material _lightMaterial;

        [SerializeField]
        private Material _currentMaterial;

        [SerializeField]
        internal void TurnOnLight(GameObject hit)
        {
             hit.GetComponent<Renderer>().material = _lightMaterial;
        }

        internal void TurnOffLight(GameObject _hitObj)
        {
            _hitObj.GetComponent<Renderer>().material = _currentMaterial;
        }
    }
}