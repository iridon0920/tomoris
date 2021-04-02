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

            mockBlocks.Setup(m => m.Spin()).Returns(mockBlocks2);
            var controlBlocks = new ControlBlocks(5, 15, mockBlocks.Object);

            controlBlocks.Spin();
            Assert.AreEqual(mockBlocks2, controlBlocks.Blocks);
        }
    }
}
