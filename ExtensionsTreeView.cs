using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace System
{
	/// <summary>
	/// Extensions
	/// </summary>
    public static partial class Extensions
    {

		/// <summary>
		/// Get All nodes
		/// </summary>
		/// <param name="treeNode"></param>
		/// <returns></returns>
		public static List<TreeNode> allNodes (this TreeNode treeNode)
		{
			List<TreeNode>	result	= new List<TreeNode> ();
			List<TreeNode>	queue	= new List<TreeNode> ();

			if (null != treeNode)
			{
				// Add to queue
				queue.Add (treeNode);
				
				while (queue.Count > 0)
				{
					TreeNode		node;
					List<TreeNode>	childNodes;

					// Get first node
					node	= queue[0];

					// Add to result
					result.Add (node);

					// Add childs
					if (0 < node.Nodes.Count)
					{
						childNodes	= new List<TreeNode> ();

						foreach (TreeNode child in node.Nodes)
							childNodes.Add (child);

						queue.InsertRange (1, childNodes.ToArray ());
					}

					// Remove first node
					queue.Remove (node);
				}
			}

			return result;
		}

		/// <summary>
		/// Get All nodes
		/// </summary>
		/// <param name="treeView"></param>
		/// <returns></returns>
		public static List<TreeNode> allNodes (this TreeView treeView)
		{
			List<TreeNode>	result	= new List<TreeNode> ();
			List<TreeNode>	queue	= new List<TreeNode> ();

			if (null != treeView)
			{
				// Add base nodes
				foreach (TreeNode node in treeView.Nodes)
					queue.Add (node);

				while (queue.Count > 0)
				{
					TreeNode		node;
					List<TreeNode>	childNodes;

					// Get first node
					node	= queue[0];

					// Add to result
					result.Add (node);

					// Add childs
					if (0 < node.Nodes.Count)
					{
						childNodes	= new List<TreeNode> ();

						foreach (TreeNode child in node.Nodes)
							childNodes.Add (child);

						queue.InsertRange (1, childNodes.ToArray ());
					}

					// Remove first node
					queue.Remove (node);
				}
			}

			return result;
		}
	}
}
