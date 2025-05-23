using UnityEngine;
using TMPro;

public class UI_StatDisplay : MonoBehaviour
{
    [SerializeField] Character target;
    [SerializeField] TMP_Text moveSpeedText;
    [SerializeField] TMP_Text jumpForceText;
    [SerializeField] TMP_Text curHealthText;
    [SerializeField] TMP_Text maxHealthText;
    [SerializeField] TMP_Text defenseText;

    private void Awake()
    {
        target.OnMoveSpeedChangeEvent.AddListener(UpdateText_MoveSpeed);
        target.OnJumpForceChangeEvent.AddListener(UpdateText_JumpForce);
        target.OnCurHealthChangeEvent.AddListener(UpdateText_CurHealth);
        target.OnMaxHealthChangeEvent.AddListener(UpdateText_MaxHealth);
        target.OnDefenseChangeEvent.AddListener(UpdateText_Defense);
    }

    void UpdateText_MoveSpeed(float value) { moveSpeedText.text = "Move speed: " + value; }
    void UpdateText_JumpForce(float value) { jumpForceText.text = "Jump force: " + value; }
    void UpdateText_CurHealth(float value) { curHealthText.text = "Cur health: " + value; }
    void UpdateText_MaxHealth(float value) { maxHealthText.text = "Max health: " + value; }
    void UpdateText_Defense(float value) { defenseText.text = "Defense: " + value; }
}
