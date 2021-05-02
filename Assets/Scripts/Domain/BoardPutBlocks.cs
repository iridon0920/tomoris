using System;
using System.Linq;
using System.Collections.Generic;

public class BoardPutBlocks
{
    private readonly List<BoardPutBlock> Blocks;

    public BoardPutBlocks(List<BoardPutBlock> blocks)
    {
        Blocks = blocks;
    }

    public void AddBoardPutBlocks(List<BoardPutBlock> addBlocks)
    {
        addBlocks.ForEach(addBlock =>
        {
            Blocks.Add(addBlock);
        });
    }

    public List<BoardPutBlock> RemoveYPositionMaxCountBlocks(int yPositionMaxCount)
    {
        var removeTargetBlocks = new List<BoardPutBlock>();
        for (var y = 0; y < GetMaxYPosition(); y++)
        {
            var blockByPositionY = Blocks.Where(block => block.GetY() == y).ToList();
            if (blockByPositionY.Count == yPositionMaxCount)
            {
                foreach (var lineBlock in blockByPositionY)
                {
                    removeTargetBlocks.Add(lineBlock);
                }
            }
        }

        RemoveBoardPutBlocks(removeTargetBlocks);
        return removeTargetBlocks;
    }

    public List<BoardPutBlock> SqueezeEmptyYPosition()
    {
        var squeezeTargetBlocks = new List<BoardPutBlock>();
        int emptyMinYPosition = -1;
        int emptyYPositionsCount = 0;

        for (var y = 0; y < GetMaxYPosition() + 1; y++)
        {
            var emptyLineBlocks = Blocks.Where(block => block.GetY() == y).ToList();
            if (emptyLineBlocks.Count == 0)
            {
                if (emptyMinYPosition == -1)
                {
                    emptyMinYPosition = y;
                }
                emptyYPositionsCount++;
            }
        }

        Blocks.Where(block => block.GetY() >= emptyMinYPosition).Select(block =>
        {
            for (var i = 0; i < emptyYPositionsCount; i++)
            {
                block.MoveDown();
            }
            squeezeTargetBlocks.Add(block);
            return block;
        }).ToList();

        return squeezeTargetBlocks;
    }

    private void RemoveBoardPutBlocks(List<BoardPutBlock> removeBlocks)
    {
        removeBlocks.ForEach(removeBlock =>
        {
            Blocks.Remove(removeBlock);
        });
    }

    public int GetMaxYPosition()
    {
        return Blocks.Select(boardblock => boardblock.GetY()).Max();
    }


    public bool ExistBlockByPosition(int x, int y)
    {
        return Blocks.Any(block => block.GetX() == x && block.GetY() == y);
    }

}
