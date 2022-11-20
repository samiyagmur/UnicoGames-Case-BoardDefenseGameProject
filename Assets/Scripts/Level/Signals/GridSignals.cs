using Extantions;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class GridSignals : MonoSingleton<GridSignals>
    {
        public UnityAction<GameObject> onClickGridElement = delegate { };
    }
}