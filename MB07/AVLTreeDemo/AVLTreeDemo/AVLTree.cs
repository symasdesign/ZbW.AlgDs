using System;


namespace AVLTreeDemo {
    public enum TraverseModeEnum {
        PreOrder,
        PostOrder,
        InOrder,
        ReverseInOrder
    }

    public class AVLTree<T> where T : IComparable<T> {
        internal AVLTreeNode<T> root;

        public AVLTreeNode<T> Root => this.root;

        public int Count { get; private set; }
        public TraverseModeEnum TraverseMode { get; set; }

        public AVLTree() {
            TraverseMode = TraverseModeEnum.PreOrder;
        }

        public void Add(T item) {
            if (root == null)
                root = new AVLTreeNode<T>(item, null, this);
            else
                AddTo(root, item);

            Count++;
        }

        public void AddRange(T[] items) {
            foreach (var item in items)
                Add(item);
        }

        private void AddTo(AVLTreeNode<T> node, T item) {
            if (item.CompareTo(node.Item) < 0) {
                if (node.Left == null) {
                    node.Left = new AVLTreeNode<T>(item, node, this);
                } else {
                    AddTo(node.Left, item);
                }
            } else {
                if (node.Right == null) {
                    node.Right = new AVLTreeNode<T>(item, node, this);
                } else {
                    AddTo(node.Right, item);
                }
            }
            // *** AddTo will recusivly be called. So, the node.Balance() will also be recusivly called to the root.
            node.Balance();
        }

        public bool Contains(T item) {
            // defer to the node search function.
            return Search(item) != null;
        }

        public AVLTreeNode<T> Search(T item) {
            var node = root;

            while (node != null) {
                var c = item.CompareTo(node.Item);

                if (c == 0) {
                    return node;
                }

                if (c < 0) {
                    node = node.Left;
                } else {
                    node = node.Right;
                }
            }
            return null;
        }

        
        public void Clear() {
            root = null;
            Count = 0;
        }

        public override string ToString() {
            var s = "";
            var level = 0;

            Traverse(root, level, ref s, null);

            return s;
        }

        private void Traverse(AVLTreeNode<T> node, int level, ref string s, Action<T> action) {
            if (node == null) {
                return;
            }


            var reverse = TraverseMode == TraverseModeEnum.ReverseInOrder;

            if (TraverseMode == TraverseModeEnum.PreOrder) {
                s += "".PadLeft(level, ' ') + node.Item + "\n";
                action?.Invoke(node.Item);
            }
            Traverse(reverse ? node.Right : node.Left, level + 2, ref s, action);

            if (TraverseMode == TraverseModeEnum.InOrder || TraverseMode == TraverseModeEnum.ReverseInOrder) {
                s += "".PadLeft(level, ' ') + node.Item + "\n";
                action?.Invoke(node.Item);
            }
            Traverse(reverse ? node.Left : node.Right, level + 2, ref s, action);

            if (TraverseMode == TraverseModeEnum.PostOrder) {
                s += "".PadLeft(level, ' ') + node.Item + "\n";
                action?.Invoke(node.Item);
            }
        }

        public void Traverse(TraverseModeEnum mode, Action<T> action) {
            var currentMode = TraverseMode;
            TraverseMode = mode;
            var s = "";
            var level = 0;
            Traverse(root, level, ref s, action);
            TraverseMode = currentMode;
        }

        #region Remove
        /// <summary>
        /// Removes the first occurance of the specified value from the tree.
        /// </summary>
        /// <param name="value">The value to remove</param>
        /// <returns>True if the value was removed, false otherwise</returns>
        public bool Remove(T value) {
            var current = this.Search(value);

            if (current == null) {
                return false;
            }

            var treeToBalance = current.Parent;

            Count--;

            // not leaf?
            if (current.Right != null || current.Left != null) {
                // Case 1: If current has no right child, then current's left replaces current
                if (current.Right == null) {
                    if (current.Parent == null) {
                        root = current.Left;
                        if (root != null) {
                            root.Parent = null;
                        }
                    } else {
                        // Determine, if the current node (which will be removed) is left or right of its parent
                        int result = current.Item.CompareTo(current.Parent.Item);
                        if (result < 0) {
                            // if parent value is less than current value
                            // make the current left child a left child of parent
                            current.Parent.Left = current.Left;
                            treeToBalance = current.Left;
                        } else if (result > 0) {
                            // if parent value is greater than current value
                            // make the current left child a right child of parent
                            current.Parent.Right = current.Left;
                            treeToBalance = current.Left;
                        }
                    }
                }
                // Case 2: If current has no left child, then current's right replaces current
                else if (current.Left == null) {
                    if (current.Parent == null) {
                        root = current.Right;
                        if (root != null) {
                            root.Parent = null;
                        }
                    } else {
                        // Determine, if the current node (which will be removed) is left or right of its parent
                        var result = current.Item.CompareTo(current.Parent.Item);
                        if (result < 0) {
                            // if parent value is less than current value
                            // make the current right child a left child of parent
                            current.Parent.Left = current.Right;
                            treeToBalance = current.Right;
                        } else if (result > 0) {
                            // if parent value is greater than current value
                            // make the current right child a right child of parent
                            current.Parent.Right = current.Right;
                            treeToBalance = current.Right;
                        }
                    }
                }
                // Case 3: If current's right child has a left child, replace current with current's
                //         right child's left-most child
                else {
                    // find the right's left-most child
                    AVLTreeNode<T> leftmost = current.Right.Left;

                    if (leftmost == null) {
                        leftmost = current.Right;
                        // assign leftmost's left and right to current's left and right children
                        leftmost.Left = current.Left;
                    }
                    else {
                        while (leftmost.Left != null) {
                            leftmost = leftmost.Left;
                        }

                        // the parent's left subtree becomes the leftmost's right subtree
                        leftmost.Parent.Left = leftmost.Right;

                        // assign leftmost's left and right to current's left and right children
                        leftmost.Left = current.Left;

                        leftmost.Right = current.Right;
                    }

                    if (current.Parent == null) {
                        root = leftmost;
                        if (root != null) {
                            root.Parent = null;
                        }
                    } else {
                        // Determine, if the current node (which will be removed) is left or right of its parent
                        int result = current.Item.CompareTo(current.Parent.Item);
                        if (result < 0) {
                            // if parent value is less than current value
                            // make leftmost the parent's left child
                            current.Parent.Left = leftmost;
                            treeToBalance = leftmost;
                        } else if (result > 0) {
                            // if parent value is greater than current value
                            // make leftmost the parent's right child
                            current.Parent.Right = leftmost;
                            treeToBalance = leftmost;

                        }
                    }
                }
            }

            if (treeToBalance != null) {
                // Belancing is going to the top
                while (treeToBalance != null) {
                    var next = treeToBalance.Parent;
                    treeToBalance.Balance();
                    treeToBalance = next;
                }
            } else {
                this.root?.Balance();
            }

            return true;
        }
        #endregion
    }

}
