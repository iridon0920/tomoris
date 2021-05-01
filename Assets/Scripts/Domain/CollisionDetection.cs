using UnityEngine;
using System.Linq;
using Zenject;

public class CollisionDetection
{
    private IBoard Board { get; }

    [Inject]
    public CollisionDetection(IBoard board)
    {
        Board = board;
    }

    public bool IsCollision(IControlBlocks controlBlocks)
    {
        bool result = false;

        controlBlocks.Blocks.BlockList.ToList().ForEach(block =>
        {
            if (IsCollisionBlock(controlBlocks, block))
            {
                result = true;
                return;
            }
        });

        return result;
    }

    public bool IsCollisionBlock(IControlBlocks controlBlocks, IBlock block)
    {
        return IsCollisionBlockForRightWall(controlBlocks, block)
            || IsCollisionBlockForLeftWall(controlBlocks, block)
            || IsCollisionBlockForGround(controlBlocks, block)
            || IsCollisionBlockForPutBlock(controlBlocks, block);
    }

    public bool IsCollisionPutPosition(IControlBlocks controlBlocks)
    {
        return IsCollisionGround(controlBlocks)
            || IsCollisionPutBlock(controlBlocks);
    }

    private bool IsCollisionBlockForRightWall(IControlBlocks controlBlocks, IBlock block)
    {
        return GetBoardPositionX(controlBlocks, block) > Board.Width - 1;
    }

    private bool IsCollisionBlockForLeftWall(IControlBlocks controlBlocks, IBlock block)
    {
        return GetBoardPositionX(controlBlocks, block) < 0;
    }

    private bool IsCollisionGround(IControlBlocks controlBlocks)
    {
        return controlBlocks.Blocks.BlockList.Any(block =>
        {
            return IsCollisionBlockForGround(controlBlocks, block);
        });
    }


    private bool IsCollisionBlockForGround(IControlBlocks controlBlocks, IBlock block)
    {
        return GetBoardPositionY(controlBlocks, block) < 0;
    }

    // ブロックとの衝突判定
    private bool IsCollisionPutBlock(IControlBlocks controlBlocks)
    {
        return controlBlocks.Blocks.BlockList.Any(block =>
        {
            return IsCollisionBlockForPutBlock(controlBlocks, block);
        });
    }

    private bool IsCollisionBlockForPutBlock(IControlBlocks controlBlocks, IBlock block)
    {
        return Board.ExistPosition(
            GetBoardPositionX(controlBlocks, block),
            GetBoardPositionY(controlBlocks, block)
        );
    }

    private int GetBoardPositionX(IControlBlocks controlBlocks, IBlock block)
    {
        return controlBlocks.X + block.X;
    }

    private int GetBoardPositionY(IControlBlocks controlBlocks, IBlock block)
    {
        return controlBlocks.Y + block.Y;
    }
}
