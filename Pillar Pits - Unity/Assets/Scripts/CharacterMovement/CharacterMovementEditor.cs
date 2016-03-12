using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CharacterMovementEditor : CharacterMovementUIElements {

    [Header("Editor Attributes")]
    public GameObject UI;
    public vp_FPPlayerEventHandler FPPlayer;
    public vp_FPController movementScript;
    public vp_FPInput inputScript;
    public ac_FPParkour parkourScript;

    private Slider activeSlider;
    public GameObject SavingAndLoading;

    private enum menuMode
    {
        save,
        load,
    }

    private menuMode currentMenuMode;

    void Start()
    {
        UI.SetActive(false);

        SetSliderValues();
    }

    #region Motor

    public void SetAcceleration(Slider slider)
    {
        activeSlider = slider;
        movementScript.MotorAcceleration = slider.value;
    }

    public void SetMotorDampining(Slider slider)
    {
        activeSlider = slider;
        movementScript.MotorDamping = slider.value;
    }

    public void SetBackwardsSpeed(Slider slider)
    {
        activeSlider = slider;
        movementScript.MotorBackwardsSpeed = slider.value;
    }

    public void SetAirSpeed(Slider slider)
    {
        activeSlider = slider;
        movementScript.MotorAirSpeed = slider.value;
    }

    public void SetSlopeSpeedUp(Slider slider)
    {
        activeSlider = slider;
        movementScript.MotorSlopeSpeedUp = slider.value;
    }

    public void SetSlopeSpeedDown(Slider slider)
    {
        activeSlider = slider;
        movementScript.MotorSlopeSpeedDown = slider.value;
    }



    #endregion

    #region Jumping

    public void SetJumpForce(Slider slider)
    {
        activeSlider = slider;
        movementScript.MotorJumpForce = slider.value;
    }

    public void SetJumpForceDamping(Slider slider)
    {
        activeSlider = slider;
        movementScript.MotorJumpForceDamping = slider.value;
    }

    public void SetJumpForceHold(Slider slider)
    {
        activeSlider = slider;
        movementScript.MotorJumpForceHold = slider.value;
    }

    public void SetJumpForceHoldDamping(Slider slider)
    {
        activeSlider = slider;
        movementScript.MotorJumpForceHoldDamping = slider.value;
    }

    #endregion

    #region Physics

    public void SetPhysicsForceDamping(Slider slider)
    {
        activeSlider = slider;
        movementScript.PhysicsForceDamping = slider.value;
    }

    public void SetPhysicsPushForce(Slider slider)
    {
        activeSlider = slider;
        movementScript.PhysicsPushForce = slider.value;
    }

    public void SetPhysicsGravityModifier(Slider slider)
    {
        activeSlider = slider;
        movementScript.PhysicsGravityModifier = slider.value;
    }

    public void SetPhysicsWallBounce(Slider slider)
    {
        activeSlider = slider;
        movementScript.PhysicsWallBounce = slider.value;
    }

    public void SetPhysicWallFriction(Slider slider)
    {
        activeSlider = slider;
        movementScript.PhysicsWallFriction = slider.value;
    }

    #endregion

    #region Parkour

    #region DoubleJump

    public void SetDoubleJumpCount(Slider slider)
    {
        activeSlider = slider;
        parkourScript.DoubleJumpCount = (int)slider.value;
    }

    public void SetDoubleJumpForce(Slider slider)
    {
        activeSlider = slider;
        parkourScript.DoubleJumpForce = slider.value;
    }

    public void SetDoubleJumpForceForward(Slider slider)
    {
        activeSlider = slider;
        parkourScript.DoubleJumpForwardForce = slider.value;
    }

    #endregion

    #region WallRun

    public void SetWallRunDuration(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallRunDuration = slider.value;
    }

    public void SetWallRunGravity(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallRunGravity = slider.value;
    }

    public void SetWallRunMinSpeed(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallRunSpeedMinimum = slider.value;
    }

    public void SetWallRunCooldown(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallRunAgainTimeout = slider.value;
    }

    public void SetWallRunUpForce(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallRunUpForce = slider.value;
    }

    public void SetWallRunDismountForce(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallRunDismountForce = slider.value;
    }

    public void SetWallRunCameraTilt(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallRunTilt = slider.value;
    }

    public void SetWallRunRange(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallRunRange = slider.value;
    }

    #endregion

    #region Wall Jump

    public void SetWallJumpUpForce(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallJumpUpForce = slider.value;
    }

    public void SetWallJumpForce(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallJumpForce = slider.value;
    }

    public void SetWallJumpForwardForce(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallJumpForwardForce = slider.value;
    }

    #endregion

    #region WallHang

    public void SetWallHangDuration(Slider slider)
    {
        activeSlider = slider;
        parkourScript.WallHangDuration = slider.value;
    }

    public void SetWallHangLostGripStart(Slider slider)
    {
        activeSlider = slider;
        parkourScript.LosingGripStart = slider.value;
    }

    public void SetWallHangLostGripGravity(Slider slider)
    {
        activeSlider = slider;
        parkourScript.LosingGripGravity = slider.value;
    }

    #endregion

    #endregion

    public void SetTextValue(Text textValue)
    {
        textValue.text = activeSlider.value.ToString("F2");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            switch (FPPlayer.Pause.Get())
            {
                case (false):
                    FPPlayer.Pause.Set(true);
                    inputScript.MouseCursorForced = true;
                    inputScript.MouseCursorBlocksMouseLook = false;
                    UI.SetActive(true);

                    break;
                case (true):
                    FPPlayer.Pause.Set(false);
                    inputScript.MouseCursorForced = false;
                    inputScript.MouseCursorBlocksMouseLook = true;
                    UI.SetActive(false);
                    break;
            }

            Debug.Log(FPPlayer.Pause.Get());
        }

    }

    #region UI Elements   

    void SetSliderValues()
    {
        sliderMotorAcceleration.value = movementScript.MotorAcceleration;
        slidermotorDamping.value = movementScript.MotorDamping;
        slidermotorbackwardsSpeed.value = movementScript.MotorBackwardsSpeed;
        slidermotorAirSpeed.value = movementScript.MotorAirSpeed;
        slidermotorSlopeSpeedUp.value = movementScript.MotorSlopeSpeedUp;
        slidermotorSlopeSpeedDown.value = movementScript.MotorSlopeSpeedDown;

        sliderjumpingForce.value = movementScript.MotorJumpForce;
        sliderjumpingforceDamping.value = movementScript.MotorJumpForceDamping;
        sliderjumpingForceHold.value = movementScript.MotorJumpForceHold;
        sliderjumpingForceHoldDamping.value = movementScript.MotorJumpForceHoldDamping;

        sliderphysicsForceDamping.value = movementScript.PhysicsForceDamping;
        sliderphysicsPushForce.value = movementScript.PhysicsPushForce;
        sliderphysicsGravityMod.value = movementScript.PhysicsGravityModifier;
        sliderphysicsWallBounce.value = movementScript.PhysicsWallBounce;
        sliderphysicsWallFriction.value = movementScript.PhysicsWallFriction;

        //Parkour

        sliderdoubleJumpCount.value = (float)parkourScript.DoubleJumpCount;
        sliderdoubleJumpForce.value = parkourScript.DoubleJumpForce;
        sliderdoubleJumpMomentumForce.value = parkourScript.DoubleJumpForwardForce;

        sliderwallRunDuration.value = parkourScript.WallRunDuration;
        sliderwallRunGravity.value = parkourScript.WallRunGravity;
        sliderwallRunMinSpeed.value = parkourScript.WallRunSpeedMinimum;
        sliderWallRunCooldown.value = parkourScript.WallRunAgainTimeout;
        sliderwallJumpUpForce.value = parkourScript.WallRunUpForce;
        sliderwallRunDismountForce.value = parkourScript.WallRunDismountForce;
        sliderwallRunCameraTilt.value = parkourScript.WallRunTilt;
        sliderwallrunRange.value = parkourScript.WallRunRange;

        sliderwallJumpUpForce.value = parkourScript.WallJumpUpForce;
        sliderwallJumpWallForce.value = parkourScript.WallJumpForce;
        sliderwallJumpForwardForce.value = parkourScript.WallJumpForwardForce;

        sliderwallHangDuration.value = parkourScript.WallHangDuration;
        sliderwallHangLostGripStart.value = parkourScript.LosingGripStart;
        sliderwallHangLostGripGravity.value = parkourScript.LosingGripGravity;

        SetTextValues();
    }

    void SetTextValues()
    {
        motorAcceleration.text = sliderMotorAcceleration.value.ToString("F2");
        motorDamping.text = slidermotorDamping.value.ToString("F2");
        motorbackwardsSpeed.text = slidermotorbackwardsSpeed.value.ToString("F2");
        motorAirSpeed.text = slidermotorAirSpeed.value.ToString("F2");
        motorSlopeSpeedUp.text = slidermotorSlopeSpeedUp.value.ToString("F2");
        motorSlopeSpeedDown.text = slidermotorSlopeSpeedDown.value.ToString("F2");

        jumpingForce.text = sliderjumpingForce.value.ToString("F2");
        jumpingforceDamping.text = sliderjumpingforceDamping.value.ToString("F2");
        jumpingForceHold.text = sliderjumpingForceHold.value.ToString("F2");
        jumpingForceHoldDamping.text = sliderjumpingForceHoldDamping.value.ToString("F2");

        physicsForceDamping.text = sliderphysicsForceDamping.value.ToString("F2");
        physicsPushForce.text = sliderphysicsPushForce.value.ToString("F2");
        physicsGravityMod.text = sliderphysicsGravityMod.value.ToString("F2");
        physicsWallBounce.text = sliderphysicsWallBounce.value.ToString("F2");
        physicsWallFriction.text = sliderphysicsWallFriction.value.ToString("F2");

        //Parkour

        doubleJumpCount.text = sliderdoubleJumpCount.value.ToString("F2");
        doubleJumpForce.text = sliderdoubleJumpForce.value.ToString("F2");
        doubleJumpMomentumForce.text = sliderdoubleJumpMomentumForce.value.ToString("F2");

        wallRunDuration.text = sliderwallRunDuration.value.ToString("F2");
        wallRunGravity.text = sliderwallRunGravity.value.ToString("F2");
        wallRunMinSpeed.text = sliderwallRunMinSpeed.value.ToString("F2");
        WallRunCooldown.text = sliderWallRunCooldown.value.ToString("F2");
        WallRunUpForceText.text = sliderwallJumpUpForce.value.ToString("F2");
        wallRunDismountForce.text = sliderwallRunDismountForce.value.ToString("F2");
        wallRunCameraTilt.text = sliderwallRunCameraTilt.value.ToString("F2");
        wallrunRange.text = sliderwallrunRange.value.ToString("F2");

        wallJumpUpForce.text = sliderwallJumpUpForce.value.ToString("F2");
        wallJumpWallForce.text = sliderwallJumpWallForce.value.ToString("F2");
        wallJumpForwardForce.text = sliderwallJumpForwardForce.value.ToString("F2");

        wallHangDuration.text = sliderwallHangDuration.value.ToString("F2");
        wallHangLostGripStart.text = sliderwallHangLostGripStart.value.ToString("F2");
        wallHangLostGripGravity.text = sliderwallHangLostGripGravity.value.ToString("F2");

    }

    #endregion

    public void SaveAndLoadState(string _menuState)
    {
        if(_menuState == "Save")
        {
            currentMenuMode = menuMode.save;
        }

        if(_menuState == "Load")
        {
            currentMenuMode = menuMode.load;
        }

        SavingAndLoading.SetActive(true);
    }

    public void DetermineWhetherSaveOrLoad(Text _text)
    {
        string _fileName = _text.text;

        if (currentMenuMode == menuMode.save)
        {
            SaveValues(_fileName);
        }

        if (currentMenuMode == menuMode.load)
        {
            Load(_fileName);
        }

        SavingAndLoading.SetActive(false);

        _text.text = "";

        Debug.Log(_fileName);
    }

    public void SaveValues(string _fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "\\" + _fileName + ".dat");
        //FileStream file = File.Create(Application.persistentDataPath + "/" + _fileName + ".dat");

        CharacterMovementValues data = new CharacterMovementValues();

        data.motorAcceleration = movementScript.MotorAcceleration;
        data.motorDamping = movementScript.MotorDamping;
        data.motorbackwardsSpeed = movementScript.MotorBackwardsSpeed;
        data.motorSlopeSpeedUp = movementScript.MotorSlopeSpeedUp;
        data.motorSlopeSpeedDown = movementScript.MotorSlopeSpeedDown;

        data.jumpingForce = movementScript.MotorJumpForce;
        data.jumpingforceDamping = movementScript.MotorJumpForceDamping;
        data.jumpingForceHold = movementScript.MotorJumpForceHold;
        data.jumpingForceHoldDamping = movementScript.MotorJumpForceHoldDamping;

        data.physicsForceDamping = movementScript.PhysicsForceDamping;
        data.physicsPushForce = movementScript.PhysicsPushForce;
        data.physicsGravityMod = movementScript.PhysicsGravityModifier;
        data.physicsWallBounce = movementScript.PhysicsWallBounce;
        data.physicsWallFriction = movementScript.PhysicsWallFriction;

        data.doubleJumpCount = parkourScript.DoubleJumpCount;
        data.doubleJumpForce = parkourScript.DoubleJumpForce;
        data.doubleJumpMomentumForce = parkourScript.DoubleJumpForwardForce;

        data.wallRunDuration = parkourScript.WallRunDuration;
        data.wallRunGravity = parkourScript.WallRunGravity;
        data.wallRunMinSpeed = parkourScript.WallRunSpeedMinimum;
        data.WallRunCooldown = parkourScript.WallRunAgainTimeout;
        data.WallRunUpForceText = parkourScript.WallRunUpForce;
        data.wallRunDismountForce = parkourScript.WallRunDismountForce;
        data.wallRunCameraTilt = parkourScript.WallRunTilt;
        data.wallrunRange = parkourScript.WallRunRange;

        data.wallJumpUpForce = parkourScript.WallJumpForce;
        data.wallJumpWallForce = parkourScript.WallJumpForce;
        data.wallJumpForwardForce = parkourScript.WallJumpForwardForce;

        data.wallHangDuration = parkourScript.WallHangDuration;
        data.wallHangLostGripStart = parkourScript.LosingGripStart;
        data.wallHangLostGripGravity = parkourScript.LosingGripGravity;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load(string _fileName)
    {
        if(File.Exists(Application.dataPath + "\\" + _fileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "\\" + _fileName + ".dat", FileMode.Open);
            //FileStream file = File.Open(Application.persistentDataPath + "/" + _fileName + ".dat", FileMode.Open);
            CharacterMovementValues data = (CharacterMovementValues)bf.Deserialize(file);
            file.Close();

            movementScript.MotorAcceleration = data.motorAcceleration;
            movementScript.MotorDamping = data.motorDamping;
            movementScript.MotorBackwardsSpeed = data.motorbackwardsSpeed;
            movementScript.MotorAirSpeed = data.motorAirSpeed;
            movementScript.MotorSlopeSpeedUp = data.motorSlopeSpeedUp;
            movementScript.MotorSlopeSpeedDown = data.motorSlopeSpeedDown;

            movementScript.MotorJumpForce = data.jumpingForce;
            movementScript.MotorJumpForceDamping = data.jumpingforceDamping;
            movementScript.MotorJumpForceHold = data.jumpingForceHold;
            movementScript.MotorJumpForceHoldDamping = data.jumpingForceHoldDamping;

            movementScript.PhysicsForceDamping = data.physicsForceDamping;
            movementScript.PhysicsPushForce = data.physicsPushForce;
            movementScript.PhysicsGravityModifier = data.physicsGravityMod;
            movementScript.PhysicsWallBounce = data.physicsWallBounce;
            movementScript.PhysicsWallFriction = data.physicsWallFriction;

            parkourScript.DoubleJumpCount = (int)data.doubleJumpCount;
            parkourScript.DoubleJumpForce = data.doubleJumpForce;
            parkourScript.DoubleJumpForwardForce = data.doubleJumpMomentumForce;

            parkourScript.WallRunDuration = data.wallRunDuration;
            parkourScript.WallRunGravity = data.wallRunGravity;
            parkourScript.WallRunSpeedMinimum = data.wallRunMinSpeed;
            parkourScript.WallRunAgainTimeout = data.WallRunCooldown;
            parkourScript.WallRunUpForce = data.WallRunUpForceText;
            parkourScript.WallRunDismountForce = data.wallRunDismountForce;
            parkourScript.WallRunTilt = data.wallRunCameraTilt;
            parkourScript.WallRunRange = data.wallrunRange;

            parkourScript.WallJumpUpForce = data.wallJumpUpForce;
            parkourScript.WallJumpForce = data.wallJumpWallForce;
            parkourScript.WallJumpForwardForce = data.wallJumpForwardForce;

            parkourScript.WallHangDuration = data.wallHangDuration;
            parkourScript.LosingGripStart = data.wallHangLostGripStart;
            parkourScript.LosingGripGravity = data.wallHangLostGripGravity;
            Debug.Log("Success");

            SetSliderValues();
        }
    }

    [Serializable]
    class CharacterMovementValues
    {
        public float motorAcceleration;
        public float motorDamping;
        public float motorbackwardsSpeed;
        public float motorAirSpeed;
        public float motorSlopeSpeedUp;
        public float motorSlopeSpeedDown;

        public float jumpingForce;
        public float jumpingforceDamping;
        public float jumpingForceHold;
        public float jumpingForceHoldDamping;

        public float physicsForceDamping;
        public float physicsPushForce;
        public float physicsGravityMod;
        public float physicsWallBounce;
        public float physicsWallFriction;

        public float doubleJumpCount;
        public float doubleJumpForce;
        public float doubleJumpMomentumForce;

        public float wallRunDuration;
        public float wallRunGravity;
        public float wallRunMinSpeed;
        public float WallRunCooldown;
        public float WallRunUpForceText;
        public float wallRunDismountForce;
        public float wallRunCameraTilt;
        public float wallrunRange;

        public float wallJumpUpForce;
        public float wallJumpWallForce;
        public float wallJumpForwardForce;

        public float wallHangDuration;
        public float wallHangLostGripStart;
        public float wallHangLostGripGravity;
     }
}