using UnityEngine;

public class PaladinController : MonoBehaviour
{
    [SerializeField]
    private float StandingMovementAcceleration, StandingMovementDeceleration,
        CrouchingMovementAcceleration, CrouchingMovementDeceleration,
        JumpForce;
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
        animatorParameter_CastWithShield = "Cast With Shield";


    private float currentForwardSpeed = 0,
        currentRightwardSpeed = 0;
    private bool isStanding = true,
        isBlocking = false;

    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody>();
        StandUp();
    }
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Get Movement Inputs
        float forwardMovement = Input.GetAxis("Vertical");
        float rightwardMovement = Input.GetAxis("Horizontal");
        //Debug.Log(forwardMovement);
        /*if (Mathf.Approximately(forwardMovement, 0))
        {
            // Character not moving forward nor backward
            Decelerate(ref currentForwardSpeed, currentMovementDeceleration);
        }
        else
        {
            // Character is moving forward or backward
            Accelerate(ref currentForwardSpeed, forwardMovement > 0 ? currentMovementAcceleration : -currentMovementAcceleration);
        }
        if (Mathf.Approximately(rightwardMovement, 0))
        {
            // Character not moving left nor right
            Decelerate(ref currentRightwardSpeed, currentMovementDeceleration);
        }
        else
        {
            // Character is moving left or right
            Accelerate(ref currentRightwardSpeed, rightwardMovement > 0 ? currentMovementAcceleration : -currentMovementAcceleration);
        }*/
        currentForwardSpeed = forwardMovement;
        currentRightwardSpeed = rightwardMovement;
        UpdateMovement();

        // Borrowed code to handle character rotation
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, -3 * angle, 0f));

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

    }
    private void Decelerate(ref float currentSpeed, float deceleration, float zero = 0f)
    {
        if (currentSpeed > zero)
        { currentSpeed = currentSpeed - deceleration > zero ? currentSpeed - deceleration : zero; }
        else if (currentSpeed < zero)
        { currentSpeed = currentSpeed + deceleration < zero ? currentSpeed + deceleration : zero; }
    }
    private void Accelerate(ref float currentSpeed, float acceleration, float lowerBound = -1f, float upperBound = 1f)
    {
        if (currentSpeed + acceleration > upperBound)
        { currentSpeed = upperBound; }
        else if (currentSpeed + acceleration < lowerBound)
        { currentSpeed = lowerBound; }
        else
        { currentSpeed += acceleration; }
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
        {
            myAnimator.SetTrigger(animatorParameter_LightAttack_1);
        }
    }
    private void SwingAttack()
    {
        if (isBlocking) { return; }
        if (isStanding)
        {
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
    }
    private void JumpAttack()
    {
        if (isBlocking) { return; }
        if (isStanding)
        {
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
    }
    private void Jump()
    {
        if (isBlocking) { return; }
        if (isStanding)
        {
            if (currentForwardSpeed > 0.9 && Mathf.Abs(currentRightwardSpeed) < 0.2)
            {
                myAnimator.SetTrigger(animatorParameter_JumpForward);
            }
            else
            {
                myAnimator.SetTrigger(animatorParameter_JumpUpward);
            }
            Vector3 force = (transform.right * currentRightwardSpeed + transform.up + transform.forward * currentForwardSpeed).normalized * JumpForce;
            Debug.Log("Force " + force);
            myRigidbody.AddForce(force, ForceMode.Impulse);

        }
    }
    private void PowerUp()
    {
        if (isBlocking) { return; }
        if (isStanding)
        {
            myAnimator.SetTrigger(animatorParameter_PowerUp);
        }
    }
    private void CastWithSword()
    {
        if (isBlocking) { return; }
        if (isStanding)
        {
            myAnimator.SetTrigger(animatorParameter_CastWithSword);
        }
    }
    private void CastWithShield()
    {
        if (isBlocking) { return; }
        if (isStanding)
        {
            myAnimator.SetTrigger(animatorParameter_CastWithShield);
        }
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
        myAnimator.SetFloat(animatorParameter_MovementForward, currentForwardSpeed);
        myAnimator.SetFloat(animatorParameter_MovementRightward, currentRightwardSpeed);
    }
    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
