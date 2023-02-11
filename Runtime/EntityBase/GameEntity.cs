using System;
using Northgard.Core.UnityExtensions.Attributes.UnityHideIfEmpty;
using Northgard.Core.UnityExtensions.Attributes.UnityReadOnlyField;

namespace Northgard.Core.EntityBase
{
    public class GameEntity
    {
        [ReadOnlyField] [HideIfEmpty] public string id;
        
        protected static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}