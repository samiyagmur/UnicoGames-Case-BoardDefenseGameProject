using Data.ValueObject;
using Extantions;
using System;
using Type;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<int> onLevelInitilize = delegate { };

        public UnityAction<LevelData> onGetLevelData = delegate { };

        public Func<LevelData> onGetLevelDataWhenSpawn = delegate { return null; };

        public UnityAction onPlay = delegate { };

        public UnityAction rePlay = delegate { };

        public UnityAction onFail = delegate { };

        public UnityAction onLevelSuccessfull = delegate { };

        public UnityAction onClearActiveLevel = delegate { };

        public UnityAction onReset = delegate { };

        public UnityAction onNextLevel = delegate { };

        public UnityAction<bool> onUpdateVibrationStatus = delegate { };

        public UnityAction<DefanderType> onClickCharacterButton = delegate { };

        public UnityAction<DefanderData> onInitDefenderInfo = delegate { };
    }
}