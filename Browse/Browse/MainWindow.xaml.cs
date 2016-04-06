using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Browse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DocManager docManager;
        List<String> data = null, data2 = null;

        public MainWindow()
        {
            InitializeComponent();
            docManager = new DocManager();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.FileName = "Doc"; // Default file name
            dlg.DefaultExt = ".docx"; // Default file extension
            
            // Only works with PDF so removed temporaly
            //dlg.Filter = "Office Doc Files (*.doc)|*.doc | Office DocX Files (*.docx)|*.docx | PDF Files (*.pdf)|*.pdf";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                Console.WriteLine(filename.ToString());
                data = docManager.GetData(filename);
            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.FileName = "Doc"; // Default file name
            dlg.DefaultExt = ".docx"; // Default file extension
            //dlg.Filter = "Office Doc Files (*.doc)|*.doc | Office DocX Files (*.docx)|*.docx | PDF Files (*.pdf)|*.pdf";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                Console.WriteLine(filename.ToString());
                data2 = docManager.GetData(filename);
            }


        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {
            CreateVectorsAndCalculate();
        }

        public void CreateVectorsAndCalculate()
        {
            
            HashSet<String> x = new HashSet<string>();
            x.UnionWith(data);
            x.UnionWith(data2);

            List<int> vector1 = new List<int>();
            List<int> vector2 = new List<int>();

            foreach (String s in x)
            {
                vector1.Add(CountString(s, 1));
                vector2.Add(CountString(s, 2));
            }

            // time to calculate cosine sim

            double dotProduct = 0f, magnitude1 = 0f, magnitude2 = 0f, cosineSimilarity = 0;

            for (int i = 0; i < vector1.Count; i++)
            {
                dotProduct += vector1[i] * 1.0f * vector2[i];
                magnitude1 += Math.Pow(vector1[i], 2);
                magnitude2 += Math.Pow(vector2[i], 2);
            }
            if (magnitude1 != 0 && magnitude2 != 0)
            {
                cosineSimilarity = dotProduct / Math.Sqrt(magnitude1 * magnitude2);
            }
            else {
                cosineSimilarity = 0f;
            }

            // Showing in percents 
            cosineSimilarity *= 100.0f;

            MessageBox.Show(cosineSimilarity.ToString() + " % similar");
        }

        private int CountString(String word, int dataSet)
        {
            int count = 0;

            if (dataSet == 1)
            {
                foreach (String s in data)
                    if (s.Equals(word)) count++;
            }
            else
            {
                foreach (String s in data2)
                    if (s.Equals(word)) count++;
            }

            return count;
        }
    }
}
