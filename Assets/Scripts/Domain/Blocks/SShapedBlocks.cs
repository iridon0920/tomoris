using System;
using System.Collections.Generic;
public class SShapedBlocks : Blocks
{
    public SShapedBlocks()
    {
        BlockList = new List<IBlock>
            {
                new Block(1, 0, BlockColor.Green),
                new Block(0, 0, BlockColor.Green),
                new Block(0, -1, BlockColor.Green),
                new Block(-1, -1, BlockColor.Green)
            };
    }
}
