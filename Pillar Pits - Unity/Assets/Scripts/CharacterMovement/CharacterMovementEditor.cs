using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterMovementEditor : CharacterMovementUIElements {

    [Header("Editor Attributes")]
    public GameObject UI;
    public vp_FPPlayerEventHandler FPPlayer;
    public vp_FPController movementScript;
    public vp_FPInput inputScript;
    public ac_FPParkour parkourScript;

    private Slider activeSlider;



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
}