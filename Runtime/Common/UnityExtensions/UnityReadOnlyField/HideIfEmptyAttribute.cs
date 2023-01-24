using System;
using UnityEngine;

namespace Northgard.Core.Common.UnityExtensions.UnityReadOnlyField
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class HideIfEmptyAttribute : PropertyAttribute { }
}