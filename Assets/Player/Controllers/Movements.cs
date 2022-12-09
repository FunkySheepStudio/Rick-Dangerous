using UnityEngine;

public class Movements : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 moves = inputActions.Movements.Move.ReadValue<Vector3>();

        if (moves.x != 0)
        {
            animator.SetBool("Walk", true);

            if (moves.x > 0)
            {
                if (!Mathf.Round(transform.localEulerAngles.y).Equals(180f))
                {
                    transform.Rotate(0, 180, 0, Space.Self);
                }
            }
            else if (moves.x < 0)
            {
                if (!Mathf.Round(transform.localEulerAngles.y).Equals(0f))
                {
                    transform.Rotate(0, 180, 0, Space.Self);
                }
            }

            characterController.Move(Vector3.right * moves.x * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
}
