using UnityEngine;

public class P_Stat : MonoBehaviour
{
    [SerializeField] float moveSpeed, runSpeed;
    public float MoveSpeed => !IsRun ? moveSpeed : runSpeed;
    


    public float JumpSpeed;

    public bool IsRun;
}
