using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Buff_Debuff", menuName = "Scriptable Objects/Buff_or_Debuff")]
public class BuffDebuff_SO : ScriptableObject
{
    public DataType Data_Type;
    public EffectType Effect_Type;
    public float Value;
    public bool HasDuration = true;
    public float Duration;
    public bool CanStack;
    public int MaxStack;


    public enum DataType
    {
        Float_Value,
        Float_Percentage,
        Boolean
    }
    public enum EffectType
    { 
        // Float type
        MoveSpeed,
        JumpForce,
        MaxHealth,
        Defense,

        // Boolean type
        LifeDrain
    }
}



[CustomEditor(typeof(BuffDebuff_SO))] // This links the editor to your BuffDebuff ScriptableObject
public class BuffDebuffEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get a reference to the actual BuffDebuff object being inspected
        BuffDebuff_SO buffDebuff = (BuffDebuff_SO)target;

        // Use SerializedObject to work with properties for proper Undo/Redo and serialization.
        // Get SerializedProperty for each field you want to draw or access.
        SerializedProperty effectTypeProp = serializedObject.FindProperty("Effect_Type");
        SerializedProperty dataTypeProp = serializedObject.FindProperty("Data_Type");
        SerializedProperty valueProp = serializedObject.FindProperty("Value");
        SerializedProperty hasDurationProp = serializedObject.FindProperty("HasDuration");
        SerializedProperty durationProp = serializedObject.FindProperty("Duration");
        SerializedProperty canStackProp = serializedObject.FindProperty("CanStack");
        SerializedProperty maxStackProp = serializedObject.FindProperty("MaxStack");

        // --- Draw fields that are always visible ---
        EditorGUILayout.PropertyField(effectTypeProp);
        EditorGUILayout.PropertyField(dataTypeProp); // Data_Type must be drawn first as it affects 'Value'

        // --- Conditional display for 'Value' ---
        // Hide 'Value' if Data_Type is DataType.Boolean, UNLESS Effect_Type is LifeDrain
        if (buffDebuff.Data_Type != BuffDebuff_SO.DataType.Boolean || buffDebuff.Effect_Type == BuffDebuff_SO.EffectType.LifeDrain)
        {
            EditorGUILayout.PropertyField(valueProp);
        }

        // --- Draw 'HasDuration' (which controls 'Duration') ---
        EditorGUILayout.PropertyField(hasDurationProp); // HasDuration must be drawn before Duration

        // --- Conditional display for 'Duration' ---
        // Hide 'Duration' if HasDuration is false
        if (buffDebuff.HasDuration)
        {
            EditorGUILayout.PropertyField(durationProp);
        }

        // --- Conditional display for 'CanStack' and 'MaxStack' ---
        // Hide 'CanStack' and 'MaxStack' if DataType is Boolean
        if (buffDebuff.Data_Type != BuffDebuff_SO.DataType.Boolean)
        {
            EditorGUILayout.PropertyField(canStackProp);

            // --- Conditional display for 'MaxStack' ---
            // Hide 'MaxStack' if CanStack is false
            if (buffDebuff.CanStack)
            {
                EditorGUILayout.PropertyField(maxStackProp);
            }
        }

        // Apply all modified properties back to the actual object.
        // This is crucial for saving changes and enabling Undo/Redo.
        serializedObject.ApplyModifiedProperties();
    }
}