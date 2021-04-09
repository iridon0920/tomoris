using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
using UniRx;
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

            var mockControlBlocks = new Mock<IControlBlocks>();
            mockControlBlocks.Setup(m => m.GetBoardPositionBlockList()).Returns(
                new List<IBlock>
                {
                    new Block(0, 2),
                    new Block(1, 3)
                }
            );

            Assert.AreEqual(0, board.RxBlocks.Count);

            board.PutBlocks(mockControlBlocks.Object);

            Assert.AreEqual(2, board.RxBlocks.Count);
            Assert.AreEqual(0, board.RxBlocks[0].X);
            Assert.AreEqual(2, board.RxBlocks[0].Y);
            Assert.AreEqual(1, board.RxBlocks[1].X);
            Assert.AreEqual(3, board.RxBlocks[1].Y);
        }

        // [Test]
        // public void AlignBlockEraseTest()
        // {
        //     var board = new Board(8, 20);
        //     // 1つ目のブロックを左下へ
        //     var controlBlocks = new ControlBlocks(2, 0, new Blocks(CreateIBlockList()));
        //     controlBlocks.LeftSpin();
        //     board.PutBlocks(controlBlocks);

        //     // 2つ目のブロックを左下二段目へ
        //     controlBlocks = new ControlBlocks(2, 1, new Blocks(CreateIBlockList()));
        //     controlBlocks.LeftSpin();
        //     board.PutBlocks(controlBlocks);

        //     // 3つ目のブロックを左下三段目へ
        //     controlBlocks = new ControlBlocks(2, 2, new Blocks(CreateIBlockList()));
        //     controlBlocks.LeftSpin();
        //     board.PutBlocks(controlBlocks);

        //     // まだブロックは揃っていない
        //     Assert.IsTrue(board.StatusByPositions[0, 2]);
        //     Assert.IsTrue(board.StatusByPositions[1, 2]);
        //     Assert.IsTrue(board.StatusByPositions[2, 2]);
        //     Assert.IsTrue(board.StatusByPositions[3, 2]);
        //     Assert.IsTrue(board.StatusByPositions[0, 1]);
        //     Assert.IsTrue(board.StatusByPositions[1, 1]);
        //     Assert.IsTrue(board.StatusByPositions[2, 1]);
        //     Assert.IsTrue(board.StatusByPositions[3, 1]);
        //     Assert.IsTrue(board.StatusByPositions[0, 0]);
        //     Assert.IsTrue(board.StatusByPositions[1, 0]);
        //     Assert.IsTrue(board.StatusByPositions[2, 0]);
        //     Assert.IsTrue(board.StatusByPositions[3, 0]);
        //     Assert.IsFalse(board.StatusByPositions[4, 0]);
        //     Assert.IsFalse(board.StatusByPositions[5, 0]);
        //     Assert.IsFalse(board.StatusByPositions[6, 0]);
        //     Assert.IsFalse(board.StatusByPositions[7, 0]);

        //     // 4つ目のブロックを右下へ
        //     controlBlocks = new ControlBlocks(6, 0, new Blocks(CreateIBlockList()));
        //     controlBlocks.LeftSpin();
        //     board.PutBlocks(controlBlocks);

        //     // ブロックが揃ったことで一番下の段が消え、上に積み重なっていたブロックが下に落ちる
        //     Assert.IsFalse(board.StatusByPositions[0, 2]);
        //     Assert.IsFalse(board.StatusByPositions[1, 2]);
        //     Assert.IsFalse(board.StatusByPositions[2, 2]);
        //     Assert.IsFalse(board.StatusByPositions[3, 2]);
        //     Assert.IsTrue(board.StatusByPositions[0, 1]);
        //     Assert.IsTrue(board.StatusByPositions[1, 1]);
        //     Assert.IsTrue(board.StatusByPositions[2, 1]);
        //     Assert.IsTrue(board.StatusByPositions[3, 1]);
        //     Assert.IsTrue(board.StatusByPositions[0, 0]);
        //     Assert.IsTrue(board.StatusByPositions[1, 0]);
        //     Assert.IsTrue(board.StatusByPositions[2, 0]);
        //     Assert.IsTrue(board.StatusByPositions[3, 0]);
        //     Assert.IsFalse(board.StatusByPositions[4, 0]);
        //     Assert.IsFalse(board.StatusByPositions[5, 0]);
        //     Assert.IsFalse(board.StatusByPositions[6, 0]);
        //     Assert.IsFalse(board.StatusByPositions[7, 0]);
        // }


        // I字ブロックのリスト作成
        private List<IBlock> CreateIBlockList()
        {
            return new List<IBlock>
            {
                new Block(0, 2),
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1)
            };
        }

        // L字ブロックのリスト作成
        private List<IBlock> CreateLBlockList()
        {
            return new List<IBlock>
            {
            new Block(0, 1),
            new Block(0, 0),
            new Block(0, -1),
            new Block(1, -1)
            };
        }

    }
}
