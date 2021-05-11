using UnityEngine;
using DG.Tweening;

public class BlockView : MonoBehaviour
{
    public void MoveToTargetPosition(Vector3 targetPosition)
    {
        this.transform.DOMove(targetPosition, 0.05f)
            .SetEase(Ease.OutCubic)
            .SetLink(gameObject);
    }

    public void FallToTargetPosition(Vector3 targetPosition)
    {
        this.transform.DOMove(targetPosition, 0.3f)
            .SetEase(Ease.InQuad);
    }
}
