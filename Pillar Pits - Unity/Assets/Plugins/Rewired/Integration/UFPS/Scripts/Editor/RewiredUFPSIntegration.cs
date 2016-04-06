// Copyright (c) 2015 Augie R. Maddox, Guavaman Enterprises. All rights reserved.

using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Rewired.Integration.UFPS.Editor {

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public sealed class RewiredUFPSIntegration {
        
        private const uint version = 1;
        private const string rewiredUFPSInputManagerPrefabGUID = "0c0a74c0ad74ada41b39214a96bf141c"; // GUID of the prefab file

        [MenuItem(Consts.menuRoot + "/Integration/UFPS/About")]
        public static void Integration_UFPS_About() {
            EditorUtility.DisplayDialog("Rewired UFPS Integration Pack", "Version: " + version.ToString(), "Close");
        }

        [MenuItem(Consts.menuRoot + "/Integration/UFPS/Create Rewired UFPS Input Manager")]
        public static void Integration_UFPS_CreateInputManager() {

            string pathToPrefab = AssetDatabase.GUIDToAssetPath(rewiredUFPSInputManagerPrefabGUID);
            if(pathToPrefab == null || pathToPrefab == string.Empty) {
                Debug.LogWarning("Rewired UFPS Input Manager prefab not found at expected Guid! Please reinstall the UFPS Integration Pack.");
                return;
            }

            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(pathToPrefab, typeof(GameObject));
            if(prefab == null) {
                Debug.LogWarning("Rewired UFPS Input Manager prefab not found! Please reinstall the UFPS Integration Pack.");
                return;
            }
            
            InputManager im = prefab.GetComponent<Rewired.InputManager>();
            if(im == null) {
                Debug.LogWarning("Rewired Input Manager component not found on prefab! Please reinstall the UFPS Integration Pack.");
                return;
            }

            // Instantiate the prefab with the default UFPS settings
            GameObject go = (GameObject)UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            go.name = prefab.name; // rename the prefab
        }
    }
}