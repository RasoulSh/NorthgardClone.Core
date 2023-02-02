using UnityEngine;

namespace Northgard.Core.Abstraction.Common
{
    internal interface IObjectCreator
    {
        T Instantiate<T>(T original) where T : Object;
    }
}