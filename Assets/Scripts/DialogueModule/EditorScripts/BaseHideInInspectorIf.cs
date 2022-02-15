using UnityEngine;

namespace Fog.Editor
{
    public abstract class BaseHideInInspectorIf : PropertyAttribute
    {
        public string conditionName;
        public bool invertCondition;
        public BaseHideInInspectorIf(string conditionName, bool invertCondition)
        {
			this.conditionName = conditionName;
            this.invertCondition = invertCondition;
        }
    }
}