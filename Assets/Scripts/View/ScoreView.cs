using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private Text Points;
    public void UpdatePointsText(int points)
    {
        Points.text = points.ToString();
    }
}
