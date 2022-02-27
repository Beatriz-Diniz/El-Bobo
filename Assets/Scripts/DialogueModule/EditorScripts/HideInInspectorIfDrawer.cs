using UnityEditor;
using UnityEngine;

namespace Fog.Editor
{
    #if UNIITY_EDITOR
    [CustomPropertyDrawer(typeof(HideInInspectorIf))]
    [CustomPropertyDrawer(typeof(HideInInspectorIfNot))]
    public class HideInInspectorIfDrawer : PropertyDrawer
    {
        private string ConditionName { get { return ((BaseHideInInspectorIf) attribute).conditionName; } }
        private bool InvertCondition { get { return ((BaseHideInInspectorIf) attribute).invertCondition; } }
        private bool IsHidden(SerializedProperty property)
        {
            SerializedProperty condition = property.serializedObject.FindProperty(property.propertyPath.Replace(property.name, ConditionName));
            if (condition != null)
            {
                return InvertCondition ? !condition.boolValue : condition.boolValue;
            }
            else
            {
                Debug.LogWarning($"{property.GetType()}.{property.name}: Can't find field with name: {ConditionName}");
                return false;
            }
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!IsHidden(property))
            {
                EditorGUI.BeginProperty(position, label, property);
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
                EditorGUI.EndProperty();
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsHidden(property))
            {
                return -EditorGUIUtility.standardVerticalSpacing;
            }
            else
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
        }
    }
    #endif
}