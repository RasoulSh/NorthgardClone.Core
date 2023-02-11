using System;
using UnityEngine;

namespace Northgard.Core.UnityExtensions.Attributes.UnityHideIfEmpty
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class HideIfEmptyAttribute : PropertyAttribute { }
}