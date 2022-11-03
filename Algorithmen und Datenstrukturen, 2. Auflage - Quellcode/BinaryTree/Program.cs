using System;
using My.Collections;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new BinaryTree<string, int>();

            tree.Add("Hans", 20);
            tree.Add("Berta", 31);
            tree.Add("Andreas", 18);
            tree.Add("Kurt", 55);
            tree.Add("Nele", 35);
            tree.Add("Ken", 16);
            tree.Add("Marie", 8);
            tree.Add("Paul", 6);
            tree.Add("Peter", 66);
            tree.Add("Jo", 41);
            tree.Add("Zia", 64);

            Console.WriteLine(tree.Contains("Zia"));
            Console.WriteLine(tree.Contains("Coco"));

            if(tree.Contains("Marie"))
                Console.WriteLine(tree["Marie"]);

            Console.WriteLine(tree);

            tree.TraverseMode = TraverseModeEnum.PreOrder;
            Console.WriteLine(tree);

            tree.TraverseMode = TraverseModeEnum.PostOrder;
            Console.WriteLine(tree);

            tree.TraverseMode = TraverseModeEnum.InOrder;
            Console.WriteLine(tree);

            tree.TraverseMode = TraverseModeEnum.ReverseInOrder;
            Console.WriteLine(tree);

            // 4. Aufgabe
            tree.Clear();
            tree.AddIterative("Hans", 20);
            tree.AddIterative("Berta", 31);
            tree.AddIterative("Andreas", 18);
            tree.AddIterative("Kurt", 55);
            tree.AddIterative("Nele", 35);
            tree.AddIterative("Ken", 16);
            tree.AddIterative("Marie", 8);
            tree.AddIterative("Paul", 6);
            tree.AddIterative("Peter", 66);
            tree.AddIterative("Jo", 41);
            tree.AddIterative("Zia", 64);

            tree.TraverseMode = TraverseModeEnum.PreOrder;
            Console.WriteLine(tree);

            // 5. Aufgabe
            Console.WriteLine(tree.Min);
            Console.WriteLine(tree.Max);
        }
    }
}
