using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeDemo {
    class Program {
        static void Main(string[] args) {

            //WordSorter();

            var tree = new BinaryTree<int>();

            //tree.AddRange(new int[] { 5, 3, 8, 2, 4, 7, 9 });
            //tree.AddRange(new int[] { 4, 2, 1, 3, 6, 5, 7 });
            //tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7 });
            //tree.AddRange(new int[] { 110, 130, 135, 140, 150, 160 });
            tree.AddRange(new int[] { 4,2,6,1,3,7,8});

            Console.WriteLine(tree);
            tree.Remove(6);
            Console.WriteLine(tree);

            Console.WriteLine(tree.Contains(10));
            //Console.WriteLine(tree.Contains(11));
            Console.WriteLine(tree.Contains(2));
            //Console.WriteLine(tree.Contains(0));

            //Console.WriteLine(tree);

            tree.TraverseMode = TraverseModeEnum.PreOrder;
            Console.WriteLine(tree);

            tree.TraverseMode = TraverseModeEnum.PostOrder;
            Console.WriteLine(tree);

            tree.TraverseMode = TraverseModeEnum.InOrder;
            Console.WriteLine(tree);

            tree.TraverseMode = TraverseModeEnum.ReverseInOrder;
            Console.WriteLine(tree);

            tree.Remove(3);
            Console.WriteLine(tree);

            // *** Remove Szenario 1
            tree.Clear();
            tree.AddRange(new int[] { 4, 2, 1, 3, 8, 6, 5, 7 });
            Console.WriteLine(tree);
            tree.Remove(8);
            Console.WriteLine(tree);

            // *** Remove Szenario 2
            tree.Clear();
            tree.AddRange(new int[] { 4, 2, 1, 3, 6, 7, 8 });
            Console.WriteLine(tree);
            tree.Remove(6);
            Console.WriteLine(tree);

            // *** Remove Szenario 3
            tree.Clear();
            //tree.AddRange(new int[] { 4, 2, 1, 3, 6, 5, 8, 7 });
            tree.AddRange(new int[] { 4, 2, 1, 3, 6, 5, 7, 8 });
            Console.WriteLine(tree);
            tree.Remove(6);
            Console.WriteLine(tree);

            Console.ReadLine();
        }

        static void WordSorter() {
            BinaryTree<string> tree = new BinaryTree<string>();

            string input = string.Empty;

            while (!input.Equals("quit", StringComparison.CurrentCultureIgnoreCase)) {
                // read the line from the user
                Console.Write("> ");
                input = Console.ReadLine();

                // split the line into words (on space)
                string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // add each word to the tree
                foreach (string word in words) {
                    tree.Add(word);
                }

                // print the number of words
                Console.WriteLine("{0} words", tree.Count);

                tree.TraverseMode = TraverseModeEnum.InOrder;
                Console.WriteLine(tree);

                // and print each word using the default (in-order) enumerator
                //foreach (string word in tree) {
                //    Console.Write("{0} ", word);
                //}

                Console.WriteLine();

                tree.Clear();
            }

        }
    }
}
