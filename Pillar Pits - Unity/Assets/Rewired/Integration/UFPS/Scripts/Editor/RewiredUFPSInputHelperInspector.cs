// Copyright (c) 2015 Augie R. Maddox, Guavaman Enterprises. All rights reserved.

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Rewired.Integration.UFPS.Editor {

    [CustomEditor(typeof(RewiredUFPSInputHelper))]
    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public sealed class RewiredUFPSInputHelperInspector : UnityEditor.Editor {

        private const string fixJoystickLookSensitivity = "m_fixJoystickLookSensitivity";
        private const string lookActionX = "m_lookActionX";
        private const string lookActionY = "m_lookActionY";
        private const string fixJLSTargetFPS = "m_fixJLSTargetFPS";
        private const string fixJLSUseSmoothDeltaTime = "m_fixJLSUseSmoothDeltaTime";

        private Dictionary<string, SerializedProperty> properties;
        private bool showAdvancedOptions;

        private Rewired.InputManager inputManager;

        void OnEnable() {
            if(properties == null) properties = new Dictionary<string, SerializedProperty>();
            else properties.Clear();

            AddProperty(fixJoystickLookSensitivity);
            AddProperty(lookActionX);
            AddProperty(lookActionY);
            AddProperty(fixJLSTargetFPS);
            AddProperty(fixJLSUseSmoothDeltaTime);

            inputManager = (target as MonoBehaviour).GetComponent<Rewired.InputManager>();
            if(inputManager == null) Debug.LogError("Rewired UFPS: Rewired Input Manager component is missing!");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(properties[fixJoystickLookSensitivity]);

            if(properties[fixJoystickLookSensitivity].boolValue) {

                string[] actionNames = inputManager.userData.GetActionNames();
                int[] actionIds = inputManager.userData.GetActionIds();

                DrawPopupProperty(actionIds, actionNames, properties[lookActionX]);
                DrawPopupProperty(actionIds, actionNames, properties[lookActionY]);
                showAdvancedOptions = EditorGUILayout.Foldout(showAdvancedOptions, "Advanced");
                if(showAdvancedOptions) {
                    DrawFloatProperty(properties[fixJLSTargetFPS], new GUIContent("Target FPS", "This must match the value set in Delta/SDelta in vp_Component.cs."), 0f, Mathf.Infinity);
                    DrawBoolProperty(properties[fixJLSUseSmoothDeltaTime], new GUIContent("Use Smooth Delta Time", "Only enable this if you've changed vp_FPInput.cs to use SDelta instead of Delta for mouse look calculations."));
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawPopupProperty(int[] values, string[] names, SerializedProperty serializedProperty) {
            DrawPopupProperty(new GUIContent(serializedProperty.displayName, serializedProperty.tooltip), values, names, serializedProperty);
        }
        private void DrawPopupProperty(GUIContent label, int[] values, string[] names, SerializedProperty serializedProperty) {
            int valueCount = values != null ? values.Length : 0;
            int nameCount = names != null ? names.Length : 0;
            if(valueCount != nameCount) throw new System.Exception("values.Length must equal names.Length!");

            int selectedIndex = valueCount > 0 ? System.Array.IndexOf<int>(values, serializedProperty.intValue) : -1;

            int newIndex = EditorGUILayout.Popup(label, selectedIndex, ToGUIContentArray(names));
            if(newIndex != selectedIndex) { // 
                serializedProperty.intValue = values[newIndex];
            }
        }

        private void DrawFloatProperty(SerializedProperty serializedProperty, GUIContent guiContent, float min, float max) {
            if(serializedProperty.floatValue < min) serializedProperty.floatValue = min;
            if(serializedProperty.floatValue > max) serializedProperty.floatValue = max;
            float value = EditorGUILayout.FloatField(guiContent, serializedProperty.floatValue);
            if(value != serializedProperty.floatValue) {
                serializedProperty.floatValue = value;
            }
        }

        private void DrawBoolProperty(SerializedProperty serializedProperty, GUIContent guiContent) {
            bool value = EditorGUILayout.Toggle(guiContent, serializedProperty.boolValue);
            if(value != serializedProperty.boolValue) {
                serializedProperty.boolValue = value;
            }
        }

        private GUIContent[] ToGUIContentArray(string[] array) {
            if(array == null) return null;
            GUIContent[] retVal = new GUIContent[array.Length];
            for(int i = 0; i < array.Length; i++) {
                retVal[i] = new GUIContent(array[i]);
            }
            return retVal;
        }

        private void AddProperty(string name) {
            properties.Add(name, serializedObject.FindProperty(name));
        }
    }
}