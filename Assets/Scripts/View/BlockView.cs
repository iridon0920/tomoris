using UnityEngine;
public class BlockView : MonoBehaviour
{
    private Vector3 TargetPosition;

    void Awake()
    {
        TargetPosition = transform.position;
    }

    void Update()
    {
        var t = 1 - Mathf.Pow(0.1f, Time.deltaTime / 0.1f);
        transform.position = Vector3.Lerp(transform.position, TargetPosition, t);
    }

    public void MoveToTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
    }
}
