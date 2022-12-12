using UnityEngine;
using UnityEngine.InputSystem;

public class Movements : MonoBehaviour
{
    public float speed = 5;
    public float jumpSpeed = 8;
    const float gravity = 9.8f;
    float vSpeed = 0f;
    bool isCrouched = false;

    PlayerInputs inputActions;
    CharacterController characterController;
    Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputActions = new PlayerInputs();
        inputActions.Enable();
    }
    private void FixedUpdate()
    {
        Vector3 moveInputs = inputActions.Movements.Move.ReadValue<Vector3>();
        Crouch(moveInputs);
        Move(moveInputs);
        ApplyGravityAndJump(moveInputs);
    }

    void Crouch(Vector3 moveInputs)
    {
        LayerMask mask = LayerMask.GetMask("Ground");
        bool isCeilled = Physics.CheckBox(transform.position + Vector3.up, Vector3.one * 0.05f, Quaternion.identity, mask);

        if (moveInputs.y < 0 && vSpeed <= 0.1f)
        {
            isCrouched = true;
            animator.SetBool("Crouch", true);
            characterController.height = 0.91f;
            characterController.center = Vector3.up * 0.46f;
        } else if (!isCeilled)
        {
            isCrouched = false;
            animator.SetBool("Crouch", false);
            characterController.height = 1.45f;
            characterController.center = Vector3.up * 0.7f;
        }
    }

    void ApplyGravityAndJump(Vector3 moveInputs)
    {
        LayerMask mask = LayerMask.GetMask("Ground");
        bool isGrounded = Physics.CheckBox(transform.position, Vector3.one * 0.05f, Quaternion.identity, mask);
        bool isCeilled = Physics.CheckBox(transform.position + Vector3.up * 1.5f, Vector3.one * 0.05f, Quaternion.identity, mask);
        Vector3 vel = Vector3.down;

        if (isCeilled)
        {
            vSpeed = 0;
        }

        if (isGrounded)
        {
            //animator.SetBool("Grounded", true);
            animator.SetBool("Jump", false);
            vSpeed = 0; // grounded character has vSpeed = 0...
            if (moveInputs.y > 0 && !isCrouched && !isCeilled)
            { // unless it jumps:
                vSpeed = jumpSpeed;
            }
        }
        // apply gravity acceleration to vertical speed:
        vSpeed -= gravity * Time.deltaTime;
        vel.y = vSpeed; // include vertical speed in vel
                        // convert vel to displacement and Move the character:

        if (vel.y >= 0.1)
        {
            animator.SetBool("Jump", true);
        }

        characterController.Move(vel * Time.deltaTime);
    }

    void Move(Vector3 moveInputs)
    {
        Vector3 movement = Vector3.zero;

        if (moveInputs.x != 0)
        {
            animator.SetBool("Walk", true);

            if (moveInputs.x > 0)
            {
                if (!Mathf.Round(transform.localEulerAngles.y).Equals(180f))
                {
                    transform.Rotate(0, 180, 0, Space.Self);
                }
            }
            else if (moveInputs.x < 0)
            {
                if (!Mathf.Round(transform.localEulerAngles.y).Equals(0f))
                {
                    transform.Rotate(0, 180, 0, Space.Self);
                }
            }

            movement.x = moveInputs.x;
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        characterController.Move(movement * Time.deltaTime * speed);
    }
}
