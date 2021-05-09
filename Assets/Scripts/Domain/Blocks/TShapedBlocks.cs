using System;
using System.Collections.Generic;
public class TShapedBlocks : Blocks
{
    public TShapedBlocks()
    {
        BlockList = new List<IBlock>
            {
                new Block(0, 0, BlockColor.Purple),
                new Block(-1, -1, BlockColor.Purple),
                new Block(0, -1, BlockColor.Purple),
                new Block(1, -1, BlockColor.Purple)
            };
    }
}
