using UnityEngine;

public interface IControlBlocksCollisionDetection
{
    bool IsCollision(IControlBlocks controlBlocks);
}
public class ControlBlocksCollisionDetection : IControlBlocksCollisionDetection
{
    private IBoard Board { get; }

    public ControlBlocksCollisionDetection(IBoard board)
    {
        Board = board;
    }

    public bool IsCollision(IControlBlocks controlBlocks)
    {
        return IsCollisionRightWall(controlBlocks)
            || IsCollisionLeftWall(controlBlocks)
            || IsCollisionGround(controlBlocks);
    }

    private bool IsCollisionRightWall(IControlBlocks controlBlocks)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.Blocks.BlockList)
        {
            var movePlanPositionX = controlBlocks.X + controlBlock.X;
            var movePlanPositionY = controlBlocks.Y + controlBlock.Y;
            if (movePlanPositionX > Board.Width - 1
                || IsMovePlanCollisionPutBlock(movePlanPositionX, movePlanPositionY))
            {
                result = true;
            }
        }
        return result;
    }

    private bool IsCollisionLeftWall(IControlBlocks controlBlocks)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.Blocks.BlockList)
        {
            var movePlanPositionX = controlBlocks.X + controlBlock.X;
            var movePlanPositionY = controlBlocks.Y + controlBlock.Y;
            if (movePlanPositionX < 0
                || IsMovePlanCollisionPutBlock(movePlanPositionX, movePlanPositionY))
            {
                result = true;
            }
        }
        return result;
    }

    private bool IsCollisionGround(IControlBlocks controlBlocks)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.Blocks.BlockList)
        {
            var movePlanPositionX = controlBlocks.X + controlBlock.X;
            var movePlanPositionY = controlBlocks.Y + controlBlock.Y;

            if (movePlanPositionY < 0
                || IsMovePlanCollisionPutBlock(movePlanPositionX, movePlanPositionY))
            {
                result = true;
            }
        }
        return result;
    }

    // ブロックとの衝突判定
    private bool IsMovePlanCollisionPutBlock(int movePlanX, int movePlanY)
    {
        if (movePlanX >= 0
            && movePlanX <= Board.Width - 1
            && movePlanY >= 0
            && movePlanY <= Board.Height - 1)
        {
            if (Board.StatusByPositions[movePlanX, movePlanY])
            {
                return true;
            }
        }
        return false;
    }
}
