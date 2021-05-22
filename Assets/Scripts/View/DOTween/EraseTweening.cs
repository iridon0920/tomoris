using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class EraseTweening : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer SpriteRenderer;
    public void Erase(float duration)
    {
        SpriteRenderer.DOFade(0f, duration)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            })
            .SetLink(gameObject);
    }
}
