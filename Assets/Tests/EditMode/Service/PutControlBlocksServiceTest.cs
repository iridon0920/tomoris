using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
namespace Tests
{
    public class PutControlBlocksServiceTest
    {
        private PutControlBlocksService Service;

        private Mock<IBoard> MockBoard;
        private IBlocks IBlocks;
        [SetUp]
        public void SetUp()
        {
            MockBoard = new Mock<IBoard>();
            MockBoard.Setup(m => m.Height).Returns(20);

            MockBoard.Setup(m => m.ExistPosition(9, 0)).Returns(true);

            Service = new PutControlBlocksService(MockBoard.Object);

            IBlocks = new Blocks(new List<IBlock>
            {
                new Block(0, 2),
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1)
            });
        }

        [Test]
        public void PutGroundSuccessTest()
        {
            var controlBlocks = new ControlBlocks(5, 0, IBlocks);
            controlBlocks.SetTruePutable();

            Service.Execute(controlBlocks);
            MockBoard.Verify(m => m.PutBlocks(It.IsAny<ControlBlocks>()), Times.Once());
        }

        [Test]
        public void PutGroundFailsTest()
        {
            var controlBlocks = new ControlBlocks(5, 1, IBlocks);
            Service.Execute(controlBlocks);
            MockBoard.Verify(m => m.PutBlocks(It.IsAny<ControlBlocks>()), Times.Never());
        }

        // 空中であっても設置可能判定があれば設置を行う
        [Test]
        public void PutBlocksSuccessTest()
        {
            var controlBlocks = new ControlBlocks(5, 5, IBlocks);
            controlBlocks.SetTruePutable();
            Service.Execute(controlBlocks);
            MockBoard.Verify(m => m.PutBlocks(It.IsAny<ControlBlocks>()), Times.Once());
        }
    }
}