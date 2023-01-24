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
        public IGameObjectBehaviour<T> Instantiate(T initialData = null)
        {
            var instance = Instantiate(this);
            instance.Data.ConvertToInstance();
            if (initialData is { isInstance: true })
            {
                instance.Initialize(initialData);
            }
            instance.IsInstance = true;
            return instance;
        }

        protected virtual void Initialize(T initialData)
        {
            Data.id = initialData.id;
            Data.title = initialData.title;
            SetPosition(initialData.position);
            SetRotation(initialData.rotation);
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
            Data.rotation = Transform.rotation = rotation;
            UpdateLocationData();
            OnRotationChanged?.Invoke(this);
        }

        protected virtual void OnValidate() => UpdateLocationData();

        private void UpdateLocationData()
        {
            if (Data == null)
            {
                return;
            }
            Data.position = Transform.position;
            Data.rotation = Transform.rotation;
            Data.bounds = BoundaryCollider.bounds;
        }
    }
}