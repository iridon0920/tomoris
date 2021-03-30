using System;
using System.Collections.Generic;
using System.Linq;
public class ControlBlocks : IEquatable<ControlBlocks>
{
    public List<ControlBlock> BlockList { get; }
    public ControlBlocks(List<ControlBlock> blockList)
    {
        BlockList = blockList;
    }

    public ControlBlocks Spin()
    {
        var newBlockList = new List<ControlBlock>();
        foreach (var block in BlockList)
        {
            newBlockList.Add(new ControlBlock(-block.Y, block.X));
        }

        return new ControlBlocks(newBlockList);
    }

    public bool Equals(ControlBlocks controlBlocks)
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
