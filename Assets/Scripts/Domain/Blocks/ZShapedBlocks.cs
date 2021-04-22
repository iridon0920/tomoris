using System;
using System.Collections.Generic;
public class ZShapedBlocks : Blocks
{
    public ZShapedBlocks()
    {
        BlockList = new List<IBlock>
            {
                new Block(-1, 0),
                new Block(0, 0),
                new Block(0, -1),
                new Block(1, -1)
            };
    }
}
