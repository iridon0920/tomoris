using System;
using System.Collections.Generic;
public class JShapedBlocks : Blocks
{
    public JShapedBlocks()
    {
        BlockList = new List<IBlock>
            {
                new Block(0, 1, BlockColor.Blue),
                new Block(0, 0, BlockColor.Blue),
                new Block(0, -1, BlockColor.Blue),
                new Block(-1, -1, BlockColor.Blue)
            };
    }
}
