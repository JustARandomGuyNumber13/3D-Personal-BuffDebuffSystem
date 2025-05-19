using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] Transform p;
    Vector3 offSet;

    private void Start()
    {
        offSet = transform.position - p.position;
    }
    private void LateUpdate()
    {
        transform.position = p.position + offSet;
    }
}
