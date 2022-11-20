using Signals;
using System;
using System.Collections;
using UnityEngine;

namespace Controller
{
    public class SelectManager : MonoBehaviour
    {

        [SerializeField]
        private SelectedObjectMeshController selectedObjectMeshController;

        private GameObject _hitObj;

        private int IsThisFirstTimes=0;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTouch += OnInputTouch;
            InputSignals.Instance.onDragMouse += OnDragMouse;
            InputSignals.Instance.onInputReleased += OnInputReleased;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTouch -= OnInputTouch;
            InputSignals.Instance.onDragMouse -= OnDragMouse;
            InputSignals.Instance.onInputReleased -= OnInputReleased;
        }


        private void OnDisable() => UnsubscribeEvents();
        private void OnDragMouse()
        {
           
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {

              
                if (hit.transform == null) UnClick(_hitObj);
          
                if (_hitObj!= hit.transform.gameObject)
                {
                   

                    UnClick(_hitObj);
                }

                _hitObj = hit.transform.gameObject;

                Click(hit.transform.gameObject);
            }
            else
            {
                UnClick(_hitObj);
            }
            
        }

        private void UnClick(GameObject hitObj)
        {
            if(hitObj == null) return;
             Debug.Log(_hitObj);
            selectedObjectMeshController.TurnOffLight(hitObj);
        }

        private void Click(GameObject hitObj) => selectedObjectMeshController.TurnOnLight(hitObj);


        private void OnInputTouch()
        {

        }

        private void OnInputReleased()
        {
            throw new NotImplementedException();
        }

    }
}