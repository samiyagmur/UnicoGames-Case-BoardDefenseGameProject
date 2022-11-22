using Signals;
using System;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private bool _isFirstTouchTaken;
        private bool _isDrag;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onFail += OnFail;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onFail -= OnFail;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int mask1 = 1 << LayerMask.NameToLayer("Ground");
            int mask2 = 1 << LayerMask.NameToLayer("GridElement");

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, mask1| mask2))
            {
               // Debug.Log(hit.rigidbody.name);
                InputSignals.Instance.onDragMouse?.Invoke(hit);
            }
            //else
            //{
            //    hit.;
            //}
            if (_isFirstTouchTaken)
            {
               
                if (Input.GetMouseButtonDown(0))
                {
                    _isDrag = false;
                    InputSignals.Instance.onInputTouch?.Invoke();
                   
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    InputSignals.Instance.onInputReleased?.Invoke();
                    _isDrag = true;
                }
            }


            
        }
        private void Start()
        {
            _isFirstTouchTaken = true;
            _isDrag= true;
        }
        private void OnPlay() => StartToInput();

        private void OnFail() => StopToInput();

        private void StartToInput() => _isFirstTouchTaken = true;

        private void StopToInput() => _isFirstTouchTaken = false;
    }
}