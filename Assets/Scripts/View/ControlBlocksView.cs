using System.Collections.Generic;
using UnityEngine;
public class ControlBlocksView : MonoBehaviour
{
    private List<BlockView> Blocks = new List<BlockView>();

    [SerializeField]
    private BlockViewFactory BlockViewFactory;

    public async void DrawControlBlocks(IControlBlocks controlBlocks)
    {
        Blocks = new List<BlockView>();
        foreach (var block in controlBlocks.Blocks.BlockList)
        {
            var newPosition = transform.position;
            newPosition.x += controlBlocks.X + block.X;
            newPosition.y += controlBlocks.Y + block.Y;

            var blockObject = await BlockViewFactory.InstantiateBlock(
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
            Destroy(block.gameObject);
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

                Blocks[i].MoveToTargetPosition(newPosition);
                i++;
            }
        }

    }
}
