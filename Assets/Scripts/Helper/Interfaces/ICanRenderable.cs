using UnityEngine;

namespace Interfaces
{
    internal interface ICanRenderable
    {
        bool CanSeeOnCamera(GameObject viewGameObject);
    }
}