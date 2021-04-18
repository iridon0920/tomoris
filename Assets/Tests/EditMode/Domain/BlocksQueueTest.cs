using System;
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
            var Queue = new BlocksQueue(4, new BlocksFactory(new System.Random()));
            var BlocksArray = Queue.Queue.ToArray();
            Assert.AreEqual(4, BlocksArray.Length);
            Assert.IsTrue(BlocksArray[0] is Blocks);
            Assert.IsTrue(BlocksArray[1] is Blocks);
            Assert.IsTrue(BlocksArray[2] is Blocks);
            Assert.IsTrue(BlocksArray[3] is Blocks);
        }

        [Test]
        public void DequeueTest()
        {
            var Queue = new BlocksQueue(4, new BlocksFactory(new System.Random()));
            Assert.AreEqual(4, Queue.Queue.ToArray().Length);

            var DequeueBlocks = Queue.Dequeue();
            Assert.IsTrue(DequeueBlocks is Blocks);
            Assert.AreEqual(4, Queue.Queue.ToArray().Length);
        }
    }
}
