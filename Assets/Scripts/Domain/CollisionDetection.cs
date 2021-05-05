using UnityEngine;
using System.Linq;
using Zenject;
using System.Collections.Generic;

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
        return IsCollisionByBlockList(controlBlocks.GetBoardPositionBlockList());
    }

    public bool IsCollisionPutPosition(IControlBlocks controlBlocks)
    {
        return IsCollisionPutPositionByBlockList(controlBlocks.GetBoardPositionBlockList());
    }

    public bool IsCollisionControlBlocksUpper(IControlBlocks controlBlocks)
    {
        return IsCollisionByBlockList(controlBlocks.GetBoardPositionBlockListByUpper());
    }

    public bool IsCollisionControlBlocksLower(IControlBlocks controlBlocks)
    {
        return IsCollisionByBlockList(controlBlocks.GetBoardPositionBlockListByLower());
    }

    public bool IsCollisionControlBlocksLeftSide(IControlBlocks controlBlocks)
    {
        return IsCollisionByBlockList(controlBlocks.GetBoardPositionBlockListByLeftSide());
    }

    public bool IsCollisionControlBlocksRightSide(IControlBlocks controlBlocks)
    {
        return IsCollisionByBlockList(controlBlocks.GetBoardPositionBlockListByRightSide());
    }

    private bool IsCollisionByBlockList(List<IBlock> blockList)
    {
        bool result = false;

        blockList.ForEach(block =>
        {
            if (
                IsCollisionBlockForRightWall(block)
                    || IsCollisionBlockForLeftWall(block)
                    || IsCollisionBlockForGround(block)
                    || IsCollisionBlockForPutBlock(block)
            )
            {
                result = true;
                return;
            }
        });

        return result;
    }
    private bool IsCollisionPutPositionByBlockList(List<IBlock> blockList)
    {
        bool result = false;

        blockList.ForEach(block =>
        {
            if (
                IsCollisionBlockForGround(block)
                || IsCollisionBlockForPutBlock(block)
            )
            {
                result = true;
                return;
            }
        });

        return result;
    }

    private bool IsCollisionBlockForRightWall(IBlock block)
    {
        return block.X > Board.Width - 1;
    }

    private bool IsCollisionBlockForLeftWall(IBlock block)
    {
        return block.X < 0;
    }

    private bool IsCollisionBlockForGround(IBlock block)
    {
        return block.Y < 0;
    }

    // ブロックとの衝突判定
    private bool IsCollisionBlockForPutBlock(IBlock block)
    {
        return Board.ExistPosition(block.X, block.Y);
    }
}
