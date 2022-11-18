using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface ISaver
    {
        void UpdateSave<T>(T saveableData);
    }
}