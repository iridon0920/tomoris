using System;
using System.Collections.Generic;
using System.Linq;
public class ControlBlocks
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
}
