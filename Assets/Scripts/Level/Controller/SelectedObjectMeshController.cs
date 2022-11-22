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


       // private GridElementMovementYStatus _gridElementMovementYStatus;

        internal void TurnOnLight(GameObject hit)
        {
             hit.GetComponent<Renderer>().material = _lightMaterial;
        }

        internal void TurnOffLight(GameObject _hitObj)
        {
            _hitObj.GetComponent<Renderer>().material = _currentMaterial;
        }

        internal void MoveToTop(GameObject _hitObj)
        {
            //if (_hitObj.transform.position.y == 0)
            //{
            //    _hitObj.transform.position = new Vector3(_hitObj.transform.position.x,
            //                                                 _hitObj.transform.position.y + _hitObj.transform.localScale.y / 4,
            //                                                 _hitObj.transform.position.z);

            //}
       
        }

        internal void GetBackPositon(GameObject _hitObj)
        {
            //if (_hitObj.transform.position.y == _hitObj.transform.localScale.y / 4)
            //{
            //    _hitObj.transform.position = new Vector3(_hitObj.transform.position.x,
            //                                                 _hitObj.transform.position.y - _hitObj.transform.localScale.y / 4,
            //                                                 _hitObj.transform.position.z);
            //}
        }
    }
}