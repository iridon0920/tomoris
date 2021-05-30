using System;
using System.Collections.Generic;
using Zenject;

public class BlocksFactory
{
    private List<int> CreatedNumbers;
    private readonly Random Random;
    const int NUMBER_OF_TYPES = 7;

    [Inject]
    public BlocksFactory(Random random)
    {
        Random = random;
        CreatedNumbers = new List<int>();
    }

    public Blocks Create()
    {
        int romdomNumber;

        // 一度作られたブロックは一巡するまで作られないように
        while (true)
        {
            var number = Random.Next(NUMBER_OF_TYPES);
            var findIndex = CreatedNumbers.FindIndex(createdNumber => createdNumber == number);
            if (findIndex == -1)
            {
                CreatedNumbers.Add(number);
                romdomNumber = number;
                break;
            }
        }

        if (CreatedNumbers.Count >= NUMBER_OF_TYPES)
        {
            CreatedNumbers = new List<int>();
        }
        return CreateBlocksByNumber(romdomNumber);
    }

    private Blocks CreateBlocksByNumber(int number)
    {
        switch (number)
        {
            case 0:
                return new IShapedBlocks();
            case 1:
                return new LShapedBlocks();
            case 2:
                return new JShapedBlocks();
            case 3:
                return new ZShapedBlocks();
            case 4:
                return new SShapedBlocks();
            case 5:
                return new TShapedBlocks();
            case 6:
                return new OShapedBlocks();
            default:
                return new IShapedBlocks();
        }
    }

}
