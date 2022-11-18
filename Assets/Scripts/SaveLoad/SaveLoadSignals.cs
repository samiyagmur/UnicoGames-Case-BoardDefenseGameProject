using Extantions;
using Signals;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SaveLoadSignals<T>
    {
        public UnityAction<T> onSaveGame = delegate {   };

   
    }
}