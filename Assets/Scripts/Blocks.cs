using System;
using System.Collections.Generic;

public interface IBlocks : IEquatable<IBlocks>
{
    List<Block> BlockList { get; }
    IBlocks LeftSpin();
    IBlocks RightSpin();
}
public class Blocks : IBlocks
{
    public List<Block> BlockList { get; }
    public Blocks(List<Block> blockList)
    {
        BlockList = blockList;
    }

    public IBlocks LeftSpin()
    {
        var newBlockList = new List<Block>();
        foreach (var block in BlockList)
        {
            newBlockList.Add(new Block(-block.Y, block.X));
        }

        return new Blocks(newBlockList);
    }

    public IBlocks RightSpin()
    {
        var newBlockList = new List<Block>();
        foreach (var block in BlockList)
        {
            newBlockList.Add(new Block(block.Y, -block.X));
        }

        return new Blocks(newBlockList);
    }

    public bool Equals(IBlocks blocks)
    {
        if (blocks.GetType() != typeof(Blocks))
        {
            return false;
        }

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
