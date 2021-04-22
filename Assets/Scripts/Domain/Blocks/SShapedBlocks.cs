using System;
using System.Collections.Generic;
public class SShapedBlocks : Blocks
{
    public SShapedBlocks()
    {
        BlockList = new List<IBlock>
            {
                new Block(1, 0),
                new Block(0, 0),
                new Block(0, -1),
                new Block(-1, -1)
            };
    }
}
