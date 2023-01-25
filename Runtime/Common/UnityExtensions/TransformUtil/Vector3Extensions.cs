using UnityEngine;

namespace Northgard.Core.Common.UnityExtensions.TransformUtil
{
    public static class Vector3Extensions
    {
        public static Vector3 Multiply(this Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
    }
}