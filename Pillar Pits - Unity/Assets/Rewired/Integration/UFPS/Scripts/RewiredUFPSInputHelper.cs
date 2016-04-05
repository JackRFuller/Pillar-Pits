// Copyright (c) 2015 Augie R. Maddox, Guavaman Enterprises. All rights reserved.

using UnityEngine;
using Rewired;

namespace Rewired.Integration.UFPS {

    [AddComponentMenu("")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class RewiredUFPSInputHelper : vp_Input {

        // Inspector vars
        [SerializeField]
        public bool m_fixJoystickLookSensitivity = true;
        [SerializeField]
        protected int m_lookActionX = 18;
        [SerializeField]
        protected int m_lookActionY = 19;
        [SerializeField]
        protected float m_fixJLSTargetFPS = 60.0f;
        [SerializeField]
        protected bool m_fixJLSUseSmoothDeltaTime = false;

        // Working vars
        protected Rewired.Player player;
        protected string lookActionXName;
        protected string lookActionYName;
        protected bool fixJLSInitialized;

        // Properties
        public bool fixJoystickLookSensitivity { get { return m_fixJoystickLookSensitivity; } set { if(value == m_fixJoystickLookSensitivity) return; m_fixJoystickLookSensitivity = value; if(value) InitializeFixJLS(); } }
        public int lookActionX { get { return m_lookActionX; } set { if(value == m_lookActionX) return; m_lookActionX = value; InitializeFixJLS(); } }
        public int lookActionY { get { return m_lookActionY; } set { if(value == m_lookActionY) return; m_lookActionY = value; InitializeFixJLS(); } }
        public float fixJLSTargetFPS { get { return m_fixJLSTargetFPS; } set { m_fixJLSTargetFPS = value; } }
        public bool fixJLSUseSmoothDeltaTime { get { return m_fixJLSUseSmoothDeltaTime; } set { m_fixJLSUseSmoothDeltaTime = value; } }


        protected override void Awake() {

            // Set up vars for fix joystick look sensitivity
            if(m_fixJoystickLookSensitivity) InitializeFixJLS();

            // Get the Rewired Player
            player = ReInput.players.GetPlayer(0);

            // Set up singleton
            m_Instance = this;
        }

        void OnValidate() {
            // Update data when inspector vars change
            if(Application.isPlaying && m_fixJoystickLookSensitivity) InitializeFixJLS();
        }

        // Override Methods

        // Override all input methods in the base class to get input from Rewired.

        public override bool DoGetButtonAny(string button) {
            return DoGetButton(button) || DoGetButtonDown(button) || DoGetButtonUp(button);
        }

        public override bool DoGetButton(string button) {
            return player.GetButton(button);
        }

        public override bool DoGetButtonDown(string button) {
            return player.GetButtonDown(button);
        }

        public override bool DoGetButtonUp(string button) {
            return player.GetButtonUp(button);
        }

        public override float DoGetAxisRaw(string axis) {

            float value = player.GetAxisRaw(axis);
            
            
            // Scale joystick values for Look axes so they stay constant regardless of frame rate.
            // This negates UFPS's look axis processing that expects a mouse delta instead of -1.0f to 1.0f joystick axis value.
            if(m_fixJoystickLookSensitivity && (axis == lookActionXName || axis == lookActionYName)) {
                Controller controller = player.controllers.GetLastActiveController(); // determine the last controller type used by the player
                if(controller != null && (controller.type == ControllerType.Joystick || controller.type == ControllerType.Custom)) { // joystick or custom controller axis
                    value *= (m_fixJLSUseSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * m_fixJLSTargetFPS; // scale value to negate UFPS's processing in vp_FPInput.cs that expects a mouse delta
                }
            }

            return value;
        }

        // Override methods in the base class which no longer have any useful function.

        public override void SetDirty(bool dirty) { LogNotSupportedError("SetDirty"); }
        public override void SetupDefaults(string type = "") { LogNotSupportedError("SetupDefaults"); }
        public override void AddButton(string n, KeyCode k = KeyCode.None) { LogNotSupportedError("AddButton"); }
        public override void AddAxis(string n, KeyCode pk = KeyCode.None, KeyCode nk = KeyCode.None) { LogNotSupportedError("AddAxis"); }
        public override void AddUnityAxis(string n) { LogNotSupportedError("AddUnityAxis"); }
        public override void UpdateDictionaries() { LogNotSupportedError("UpdateDictionaries"); }

        // Protected Methods

        protected virtual void InitializeFixJLS() {
            if(!ReInput.isReady) return;

            // Get Look axis actions
            Rewired.InputAction action = ReInput.mapping.GetAction(m_lookActionX);
            if(action == null) {
                Debug.LogError("Rewired UFPS: The Action chosen for Look Action X does not exist!");
                m_fixJoystickLookSensitivity = false; // disable fix
            } else {
                lookActionXName = action.name; // cache action name
            }

            action = ReInput.mapping.GetAction(m_lookActionY);
            if(action == null) {
                Debug.LogError("Rewired UFPS: The Action chosen for Look Action Y does not exist!");
                m_fixJoystickLookSensitivity = false; // disable fix
            } else {
                lookActionYName = action.name; // cache action name
            }

            fixJLSInitialized = true;
        }

        protected virtual void LogNotSupportedError(string name) {
            Debug.LogError("Rewired UFPS: The method \"" + name + "\" is not supported. Use Rewired to manage input and reassignment instead.");
        }
    }
}