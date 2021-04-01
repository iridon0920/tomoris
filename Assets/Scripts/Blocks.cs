using System;
using System.Collections.Generic;

public class Blocks : IEquatable<Blocks>
{
    public List<Block> BlockList { get; }
    public Blocks(List<Block> blockList)
    {
        BlockList = blockList;
    }

    public Blocks Spin()
    {
        var newBlockList = new List<Block>();
        foreach (var block in BlockList)
        {
            newBlockList.Add(new Block(-block.Y, block.X));
        }

        return new Blocks(newBlockList);
    }

    public bool Equals(Blocks controlBlocks)
    {
        if (BlockList.Count != controlBlocks.BlockList.Count)
        {
            return false;
        }

        var result = true;
        for (var i = 0; i < BlockList.Count; i++)
        {
            if (!BlockList[i].Equals(controlBlocks.BlockList[i]))
            {
                result = false;
            }
        }
        return result;
    }
}
