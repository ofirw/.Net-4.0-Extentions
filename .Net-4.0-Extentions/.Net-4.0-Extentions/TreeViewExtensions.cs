
namespace Net_4._0_Extentions
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    #endregion

    public static class TreeViewExtensions
    {
        public static IEnumerable<TreeNode> Descendants(this TreeNode root)
        {
            var nodes = new Queue<TreeNode>();
            foreach (TreeNode n in root.Nodes)
            {
                nodes.Enqueue(n);
            }

            while (nodes.Any())
            {
                var node = nodes.Dequeue();
                yield return node;

                foreach (TreeNode n in node.Nodes)
                {
                    nodes.Enqueue(n);
                }
            }
        }
    }
}