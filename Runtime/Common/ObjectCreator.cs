using System;
using JetBrains.Annotations;
using Northgard.Core.Abstraction.Common;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Northgard.Core.Common
{
    [UsedImplicitly]
    internal class ObjectCreator : MonoBehaviour, IObjectCreator
    {
        [Inject] private DiContainer _container;

        private static IObjectCreator _instance;
        internal static T InstantiatePrefab<T>(T original) where T : Object
        {
            return _instance.Instantiate(original);
        }

        private void Start()
        {
            _instance = this;
        }

        public T Instantiate<T>(T original) where T : Object
        {
            return _container.InstantiatePrefabForComponent<T>(original);
        }
    }
}