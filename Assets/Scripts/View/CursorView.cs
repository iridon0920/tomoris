using UnityEngine;
using DG.Tweening;

public class CursorView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer SpriteRenderer;

    public void MoveToTargetPosition(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, 0.05f)
            .SetEase(Ease.OutCubic)
            .SetLink(gameObject);
    }
}
