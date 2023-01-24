using Northgard.Core.Abstraction.Behaviours;
using Northgard.Core.Entities;
using UnityEngine;

namespace Northgard.Core.Application.Behaviours
{
    [RequireComponent(typeof(Collider))]
    public abstract class GameObjectBehaviour<T> : MonoBehaviour, IGameObjectBehaviour<T> where T : GameObjectEntity
    {
        [SerializeField] private T data;
        private Collider _boundaryCollider;
        private Transform _transform;
        private Collider BoundaryCollider => _boundaryCollider ??= GetComponent<Collider>();
        private Transform Transform => _transform ??= transform;
        public virtual T Data => data;

        public bool IsInstance { get; private set; }
        public event IGameObjectBehaviour<T>.GameObjectBehaviourDelegate OnPositionChanged;
        public event IGameObjectBehaviour<T>.GameObjectBehaviourDelegate OnRotationChanged;
        public event IGameObjectBehaviour<T>.GameObjectBehaviourDelegate OnDestroying; 
        public IGameObjectBehaviour<T> Instantiate()
        {
            var instance = Instantiate(this);
            instance.data.RenewId();
            instance.IsInstance = true;
            return instance;
        }

        public void Destroy()
        {
            if (IsInstance == false)
            {
                return;
            }
            OnDestroying?.Invoke(this);
            Destroy(gameObject);
        }

        public void SetPosition(Vector3 position)
        {
            Transform.position = position;
            UpdateLocationData();
            OnPositionChanged?.Invoke(this);
        }

        public void SetRotation(Quaternion rotation)
        {
            data.rotation = Transform.rotation = rotation;
            UpdateLocationData();
            OnRotationChanged?.Invoke(this);
        }

        protected virtual void OnValidate() => UpdateLocationData();

        private void UpdateLocationData()
        {
            data.position = Transform.position;
            data.rotation = Transform.rotation;
            data.bounds = BoundaryCollider.bounds;
        }
    }
}