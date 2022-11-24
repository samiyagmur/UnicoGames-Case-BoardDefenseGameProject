using Type;
using UnityEngine;

namespace Interfaces
{
    public interface IAddObject
    {
        void AddToPool(PoolObjectType poolObjectType, GameObject obj);
    }
}