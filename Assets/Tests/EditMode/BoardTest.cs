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

        // [Test]
        // public void BlocksPutTest()
        // {


        //     // ブロックが一度右移動後一番下に着地するように置かれる。
        //     Assert.IsTrue(board.MoveBlocksRight());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());

        //     // まだブロックは置かれていない。
        //     Assert.IsFalse(board.StatusByPositions[5, 3]);
        //     Assert.IsFalse(board.StatusByPositions[5, 2]);
        //     Assert.IsFalse(board.StatusByPositions[5, 1]);
        //     Assert.IsFalse(board.StatusByPositions[5, 0]);

        //     // 接地するとFalseが返る。
        //     Assert.IsFalse(board.MoveBlocksDown());

        //     // I字ブロックがゲーム盤に適用されている。
        //     Assert.IsTrue(board.StatusByPositions[5, 3]);
        //     Assert.IsTrue(board.StatusByPositions[5, 2]);
        //     Assert.IsTrue(board.StatusByPositions[5, 1]);
        //     Assert.IsTrue(board.StatusByPositions[5, 0]);

        //     // CurrentBlocks関係のプロパティが初期化されている
        //     Assert.IsNull(board.CurrentBlocks);
        //     Assert.AreEqual(4, board.CurrentBlocksPositionX);
        //     Assert.AreEqual(9, board.CurrentBlocksPositionY);

        //     // 2つ目のブロック投入
        //     IBlocks = new Blocks(CreateIBlockList());
        //     board.InsertBlocks(IBlocks);

        //     // 1つ目に置いたブロックの上に置くようにブロックを動かす
        //     Assert.IsTrue(board.MoveBlocksRight());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     // 既に存在するブロックの上に置こうとするとFalseが返る。
        //     Assert.IsFalse(board.MoveBlocksDown());

        //     // 1個目の上にI字ブロックが新たにゲーム盤に適用されている。
        //     Assert.IsTrue(board.StatusByPositions[5, 7]);
        //     Assert.IsTrue(board.StatusByPositions[5, 6]);
        //     Assert.IsTrue(board.StatusByPositions[5, 5]);
        //     Assert.IsTrue(board.StatusByPositions[5, 4]);

        //     // CurrentBlocks関係のプロパティが初期化されている
        //     Assert.IsNull(board.CurrentBlocks);
        //     Assert.AreEqual(4, board.CurrentBlocksPositionX);
        //     Assert.AreEqual(9, board.CurrentBlocksPositionY);
        // }

        // [Test]
        // public void AlignBlockEraseTest()
        // {
        //     var board = new Board(8, 3);
        //     var IBlocks = new Blocks(CreateIBlockList());

        //     // 1つ目のブロックを左下へ
        //     board.InsertBlocks(IBlocks);
        //     Assert.IsTrue(board.SpinBlocks());
        //     Assert.IsTrue(board.MoveBlocksLeft());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsFalse(board.MoveBlocksDown());

        //     // 2つ目のブロックを左下二段目へ
        //     board.InsertBlocks(IBlocks);
        //     Assert.IsTrue(board.SpinBlocks());
        //     Assert.IsTrue(board.MoveBlocksLeft());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsFalse(board.MoveBlocksDown());

        //     // 3つ目のブロックを左下三段目へ
        //     board.InsertBlocks(IBlocks);
        //     Assert.IsTrue(board.SpinBlocks());
        //     Assert.IsTrue(board.MoveBlocksLeft());
        //     Assert.IsFalse(board.MoveBlocksDown());

        //     // 4つ目のブロックを右下へ
        //     board.InsertBlocks(IBlocks);
        //     Assert.IsTrue(board.MoveBlocksRight());
        //     Assert.IsTrue(board.MoveBlocksRight());
        //     Assert.IsTrue(board.MoveBlocksRight());
        //     Assert.IsTrue(board.SpinBlocks());
        //     Assert.IsTrue(board.MoveBlocksDown());
        //     Assert.IsTrue(board.MoveBlocksDown());

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

        //     Assert.IsFalse(board.MoveBlocksDown());

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
