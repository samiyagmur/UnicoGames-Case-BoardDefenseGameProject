using Controller;
using Extantions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class EnemySignals : MonoSingleton<EnemySignals>
    {
        public UnityAction<List<GridElement>> onLevelInitilize = delegate { };

        public UnityAction onPlay = delegate { };

        public UnityAction onFail = delegate { };

        public UnityAction onLevelSuccessfull = delegate { };

        public UnityAction onClearActiveLevel = delegate { };

        public UnityAction onReset = delegate { };

        public UnityAction onNextLevel = delegate { };

    }
}