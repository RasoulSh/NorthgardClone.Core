using Northgard.Core.Abstraction.Behaviours;
using Northgard.Core.Common.UnityExtensions.TransformUtil;
using Northgard.Core.Entities;
using UnityEngine;
using Zenject;
using ILogger = Northgard.Core.Abstraction.Logger.ILogger;

namespace Northgard.Core.Application.Behaviours
{
    [RequireComponent(typeof(Collider))]
    public abstract class GameObjectBehaviour<T> : MonoBehaviour, IGameObjectBehaviour<T> where T : GameObjectEntity
    {
        [Inject] private ILogger _logger;
        [SerializeField] private T data;
        private BoxCollider _boundaryCollider;
        private Transform _transform;
        private BoxCollider BoundaryCollider => _boundaryCollider ??= GetComponent<BoxCollider>();
        private Transform Transform => _transform ??= transform;

        public virtual T Data => data;

        public bool IsInstance { get; private set; }
        public event IGameObjectBehaviour<T>.GameObjectBehaviourDelegate OnPositionChanged;
        public event IGameObjectBehaviour<T>.GameObjectBehaviourDelegate OnRotationChanged;
        public event IGameObjectBehaviour<T>.GameObjectBehaviourDelegate OnDestroying;

        public IGameObjectBehaviour<T> Instantiate()
        {
            var instance = Instantiate(this);
            instance.Data.ConvertToInstance();
            instance.IsInstance = true;
            return instance;
        }

        public virtual void Initialize(T initialData)
        {
            if (initialData == null)
            {
                _logger.LogError("You are trying to initialize a game object using null data", this);
                return;
            }
            if (initialData.isInstance == false)
            {
                _logger.LogError("You are trying to initialize a game object that is not an instance", this);
                return;
            }
            data = initialData;
            SetPosition(initialData.position, true);
            SetRotation(initialData.rotation, true);
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

        public void SetPosition(Vector3 position) => SetPosition(position, false);
        private void SetPosition(Vector3 position, bool ignoreNotify)
        {
            Transform.position = position;
            if (ignoreNotify == false)
            {
                UpdateLocationData();
                OnPositionChanged?.Invoke(this);   
            }
        }

        public void SetRotation(Quaternion rotation) => SetRotation(rotation, false);
        private void SetRotation(Quaternion rotation, bool ignoreNotify)
        {
            Data.rotation = Transform.rotation = rotation;
            if (ignoreNotify == false)
            {
                UpdateLocationData();
                OnRotationChanged?.Invoke(this);   
            }
        }

        protected virtual void OnValidate() => UpdateLocationData();

        private void UpdateLocationData()
        {
            #if UNITY_EDITOR
            if (Data == null)
            {
                return;
            }
            #endif
            Data.position = Transform.position;
            Data.rotation = Transform.rotation;
            var col = BoundaryCollider;
            var trans = Transform;
            Data.boundsSize = col.size.Multiply(trans.lossyScale);
            data.boundsCenter = col.center + trans.position;
        }
    }
}