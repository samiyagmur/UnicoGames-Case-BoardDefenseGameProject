using Controller;
using Extantions;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Signals
{
    public class EnemySignals : MonoSingleton<EnemySignals>
    {
        public UnityAction<List<GridElements>> onLevelInit = delegate { };

        public UnityAction onEnemyDeadFromDefander = delegate { };

        public UnityAction onPassEnemyFromPortal = delegate { };
    }
}