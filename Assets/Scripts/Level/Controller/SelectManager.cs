using Signals;
using UnityEngine;

namespace Controller
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField]
        private SelectedObjectMeshController selectedObjectMeshController;

        private GameObject _hitObj;
        private Vector3 _hitObjPoint;
        private int _triggerCount;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onDragMouse += OnDragMouse;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onDragMouse -= OnDragMouse;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnDragMouse(RaycastHit hit)
        {
            if (hit.transform.CompareTag("GridElement"))
            {
                if (_hitObj != hit.transform.gameObject) UnClick(_hitObj);

                _hitObj = hit.transform.gameObject;

                Click();
            }
            else
            {
                UnClick(_hitObj);
            }
        }

        private void Click()
        {
            selectedObjectMeshController.TurnOnLight(_hitObj);
        }

        private void UnClick(GameObject hitObj)
        {
            if (hitObj == null) return;

            selectedObjectMeshController.TurnOffLight(hitObj);
        }
    }
}