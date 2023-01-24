using System;
using Northgard.Core.Common.UnityExtensions.UnityReadOnlyField;
using UnityEngine;

namespace Northgard.Core.Entities
{
    [Serializable]
    public abstract class GameObjectEntity
    {
        [ReadOnlyField] public string id = GenerateGuid();
        [HideInInspector] public Vector3 position;
        [HideInInspector] public Quaternion rotation;
        [HideInInspector] public Bounds bounds;

        internal void RenewId()
        {
            id = GenerateGuid();
        }

        private static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}