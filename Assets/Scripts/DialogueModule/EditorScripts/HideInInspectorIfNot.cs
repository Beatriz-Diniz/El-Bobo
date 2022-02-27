using System;
using UnityEngine;

namespace Fog.Editor
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class HideInInspectorIfNot : BaseHideInInspectorIf
    {
        public HideInInspectorIfNot(string conditionName) : base(conditionName, true) {}
    }
}