namespace AmazonQuestions.Easy
{
    // https://leetcode.com/problems/subtree-of-another-tree/
    public class IsSubtree: TestQuestion<TreeNode, TreeNode, bool>
    {
        public override (TreeNode arg1, TreeNode arg2)[] TestCases => new[]
        {
            (
                new TreeNode(3, new(4, new(1), new(2)), new(5)),
                new TreeNode(4, new(1), new(2))
            ),
            (
                new TreeNode(3, new(4, new(1), new(2, new(0))), new(5)),
                new TreeNode(4, new(1), new(2))
            ),
            (
                new TreeNode(1, new(1), null),
                new TreeNode(1)
            )
        };

        public override bool[] TestAnswers => new[] { true, false, true };

        static bool AreNodesEqual(TreeNode left, TreeNode right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.val == right.val && AreNodesEqual(left.left, right.left) && AreNodesEqual(left.right, right.right);
        }

        public override bool Solution(TreeNode root, TreeNode subRoot)
        {
            if (root == null)
                return false;

            if (subRoot == null)
                return true;

            if (AreNodesEqual(root, subRoot))
                return true;

            return Solution(root.left, subRoot) || Solution(root.right, subRoot);
        }
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