using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UniRx;
namespace Tests
{

    public class BoardTest
    {
        private IBoard Board;
        private IBlocks LBlocks;
        private IBlocks IBlocks;


        [SetUp]
        public void SetUp()
        {
            Board = new Board(10, 20);

            IBlocks = new Blocks(new List<IBlock>
            {
                new Block(0, 2),
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1)
            });

            LBlocks = new Blocks(new List<IBlock>
            {
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1),
                new Block(1, -1)
            });
        }

        [Test]
        public void CreateInstanceTest()
        {
            Assert.AreEqual(4, Board.InsertPositionX);
        }

        [Test]
        public void PutBlocksTest()
        {
            var controlBlocks = new ControlBlocks(0, 1, LBlocks);

            Assert.AreEqual(0, Board.RxBlocks.Count);

            Board.PutBlocks(controlBlocks);

            Assert.AreEqual(4, Board.RxBlocks.Count);
            Assert.AreEqual(0, Board.RxBlocks[0].X);
            Assert.AreEqual(2, Board.RxBlocks[0].Y);
            Assert.AreEqual(0, Board.RxBlocks[1].X);
            Assert.AreEqual(1, Board.RxBlocks[1].Y);
            Assert.AreEqual(0, Board.RxBlocks[2].X);
            Assert.AreEqual(0, Board.RxBlocks[2].Y);
            Assert.AreEqual(1, Board.RxBlocks[3].X);
            Assert.AreEqual(0, Board.RxBlocks[3].Y);
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
    }
}
