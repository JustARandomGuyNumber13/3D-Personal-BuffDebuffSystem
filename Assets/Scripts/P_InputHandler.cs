using UnityEngine;
using UnityEngine.InputSystem;

public class P_InputHandler : MonoBehaviour
{
    Vector2 moveInput;
    Vector3 moveDir;
    Animator anim;
    Rigidbody rb;
    Character pStat;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        pStat = GetComponent<Character>();
    }
    private void FixedUpdate()
    {
        Action_Move();
    }


    void Action_Move()
    {
        Helper_SetMoveDir();
        anim.SetFloat("moveInputX", moveInput.x);
        anim.SetFloat("moveInputY", moveInput.y);
        rb.linearVelocity = moveDir;
    }
    void Action_Jump() 
    {
        anim.SetTrigger("jump");
    }

    void Helper_SetMoveDir()
    { 
        moveDir.x = moveInput.x * pStat.Base_MoveSpeed;
        moveDir.y = rb.linearVelocity.y;
        moveDir.z = moveInput.y * pStat.Base_MoveSpeed;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if (moveInput.x != 0)
            moveInput.x = moveInput.x > 0 ? 1 : -1;

        if (moveInput.y != 0)
            moveInput.y = moveInput.y> 0 ? 1 : -1;
    }
    void OnJump(InputValue value)
    {
        if (Mathf.Ceil(value.Get<float>()) == 1)
            Action_Jump();
    }
}
