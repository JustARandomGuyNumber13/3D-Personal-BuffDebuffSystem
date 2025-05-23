using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public float Base_MoveSpeed { get; private set; }
    public float Cur_MoveSpeed { get; private set; }

    [SerializeField] float jumpForce;
    public float Base_JumpForce { get; private set; }
    public float Cur_JumpForce { get; private set; }

    [SerializeField] float maxHealth;
    public float Base_MaxHealth { get; private set; }
    public float Cur_MaxHealth { get; private set; }

    public float Cur_Health { get; private set; }

    [SerializeField] float defense;
    public float Base_Defense { get; private set; }
    public float Cur_Defense { get; private set; }

    public bool LifeStealActive;
    public float LifeStealValue { get; set; }

    [Header("Events")]
    public UnityEvent<float> OnMoveSpeedChangeEvent;
    public UnityEvent<float> OnJumpForceChangeEvent;
    public UnityEvent<float> OnMaxHealthChangeEvent;
    public UnityEvent<float> OnCurHealthChangeEvent;
    public UnityEvent<float> OnDefenseChangeEvent;

    /*
     When add new variable, check for: void Start, void P_AddStat, enum StatType, BuffDebuff.cs (enum & void P_Apply), and UI_StatDisplay.cs.
     */
    private void Start()
    {
        Cur_MoveSpeed  = Base_MoveSpeed = moveSpeed;
        Cur_JumpForce = Base_JumpForce = jumpForce;
        Cur_MaxHealth = Base_MaxHealth = maxHealth;
        Cur_Defense = Base_Defense = defense;
        Cur_Health = Cur_MaxHealth;

        OnMoveSpeedChangeEvent?.Invoke(Cur_MoveSpeed);
        OnJumpForceChangeEvent.Invoke(Cur_JumpForce);
        OnCurHealthChangeEvent.Invoke(Cur_Health);
        OnMaxHealthChangeEvent.Invoke(Cur_MaxHealth);
        OnDefenseChangeEvent.Invoke(Cur_Defense);
    }

    int layer;
    //private void OnTriggerEnter(Collider other)
    //{
    //    layer = other.gameObject.layer;

    //    if (layer == Global.BuffDebuffLayerIndex)
    //    {
    //        BuffDebuffFloat test;
    //        other.TryGetComponent<BuffDebuffFloat>( out test);
    //        if (test != null)
    //        {
    //            //float buffDuration = test.P_Apply(this);
    //            //StartCoroutine(BuffCoroutine(test, buffDuration));
    //            StartCoroutine(BuffCoroutine(test, test.P_Apply(this)));
    //        }
    //    }
    //}

    //IEnumerator BuffCoroutine(BuffDebuffFloat buff, float duration)
    //{ 
    //    yield return new WaitForSeconds(duration);
    //    buff.P_Remove(this);
    //}

    public void P_AddStat(StatType type, float value)
    {
        switch (type)
        {
            case StatType.MoveSpeed:
                Cur_MoveSpeed += value;
                OnMoveSpeedChangeEvent?.Invoke(Cur_MoveSpeed);
                break;
            case StatType.JumpForce:
                Cur_JumpForce += value;
                OnJumpForceChangeEvent?.Invoke(Cur_JumpForce);
                break;
            case StatType.CurHealth:
                Cur_Health += value;
                if(Cur_Health > Cur_MaxHealth)
                    Cur_Health = Cur_MaxHealth;
                OnCurHealthChangeEvent?.Invoke(Cur_Health);
                break;
            case StatType.MaxHealth:
                Cur_MaxHealth += value;
                if (Cur_Health > Cur_MaxHealth)
                    Cur_Health = Cur_MaxHealth;
                OnMaxHealthChangeEvent?.Invoke(Cur_MaxHealth);
                break;
            case StatType.Defense:
                Cur_Defense += value;
                OnDefenseChangeEvent?.Invoke(Cur_Defense);
                break;
        }
    }
    //public void P_UpdateBaseStats(StatType type, float value)
    //{
    //    switch (type)
    //    {
    //        case StatType.MoveSpeed:
    //            Cur_MoveSpeed -= Base_MoveSpeed;
    //            Base_MoveSpeed = value;
    //            Cur_MoveSpeed += Base_MoveSpeed;
    //            OnMoveSpeedChangeEvent?.Invoke(Cur_MoveSpeed);
    //            break;
    //        case StatType.JumpForce:
    //            Cur_JumpForce -= Base_JumpForce;
    //            Base_JumpForce = value;
    //            Cur_JumpForce += Base_JumpForce;
    //            OnJumpForceChangeEvent?.Invoke(Cur_JumpForce);
    //            break;
    //        case StatType.MaxHealth:
    //            Cur_MaxHealth -= Base_MaxHealth;
    //            Base_MaxHealth = value;
    //            Cur_MaxHealth += Base_MaxHealth;
    //            OnMaxHealthChangeEvent?.Invoke(Cur_MaxHealth);
    //            break;
    //        case StatType.Defense:
    //            Cur_Defense -= Base_Defense;
    //            Base_Defense = value;
    //            Cur_Defense += Base_Defense;
    //            OnDefenseChangeEvent?.Invoke(Cur_Defense);
    //            break;
    //    }
    //}
    public enum StatType
    { 
        MoveSpeed,
        JumpForce,
        CurHealth,
        MaxHealth,
        Defense
    }
}
