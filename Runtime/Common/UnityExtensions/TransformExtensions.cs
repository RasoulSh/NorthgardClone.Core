using UnityEngine;

namespace Northgard.Core.Common.UnityExtensions
{
    public static class TransformExtensions
    {
        public static void DestroyAllChildren(this Transform transform)
        {
            var gridTransform = transform;
            var itemsCount = gridTransform.childCount;
            for (int i = 0; i < itemsCount; i++)
            {
                Object.Destroy(gridTransform.GetChild(i).gameObject);
            }
        }
    }
}