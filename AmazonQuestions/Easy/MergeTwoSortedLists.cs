using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace AmazonQuestions.Easy
{
    // https://leetcode.com/problems/merge-two-sorted-lists/
    public class MergeTwoSortedLists: TestQuestion<ListNode, ListNode, ListNode>
    {
        public override bool CompareByReference { get; } = false;

        public override (ListNode arg1, ListNode arg2)[] TestCases
            => new[]
            {
                (
                    ListNode.Create(1, 2, 4),
                    ListNode.Create(1, 3, 4)
                ),
                (
                    null,
                    null
                ),
                (
                    null,
                    ListNode.Create(0)
                ),
                (
                    ListNode.Create(2),
                    ListNode.Create(1)
                )
            };

        public override ListNode[] TestAnswers
            => new[]
            {
                ListNode.Create(1, 1, 2, 3, 4, 4),
                null,
                ListNode.Create(0),
                ListNode.Create(1, 2),
            };

        public override ListNode Solution(ListNode l1, ListNode l2)
        {
            if (l1 is null)
                return l2 ?? null;

            if (l2 is null)
                return l1;

            var startNode = l1.val >= l2.val ? l2 : l1;
            var result = startNode;

            l1 = startNode == l1 ? l1.next : l1;
            l2 = startNode == l2 ? l2.next : l2;

            while ((l1, l2) != (null, null))
            {
                ListNode nextNode = null;
                if (l1 == null)
                {
                    nextNode = l2;
                    l2 = l2.next;
                } else if (l2 == null)
                {
                    nextNode = l1;
                    l1 = l1.next;
                } else if (l1.val <= l2.val)
                {
                    nextNode = l1;
                    l1 = l1.next;
                }
                else
                {
                    nextNode = l2;
                    l2 = l2.next;
                }

                result = result.next = nextNode;
            }

            return startNode;
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }

        public static ListNode Create(params int[] values)
        {
            if (values.Length == 0)
                return null;

            var start = new ListNode(values[0]);
            _ = values.Skip(1).Aggregate(start, (prev, next) => prev.next = new ListNode(next));

            return start;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ListNode node)
                return false;

            return this.val == node.val && this.next == node.next;
        }
    }
}