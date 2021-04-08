using System.Collections.Generic;
using UnityEngine;
public class BoardView : MonoBehaviour
{
    [SerializeField]
    private GameObject BlockPrefab;
    private List<GameObject> Blocks = new List<GameObject>();
    public void DrawBoardBlock(IBlock block)
    {
        var newPosition = transform.position;
        newPosition.x += block.X;
        newPosition.y += block.Y;
        Blocks.Add(Instantiate(BlockPrefab, newPosition, Quaternion.identity, transform));
    }
}
