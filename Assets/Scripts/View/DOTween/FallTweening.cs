using UnityEngine;
using DG.Tweening;

public class FallTweening : MonoBehaviour
{
    public void FallToTargetPosition(Vector3 targetPosition, float duration)
    {
        transform.DOMove(targetPosition, duration)
            .SetEase(Ease.InQuad);
    }
}
