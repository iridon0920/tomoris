using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class BlocksQueueView : MonoBehaviour
{
    private readonly Queue<List<BlockView>> QueueBlocks = new Queue<List<BlockView>>();

    [SerializeField]
    private BlockViewFactory BlockViewFactory;

    private int IntervalY = 5;

    public void ChangePosition(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    public void DrawQueueBlocks(IBlocks blocks)
    {
        var index = QueueBlocks.Count;

        var blockObjects = new List<BlockView>();
        foreach (var block in blocks.BlockList)
        {
            var newPosition = transform.position;
            newPosition.x += block.X;
            newPosition.y -= index * IntervalY - block.Y;

            var blockObject = BlockViewFactory.InstantiateBlock(
                block.BlockColor,
                newPosition,
                transform
            );
            blockObjects.Add(blockObject);
        }
        QueueBlocks.Enqueue(blockObjects);
    }

    public void DeleteTopBlocks()
    {
        if (QueueBlocks.Count > 0)
        {
            var deleteTargetBlocks = QueueBlocks.Dequeue();
            foreach (var deleteTargetBlock in deleteTargetBlocks)
            {
                deleteTargetBlock.Erase();
                Destroy(deleteTargetBlock.gameObject);
            }
        }
    }

    public void SqueezeEmptyPosition()
    {
        var index = 0;
        foreach (var blockViews in QueueBlocks.ToArray())
        {
            foreach (var blockView in blockViews)
            {
                var newPosition = blockView.transform.position;
                newPosition.y += IntervalY;

                blockView.MoveToTargetPosition(newPosition);
            }
            index++;
        }
    }
}
