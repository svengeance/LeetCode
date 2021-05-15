using System.Collections.Generic;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/copy-list-with-random-pointer
    public class CopyRandomList
    {
        public class Node
        {
            public int val;
            public Node next;
            public Node random;

            public Node(int _val)
            {
                val = _val;
                next = null;
                random = null;
            }
        }

        public Node Solution(Node head)
        {
            if (head == null)
                return null;

            var nodes = new Dictionary<Node, Node>();
            void CopyRandom(Node newer, Node older)
            {
                if (older.random == null)
                    return;

                if (nodes.TryGetValue(older.random, out var newCandidate))
                {
                    newer.random = newCandidate;
                }
                else
                {
                    var newNode = new Node(older.random.val);
                    nodes[older.random] = newNode;
                    newer.random = newNode;
                }
            }

            var newHead = new Node(head.val);
            nodes[head] = newHead;
            CopyRandom(newHead, head);

            var newCurrent = newHead;
            var oldCurrent = head;
            while ((oldCurrent = oldCurrent.next) != null)
            {
                if (nodes.TryGetValue(oldCurrent, out var newer))
                {
                    newCurrent.next = newer;
                }
                else
                {
                    var newNode = new Node(oldCurrent.val);
                    nodes[oldCurrent] = newNode;
                    newCurrent.next = newNode;
                }

                newCurrent = newCurrent.next;

                CopyRandom(newCurrent, oldCurrent);
            }

            return newHead;
        }
    }
}