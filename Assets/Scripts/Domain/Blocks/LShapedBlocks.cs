using System;
using System.Collections.Generic;
public class LShapedBlocks : Blocks
{
    public LShapedBlocks()
    {
        BlockList = new List<IBlock>
            {
                new Block(0, 1, BlockColor.Orange),
                new Block(0, 0, BlockColor.Orange),
                new Block(0, -1, BlockColor.Orange),
                new Block(1, -1, BlockColor.Orange)
            };
    }
}
