using System;
using System.Collections.Generic;

public interface IBlocks : IEquatable<IBlocks>
{
    List<IBlock> BlockList { get; }
    IBlocks LeftSpin();
    IBlocks RightSpin();
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
}
