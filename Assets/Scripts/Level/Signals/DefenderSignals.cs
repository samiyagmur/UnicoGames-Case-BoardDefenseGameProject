using Extantions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class DefenderSignals : MonoSingleton<DefenderSignals>
    {
        public UnityAction<RaycastHit> onMouseFollow = delegate { };
    }
}