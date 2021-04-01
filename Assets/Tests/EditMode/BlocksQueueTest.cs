using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests
{
    public class BlocksQueueTest
    {
        [Test]
        public void CreateQueueTest()
        {
            var Queue = new BlocksQueue(4);
            var BlocksArray = Queue.Queue.ToArray();
            Assert.AreEqual(4, BlocksArray.Length);
            Assert.IsTrue(BlocksArray[0].Equals(CreateIBlocks()));
            Assert.IsTrue(BlocksArray[1].Equals(CreateLBlocks()));
            Assert.IsTrue(BlocksArray[2].Equals(CreateIBlocks()));
            Assert.IsTrue(BlocksArray[3].Equals(CreateLBlocks()));
        }

        [Test]
        public void DequeueTest()
        {
            var Queue = new BlocksQueue(4);
            var DequeueBlocks = Queue.Dequeue();
            Assert.IsTrue(DequeueBlocks.Equals(CreateIBlocks()));

            var BlocksArray = Queue.Queue.ToArray();
            Assert.AreEqual(4, BlocksArray.Length);
            Assert.IsTrue(BlocksArray[0].Equals(CreateLBlocks()));
            Assert.IsTrue(BlocksArray[1].Equals(CreateIBlocks()));
            Assert.IsTrue(BlocksArray[2].Equals(CreateLBlocks()));
            Assert.IsTrue(BlocksArray[3].Equals(CreateIBlocks()));

            DequeueBlocks = Queue.Dequeue();
            Assert.IsTrue(DequeueBlocks.Equals(CreateLBlocks()));

            BlocksArray = Queue.Queue.ToArray();
            Assert.AreEqual(4, BlocksArray.Length);
            Assert.IsTrue(BlocksArray[0].Equals(CreateIBlocks()));
            Assert.IsTrue(BlocksArray[1].Equals(CreateLBlocks()));
            Assert.IsTrue(BlocksArray[2].Equals(CreateIBlocks()));
            Assert.IsTrue(BlocksArray[3].Equals(CreateLBlocks()));
        }
        private Blocks CreateIBlocks()
        {
            return new Blocks(
                new List<Block>
                {
                    new Block(0, 2),
                    new Block(0, 1),
                    new Block(0, 0),
                    new Block(0, -1)
                }
            );
        }

        private Blocks CreateLBlocks()
        {
            return new Blocks(
                new List<Block>
                {
                    new Block(0, 1),
                    new Block(0, 0),
                    new Block(0, -1),
                    new Block(1, -1)
                }
            );
        }
    }


}
