using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HideField))]
public class HideFieldPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Retrieve attribute data
        HideField hideFieldAttribute = (HideField)attribute;
        // Check if the property should be enabled
        bool enabled = GetHideAttributeResult(hideFieldAttribute, property);

        // Enable/disable the property
        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;

        // Check if we should draw the property
        if (!hideFieldAttribute.HideInInspector || enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        // Ensure that the next property being drawn uses the correct settings
        GUI.enabled = wasEnabled;
    }

    private bool GetHideAttributeResult(HideField hideFieldAttribute, SerializedProperty property)
    {
        bool enabled = true;

        //Look for the sourcefield that the property belongs to
        string propertyPath = property.propertyPath;
        string conditionPath = propertyPath.Replace(property.name, hideFieldAttribute.ConditionalSourceField);
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

        if (sourcePropertyValue != null)
        {
            enabled = sourcePropertyValue.boolValue;
        }
        else
        {
            Debug.LogWarning($"Attempting to use a HideField attribute but no matching SourcePropertyValue found in object: {hideFieldAttribute.ConditionalSourceField}");
        }

        return enabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        HideField hideFieldAttribute = (HideField)attribute;
        bool enabled = GetHideAttributeResult(hideFieldAttribute, property);

        if (!hideFieldAttribute.HideInInspector || enabled)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        } else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }
}
