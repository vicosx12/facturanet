using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Facturanet.WinformsClient.Util
{
    public static class TreeNodeCollectionExtensions
    {
        public static IEnumerable<TreeNode> Backtracking(this TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                yield return node;
                foreach (TreeNode subnode in nodes.Backtracking())
                    yield return subnode;
            }
        }
    }
}
