using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTreeView.BinaryTree {
    /// <summary>
    /// A binary tree node class - encapsulates the value and left/right pointers.
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    internal class BinaryTreeNode<TNode> : IComparable<TNode>
        where TNode : IComparable<TNode> {
        public BinaryTreeNode(TNode value) {
            Value = value;
        }

        public BinaryTreeNode<TNode> Left { get; set; }
        public BinaryTreeNode<TNode> Right { get; set; }
        public TNode Value { get; private set; }

        /// <summary>
        /// Compares the current node to the provided value
        /// </summary>
        /// <param name="other">The node value to compare to</param>
        /// <returns>1 if the instance value is greater than the provided value, -1 if less or 0 if equal.</returns>
        public int CompareTo(TNode other) {
            return Value.CompareTo(other);
        }
    }

}
