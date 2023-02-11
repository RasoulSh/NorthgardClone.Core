using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Northgard.Core.UnityExtensions.Attributes.UnityReadOnlyField;
using UnityEngine;

namespace Northgard.Core.EntityBase
{
    [Serializable]
    public abstract class GameObjectEntity : GameEntity
    {
        [HideInInspector] public bool isInstance;
        [ReadOnlyField] public string prefabId = GenerateGuid();
        [HideInInspector] public Vector3 position;
        [HideInInspector] public Quaternion rotation;
        [HideInInspector] public Vector3 boundsCenter;
        [HideInInspector] public Vector3 boundsSize;
        public string title;
        [JsonIgnore][XmlIgnore] public Bounds Bounds => new Bounds(boundsCenter, boundsSize);

        public void ConvertToInstance()
        {
            id = GenerateGuid();
            isInstance = true;
        }
    }
}