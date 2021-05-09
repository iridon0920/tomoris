using System;
using System.Collections.Generic;
public class IShapedBlocks : Blocks
{
    public IShapedBlocks()
    {
        BlockList = new List<IBlock>
            {
                new Block(0, 2, BlockColor.LightBlue),
                new Block(0, 1, BlockColor.LightBlue),
                new Block(0, 0, BlockColor.LightBlue),
                new Block(0, -1, BlockColor.LightBlue)
            };
    }
}
