/*using System;
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

namespace BugGraph
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}*/
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media; // Pen

using System.IO;
using Microsoft.Research.DynamicDataDisplay; // Core functionality
using Microsoft.Research.DynamicDataDisplay.DataSources; // EnumerableDataSource
using Microsoft.Research.DynamicDataDisplay.PointMarkers; // CirclePointMarker
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BugGraph
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Window1_Loaded);
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            List<BugInfo> bugInfoList = LoadBugInfo("..\\..\\BugInfo.txt");

            int[] dates = new int[bugInfoList.Count];
            int[] numberOpen = new int[bugInfoList.Count];
            int[] numberClosed = new int[bugInfoList.Count];

            for (int i = 0; i < bugInfoList.Count; ++i)
            {
                dates[i] = bugInfoList[i].date;
                numberOpen[i] = bugInfoList[i].numberOpen;
                numberClosed[i] = bugInfoList[i].numberClosed;
            }

            var datesDataSource = new EnumerableDataSource<int>(dates);
            datesDataSource.SetXMapping(x => x);

            var numberOpenDataSource = new EnumerableDataSource<int>(numberOpen);
            numberOpenDataSource.SetYMapping(y => y);

            var numberClosedDataSource = new EnumerableDataSource<int>(numberClosed);
            numberClosedDataSource.SetYMapping(y => y);

            CompositeDataSource compositeDataSource1 = new
              CompositeDataSource(datesDataSource, numberOpenDataSource);
            CompositeDataSource compositeDataSource2 = new
              CompositeDataSource(datesDataSource, numberClosedDataSource);

            plotter.AddLineGraph(compositeDataSource1,
              new Pen(Brushes.Blue, 2),
              new CirclePointMarker { Size = 10.0, Fill = Brushes.Red },
              new PenDescription("Number bugs open"));

            plotter.AddLineGraph(compositeDataSource2,
              new Pen(Brushes.Green, 2),
              new TrianglePointMarker
              {
                  Size = 10.0,
                  Pen = new Pen(Brushes.Black, 2.0),
                  Fill = Brushes.GreenYellow
              },
              new PenDescription("Number bugs closed"));

            plotter.Viewport.FitToView();

        } // Window1_Loaded()

        private static List<BugInfo> LoadBugInfo(string fileName)
        {
            var result = new List<BugInfo>();
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] pieces = line.Split(':');

               
                int numopen = int.Parse(pieces[1]);
                int d = int.Parse(pieces[0]);
                int numclosed = int.Parse(pieces[2]);
                BugInfo bi = new BugInfo(d, numopen, numclosed);
                result.Add(bi);
            }
            sr.Close();
            fs.Close();
            return result;
        }

    } // class Window1

    
} // ns
