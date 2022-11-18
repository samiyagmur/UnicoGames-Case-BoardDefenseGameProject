using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface ILoader
    {
        T UpdateLoad<T>();
    }
}