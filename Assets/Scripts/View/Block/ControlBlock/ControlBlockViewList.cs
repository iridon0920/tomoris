using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ControlBlockViewPosition))]
public class ControlBlockViewList : MonoBehaviour
{
    private List<BlockView> Blocks = new List<BlockView>();

    [SerializeField]
    private BlockViewFactory BlockViewFactory;
    [SerializeField]
    private ControlBlockViewPosition ControlBlockViewPosition;

    public void InstantiateBlocks(IControlBlocks controlBlocks, Transform transform)
    {
        Blocks = new List<BlockView>();
        foreach (var block in controlBlocks.Blocks.BlockList)
        {
            var blockObject = BlockViewFactory.InstantiateBlock(
                block.BlockColor,
                ControlBlockViewPosition.GetPositionByControlBlocks(
                    controlBlocks,
                    block,
                    transform
                ),
                transform
            );

            Blocks.Add(blockObject);
        }
    }

    public void RemoveAll()
    {
        foreach (var block in Blocks)
        {
            Destroy(block.gameObject);
        }
    }

    public void MoveToTargetPosition(IControlBlocks controlBlocks, Transform transform)
    {
        if (Blocks.Count > 0)
        {
            int i = 0;
            foreach (var block in controlBlocks.Blocks.BlockList)
            {
                var newPosition = ControlBlockViewPosition.GetPositionByControlBlocks(
                    controlBlocks,
                    block,
                    transform
                );

                Blocks[i].MoveToTargetPosition(newPosition);
                i++;
            }
        }
    }
}
