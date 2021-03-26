using System;
using System.Linq;
public class Blocks
{
    public Block[] blockArray { get; }
    public Blocks(Block[] blockArray)
    {
        this.blockArray = blockArray;
    }

    public void spin()
    {
        foreach (var (block, index) in blockArray.Select((block, index) => (block, index)))
        {
            this.blockArray[index] = new Block(-block.y, block.x);
        }
    }
}
