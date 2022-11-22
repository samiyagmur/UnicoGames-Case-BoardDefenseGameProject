using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using Type;
using UnityEngine;

namespace Controller
{
    public class SelectManager : MonoBehaviour
    {

        [SerializeField]
        private SelectedObjectMeshController selectedObjectMeshController;

        [SerializeField]
        private List<GameObject> SelectableGridElementList;

        private GameObject _hitObj;
        private Vector3 _hitObjPoint;
        private int _triggerCount;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTouch += OnInputTouch;
            InputSignals.Instance.onDragMouse += OnDragMouse;
            SelectSignals.Instance.onSelectedGrid += onSelectedGrid;
        }

        private void UnsubscribeEvents()
        {
            
            InputSignals.Instance.onInputTouch -= OnInputTouch;
            InputSignals.Instance.onDragMouse -= OnDragMouse;
            SelectSignals.Instance.onSelectedGrid -= onSelectedGrid;

            //if kill defender activete list
        }


        private void OnDisable() => UnsubscribeEvents();
        private void OnDragMouse(RaycastHit hit)
        {
            if (SelectableGridElementList.Contains(hit.transform.gameObject))
            {
                if (hit.transform.gameObject == null) return;

                if (_hitObj != hit.transform.gameObject)
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
            if (hit.transform.CompareTag("Ground"))
            {
                UnClick(_hitObj);
            }
        }

        private void onSelectedGrid(List<GridElements> gridElements)
        {
            for (int i = 0; i < gridElements.Count; i++)
            {
                if (gridElements[i].gridElementStatus == GridElementStatus.Selectable)
                {
                    SelectableGridElementList.Add(gridElements[i]._gridElement);
                }
            }
        }

        private void Click(GameObject hitObj)
        {
            selectedObjectMeshController.TurnOnLight(hitObj);
            selectedObjectMeshController.MoveToTop(hitObj);

        }

        private void UnClick(GameObject hitObj)
        {
            if(hitObj == null) return;

            selectedObjectMeshController.TurnOffLight(hitObj);
            selectedObjectMeshController.GetBackPositon(hitObj);
     

        }

        private void OnInputTouch()
        {
            if (!SelectableGridElementList.Contains(_hitObj)) return;
            SelectableGridElementList.Remove(_hitObj);
        }


    }
}

