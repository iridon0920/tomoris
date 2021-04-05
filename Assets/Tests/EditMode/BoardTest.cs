using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
namespace Tests
{

    public class BoardTest
    {
        private Mock<IBlocks> MockLBlocks;
        private Mock<IBlocks> MockIBlocks;


        [SetUp]
        public void SetUp()
        {
            MockLBlocks = new Mock<IBlocks>();
            MockLBlocks.Setup(m => m.BlockList).Returns(
                CreateLBlockList()
            );

            MockIBlocks = new Mock<IBlocks>();
            MockIBlocks.Setup(m => m.BlockList).Returns(
                CreateIBlockList()
            );
        }

        [Test]

        public void CreateInstanceTest()
        {
            var board = new Board(10, 20);
            Assert.AreEqual(4, board.InsertPositionX);
        }

        [Test]

        public void PutBlocksTest()
        {
            var board = new Board(10, 20);
            var controlBlocks = new ControlBlocks(1, 4, 1, MockIBlocks.Object);

            Assert.IsFalse(board.StatusByPositions[4, 4]);
            Assert.IsFalse(board.StatusByPositions[4, 3]);
            Assert.IsFalse(board.StatusByPositions[4, 2]);
            Assert.IsFalse(board.StatusByPositions[4, 1]);
            Assert.IsFalse(board.StatusByPositions[4, 0]);

            board.PutBlocks(controlBlocks);

            Assert.IsFalse(board.StatusByPositions[4, 4]);
            Assert.IsTrue(board.StatusByPositions[4, 3]);
            Assert.IsTrue(board.StatusByPositions[4, 2]);
            Assert.IsTrue(board.StatusByPositions[4, 1]);
            Assert.IsTrue(board.StatusByPositions[4, 0]);
        }

        [Test]
        public void AlignBlockEraseTest()
        {
            var board = new Board(8, 20);
            // 1つ目のブロックを左下へ
            var controlBlocks = new ControlBlocks(1, 2, 0, new Blocks(CreateIBlockList()));
            controlBlocks.Spin();
            board.PutBlocks(controlBlocks);

            // 2つ目のブロックを左下二段目へ
            controlBlocks.Initialization(2, 1, new Blocks(CreateIBlockList()));
            controlBlocks.Spin();
            board.PutBlocks(controlBlocks);

            // 3つ目のブロックを左下三段目へ
            controlBlocks.Initialization(2, 2, new Blocks(CreateIBlockList()));
            controlBlocks.Spin();
            board.PutBlocks(controlBlocks);

            // まだブロックは揃っていない
            Assert.IsTrue(board.StatusByPositions[0, 2]);
            Assert.IsTrue(board.StatusByPositions[1, 2]);
            Assert.IsTrue(board.StatusByPositions[2, 2]);
            Assert.IsTrue(board.StatusByPositions[3, 2]);
            Assert.IsTrue(board.StatusByPositions[0, 1]);
            Assert.IsTrue(board.StatusByPositions[1, 1]);
            Assert.IsTrue(board.StatusByPositions[2, 1]);
            Assert.IsTrue(board.StatusByPositions[3, 1]);
            Assert.IsTrue(board.StatusByPositions[0, 0]);
            Assert.IsTrue(board.StatusByPositions[1, 0]);
            Assert.IsTrue(board.StatusByPositions[2, 0]);
            Assert.IsTrue(board.StatusByPositions[3, 0]);
            Assert.IsFalse(board.StatusByPositions[4, 0]);
            Assert.IsFalse(board.StatusByPositions[5, 0]);
            Assert.IsFalse(board.StatusByPositions[6, 0]);
            Assert.IsFalse(board.StatusByPositions[7, 0]);

            // 4つ目のブロックを右下へ
            controlBlocks.Initialization(6, 0, new Blocks(CreateIBlockList()));
            controlBlocks.Spin();
            board.PutBlocks(controlBlocks);

            // ブロックが揃ったことで一番下の段が消え、上に積み重なっていたブロックが下に落ちる
            Assert.IsFalse(board.StatusByPositions[0, 2]);
            Assert.IsFalse(board.StatusByPositions[1, 2]);
            Assert.IsFalse(board.StatusByPositions[2, 2]);
            Assert.IsFalse(board.StatusByPositions[3, 2]);
            Assert.IsTrue(board.StatusByPositions[0, 1]);
            Assert.IsTrue(board.StatusByPositions[1, 1]);
            Assert.IsTrue(board.StatusByPositions[2, 1]);
            Assert.IsTrue(board.StatusByPositions[3, 1]);
            Assert.IsTrue(board.StatusByPositions[0, 0]);
            Assert.IsTrue(board.StatusByPositions[1, 0]);
            Assert.IsTrue(board.StatusByPositions[2, 0]);
            Assert.IsTrue(board.StatusByPositions[3, 0]);
            Assert.IsFalse(board.StatusByPositions[4, 0]);
            Assert.IsFalse(board.StatusByPositions[5, 0]);
            Assert.IsFalse(board.StatusByPositions[6, 0]);
            Assert.IsFalse(board.StatusByPositions[7, 0]);
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
