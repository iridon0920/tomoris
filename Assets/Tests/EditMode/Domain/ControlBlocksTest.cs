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
        private IBlocks IBlocks;

        [SetUp]
        public void SetUp()
        {
            IBlocks = new IShapedBlocks();
        }

        [Test]
        public void インスタンス生成時にパラメータが適切にセットされるかのテスト()
        {
            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        [Test]
        public void クローンオブジェクト生成メソッドのテスト()
        {
            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            var controlBlocks2 = controlBlocks.Clone();
            controlBlocks2.MoveDown();
            var controlBlocks3 = controlBlocks.Clone();
            controlBlocks3.MoveRight();

            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
            Assert.AreEqual(5, controlBlocks2.X);
            Assert.AreEqual(9, controlBlocks2.Y);
            Assert.AreEqual(6, controlBlocks3.X);
            Assert.AreEqual(10, controlBlocks3.Y);
        }

        [Test]
        public void 右移動テスト()
        {
            var controlBlocks = new ControlBlocks(4, 14, IBlocks);
            controlBlocks.MoveRight();
            Assert.AreEqual(5, controlBlocks.X);
        }

        [Test]
        public void 左移動テスト()
        {
            var controlBlocks = new ControlBlocks(20, 14, IBlocks);
            controlBlocks.MoveLeft();
            Assert.AreEqual(19, controlBlocks.X);
        }

        [Test]
        public void 下移動テスト()
        {
            var controlBlocks = new ControlBlocks(4, 14, IBlocks);
            controlBlocks.MoveDown();
            Assert.AreEqual(13, controlBlocks.Y);
        }

        [Test]
        public void 左回転テストt()
        {
            var controlBlocks = new ControlBlocks(5, 15, IBlocks);
            var IBlocksLeftSpin = IBlocks.LeftSpin();

            controlBlocks.LeftSpin();
            Assert.AreEqual(IBlocksLeftSpin, controlBlocks.Blocks);

            var controlBlocks2 = new ControlBlocks(5, 15, IBlocks);
            var IBlocksRightSpin = IBlocks.RightSpin();
            controlBlocks2.RightSpin();
            Assert.AreEqual(IBlocksRightSpin, controlBlocks2.Blocks);
        }

        [Test]
        public void ControlBlocksの座標と保持しているブロックの座標からボードでの座標を持つブロックリストを生成するテスト()
        {
            var controlBlocks = new ControlBlocks(5, 1, IBlocks);
            var boardPositionBlockList = controlBlocks.GetBoardPositionBlockList();

            Assert.AreEqual(5, boardPositionBlockList[0].X);
            Assert.AreEqual(3, boardPositionBlockList[0].Y);
            Assert.AreEqual(5, boardPositionBlockList[1].X);
            Assert.AreEqual(2, boardPositionBlockList[1].Y);
            Assert.AreEqual(5, boardPositionBlockList[2].X);
            Assert.AreEqual(1, boardPositionBlockList[2].Y);
            Assert.AreEqual(5, boardPositionBlockList[3].X);
            Assert.AreEqual(0, boardPositionBlockList[3].Y);
        }
    }
}
