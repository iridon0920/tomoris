using System;
using System.Collections.Generic;
public class OShapedBlocks : Blocks
{
    public OShapedBlocks()
    {
        BlockList = new List<IBlock>
            {
                new Block(0, 0, BlockColor.Yellow),
                new Block(1, 0, BlockColor.Yellow),
                new Block(0, -1, BlockColor.Yellow),
                new Block(1, -1, BlockColor.Yellow)
            };
    }
}
