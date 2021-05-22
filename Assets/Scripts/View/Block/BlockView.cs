using UnityEngine;

[RequireComponent(typeof(MoveTweening))]
[RequireComponent(typeof(FallTweening))]
[RequireComponent(typeof(EraseTweening))]
public class BlockView : MonoBehaviour
{
    [SerializeField]
    private MoveTweening MoveTweening;
    [SerializeField]
    private FallTweening FallTweening;
    [SerializeField]
    private EraseTweening EraseTweening;

    const float MOVE_DURATION = 0.05f;
    const float FALL_DURATION = 0.3f;
    const float ERASE_DURATION = 0.5f;

    public void MoveToTargetPosition(Vector3 targetPosition)
    {
        MoveTweening.MoveToTargetPosition(targetPosition, MOVE_DURATION);
    }

    public void FallToTargetPosition(Vector3 targetPosition)
    {
        FallTweening.FallToTargetPosition(targetPosition, FALL_DURATION);
    }

    public void Erase()
    {
        EraseTweening.Erase(ERASE_DURATION);
    }
}
