using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests
{
    public class ControlBlocksQueueTest
    {
        [Test]
        public void CreateQueueTest()
        {
            var Queue = new ControlBlocksQueue(4);
            var BlocksArray = Queue.BlocksQueue.ToArray();
            Assert.AreEqual(4, BlocksArray.Length);
            Assert.IsTrue(BlocksArray[0].Equals(CreateIControlBlocks()));
            Assert.IsTrue(BlocksArray[1].Equals(CreateLControlBlocks()));
            Assert.IsTrue(BlocksArray[2].Equals(CreateIControlBlocks()));
            Assert.IsTrue(BlocksArray[3].Equals(CreateLControlBlocks()));
        }

        [Test]
        public void DequeueTest()
        {
            var Queue = new ControlBlocksQueue(4);
            var DequeueBlocks = Queue.Dequeue();
            Assert.IsTrue(DequeueBlocks.Equals(CreateIControlBlocks()));

            var BlocksArray = Queue.BlocksQueue.ToArray();
            Assert.AreEqual(4, BlocksArray.Length);
            Assert.IsTrue(BlocksArray[0].Equals(CreateLControlBlocks()));
            Assert.IsTrue(BlocksArray[1].Equals(CreateIControlBlocks()));
            Assert.IsTrue(BlocksArray[2].Equals(CreateLControlBlocks()));
            Assert.IsTrue(BlocksArray[3].Equals(CreateIControlBlocks()));

            DequeueBlocks = Queue.Dequeue();
            Assert.IsTrue(DequeueBlocks.Equals(CreateLControlBlocks()));

            BlocksArray = Queue.BlocksQueue.ToArray();
            Assert.AreEqual(4, BlocksArray.Length);
            Assert.IsTrue(BlocksArray[0].Equals(CreateIControlBlocks()));
            Assert.IsTrue(BlocksArray[1].Equals(CreateLControlBlocks()));
            Assert.IsTrue(BlocksArray[2].Equals(CreateIControlBlocks()));
            Assert.IsTrue(BlocksArray[3].Equals(CreateLControlBlocks()));
        }
        private ControlBlocks CreateIControlBlocks()
        {
            return new ControlBlocks(
                new List<ControlBlock>
                {
                    new ControlBlock(0, 2),
                    new ControlBlock(0, 1),
                    new ControlBlock(0, 0),
                    new ControlBlock(0, -1)
                }
            );
        }

        private ControlBlocks CreateLControlBlocks()
        {
            return new ControlBlocks(
                new List<ControlBlock>
                {
                    new ControlBlock(0, 1),
                    new ControlBlock(0, 0),
                    new ControlBlock(0, -1),
                    new ControlBlock(1, -1)
                }
            );
        }
    }


}
