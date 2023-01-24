using Northgard.Core.Entities;
using UnityEngine;

namespace Northgard.Core.Abstraction.Behaviours
{
    public interface IGameObjectBehaviour<T> where T : GameObjectEntity
    {
        T Data { get; }
        bool IsInstance { get; }
        IGameObjectBehaviour<T> Instantiate(T initialData = null);
        void Destroy();
        void SetPosition(Vector3 position);
        void SetRotation(Quaternion rotation);
        event GameObjectBehaviourDelegate OnPositionChanged;
        event GameObjectBehaviourDelegate OnRotationChanged;
        event GameObjectBehaviourDelegate OnDestroying;
        public delegate void GameObjectBehaviourDelegate(IGameObjectBehaviour<T> objectBehaviour);
    }
}