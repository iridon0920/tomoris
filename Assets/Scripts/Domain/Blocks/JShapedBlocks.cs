using System;
using System.Collections.Generic;
public class JShapedBlocks : Blocks
{
    public JShapedBlocks()
    {
        BlockList = new List<IBlock>
            {
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1),
                new Block(-1, -1)
            };
    }
}
