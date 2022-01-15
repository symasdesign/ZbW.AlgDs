using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AVLTreeDemo;
using AVLTreeView.BinaryTree;

namespace AVLTreeView {
    /// <summary>
    /// Uses "Microsoft Automatic Graph Layout"
    /// https://github.com/Microsoft/automatic-graph-layout 
    /// https://en.wikipedia.org/wiki/Microsoft_Automatic_Graph_Layout 
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        AVLTree<int> _tree = new AVLTree<int>();
        BinaryTree<int> _binaryTree = new BinaryTree<int>();
        int currentCount = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            RenderAvlTree(_tree);
            RenderBinaryTree(_binaryTree);
        }

        private void RenderAvlTree(AVLTree<int> tree) {
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("");

            AVLTreeNode<int> current = tree.Root;

            RenderAvlTree(current, graph);

            Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(graph);
            renderer.CalculateLayout();
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap((int)avlImage.Width, (int)avlImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);

            avlImage.Source = loadBitmap(bitmap);
        }

        private void RenderBinaryTree(BinaryTree<int> tree) {
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("");

            BinaryTreeNode<int> current = tree.Head;

            RenderBinaryTree(null, current, graph);

            Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(graph);
            renderer.CalculateLayout();
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap((int)binaryImage.Width, (int)binaryImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);

            binaryImage.Source = loadBitmap(bitmap);
        }

        private void RenderAvlTree(AVLTreeNode<int> node, Microsoft.Msagl.Drawing.Graph graph) {
            if (node != null) {
                RenderAvlTree(node.Right, graph);

                if (node.Parent != null) {
                    graph.AddEdge(node.Parent.Item.ToString(), node.Item.ToString());
                }

                RenderAvlTree(node.Left, graph);
            }
        }

        private void RenderBinaryTree(BinaryTreeNode<int> parent, BinaryTreeNode<int> child, Microsoft.Msagl.Drawing.Graph graph) {
            if (child != null) {
                RenderBinaryTree(child, child.Right, graph);

                if (parent != null) {
                    graph.AddEdge(parent.Value.ToString(), child.Value.ToString());
                }

                RenderBinaryTree(child, child.Left, graph);
            }
        }


        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        public static BitmapSource loadBitmap(System.Drawing.Bitmap source) {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;
            try {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            } finally {
                DeleteObject(ip);
            }

            return bs;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            string[] values = this.txtAddValue.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string value in values) {
                _binaryTree.Add(int.Parse(value.Trim()));
                _tree.Add(int.Parse(value.Trim()));
            }
            RenderAvlTree(_tree);
            RenderBinaryTree(_binaryTree);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e) {
            string[] values = this.txtRemoveValue.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string value in values) {
                _binaryTree.Remove(int.Parse(value.Trim()));
                _tree.Remove(int.Parse(value.Trim()));
            }

            RenderAvlTree(_tree);
            RenderBinaryTree(_binaryTree);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) {
            _tree.Clear();
            RenderAvlTree(_tree);

            _binaryTree.Clear();
            RenderBinaryTree(_binaryTree);
        }

        private void btnBad100_Click(object sender, RoutedEventArgs e) {
            _tree.Clear();
            _binaryTree.Clear();

            for (int i = 0; i < 20; i++) {
                _tree.Add(i);
                _binaryTree.Add(i);
            }

            RenderAvlTree(_tree);
            RenderBinaryTree(_binaryTree);
        }

        private void btnAdd100_Click(object sender, RoutedEventArgs e) {
            Random rng = new Random();
            for (int i = 0; i < 20; i++) {
                int next = rng.Next();
                while (_tree.Contains(next)) {
                    next = rng.Next();
                }

                _tree.Add(next);
                _binaryTree.Add(next);
            }

            RenderAvlTree(_tree);
            RenderBinaryTree(_binaryTree);
        }
    }
}
