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

namespace LinearAlgebraTools {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public PointCollection myPointCollection { get; set; }
        public MainWindow() {
            myPointCollection = new PointCollection {
                new Point(0, 0),
                new Point(100, 0),
                new Point(100, 100),
                new Point(0, 100)
            };
            InitializeComponent();

            DataContext = this;
        }

        private void Change(double[,] matrix) {
            Point[] points = myPointCollection.ToArray();
            for (int i = 0; i < points.Length; i++) {
                points[i] = new Point(((points[i].X * matrix[0, 0]) + (points[i].Y * matrix[0, 1])), ((points[i].X * matrix[1, 0]) + (points[i].Y * matrix[1, 1])));
            }
            ResetPolygon(points);
        }

        private void ChangeRandomly() {
            Random rand = new Random();
            Point[] points = new Point[rand.Next(3, 8)];
            for (int i = 0; i < points.Length; i++) {
                points[i] = new Point(rand.Next(50, 250), rand.Next(50, 350));
            }
            ResetPolygon(points);
        }

        private void ResetPolygon(Point[] points) {
            myPointCollection.Clear();
            spPoints.Children.Clear();
            Label[] lblPoints = new Label[points.Length];

            for (int i = 0; i < points.Length; i++) {
                myPointCollection.Add(points[i]);
                lblPoints[i] = new Label();
                lblPoints[i].Content = String.Format("({0}, {1})", points[i].X, points[i].Y);
                spPoints.Children.Add(lblPoints[i]);
            }
            DataContext = null;
            DataContext = this;
        }

        private void BtnTransform_Click(object sender, RoutedEventArgs e) {
            Double.TryParse(txt0_0.Text, out double x1);
            Double.TryParse(txt0_1.Text, out double x2);
            Double.TryParse(txt1_0.Text, out double x3);
            Double.TryParse(txt1_1.Text, out double x4);
            double[,] matrix = { { x1, x2 }, { x3, x4 } };
            //ChangeRandomly();
            Change(matrix);
        }

        private void BtnDrawShape_Click(object sender, RoutedEventArgs e) {
            string shapeText = txtPoints.Text;
            char[] splits = { '(', ')' };
            string[] shapeTextPoints = shapeText.Split(splits, StringSplitOptions.RemoveEmptyEntries);
            List<Point> points = new List<Point>();
            for (int i = 0; i < shapeTextPoints.Length; i++) {
                string[] XY = shapeTextPoints[i].Split(',');
                double.TryParse(XY[0], out double x);
                double.TryParse(XY[1], out double y);
                points.Add(new Point(x, y));
            }
            ResetPolygon(points.ToArray());
        }
    }
}
