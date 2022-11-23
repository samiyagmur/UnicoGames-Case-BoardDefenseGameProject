using Status;
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

        internal void TurnOnLight(GameObject hitObj)
        {
            hitObj.GetComponent<Renderer>().material = _lightMaterial;
        }

        internal void TurnOffLight(GameObject hitObj)
        {
            hitObj.GetComponent<Renderer>().material = _currentMaterial;
        }

    }
}