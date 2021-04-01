using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests
{
    public class BlocksTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GetBlockListTest()
        {
            var blocks = new Blocks(CreateIBlockList());
            Assert.AreEqual(CreateIBlockList(), blocks.BlockList);
            // Use the Assert class to test conditions
        }

        [Test]
        public void EqualsTest()
        {
            var blocks1 = new Blocks(CreateIBlockList());
            var blocks2 = new Blocks(CreateIBlockList());
            var blocks3 = new Blocks(CreateLBlockList());
            Assert.IsTrue(blocks1.Equals(blocks2));
            Assert.IsFalse(blocks1.Equals(blocks3));
        }

        [Test]
        public void IBlockSpinTest()
        {
            var blocks = new Blocks(CreateIBlockList());

            blocks = blocks.Spin();
            Assert.IsTrue(new Block(-2, 0).Equals(blocks.BlockList[0]));
            Assert.IsTrue(new Block(-1, 0).Equals(blocks.BlockList[1]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.BlockList[2]));
            Assert.IsTrue(new Block(1, 0).Equals(blocks.BlockList[3]));

            blocks = blocks.Spin();
            Assert.IsTrue(new Block(0, -2).Equals(blocks.BlockList[0]));
            Assert.IsTrue(new Block(0, -1).Equals(blocks.BlockList[1]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.BlockList[2]));
            Assert.IsTrue(new Block(0, 1).Equals(blocks.BlockList[3]));

            blocks = blocks.Spin();
            Assert.IsTrue(new Block(2, 0).Equals(blocks.BlockList[0]));
            Assert.IsTrue(new Block(1, 0).Equals(blocks.BlockList[1]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.BlockList[2]));
            Assert.IsTrue(new Block(-1, 0).Equals(blocks.BlockList[3]));
        }

        [Test]
        public void LBlockSpinTest()
        {
            var blocks = new Blocks(CreateLBlockList());

            blocks = blocks.Spin();
            Assert.IsTrue(new Block(-1, 0).Equals(blocks.BlockList[0]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.BlockList[1]));
            Assert.IsTrue(new Block(1, 0).Equals(blocks.BlockList[2]));
            Assert.IsTrue(new Block(1, 1).Equals(blocks.BlockList[3]));

            blocks = blocks.Spin();
            Assert.IsTrue(new Block(0, -1).Equals(blocks.BlockList[0]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.BlockList[1]));
            Assert.IsTrue(new Block(0, 1).Equals(blocks.BlockList[2]));
            Assert.IsTrue(new Block(-1, 1).Equals(blocks.BlockList[3]));

            blocks = blocks.Spin();
            Assert.IsTrue(new Block(1, 0).Equals(blocks.BlockList[0]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.BlockList[1]));
            Assert.IsTrue(new Block(-1, 0).Equals(blocks.BlockList[2]));
            Assert.IsTrue(new Block(-1, -1).Equals(blocks.BlockList[3]));
        }


        // I字ブロックのリスト作成
        private List<Block> CreateIBlockList()
        {
            return new List<Block>
            {
                new Block(0, 2),
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1)
            };
        }

        // L字ブロックのリスト作成
        private List<Block> CreateLBlockList()
        {
            return new List<Block>
            {
            new Block(0, 1),
            new Block(0, 0),
            new Block(0, -1),
            new Block(1, -1)
            };
        }
    }
}
