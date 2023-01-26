using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Northgard.Core.Common.UnityExtensions.UnityHideIfEmpty;
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
        [HideInInspector] public Vector3 position;
        [HideInInspector] public Quaternion rotation;
        [HideInInspector] public Vector3 boundsCenter;
        [HideInInspector] public Vector3 boundsSize;
        public string title;
        [JsonIgnore][XmlIgnore] public Bounds Bounds => new Bounds(boundsCenter, boundsSize);

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