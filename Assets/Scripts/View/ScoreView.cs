using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private Text Points;

    void Awake()
    {
        var parent = GameObject.Find("Scores").transform;
        transform.SetParent(parent, false);
    }

    public void ChangePointsPosition(float x, float y)
    {
        Points.rectTransform.anchoredPosition = new Vector3(x, y, 0);
    }

    public void UpdatePointsText(int points)
    {
        Points.text = points.ToString();
    }
}
