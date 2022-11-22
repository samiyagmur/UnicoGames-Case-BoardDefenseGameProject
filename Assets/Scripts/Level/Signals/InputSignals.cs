using Extantions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onInputTouch = delegate { };
        public UnityAction onInputReleased = delegate { };
        public UnityAction<RaycastHit> onDragMouse = delegate { };
    }
}