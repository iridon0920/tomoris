using System;
using System.Linq;
using System.Collections.Generic;

public interface IBlocks : IEquatable<IBlocks>
{
    List<IBlock> BlockList { get; }
    IBlocks LeftSpin();
    IBlocks RightSpin();
    List<IBlock> GetUpperBlocks();
    List<IBlock> GetLowerBlocks();
    List<IBlock> GetLeftSideBlocks();
    List<IBlock> GetRightSideBlocks();
}
public class Blocks : IBlocks
{
    public List<IBlock> BlockList { get; protected set; }

    public Blocks()
    {
        BlockList = new List<IBlock>();
    }

    public Blocks(List<IBlock> blockList)
    {
        BlockList = blockList;
    }

    public IBlocks LeftSpin()
    {
        var newBlockList = new List<IBlock>();
        foreach (var block in BlockList)
        {
            newBlockList.Add(block.RotateLeft90Degree());
        }

        return new Blocks(newBlockList);
    }

    public IBlocks RightSpin()
    {
        var newBlockList = new List<IBlock>();
        foreach (var block in BlockList)
        {
            newBlockList.Add(block.RotateRight90Degree());
        }

        return new Blocks(newBlockList);
    }

    public bool Equals(IBlocks blocks)
    {
        if (BlockList.Count != blocks.BlockList.Count)
        {
            return false;
        }

        var result = true;
        for (var i = 0; i < BlockList.Count; i++)
        {
            if (!BlockList[i].Equals(blocks.BlockList[i]))
            {
                result = false;
            }
        }
        return result;
    }

    public List<IBlock> GetUpperBlocks()
    {
        return BlockList
                    .Where(block => block.Y < 0 && block.Y >= GetMaxYPosition())
                    .ToList();
    }

    public List<IBlock> GetLowerBlocks()
    {
        return BlockList
                    .Where(block => block.Y < 0 && block.Y >= GetMinYPosition())
                    .ToList();
    }

    public List<IBlock> GetLeftSideBlocks()
    {
        return BlockList
                    .Where(block => block.X < 0 && block.X >= GetMinXPosition())
                    .ToList();
    }

    public List<IBlock> GetRightSideBlocks()
    {
        return BlockList
                    .Where(block => block.X > 0 && block.X <= GetMaxXPosition())
                    .ToList();
    }

    private int GetMinYPosition()
    {
        return BlockList.Select(selectBlock => selectBlock.Y).Min();
    }

    private int GetMaxYPosition()
    {
        return BlockList.Select(selectBlock => selectBlock.Y).Max();
    }

    private int GetMinXPosition()
    {
        return BlockList.Select(selectBlock => selectBlock.X).Min();
    }

    private int GetMaxXPosition()
    {
        return BlockList.Select(selectBlock => selectBlock.X).Max(); ;
    }
}
