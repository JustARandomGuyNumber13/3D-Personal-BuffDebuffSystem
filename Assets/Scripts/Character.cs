using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("Character stat")]
    [SerializeField] float moveSpeed;
    public float additionalMoveSpeed, additionalMoveSpeedPercentage;
    public float MoveSpeed => moveSpeed + additionalMoveSpeed + (moveSpeed * additionalMoveSpeedPercentage / 100);

    [SerializeField] float jumpForce;
    public float additionalJumpForce, additionalJumpForcePercentage;
    public float JumpForce => jumpForce + additionalJumpForce + (jumpForce * additionalJumpForcePercentage / 100);


    [SerializeField] UnityEvent OnLandEvent;
    public bool OnGround;
    private Rigidbody rb;


    [Header("Health system")]
    [SerializeField] float maxHealth;
    public float additionalMaxHealth, additionalMaxHealthPercentage;
    public float MaxHealth => maxHealth + additionalMaxHealth + (maxHealth * additionalMaxHealthPercentage / 100);
    public float CurHealth;

    [SerializeField] float defense;
    public float additionalDefense, additionalDefensePercentage;
    public float Defense => defense + additionalDefense + (defense * additionalDefensePercentage / 100);


    [SerializeField] UnityEvent OnHealthDecreaseEvent;
    [SerializeField] UnityEvent OnHealthIncreaseEvent;
    [SerializeField] UnityEvent OnDeathEvent;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    int layer;
    private void OnCollisionEnter(Collision collision)
    {
        layer = collision.gameObject.layer;
        if (layer == Global.GroundLayerIndex)
        {
            OnGround = true;
            OnLandEvent?.Invoke();
        }
    }


    public void P_DealDamage(float amount, Character target, bool ignoreDefense)
    { 
    
    }
    private float DecreaseHealthWithDefense(float amount)
    {
        float dmgAmount = amount - Defense;
        if (dmgAmount < 0) dmgAmount = 0;

        CurHealth -= dmgAmount;
        OnHealthDecreaseEvent?.Invoke();
        return dmgAmount;
    }
    private float DecreaseHealthWithOutDefense(float amount)
    {
        CurHealth -= amount;
        OnHealthDecreaseEvent?.Invoke();
        return amount;
    }
    public void P_IncreaseHealth(float amount)
    { 
        CurHealth += amount;
        OnHealthIncreaseEvent?.Invoke();
    }
}
