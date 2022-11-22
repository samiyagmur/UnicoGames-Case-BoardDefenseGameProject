using Extantions;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<int> onScoreMultiply = delegate { };

        public UnityAction<int> onUpdateGold = delegate { };

        public UnityAction<int> onUpdateGem = delegate { };

        public UnityAction<int> onInitLastGoldScore = delegate { };

        public UnityAction<int> onInitLastDiamondScore = delegate { };
    }
}