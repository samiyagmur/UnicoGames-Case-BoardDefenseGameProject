using Extantions;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<int> onScoreUpdate = delegate { };

        public UnityAction<int> onLevelInitilize = delegate { };

        public UnityAction onPlay = delegate { };

        public UnityAction onFail = delegate { };

        public UnityAction onLevelSuccessfull = delegate { };

        public UnityAction onClearActiveLevel = delegate { };

        public UnityAction onReset = delegate { };

        public UnityAction onNextLevel = delegate { };

        public UnityAction<bool>onUpdateVibrationStatus = delegate { };

        public UnityAction<int> onInitLastDiamondScore= delegate { };

        public UnityAction<int> onInitLastGoldScore = delegate { };

    }
}