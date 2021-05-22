using UnityEngine;

[RequireComponent(typeof(MoveTweening))]
[RequireComponent(typeof(CursorViewPosition))]
public class CursorView : MonoBehaviour
{
    [SerializeField]
    private MoveTweening MoveTweening;
    [SerializeField]
    private CursorViewPosition CursorViewPosition;

    const float MOVE_DURATION = 0.05f;

    public void MoveToTargetPosition(IControlBlocks controlBlocks, Transform transform)
    {
        MoveTweening.MoveToTargetPosition(
            CursorViewPosition.GetPositionByControlBlocks(controlBlocks, transform),
            MOVE_DURATION
        );
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
