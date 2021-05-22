using UnityEngine;
using DG.Tweening;

public class MoveTweening : MonoBehaviour
{
    public void MoveToTargetPosition(Vector3 targetPosition, float duration)
    {
        transform.DOMove(targetPosition, duration)
            .SetEase(Ease.OutCubic)
            .SetLink(gameObject);
    }
}
