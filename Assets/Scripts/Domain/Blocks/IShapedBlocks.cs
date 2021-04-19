using System;
using System.Collections.Generic;
public class IShapedBlocks : Blocks
{
    public IShapedBlocks()
    {
        BlockList = new List<IBlock>
            {
                new Block(0, 2),
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1)
            };
    }
}
