using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;

namespace Tests
{
    public class ControlBlocksTest
    {
        private IBlocks MockBlocks;

        [SetUp]
        public void SetUp()
        {
            MockBlocks = new Mock<IBlocks>().Object;
        }

        [Test]
        public void CreateInstanceTest()
        {
            var controlBlocks = new ControlBlocks(5, 10, MockBlocks);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        [Test]
        public void CloneTest()
        {
            var controlBlocks = new ControlBlocks(5, 10, MockBlocks);
            var controlBlocks2 = controlBlocks;
            var controlBlocks3 = controlBlocks.Clone();
            controlBlocks.MoveRight();

            Assert.AreEqual(6, controlBlocks.X);
            Assert.AreEqual(6, controlBlocks2.X);
            Assert.AreEqual(5, controlBlocks3.X);

        }

        [Test]
        public void MoveRightTest()
        {
            var controlBlocks = new ControlBlocks(4, 14, MockBlocks);
            controlBlocks.MoveRight();
            Assert.AreEqual(5, controlBlocks.X);
        }

        [Test]
        public void MoveLeftTest()
        {
            var controlBlocks = new ControlBlocks(20, 14, MockBlocks);
            controlBlocks.MoveLeft();
            Assert.AreEqual(19, controlBlocks.X);
        }

        [Test]
        public void MoveDownTest()
        {
            var controlBlocks = new ControlBlocks(4, 14, MockBlocks);
            controlBlocks.MoveDown();
            Assert.AreEqual(13, controlBlocks.Y);
        }

        [Test]
        public void SpinTest()
        {
            var mockBlocks = new Mock<IBlocks>();
            var mockBlocks2 = new Mock<IBlocks>().Object;
            var mockBlocks3 = new Mock<IBlocks>().Object;
            mockBlocks.Setup(m => m.LeftSpin()).Returns(mockBlocks2);
            mockBlocks.Setup(m => m.RightSpin()).Returns(mockBlocks3);

            var controlBlocks = new ControlBlocks(5, 15, mockBlocks.Object);
            controlBlocks.LeftSpin();
            Assert.AreEqual(mockBlocks2, controlBlocks.Blocks);

            var controlBlocks2 = new ControlBlocks(5, 15, mockBlocks.Object);
            controlBlocks2.RightSpin();
            Assert.AreEqual(mockBlocks3, controlBlocks2.Blocks);
        }

        [Test]
        public void GetBoardPositionBlockList()
        {
            var mockBlocks = new Mock<IBlocks>();
            var blockList = new List<IBlock>
            {
                new Block(5, -2),
                new Block(-3, 0)
            };
            mockBlocks.Setup(m => m.BlockList).Returns(blockList);

            var controlBlocks = new ControlBlocks(5, 10, mockBlocks.Object);
            var boardPositionBlockList = controlBlocks.GetBoardPositionBlockList();

            Assert.AreEqual(10, boardPositionBlockList[0].X);
            Assert.AreEqual(8, boardPositionBlockList[0].Y);
            Assert.AreEqual(2, boardPositionBlockList[1].X);
            Assert.AreEqual(10, boardPositionBlockList[1].Y);
        }
    }
}
