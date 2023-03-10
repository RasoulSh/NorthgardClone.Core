#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Northgard.Core.UnityExtensions.Attributes.UnityHideIfEmpty.Editor
{
    [CustomPropertyDrawer(typeof(HideIfEmptyAttribute))]
    public class HideIfEmptyPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.stringValue != "")
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}
#endif