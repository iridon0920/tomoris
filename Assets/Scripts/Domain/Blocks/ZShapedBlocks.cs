using System;
using System.Collections.Generic;
public class ZShapedBlocks : Blocks
{
    public ZShapedBlocks()
    {
        BlockList = new List<IBlock>
            {
                new Block(-1, 0, BlockColor.Red),
                new Block(0, 0, BlockColor.Red),
                new Block(0, -1, BlockColor.Red),
                new Block(1, -1, BlockColor.Red)
            };
    }
}
