using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using HashTable;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WordCount {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        HashTable<string, int> _wordCounts = new HashTable<string, int>();

        public ObservableCollection<WordCountData> WordCountCollection { get; private set; }

        public MainWindow() {
            WordCountCollection = new ObservableCollection<WordCountData>();

            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            string file = GetFileName();

            try {
                LoadFileData(file);
                DisplayTopWords();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayTopWords() {
            WordCountCollection.Clear();

            foreach (string key in _wordCounts.Keys) {
                WordCountCollection.Add(new WordCountData(key, _wordCounts[key]));
            }
        }

        private void LoadFileData(string file) {
            if (!string.IsNullOrEmpty(file)) {
                using (FileStream contents = File.OpenRead(file))
                using (StreamReader reader = new StreamReader(contents)) {
                    _wordCounts.Clear();

                    while (!reader.EndOfStream) {
                        LoadLine(reader.ReadLine());
                    }
                }
            }
        }

        private void LoadLine(string line) {
            string[] words = line.Split(" \t,.;()\"\'".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words) {
                string wordLower = word.ToLower();

                int count;
                if (!_wordCounts.TryGetValue(wordLower, out count)) {
                    count = 0;
                    _wordCounts.Add(wordLower, 0);
                }

                _wordCounts[wordLower] = count + 1;
            }
        }

        private string GetFileName() {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "*";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Return open file dialog box results
            return (result == true) ? dlg.FileName : null;
        }

        #region ListView Column Sorting

        // ListView column sorting code taking directly from MSDN
        // http://msdn.microsoft.com/en-us/library/ms745786.aspx

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e) {
            GridViewColumnHeader headerClicked =
                e.OriginalSource as GridViewColumnHeader;

            ListSortDirection direction;

            if (headerClicked != null) {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding) {
                    if (headerClicked != _lastHeaderClicked) {
                        direction = ListSortDirection.Ascending;
                    } else {
                        if (_lastDirection == ListSortDirection.Ascending) {
                            direction = ListSortDirection.Descending;
                        } else {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    string header = headerClicked.Column.Header as string;
                    Sort(header, direction);

                    if (direction == ListSortDirection.Ascending) {
                        headerClicked.Column.HeaderTemplate =
                            Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    } else {
                        headerClicked.Column.HeaderTemplate =
                            Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked) {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }


                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction) {
            ICollectionView dataView =
                CollectionViewSource.GetDefaultView(listView1.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        #endregion
    }

    public class WordCountData {
        public WordCountData(string word, int count) {
            Word = word;
            Count = count;
        }

        public string Word { get; set; }
        public int Count { get; set; }
    }
}