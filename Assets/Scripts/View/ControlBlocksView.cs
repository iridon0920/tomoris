using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BlockViewFactory))]
[RequireComponent(typeof(CursorViewFactory))]
[RequireComponent(typeof(AudioSource))]
public class ControlBlocksView : MonoBehaviour
{
    private int PlayerId;
    private List<BlockView> Blocks = new List<BlockView>();

    private CursorView Cursor;

    [SerializeField]
    private BlockViewFactory BlockViewFactory;
    [SerializeField]
    private CursorViewFactory CursorViewFactory;

    [SerializeField]
    private AudioSource CollisionSound;

    public void SetPlayerId(int playerId)
    {
        PlayerId = playerId;
    }

    public async void DrawControlBlocks(IControlBlocks controlBlocks)
    {
        Cursor = await CursorViewFactory.Instantiate(PlayerId, controlBlocks, transform);

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
        if (Blocks.Count > 0)
        {
            Cursor.Remove();

            foreach (var block in Blocks)
            {
                Destroy(block.gameObject);
            }
        }
    }

    public void ChangeControlBlocksPosition(IControlBlocks controlBlocks)
    {
        if (Cursor)
        {
            Cursor.MoveToTargetPosition(controlBlocks, transform);
        }
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

    public void PlayCollisionSound()
    {
        CollisionSound.PlayOneShot(CollisionSound.clip);
    }
}
