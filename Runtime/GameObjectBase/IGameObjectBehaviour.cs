using Northgard.Core.EntityBase;
using UnityEngine;

namespace Northgard.Core.GameObjectBase
{
    public interface IGameObjectBehaviour<T> where T : GameObjectEntity
    {
        T Data { get; }
        bool IsInstance { get; }
        IGameObjectBehaviour<T> Instantiate();
        void Destroy();
        void SetPosition(Vector3 position);
        void SetRotation(Quaternion rotation);
        event GameObjectBehaviourDelegate OnPositionChanged;
        event GameObjectBehaviourDelegate OnRotationChanged;
        event GameObjectBehaviourDelegate OnDestroying;
        public delegate void GameObjectBehaviourDelegate(IGameObjectBehaviour<T> objectBehaviour);

        TC AddComponent<TC>() where TC : Component;
        GameObject CloneFakeInstance();
    }
}