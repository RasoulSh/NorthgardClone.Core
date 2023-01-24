using System;
using Northgard.Core.Common.UnityExtensions.UnityReadOnlyField;
using UnityEngine;

namespace Northgard.Core.Entities
{
    [Serializable]
    public abstract class GameObjectEntity
    {
        [HideInInspector] public bool isInstance;
        [ReadOnlyField] [HideIfEmpty] public string id;
        [ReadOnlyField] public string prefabId = GenerateGuid();
        public string title;
        [HideInInspector] public Vector3 position;
        [HideInInspector] public Quaternion rotation;
        [HideInInspector] public Bounds bounds;

        internal void ConvertToInstance()
        {
            id = GenerateGuid();
            isInstance = true;
        }

        private static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}