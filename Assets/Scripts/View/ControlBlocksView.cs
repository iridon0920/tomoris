using System.Collections.Generic;
using UnityEngine;
public class ControlBlocksView : MonoBehaviour
{
    private int PlayerId;
    private List<BlockView> Blocks = new List<BlockView>();

    private CursorView Cursor;
    const int CURSOR_POSITION_Y = 3;

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
        var cursorPosition = transform.position;
        cursorPosition.x += controlBlocks.X;
        cursorPosition.y += controlBlocks.Y + CURSOR_POSITION_Y;

        Cursor = await CursorViewFactory.Instantiate(PlayerId, cursorPosition, transform);
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
            Destroy(Cursor.gameObject);

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
            var newCursorPosition = transform.position;
            newCursorPosition.x += controlBlocks.X;
            newCursorPosition.y += controlBlocks.Y + CURSOR_POSITION_Y;

            Cursor.MoveToTargetPosition(newCursorPosition);
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
