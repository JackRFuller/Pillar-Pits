using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterMovementUIElements : MonoBehaviour {

    [Header("TextElements")]
    public Text motorAcceleration;
    public Text motorDamping;
    public Text motorbackwardsSpeed;
    public Text motorAirSpeed;
    public Text motorSlopeSpeedUp;
    public Text motorSlopeSpeedDown;
    [Space(5)]
    public Text jumpingForce;
    public Text jumpingforceDamping;
    public Text jumpingForceHold;
    public Text jumpingForceHoldDamping;
    [Space(5)]
    public Text physicsForceDamping;
    public Text physicsPushForce;
    public Text physicsGravityMod;
    public Text physicsWallBounce;
    public Text physicsWallFriction;
    [Space(5)]
    public Text doubleJumpCount;
    public Text doubleJumpForce;
    public Text doubleJumpMomentumForce;
    [Space(5)]
    public Text wallRunDuration;
    public Text wallRunGravity;
    public Text wallRunMinSpeed;
    public Text WallRunCooldown;
    public Text WallRunUpForceText;
    public Text wallRunDismountForce;
    public Text wallRunCameraTilt;
    public Text wallrunRange;
    [Space(5)]
    public Text wallJumpUpForce;
    public Text wallJumpWallForce;
    public Text wallJumpForwardForce;
    [Space(5)]
    public Text wallHangDuration;
    public Text wallHangLostGripStart;
    public Text wallHangLostGripGravity;

    [Header("Slider Elements")]
    public Slider sliderMotorAcceleration;
    public Slider slidermotorDamping;
    public Slider slidermotorbackwardsSpeed;
    public Slider slidermotorAirSpeed;
    public Slider slidermotorSlopeSpeedUp;
    public Slider slidermotorSlopeSpeedDown;
    [Space(5)]
    public Slider sliderjumpingForce;
    public Slider sliderjumpingforceDamping;
    public Slider sliderjumpingForceHold;
    public Slider sliderjumpingForceHoldDamping;
    [Space(5)]
    public Slider sliderphysicsForceDamping;
    public Slider sliderphysicsPushForce;
    public Slider sliderphysicsGravityMod;
    public Slider sliderphysicsWallBounce;
    public Slider sliderphysicsWallFriction;
    [Space(5)]
    public Slider sliderdoubleJumpCount;
    public Slider sliderdoubleJumpForce;
    public Slider sliderdoubleJumpMomentumForce;
    [Space(5)]
    public Slider sliderwallRunDuration;
    public Slider sliderwallRunGravity;
    public Slider sliderwallRunMinSpeed;
    public Slider sliderWallRunCooldown;
    public Slider sliderWallRunUpForceText;
    public Slider sliderwallRunDismountForce;
    public Slider sliderwallRunCameraTilt;
    public Slider sliderwallrunRange;
    [Space(5)]
    public Slider sliderwallJumpUpForce;
    public Slider sliderwallJumpWallForce;
    public Slider sliderwallJumpForwardForce;
    [Space(5)]
    public Slider sliderwallHangDuration;
    public Slider sliderwallHangLostGripStart;
    public Slider sliderwallHangLostGripGravity;
}
