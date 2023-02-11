using System;
using UnityEngine;

namespace Northgard.Core.UnityExtensions.Attributes.UnityReadOnlyField
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyFieldAttribute : PropertyAttribute { }
}