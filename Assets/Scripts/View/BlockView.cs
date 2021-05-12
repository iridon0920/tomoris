using UnityEngine;
using DG.Tweening;

public class BlockView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer SpriteRenderer;

    public void MoveToTargetPosition(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, 0.05f)
            .SetEase(Ease.OutCubic)
            .SetLink(gameObject);
    }

    public void FallToTargetPosition(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, 0.3f)
            .SetEase(Ease.InQuad);
    }

    public void Erase()
    {
        SpriteRenderer.DOFade(0f, 0.5f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
