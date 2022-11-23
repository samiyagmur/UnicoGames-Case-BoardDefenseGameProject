using Interfaces;
using Signals;
using Type;
using UnityEngine;

namespace Command
{
    public class ClearActiveLevelCommand : MonoBehaviour,IPushObject
    {
        public void ClearActiveLevel(Transform levelHolder)
        {
            PushToPool(PoolObjectType.Level, levelHolder.GetChild(0).gameObject);

            levelHolder.GetChild(0).gameObject.transform.SetParent(null);
        }

        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleaseObjectFromPool(poolObjectType, obj);
        }
    }
}