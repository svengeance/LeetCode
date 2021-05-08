using System;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/maximum-average-subtree/submissions/
    public class MaximumAverageSubtree: TestQuestion<MaximumAverageSubtree.TreeNode, double>
    {
        public override TreeNode[] TestCases => new[]
        {
            new TreeNode(5, new(6), new(1)),
            new TreeNode(5),
            new TreeNode(13, new(1, new(2), new(1, new(1, new(2)))), new(10, new(6)))
        };

        public override double[] TestAnswers => new[] { 6.0d, 5.0d, 8.0d };

        (int, int) SumValueAndCountNodes(TreeNode tree, int totalValues, int totalCount)
        {
            if (tree == null)
                return (totalValues, totalCount);

            var (sumValues, sumCount) = (totalValues + tree.val, totalCount + 1);

            if (tree.left != null)
                (sumValues, sumCount) = SumValueAndCountNodes(tree.left, sumValues, sumCount);

            if (tree.right != null)
                (sumValues, sumCount) = SumValueAndCountNodes(tree.right, sumValues, sumCount);

            return (sumValues, sumCount);
        }

        public override double Solution(TreeNode root)
        {
            if (root == null)
                return 0d;

            var (values, nodes) = SumValueAndCountNodes(root, 0, 0);

            var average = (double) values / nodes;
            if (root.left != null)
                average = Math.Max(average, Solution(root.left));

            if (root.right!= null)
                average = Math.Max(average, Solution(root.right));

            return average;
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
    }
}