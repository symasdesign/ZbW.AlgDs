using System;
using System.Collections.Generic;

namespace SortComparison {

    public class BinaryTreeSort : SortAlgorithm {
        public override string Name => "BinaryTreeSort";


        public override void Sort(IList<int> arrayToSort) {
            var bt = new BinaryTree((x) => {
                var idx = arrayToSort.IndexOf(x);
                this.HighlightIndex(idx);
            });

            foreach (int arrayItem in arrayToSort) {
                bt.AddNode(arrayItem);
            }

            var ordered = bt.InOrderTraverseTree();
            for (var i = 0; i < ordered.Count; i++) {
                arrayToSort[i] = ordered[i];
            }

        }
    }


    public class BinaryTree {
        private readonly Action<int> highlighter;

        public BinaryTree(Action<int> highlighter) {
            this.highlighter = highlighter;
        }

        private Node root;

        public void AddNode(int value) {
            var newNode = new Node(value);

            if (root == null) {
                root = newNode;
            } else {
                var focusNode = root;

                while (true) {
                    var parent = focusNode;
                    this.highlighter(parent.value);

                    if (value < focusNode.value) {
                        focusNode = focusNode.leftChild;

                        if (focusNode == null) {
                            parent.leftChild = newNode;
                            return;
                        }
                    } else {
                        focusNode = focusNode.rightChild;

                        if (focusNode == null) {
                            parent.rightChild = newNode;
                            return;
                        }
                    }
                }
            }
        }

        private List<int> orderedList;

        public List<int> InOrderTraverseTree() {
            this.orderedList = new List<int>();
            this.InOrderTraverseTree(this.root);
            return this.orderedList;
        }

        private void InOrderTraverseTree(Node focusNode) {
            if (focusNode != null) {
                InOrderTraverseTree(focusNode.leftChild);
                this.orderedList.Add(focusNode.value);
                InOrderTraverseTree(focusNode.rightChild);
            }
        }
    }

    public class Node {
        public readonly int value;
        public Node leftChild;
        public Node rightChild;

        public Node(int value) {
            this.value = value;
        }
    }
}



