using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests
{
    public class ControlBlocksTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GetBlockListTest()
        {
            var controlBlocks = new ControlBlocks(CreateIBlockList());
            Assert.AreEqual(CreateIBlockList(), controlBlocks.BlockList);
            // Use the Assert class to test conditions
        }

        [Test]
        public void EqualsTest()
        {
            var controlBlocks1 = new ControlBlocks(CreateIBlockList());
            var controlBlocks2 = new ControlBlocks(CreateIBlockList());
            var controlBlocks3 = new ControlBlocks(CreateLBlockList());
            Assert.IsTrue(controlBlocks1.Equals(controlBlocks2));
            Assert.IsFalse(controlBlocks1.Equals(controlBlocks3));
        }

        [Test]
        public void IBlockSpinTest()
        {
            var controlBlocks = new ControlBlocks(CreateIBlockList());

            controlBlocks = controlBlocks.Spin();
            Assert.IsTrue(new ControlBlock(-2, 0).Equals(controlBlocks.BlockList[0]));
            Assert.IsTrue(new ControlBlock(-1, 0).Equals(controlBlocks.BlockList[1]));
            Assert.IsTrue(new ControlBlock(0, 0).Equals(controlBlocks.BlockList[2]));
            Assert.IsTrue(new ControlBlock(1, 0).Equals(controlBlocks.BlockList[3]));

            controlBlocks = controlBlocks.Spin();
            Assert.IsTrue(new ControlBlock(0, -2).Equals(controlBlocks.BlockList[0]));
            Assert.IsTrue(new ControlBlock(0, -1).Equals(controlBlocks.BlockList[1]));
            Assert.IsTrue(new ControlBlock(0, 0).Equals(controlBlocks.BlockList[2]));
            Assert.IsTrue(new ControlBlock(0, 1).Equals(controlBlocks.BlockList[3]));

            controlBlocks = controlBlocks.Spin();
            Assert.IsTrue(new ControlBlock(2, 0).Equals(controlBlocks.BlockList[0]));
            Assert.IsTrue(new ControlBlock(1, 0).Equals(controlBlocks.BlockList[1]));
            Assert.IsTrue(new ControlBlock(0, 0).Equals(controlBlocks.BlockList[2]));
            Assert.IsTrue(new ControlBlock(-1, 0).Equals(controlBlocks.BlockList[3]));
        }

        [Test]
        public void LBlockSpinTest()
        {
            var controlBlocks = new ControlBlocks(CreateLBlockList());

            controlBlocks = controlBlocks.Spin();
            Assert.IsTrue(new ControlBlock(-1, 0).Equals(controlBlocks.BlockList[0]));
            Assert.IsTrue(new ControlBlock(0, 0).Equals(controlBlocks.BlockList[1]));
            Assert.IsTrue(new ControlBlock(1, 0).Equals(controlBlocks.BlockList[2]));
            Assert.IsTrue(new ControlBlock(1, 1).Equals(controlBlocks.BlockList[3]));

            controlBlocks = controlBlocks.Spin();
            Assert.IsTrue(new ControlBlock(0, -1).Equals(controlBlocks.BlockList[0]));
            Assert.IsTrue(new ControlBlock(0, 0).Equals(controlBlocks.BlockList[1]));
            Assert.IsTrue(new ControlBlock(0, 1).Equals(controlBlocks.BlockList[2]));
            Assert.IsTrue(new ControlBlock(-1, 1).Equals(controlBlocks.BlockList[3]));

            controlBlocks = controlBlocks.Spin();
            Assert.IsTrue(new ControlBlock(1, 0).Equals(controlBlocks.BlockList[0]));
            Assert.IsTrue(new ControlBlock(0, 0).Equals(controlBlocks.BlockList[1]));
            Assert.IsTrue(new ControlBlock(-1, 0).Equals(controlBlocks.BlockList[2]));
            Assert.IsTrue(new ControlBlock(-1, -1).Equals(controlBlocks.BlockList[3]));
        }


        // I字ブロックのリスト作成
        private List<ControlBlock> CreateIBlockList()
        {
            return new List<ControlBlock>
            {
                new ControlBlock(0, 2),
                new ControlBlock(0, 1),
                new ControlBlock(0, 0),
                new ControlBlock(0, -1)
            };
        }

        // L字ブロックのリスト作成
        private List<ControlBlock> CreateLBlockList()
        {
            return new List<ControlBlock>
            {
            new ControlBlock(0, 1),
            new ControlBlock(0, 0),
            new ControlBlock(0, -1),
            new ControlBlock(1, -1)
            };
        }
    }
}
