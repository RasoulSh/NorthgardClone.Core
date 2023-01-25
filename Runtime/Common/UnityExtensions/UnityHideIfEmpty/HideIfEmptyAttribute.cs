using System;
using UnityEngine;

namespace Northgard.Core.Common.UnityExtensions.UnityHideIfEmpty
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class HideIfEmptyAttribute : PropertyAttribute { }
}