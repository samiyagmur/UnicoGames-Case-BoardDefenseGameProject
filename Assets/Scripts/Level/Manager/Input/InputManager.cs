using Signals;
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
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void Update()
        {
            if (_isDrag)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                int mask1 = 1 << LayerMask.NameToLayer("Ground");
                int mask2 = 1 << LayerMask.NameToLayer("GridElement");

                if (Physics.Raycast(ray, out RaycastHit hit, 100f, mask1 | mask2))
                {
                    InputSignals.Instance.onDragMouse?.Invoke(hit);
                }
            }
            if (_isFirstTouchTaken)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    InputSignals.Instance.onInputTouch?.Invoke();
                }
            }
        }

        private void OnPlay() => StartToInput();

        private void OnReset() => StopToInput();

        private void StartToInput()
        {
            _isFirstTouchTaken = true;
            _isDrag = true;
        }

        private void StopToInput()
        {
            _isFirstTouchTaken = true;
            _isDrag = true;
        }
    }
}