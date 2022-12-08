using UnityEngine;


public class PaladinController : MonoBehaviour
{
    [SerializeField]
    private float JumpForce,
        MinCameraXAngle = 5,
        MaxCameraXAngle = 60,
        CameraDragAngularSpeed = 1,
        GroundedOffset = 2f;
    [SerializeField]
    private GameObject cameraObject;
    public Rigidbody rb;

    private const string animatorParameter_MovementForward = "Movement Forward",
        animatorParameter_MovementRightward = "Movement Right",
        animatorParameter_LightAttack_1 = "Light Attack 1",
        animatorParameter_LightAttack_2 = "Light Attack 2",
        animatorParameter_HighSwing = "High Swing",
        animatorParameter_LowSwing = "Low Swing",
        animatorParameter_LightAttackSmallJumpAttack = "Light Attack Small Jump Attack",
        animatorParameter_KickForward = "Kick Forward",
        animatorParameter_SmallJumpAttack = "Small Jump Attack",
        animatorParameter_JumpAttack = "Jump Attack",
        animatorParameter_IsStanding = "Is Standing",
        animatorParameter_JumpForward = "Jump Forward",
        animatorParameter_JumpUpward = "Jump Upward",
        animatorParameter_Block = "Block",
        animatorParameter_Unblock = "Unblock",
        animatorParameter_PowerUp = "Power Up",
        animatorParameter_CastWithSword = "Cast With Sword",
        animatorParameter_CastWithShield = "Cast With Shield",
        animatorParameter_DodgeForward = "Dodge Forward",
        animatorParameter_DodgeBackward = "Dodge Backward",
        animatorParameter_DodgeLeft = "Dodge Left",
        animatorParameter_DodgeRight = "Dodge Right",
        animatorParameter_DieForward_1 = "Die Forward 1",
        animatorParameter_DieForward_2 = "Die Forward 2",
        animatorParameter_DieBackward_1 = "Die Backward 1",
        animatorParameter_DieBackward_2 = "Die Backward 2";


    private float currentForwardSpeed = 0,
        currentRightwardSpeed = 0;
    private bool isStanding = true,
        isBlocking = false,
        isGrounded = false,
        isCursorOn;

    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private Health myHealth;
    private CapsuleCollider myCollider;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.centerOfMass = Vector3.zero;
        myHealth = GetComponent<Health>();
        myHealth.OnDead += MyHealth_OnDead;
        myCollider = GetComponent<CapsuleCollider>();
        StandUp();
        ToggleCursorOff();
        Players.SetCurrentPlayer(gameObject);
    }
    void Update()
    {
        // Get Movement Inputs
        currentForwardSpeed = Input.GetAxis("Vertical") / Time.timeScale;
        currentRightwardSpeed = Input.GetAxis("Horizontal") / Time.timeScale;

        UpdateMovement();
        UpdateIsGrounded();
        // Move Camera
        if (!isCursorOn)
        {
            float mouseY = Input.GetAxis("Mouse Y");
            float angleCameraPlayer = Vector3.Angle(transform.forward, cameraObject.transform.forward);

            if ((angleCameraPlayer > MinCameraXAngle || mouseY < 0)
                && (angleCameraPlayer < MaxCameraXAngle || mouseY > 0))
            {
                cameraObject.transform.RotateAround(transform.position, transform.right, -mouseY);
            }
            if (angleCameraPlayer < MinCameraXAngle)
            {
                cameraObject.transform.RotateAround(transform.position, transform.right, CameraDragAngularSpeed);
            }
            else if (angleCameraPlayer > MaxCameraXAngle)
            {
                cameraObject.transform.RotateAround(transform.position, transform.right, -CameraDragAngularSpeed);
            }
            if (myHealth.IsAlive)
            {
                float mouseX = Input.GetAxis("Mouse X");
                transform.Rotate(transform.up, mouseX);

                if (Input.GetButtonDown("Fire1")) { LightAttack(); }
                if (Input.GetButtonDown("Fire2")) { SwingAttack(); }
                if (Input.GetButtonDown("Fire3")) { JumpAttack(); }
                if (Input.GetButtonDown("Block/Unblock"))
                {
                    if (isBlocking) { Unblock(); }
                    else { Block(); }
                }
                if (Input.GetButtonDown("Jump")) { Jump(); }
                if (Input.GetButtonDown("Power Up")) { PowerUp(); }
                if (Input.GetButtonDown("Cast 1")) { CastWithSword(); }
                if (Input.GetButtonDown("Cast 2")) { CastWithShield(); }
                if (Input.GetButtonDown("Stand/Crouch"))
                {
                    if (isStanding) { CrouchDown(); }
                    else { StandUp(); }
                }
                if (Input.GetButtonDown("Dodge")) { Dodge(); }
            }
        }
        if (Input.GetButtonDown("ToggleCursor")) { ToggleCursorOn(); }
        if (Input.GetButtonUp("ToggleCursor")) { ToggleCursorOff(); }
    }

    private void MyHealth_OnDead(object sender, System.EventArgs e)
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                myAnimator.SetTrigger(animatorParameter_DieForward_1);
                break;
            case 1:
                myAnimator.SetTrigger(animatorParameter_DieForward_2);
                break;
            case 2:
                myAnimator.SetTrigger(animatorParameter_DieBackward_1);
                break;
            case 3:
                myAnimator.SetTrigger(animatorParameter_DieBackward_2);
                break;
        }
        myCollider.direction = 2;
        myCollider.radius /= 2;
    }
    private void UpdateIsGrounded()
    {
        isGrounded = Physics.Raycast(transform.position + transform.up, -transform.up, out RaycastHit raycastHit, GroundedOffset);
    }
    private void ToggleCursorOn()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorOn = true;
    }
    private void ToggleCursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorOn = false;
    }
    private void Dodge()
    {
        ResetActionLayerTrigger();
        if (isBlocking) { return; }
        if (!isStanding) { return; }
        if (Mathf.Approximately(currentForwardSpeed, 0) && Mathf.Approximately(currentRightwardSpeed, 0))
        { return; }

        if (Mathf.Abs(currentForwardSpeed) > Mathf.Abs(currentRightwardSpeed))
        {
            // Dodge Forward or Backward
            if (currentForwardSpeed > 0) { myAnimator.SetTrigger(animatorParameter_DodgeForward); }
            else { myAnimator.SetTrigger(animatorParameter_DodgeBackward); }
        }
        else
        {
            // Dodge Left or Right
            if (currentRightwardSpeed > 0) { myAnimator.SetTrigger(animatorParameter_DodgeRight); }
            else { myAnimator.SetTrigger(animatorParameter_DodgeLeft); }
        }
    }
    private void StandUp()
    {
        isStanding = true;
        myAnimator.SetBool(animatorParameter_IsStanding, isStanding);
    }
    private void CrouchDown()
    {
        isStanding = false;
        myAnimator.SetBool(animatorParameter_IsStanding, isStanding);
    }
    private void LightAttack()
    {
        ResetActionLayerTrigger();
        if (isBlocking) { return; }
        if (isStanding)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    myAnimator.SetTrigger(animatorParameter_LightAttack_1);
                    break;
                case 1:
                    myAnimator.SetTrigger(animatorParameter_LightAttack_2);
                    break;
                case 2:
                    myAnimator.SetTrigger(animatorParameter_KickForward);
                    break;
            }
        }
        else
        { myAnimator.SetTrigger(animatorParameter_LightAttack_1); }
    }
    private void SwingAttack()
    {
        ResetActionLayerTrigger();
        if (isBlocking) { return; }
        if (!isStanding) { return; }
        switch (Random.Range(0, 3))
        {
            case 0:
                myAnimator.SetTrigger(animatorParameter_HighSwing);
                break;
            case 1:
                myAnimator.SetTrigger(animatorParameter_LowSwing);
                break;
            case 2:
                myAnimator.SetTrigger(animatorParameter_LightAttackSmallJumpAttack);
                break;
        }
    }
    private void JumpAttack()
    {
        ResetActionLayerTrigger();
        if (isBlocking) { return; }
        if (!isStanding) { return; }
        switch (Random.Range(0, 2))
        {
            case 0:
                myAnimator.SetTrigger(animatorParameter_SmallJumpAttack);
                break;
            case 1:
                myAnimator.SetTrigger(animatorParameter_JumpAttack);
                break;
        }
    }
    private void Jump()
    {
        ResetActionLayerTrigger();
        if (!isGrounded) { return; }
        if (isBlocking) { return; }
        if (!isStanding) { return; }
        if (currentForwardSpeed > 0.9 && Mathf.Abs(currentRightwardSpeed) < 0.2)
        { myAnimator.SetTrigger(animatorParameter_JumpForward); }
        else
        { myAnimator.SetTrigger(animatorParameter_JumpUpward); }
        Vector3 force = (transform.right * currentRightwardSpeed + transform.up + transform.forward * currentForwardSpeed).normalized * JumpForce;
        myRigidbody.AddForce(force, ForceMode.Impulse);
    }
    private void PowerUp()
    {
        ResetActionLayerTrigger();
        if (isBlocking) { return; }
        if (!isStanding) { return; }
        myAnimator.SetTrigger(animatorParameter_PowerUp);
    }
    private void CastWithSword()
    {
        ResetActionLayerTrigger();
        if (isBlocking) { return; }
        if (!isStanding) { return; }
        myAnimator.SetTrigger(animatorParameter_CastWithSword);
    }
    private void CastWithShield()
    {
        ResetActionLayerTrigger();
        if (isBlocking) { return; }
        if (!isStanding) { return; }
        myAnimator.SetTrigger(animatorParameter_CastWithShield);
    }
    private void Block()
    {
        myAnimator.SetTrigger(animatorParameter_Block);
        isBlocking = true;
    }
    private void Unblock()
    {
        myAnimator.SetTrigger(animatorParameter_Unblock);
        isBlocking = false;
    }
    private void UpdateMovement()
    {
        if (myHealth.IsAlive)
        {
            myAnimator.SetFloat(animatorParameter_MovementForward, currentForwardSpeed);
            myAnimator.SetFloat(animatorParameter_MovementRightward, currentRightwardSpeed);
        }
    }
    private void ResetActionLayerTrigger()
    {
        myAnimator.ResetTrigger(animatorParameter_LightAttack_1);
        myAnimator.ResetTrigger(animatorParameter_LightAttack_2);
        myAnimator.ResetTrigger(animatorParameter_HighSwing);
        myAnimator.ResetTrigger(animatorParameter_LowSwing);
        myAnimator.ResetTrigger(animatorParameter_LightAttackSmallJumpAttack);
        myAnimator.ResetTrigger(animatorParameter_KickForward);
        myAnimator.ResetTrigger(animatorParameter_SmallJumpAttack);
        myAnimator.ResetTrigger(animatorParameter_JumpAttack);
        myAnimator.ResetTrigger(animatorParameter_JumpForward);
        myAnimator.ResetTrigger(animatorParameter_JumpUpward);
        myAnimator.ResetTrigger(animatorParameter_Block);
        myAnimator.ResetTrigger(animatorParameter_Unblock);
        myAnimator.ResetTrigger(animatorParameter_PowerUp);
        myAnimator.ResetTrigger(animatorParameter_CastWithSword);
        myAnimator.ResetTrigger(animatorParameter_CastWithShield);
        myAnimator.ResetTrigger(animatorParameter_DodgeForward);
        myAnimator.ResetTrigger(animatorParameter_DodgeBackward);
        myAnimator.ResetTrigger(animatorParameter_DodgeLeft);
        myAnimator.ResetTrigger(animatorParameter_DodgeRight);
    }

    public void HitBySnowBall()
    {
        rb.transform.rotation = Quaternion.Euler(new Vector3(0, rb.transform.rotation.eulerAngles.y, 0));
        Vector3 forceDir = new Vector3(5.0f, 20.0f, 5.0f);
        rb.AddForce(forceDir, ForceMode.Impulse);
    }

    //private void OnApplicationFocus(bool focus)
    //{
    //    if (focus)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //}
}
