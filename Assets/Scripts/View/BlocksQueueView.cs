using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
public class BlocksQueueView : MonoBehaviour
{
    private readonly Queue<List<BlockView>> QueueBlocks = new Queue<List<BlockView>>();

    [SerializeField]
    private BlockViewFactory BlockViewFactory;

    private int IntervalY = 2;

    public async Task DrawQueueBlocks(IBlocks blocks)
    {
        var index = QueueBlocks.Count;

        var blockObjects = new List<BlockView>();
        foreach (var block in blocks.BlockList)
        {
            var newPosition = transform.position;
            newPosition.x += block.X;
            newPosition.y += index * IntervalY + block.Y;

            var blockObject = await BlockViewFactory.InstantiateBlock(
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
                newPosition.y += index * IntervalY;

                blockView.MoveToTargetPosition(newPosition);
            }
            index++;
        }
    }
}
