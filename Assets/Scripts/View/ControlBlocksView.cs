using System.Collections.Generic;
using UnityEngine;
public class ControlBlocksView : MonoBehaviour
{
    private List<GameObject> Blocks = new List<GameObject>();

    [SerializeField]
    private BlockView BlockView;

    public async void DrawControlBlocks(IControlBlocks controlBlocks)
    {
        Blocks = new List<GameObject>();
        foreach (var block in controlBlocks.Blocks.BlockList)
        {
            var newPosition = transform.position;
            newPosition.x += controlBlocks.X + block.X;
            newPosition.y += controlBlocks.Y + block.Y;

            var blockObject = await BlockView.InstantiateBlock(
                block.BlockColor,
                newPosition,
                transform
            );

            Blocks.Add(blockObject);
        }
    }

    public void DeleteControlBlocks()
    {
        foreach (var block in Blocks)
        {
            Destroy(block);
        }
    }

    public void ChangeControlBlocksPosition(IControlBlocks controlBlocks)
    {
        if (Blocks.Count > 0)
        {
            int i = 0;
            foreach (var block in controlBlocks.Blocks.BlockList)
            {

                var newPosition = transform.position;
                newPosition.x += controlBlocks.X + block.X;
                newPosition.y += controlBlocks.Y + block.Y;

                Blocks[i].transform.position = newPosition;
                i++;
            }
        }

    }
}
