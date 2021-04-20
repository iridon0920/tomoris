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
        public void Blocks同士のEqualsメソッドテスト()
        {
            var blocks1 = new IShapedBlocks();
            var blocks2 = new IShapedBlocks();
            var blocks3 = new LShapedBlocks();
            Assert.IsTrue(blocks1.Equals(blocks2));
            Assert.IsFalse(blocks1.Equals(blocks3));
        }

        [Test]
        public void I字ブロックを左回転をした際のブロック位置テスト()
        {
            IBlocks blocks = new IShapedBlocks();

            blocks = blocks.LeftSpin();
            Assert.IsTrue(new Block(-2, 0).Equals(blocks.BlockList[0]));
            Assert.IsTrue(new Block(-1, 0).Equals(blocks.BlockList[1]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.BlockList[2]));
            Assert.IsTrue(new Block(1, 0).Equals(blocks.BlockList[3]));
        }

        [Test]
        public void L字ブロックを右回転した際のブロック位置テスト()
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
