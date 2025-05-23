using UnityEngine;

public class BuffDebuff_Sender : MonoBehaviour
{
    [SerializeField] BuffDebuff_SO _buffDebuffInstance;

    private void OnTriggerEnter(Collider other)
    {
        if (_buffDebuffInstance == null) return;

        BuffDebuff_Handler target;
        other.TryGetComponent<BuffDebuff_Handler>(out target);

        if (target != null)
        {
            print("Apply Buff!");
            target.P_Apply(_buffDebuffInstance);
        }
    }
}
