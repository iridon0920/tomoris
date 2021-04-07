using System.Collections.Generic;
using UnityEngine;
public class GameInputManager : MonoBehaviour
{

    [SerializeField]
    public GameObject BlockPrefab;
    private List<GameObject> Blocks = new List<GameObject>();

    public void DrawControlBlocks(IControlBlocks controlBlocks)
    {
        if (Blocks.Count == 0)
        {
            Blocks = new List<GameObject>();
            foreach (var block in controlBlocks.Blocks.BlockList)
            {
                var newPosition = transform.position;
                newPosition.x += controlBlocks.X + block.X;
                newPosition.y += controlBlocks.Y + block.Y;
                Blocks.Add(Instantiate(BlockPrefab, newPosition, Quaternion.identity, this.transform));
            }
        }
        else
        {
            int i = 0;
            foreach (var block in controlBlocks.Blocks.BlockList)
            {
                var newPosition = new Vector3(controlBlocks.X + block.X, controlBlocks.Y + block.Y, 0);
                Blocks[i].transform.position = newPosition;
                i++;
            }
        }
    }
}
