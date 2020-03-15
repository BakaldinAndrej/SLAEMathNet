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
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using MathNet.Numerics.LinearAlgebra;

namespace SLAEMathNet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseFileName(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            string filename = "";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                filename = dialog.FileName;
            }

            FileName.Text = filename;
        }

        private void Calc(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader(FileName.Text))
                {
                    string file = sr.ReadToEnd();
                    file = file.Replace('.', ',');
                    double[,] a = null;
                    double[] b = null;

                    var solver = new SLAEsolverMathNet();
                    switch (solver.ParseSLAE(file, ref a, ref b))
                    {
                        case ErrorCode.NotEnoughtEquations:
                            {
                                Answer.Text = "Недостаточно уравнений";
                                return;
                            }
                        case ErrorCode.TooManyEquations:
                            {
                                Answer.Text = "Излишнее число уравнений может привести к не верному решению. Пожалуйста, перепроверьте систему";
                                return;
                            }
                        case ErrorCode.None:
                            {
                                double[] x = solver.SolveSLAE(a, b);

                                Answer.Text = "";
                                for (int i = 0; i < x.Length; i++)
                                    Answer.Text += x[i] + "\n";
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                Answer.Text = ex.Message;
            }
        }
    }
}
