using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests
{
    public class BlocksTest
    {
        // I字ブロック配列定義
        readonly private Block[] IBlockArray = {
            new Block(0,2),
            new Block(0,1),
            new Block(0,0),
            new Block(0,-1)
        };

        // L字ブロック配列定義
        readonly private Block[] LBlockArray = {
            new Block(0,1),
            new Block(0,0),
            new Block(0,-1),
            new Block(1, -1)
        };

        // A Test behaves as an ordinary method
        [Test]
        public void GetBlockArrayTest()
        {
            Blocks blocks = new Blocks(IBlockArray);
            Assert.AreEqual(IBlockArray, blocks.blockArray);
            // Use the Assert class to test conditions
        }

        [Test]
        public void IBlockSpinTest()
        {
            var blocks = new Blocks(IBlockArray);

            blocks.spin();
            Assert.IsTrue(new Block(-2, 0).Equals(blocks.blockArray[0]));
            Assert.IsTrue(new Block(-1, 0).Equals(blocks.blockArray[1]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.blockArray[2]));
            Assert.IsTrue(new Block(1, 0).Equals(blocks.blockArray[3]));

            blocks.spin();
            Assert.IsTrue(new Block(0, -2).Equals(blocks.blockArray[0]));
            Assert.IsTrue(new Block(0, -1).Equals(blocks.blockArray[1]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.blockArray[2]));
            Assert.IsTrue(new Block(0, 1).Equals(blocks.blockArray[3]));

            blocks.spin();
            Assert.IsTrue(new Block(2, 0).Equals(blocks.blockArray[0]));
            Assert.IsTrue(new Block(1, 0).Equals(blocks.blockArray[1]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.blockArray[2]));
            Assert.IsTrue(new Block(-1, 0).Equals(blocks.blockArray[3]));
        }

        [Test]
        public void LBlockSpinTest()
        {
            var blocks = new Blocks(LBlockArray);

            blocks.spin();
            Assert.IsTrue(new Block(-1, 0).Equals(blocks.blockArray[0]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.blockArray[1]));
            Assert.IsTrue(new Block(1, 0).Equals(blocks.blockArray[2]));
            Assert.IsTrue(new Block(1, 1).Equals(blocks.blockArray[3]));

            blocks.spin();
            Assert.IsTrue(new Block(0, -1).Equals(blocks.blockArray[0]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.blockArray[1]));
            Assert.IsTrue(new Block(0, 1).Equals(blocks.blockArray[2]));
            Assert.IsTrue(new Block(-1, 1).Equals(blocks.blockArray[3]));

            blocks.spin();
            Assert.IsTrue(new Block(1, 0).Equals(blocks.blockArray[0]));
            Assert.IsTrue(new Block(0, 0).Equals(blocks.blockArray[1]));
            Assert.IsTrue(new Block(-1, 0).Equals(blocks.blockArray[2]));
            Assert.IsTrue(new Block(-1, -1).Equals(blocks.blockArray[3]));
        }
    }
}
