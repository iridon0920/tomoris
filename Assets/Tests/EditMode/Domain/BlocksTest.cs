using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests
{
    public class BlocksTest
    {
        [Test]
        public void EqualsTest()
        {
            var blocks1 = new IShapedBlocks();
            var blocks2 = new IShapedBlocks();
            var blocks3 = new LShapedBlocks();
            Assert.IsTrue(blocks1.Equals(blocks2));
            Assert.IsFalse(blocks1.Equals(blocks3));
        }

        [Test]
        public void IBlockSpinTest()
        {
            IBlocks blocks = new IShapedBlocks();

            blocks = blocks.LeftSpin();
            Assert.IsTrue(new Block(-2, 0).Equals(blocks.BlockList[0]));
            Assert.IsTrue(new Block(-1, 0).Equals(blocks.BlockList[1]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.BlockList[2]));
            Assert.IsTrue(new Block(1, 0).Equals(blocks.BlockList[3]));
        }

        [Test]
        public void LBlockSpinTest()
        {
            IBlocks blocks = new LShapedBlocks();

            blocks = blocks.RightSpin();
            Assert.IsTrue(new Block(1, 0).Equals(blocks.BlockList[0]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.BlockList[1]));
            Assert.IsTrue(new Block(-1, 0).Equals(blocks.BlockList[2]));
            Assert.IsTrue(new Block(-1, -1).Equals(blocks.BlockList[3]));
        }
    }
}
