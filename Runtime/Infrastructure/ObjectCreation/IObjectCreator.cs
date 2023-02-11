using UnityEngine;

namespace Northgard.Core.Infrastructure.ObjectCreation
{
    public interface IObjectCreator
    {
        T Instantiate<T>(T original) where T : Object;
    }
}