using System.Collections;
using UnityEngine;

public class BuffDebuff_Handler : MonoBehaviour
{
    Character _char;
    private void Awake()
    {
        _char = GetComponent<Character>();
    }

    public void P_Apply(BuffDebuff_SO definition)
    {
        switch (definition.Data_Type)
        {
            case BuffDebuff_SO.DataType.Float_Value:
                Handler_FloatValue(definition, definition.Value);
                break;
            case BuffDebuff_SO.DataType.Float_Percentage:
                Handler_FloatPercentage(definition, definition.Value);
                break;
            case BuffDebuff_SO.DataType.Boolean:
                Handler_Boolean(definition, definition.Value);
                break;
        }
    }

    private void Handler_FloatValue(BuffDebuff_SO definition, float value)
    {
        switch (definition.Effect_Type)
        {
            case BuffDebuff_SO.EffectType.MoveSpeed:
                _char.P_AddStat(Character.StatType.MoveSpeed, value);
                break;
            case BuffDebuff_SO.EffectType.JumpForce:
                _char.P_AddStat(Character.StatType.MoveSpeed, value);
                break;
            case BuffDebuff_SO.EffectType.MaxHealth:
                _char.P_AddStat(Character.StatType.MaxHealth, value);
                break;
            case BuffDebuff_SO.EffectType.Defense:
                _char.P_AddStat(Character.StatType.Defense, value);
                break;
        }
        if (definition.HasDuration) StartCoroutine(ReverseEffectDelay(definition, definition.Value));
    }
    private void Handler_FloatPercentage(BuffDebuff_SO definition, float value)
    {
        switch (definition.Effect_Type)
        {
            case BuffDebuff_SO.EffectType.MoveSpeed:
                _char.P_AddStat(Character.StatType.MoveSpeed, _char.Base_MoveSpeed * value / 100);
                break;
            case BuffDebuff_SO.EffectType.JumpForce:
                _char.P_AddStat(Character.StatType.MoveSpeed, _char.Base_JumpForce * value / 100);
                break;
            case BuffDebuff_SO.EffectType.MaxHealth:
                _char.P_AddStat(Character.StatType.MaxHealth, _char.Base_MaxHealth * value / 100);
                break;
            case BuffDebuff_SO.EffectType.Defense:
                _char.P_AddStat(Character.StatType.Defense, _char.Base_Defense * value / 100);
                break;
        }
        if (definition.HasDuration) StartCoroutine(ReverseEffectDelay(definition, definition.Value));
    }
    private void Handler_Boolean(BuffDebuff_SO definition, float value)
    {
        bool callSuccess = false;
        switch (definition.Effect_Type)
        {
            case BuffDebuff_SO.EffectType.LifeDrain:
                if (value == -definition.Value || !_char.LifeStealActive)
                {
                    _char.LifeStealActive = !_char.LifeStealActive;
                    callSuccess = true;
                }
                break;
        }
        if (definition.HasDuration && callSuccess) StartCoroutine(ReverseEffectDelay(definition, definition.Value));
    }

    private IEnumerator ReverseEffectDelay(BuffDebuff_SO definition, float value)
    { 
        yield return new WaitForSeconds(definition.Duration);
        switch (definition.Data_Type)
        {
            case BuffDebuff_SO.DataType.Float_Value:
                Handler_FloatValue(definition, -definition.Value);
                break;
            case BuffDebuff_SO.DataType.Float_Percentage:
                Handler_FloatPercentage(definition, -definition.Value);
                break;
            case BuffDebuff_SO.DataType.Boolean:
                    Handler_Boolean(definition, -definition.Value);
                break;
        }
    }
}
