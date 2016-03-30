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

        }
    }
}
