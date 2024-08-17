using CommonEditor.Extensions;
using UnityEditor;
using UnityEngine;

namespace CommonEditor
{
    public static class UEditorGUI
    {
        public static bool PropertyField(ref Rect position, SerializedProperty property, GUIContent label = null, bool includeChildren = true)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, label);

            position.height = EditorGUI.GetPropertyHeight(property, includeChildren);
            var result = EditorGUI.PropertyField(position, property, includeChildren);

            position.y += position.height + UEditorGUIUtility.SpaceHeight;
            return result;
        }

        public static bool PropertyField(ref Rect position, SerializedProperty property, bool includeChildren = true)
        {
            return PropertyField(ref position, property, null, includeChildren);
        }

        public static bool PropertyField(ref Rect position, SerializedProperty property)
        {
            return PropertyField(ref position, property, null, true);
        }

        public static float FloatField(ref Rect position, string label, float value)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            var result = EditorGUI.FloatField(position, label, value);
            position.y += position.height + UEditorGUIUtility.SpaceHeight;
            return result;
        }

        public static float GetPropertyHeight(SerializedProperty property, bool includeChildren = true)
        {
            return EditorGUI.GetPropertyHeight(property, includeChildren) + UEditorGUIUtility.SpaceHeight;
        }

        public static float GetPropertiesHeight(params SerializedProperty[] properties)
        {
            var result = 0.0f;
            foreach (var property in properties)
                result += GetPropertyHeight(property);
            return result;
        }

        public static void UnfoldedLabelField(ref Rect position, GUIContent label, GUIStyle style)
        {
            position.x -= UEditorGUIUtility.IndentWidth;
            position.width += UEditorGUIUtility.IndentWidth;
            position.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.LabelField(position, label, style);

            position.y += position.height + UEditorGUIUtility.SpaceHeight;
        }

        public static void UnfoldedLabelField(ref Rect position, GUIContent label)
        {
            UnfoldedLabelField(ref position, label, EditorStyles.label);
        }

        public static void UnfoldedPropertyField(ref Rect position, SerializedProperty property, bool includeChildren)
        {
            foreach (var child in property.GetChildren())
            {
                position.height = EditorGUI.GetPropertyHeight(child, includeChildren);
                EditorGUI.PropertyField(position, child, includeChildren);
                position.y += position.height + UEditorGUIUtility.SpaceHeight;
            }
        }

        public static void UnfoldedPropertyField(ref Rect position, SerializedProperty property)
        {
            UnfoldedPropertyField(ref position, property, true);
        }

        public static float GetUnfoldedLabelHeight(GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + UEditorGUIUtility.SpaceHeight;
        }

        public static float GetUnfoldedPropertyHeight(SerializedProperty property)
        {
            var result = 0.0f;
            foreach (var child in property.GetChildren())
                result += EditorGUI.GetPropertyHeight(child, true) + UEditorGUIUtility.SpaceHeight;
            return result;
        }
    }
}