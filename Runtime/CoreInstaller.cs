using Northgard.Core.Abstraction.Common;
using Northgard.Core.Common;
using UnityEngine;
using Zenject;

namespace Northgard.Core
{
    [RequireComponent(typeof(ObjectCreator))]
    public class CoreInstaller : MonoInstaller
    {
        
        
        public override void InstallBindings()
        {
            var objectCreator = GetComponent<ObjectCreator>();
            Container.Bind<IObjectCreator>().To<ObjectCreator>().FromInstance(objectCreator).AsSingle();
        }
    }
}