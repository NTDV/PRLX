using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace prlx
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 100), IsEnabled = true };

        private Line ln1 = new Line() { StrokeStartLineCap = PenLineCap.Round, StrokeThickness = 2 },
                ln2 = new Line() { StrokeStartLineCap = PenLineCap.Round, StrokeThickness = 2 },
                    lnX = new Line() { StrokeStartLineCap = PenLineCap.Round, StrokeThickness = 2, X1 = 0, Y1 = 250, X2 = 500, Y2 = 250, Stroke = Brushes.DarkGray },
                        lnZ = new Line() { StrokeStartLineCap = PenLineCap.Round, StrokeThickness = 2, X1 = 250, Y1 = 0, X2 = 250, Y2 = 500, Stroke = Brushes.DarkGray };

        private void Draw()
        {
            try
            {
                if (double.Parse(numAngle1.Text) < 181 && double.Parse(numAngle1.Text) > -181 && double.Parse(numAngle2.Text) < 181 && double.Parse(numAngle2.Text) > -181 && double.Parse(numX1.Text) < 10001 && double.Parse(numX1.Text) > -10001 && double.Parse(numX2.Text) < 10001 && double.Parse(numX2.Text) > -10001 && double.Parse(numZ1.Text) < 10001 && double.Parse(numZ1.Text) > -10001 && double.Parse(numZ2.Text) < 10001 && double.Parse(numZ2.Text) > -10001)
                {
                    CanvasBox.Children.Clear();

                    ln1.X1 = double.Parse(numX1.Text) * 0.025 + 250;
                    ln2.X1 = double.Parse(numX2.Text) * 0.025 + 250;
                    ln1.Y1 = double.Parse(numZ1.Text) * 0.025 + 250;
                    ln2.Y1 = double.Parse(numZ2.Text) * 0.025 + 250;
                    double angle_1 = (double)numAngle1.Value * Math.PI / -180; // Рассчитываем угол с учетом координатной плоскости у 1 отрезка
                    double angle_2 = (double)numAngle2.Value * Math.PI / -180; // Рассчитываем угол с учетом координатной плоскости у 2 отрезка
                    ln1.X2 = ln1.X1 + 500 * Math.Sin(angle_1);
                    ln2.X2 = ln2.X1 + 500 * Math.Sin(angle_2);
                    ln1.Y2 = ln1.Y1 + 500 * Math.Cos(angle_1);
                    ln2.Y2 = ln2.Y1 + 500 * Math.Cos(angle_2);

                    ln1.Stroke = Brushes.Red;
                    ln2.Stroke = Brushes.Red;
                    CanvasBox.Children.Add(lnZ);
                    CanvasBox.Children.Add(lnX);
                    CanvasBox.Children.Add(ln1);
                    CanvasBox.Children.Add(ln2);
                }
            }
            catch (Exception) { }
        }

        private void Draw(Line dot)
        {
            try
            {
                if (double.Parse(numAngle1.Text) < 181 && double.Parse(numAngle1.Text) > -181 && double.Parse(numAngle2.Text) < 181 && double.Parse(numAngle2.Text) > -181 && double.Parse(numX1.Text) < 10001 && double.Parse(numX1.Text) > -10001 && double.Parse(numX2.Text) < 10001 && double.Parse(numX2.Text) > -10001 && double.Parse(numZ1.Text) < 10001 && double.Parse(numZ1.Text) > -10001 && double.Parse(numZ2.Text) < 10001 && double.Parse(numZ2.Text) > -10001)
                {
                    CanvasBox.Children.Clear();

                    ln1.X1 = double.Parse(numX1.Text) * 0.025 + 250;
                    ln2.X1 = double.Parse(numX2.Text) * 0.025 + 250;
                    ln1.Y1 = double.Parse(numZ1.Text) * 0.025 + 250;
                    ln2.Y1 = double.Parse(numZ2.Text) * 0.025 + 250;
                    double angle_1 = (double)numAngle1.Value * Math.PI / -180; // Рассчитываем угол с учетом координатной плоскости у 1 отрезка
                    double angle_2 = (double)numAngle2.Value * Math.PI / -180; // Рассчитываем угол с учетом координатной плоскости у 2 отрезка
                    ln1.X2 = ln1.X1 + 500 * Math.Sin(angle_1);
                    ln2.X2 = ln2.X1 + 500 * Math.Sin(angle_2);
                    ln1.Y2 = ln1.Y1 + 500 * Math.Cos(angle_1);
                    ln2.Y2 = ln2.Y1 + 500 * Math.Cos(angle_2);

                    ln1.Stroke = Brushes.DeepSkyBlue;
                    ln2.Stroke = Brushes.DeepSkyBlue;

                    CanvasBox.Children.Add(lnZ);
                    CanvasBox.Children.Add(lnX);
                    CanvasBox.Children.Add(ln1);
                    CanvasBox.Children.Add(ln2);
                    CanvasBox.Children.Add(dot);
                }
            }
            catch (Exception) { }
        }

        private Line Intersection() // Нахождение точки пересечения
        {
            Line result; // Точка пересечения

            if (numX1.Text == numX2.Text && numZ1.Text == numZ2.Text)
            {
                result = new Line() { X1 = double.Parse(numX1.Text) * 0.025 + 250, Y1 = double.Parse(numZ1.Text) * 0.025 + 250, X2 = double.Parse(numX1.Text) * 0.025 + 250 + 2, Y2 = double.Parse(numZ1.Text) * 0.025 + 250 + 2, StrokeStartLineCap = PenLineCap.Round, StrokeThickness = 4, Stroke = Brushes.White }; // Тогда присвоить точке значение
                return result;
            }
            else
            {
                /*Начало упрощения имен переменных*/
                double xo = ln1.X1, yo = ln1.Y1;
                double p = ln1.X2 - ln1.X1, q = ln1.Y2 - ln1.Y1;
                double x1 = ln2.X1, y1 = ln2.Y1;
                double p1 = ln2.X2 - ln2.X1, q1 = ln2.Y2 - ln2.Y1;
                /*Конец упрощения имен переменных*/

                /*Начало рассчета точки пересечения*/
                double x = (xo * q * p1 - x1 * q1 * p - yo * p * p1 + y1 * p * p1) / (q * p1 - q1 * p);
                double y = (yo * p * q1 - y1 * p1 * q - xo * q * q1 + x1 * q * q1) / (p * q1 - p1 * q);
                /*Конец рассчета точки пересечения*/

                /*Начало обработки особых случаев*/
                if (x <= 500 && x >= 0 && y <= 500 && y >= 0) // Если координаты НЕ за пределами плоскости
                {
                    result = new Line() { X1 = x, Y1 = y, X2 = x + 2, Y2 = y + 2, StrokeStartLineCap = PenLineCap.Round, StrokeThickness = 4, Stroke = Brushes.White }; // Тогда присвоить точке значение
                    if (IsThem(result)) // Если точка принадлежит отрезкам
                    {
                        return result; // Вернуть результат
                    }
                    else // Иначе
                    {
                        return null; // Вернуть пустую точку
                    }
                }
                else // Иначе
                {
                    return null; // Вернуть пустую точку
                }
                /*Конец обработки особых случаев*/
            }
        }

        private bool IsThem(Line A) // Нахождение принадлежности точки отрезкам
        {
            // Я не могу это никак прокомментировать, просто магия
            int istrue = 0;
            if (Math.Abs(ln1.X1 - ln1.X2) < 0.001D)
            {
                if ((Math.Abs(A.X1 - ln1.X1) < 0.001D) && (((A.Y1 <= ln1.Y1) && (A.Y1 >= ln1.Y2)) || ((A.Y1 >= ln1.Y1) && (A.Y1 <= ln1.Y2))))
                {
                    istrue++;
                }
                else
                {
                    istrue--;
                }
            }
            else
            {
                double p = (A.X1 - ln1.X2) / (ln1.X1 - ln1.X2);
                if ((Math.Abs(p * ln1.Y1 + (1D - p) * ln1.Y2 - A.Y1) < 0.001D) && (p >= 0D) && (p <= 1D))
                {
                    istrue++;
                }
                else
                {
                    istrue--;
                }
            }

            if (Math.Abs(ln2.X1 - ln2.X2) < 0.001D)
            {
                if ((Math.Abs(A.X1 - ln2.X1) < 0.001D) && (((A.Y1 <= ln2.Y1) && (A.Y1 >= ln2.Y2)) || ((A.Y1 >= ln2.Y1) && (A.Y1 <= ln2.Y2))))
                {
                    istrue++;
                }
                else
                {
                    istrue--;
                }
            }
            else
            {
                double p = (A.X1 - ln2.X2) / (ln2.X1 - ln2.X2);
                if ((Math.Abs(p * ln2.Y1 + (1D - p) * ln2.Y2 - A.Y1) < 0.001D) && (p >= 0D) && (p <= 1D))
                {
                    istrue++;
                }
                else
                {
                    istrue--;
                }
            }

            if (istrue == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Count()
        {
            Line buffer = Intersection();
            if (buffer != null)
            {
                Draw(buffer);
                numXE.Text = Math.Round((buffer.X1 - 250) / 0.025).ToString();
                numZE.Text = Math.Round((buffer.Y1 - 250) / 0.025).ToString();
            }
            else
            {
                Draw();
                numXE.Text = "не число";
                numZE.Text = "не число";
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Draw();
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Count();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Возникла непредвиденная ошибка, приносим свои извенения.\nНе закрывайте программу, все произойдет в автоматическом режиме.", "Простите", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            Directory.CreateDirectory(Environment.CurrentDirectory + @"\Saved");
            using (StreamWriter writer = new StreamWriter(App.logs) { AutoFlush = true })
            {
                writer.WriteLine((e.ExceptionObject as Exception).ToString());
            }
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}