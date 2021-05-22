using UnityEngine;

[RequireComponent(typeof(MoveTweening))]
public class CursorView : MonoBehaviour
{
    [SerializeField]
    private MoveTweening MoveTweening;

    const float MOVE_DURATION = 0.05f;

    public void MoveToTargetPosition(Vector3 targetPosition)
    {
        MoveTweening.MoveToTargetPosition(targetPosition, MOVE_DURATION);
    }
}
