using System;
using System.Drawing;
using System.Windows.Forms;

namespace prlx
{
    public partial class Form1 : Form
    {
        private PointF bpt_1, bpt_2, pt_1, pt_2; // Точки для отрезков (А, B) и (C, D), т.е. А, С, B, D

        private void DrawLaD(PointF dot) // Метод отрисовки с точкой пересечения
        {
            label7.ForeColor = Color.MediumVioletRed; // Смена цвета шрифтов
            label8.ForeColor = Color.DarkViolet; // Смена цвета шрифтов
            bpt_1 = new PointF((float)numericX1.Value * (float)0.025 + 250, (float)numericZ1.Value * (float)0.025 + 250); // Рассчитываем начальную точку с учетом координатной плоскости у 1 отрезка
            bpt_2 = new PointF((float)numericX2.Value * (float)0.025 + 250, (float)numericZ2.Value * (float)0.025 + 250); // Рассчитываем начальную точку с учетом координатной плоскости у 2 отрезка
            double angle_1 = (double)numericUpDown1.Value * Math.PI / -180; // Рассчитываем угол с учетом координатной плоскости у 1 отрезка
            double angle_2 = (double)numericUpDown2.Value * Math.PI / -180; // Рассчитываем угол с учетом координатной плоскости у 2 отрезка
            pt_1 = new PointF((float)(bpt_1.X + 500 * Math.Sin(angle_1)), (float)(bpt_1.Y + 500 * Math.Cos(angle_1))); // Рассчитываем конечную точку с учетом координатной плоскости и угла у 1 отрезка
            pt_2 = new PointF((float)(bpt_2.X + 500 * Math.Sin(angle_2)), (float)(bpt_2.Y + 500 * Math.Cos(angle_2))); // Рассчитываем конечную точку с учетом координатной плоскости и угла у 2 отрезка
            using (Pen pen = new Pen(Color.Black, 2) { StartCap = System.Drawing.Drawing2D.LineCap.RoundAnchor }) // Отрисовываем отрезки и точку с такими параметрами
            {
                pictureBox1.CreateGraphics().DrawLine(pen, new Point(0, 250), new Point(500, 250)); // Отрисовываем ось абсцисс
                pictureBox1.CreateGraphics().DrawLine(pen, new Point(250, 0), new Point(250, 500)); // Отрисовываем ось аппликат
                pictureBox1.CreateGraphics().DrawLine(new Pen(Color.MediumVioletRed, 2), bpt_1, pt_1); // Отрисовываем отрезок 1
                pictureBox1.CreateGraphics().DrawLine(new Pen(Color.DarkViolet, 2), bpt_2, pt_2); // Отрисовываем отрезок 2
                pictureBox1.CreateGraphics().DrawLine(new Pen(Color.Blue, 4), new PointF(dot.X - 1, dot.Y - 1), new PointF(dot.X + 1, dot.Y + 1)); // Отрисовываем точку пересечения
            }
        }

        private void DrawLaD() // Метод отрисовки без точки пересечения
        {
            label7.ForeColor = Color.Black;
            label8.ForeColor = Color.Black;
            bpt_1 = new PointF((float)numericX1.Value * (float)0.025 + 250, (float)numericZ1.Value * (float)0.025 + 250); // Рассчитываем начальную точку с учетом координатной плоскости у 1 отрезка
            bpt_2 = new PointF((float)numericX2.Value * (float)0.025 + 250, (float)numericZ2.Value * (float)0.025 + 250); // Рассчитываем начальную точку с учетом координатной плоскости у 2 отрезка
            double angle_1 = (double)numericUpDown1.Value * Math.PI / -180; // Рассчитываем угол с учетом координатной плоскости у 1 отрезка
            double angle_2 = (double)numericUpDown2.Value * Math.PI / -180; // Рассчитываем угол с учетом координатной плоскости у 2 отрезка
            pt_1 = new PointF((float)(bpt_1.X + 500 * Math.Sin(angle_1)), (float)(bpt_1.Y + 500 * Math.Cos(angle_1))); // Рассчитываем конечную точку с учетом координатной плоскости и угла у 1 отрезка
            pt_2 = new PointF((float)(bpt_2.X + 500 * Math.Sin(angle_2)), (float)(bpt_2.Y + 500 * Math.Cos(angle_2))); // Рассчитываем конечную точку с учетом координатной плоскости и угла у 2 отрезка
            using (Pen pen = new Pen(Color.Black, 2) { StartCap = System.Drawing.Drawing2D.LineCap.RoundAnchor }) // Отрисовываем отрезки и точку с такими параметрами
            {
                pictureBox1.CreateGraphics().DrawLine(pen, new Point(0, 250), new Point(500, 250)); // Отрисовываем ось абсцисс
                pictureBox1.CreateGraphics().DrawLine(pen, new Point(250, 0), new Point(250, 500)); // Отрисовываем ось аппликат
                pictureBox1.CreateGraphics().DrawLine(new Pen(Color.Red, 2), bpt_1, pt_1); // Отрисовываем отрезок 1
                pictureBox1.CreateGraphics().DrawLine(new Pen(Color.Red, 2), bpt_2, pt_2); // Отрисовываем отрезок 2
            }
        }

        private PointF Intersection(PointF A, PointF B, PointF C, PointF D) // Нахождение точки пересечения
        {
            PointF result; // Точка пересечения

            /*Начало упрощения имен переменных*/
            double xo = A.X, yo = A.Y;
            double p = B.X - A.X, q = B.Y - A.Y;
            double x1 = C.X, y1 = C.Y;
            /*Конец упрощения имен переменных*/

            /*Начало рассчета точки пересечения*/
            double p1 = D.X - C.X, q1 = D.Y - C.Y;
            double x = (xo * q * p1 - x1 * q1 * p - yo * p * p1 + y1 * p * p1) / (q * p1 - q1 * p);
            double y = (yo * p * q1 - y1 * p1 * q - xo * q * q1 + x1 * q * q1) / (p * q1 - p1 * q);
            /*Конец рассчета точки пересечения*/

            /*Начало обработки особых случаев*/
            if (x <= 500 && x >= 0 && y <= 500 && y >= 0) // Если координаты НЕ за пределами плоскости
            {
                result = new PointF((float)x, (float)y); // Тогда присвоить точке значение
                if (IsThem(result)) // Если точка принадлежит отрезкам
                {
                    DrawLaD(result); // Тогда отрисовть отрезки с точкой
                    return result; // Вернуть результат
                }
                else // Иначе
                {
                    DrawLaD(); // Отрисовть отрезки
                    return new PointF(float.NaN, float.NaN); // Вернуть пустую точку
                }
            }
            else // Иначе
            {
                DrawLaD(); // Отрисовть отрезки
                return new PointF(float.NaN, float.NaN); // Вернуть пустую точку
            }
            /*Конец обработки особых случаев*/
        }

        private bool IsThem(PointF A) // Нахождение принадлежности точки отрезкам
        {
            // Я не могу это никак прокомментировать, просто магия
            int istrue = 0;
            if (bpt_1.X == pt_1.X)
            {
                if ((A.X == bpt_1.X) && (((A.Y <= bpt_1.Y) && (A.Y >= pt_1.Y)) || ((A.Y >= bpt_1.Y) && (A.Y <= pt_1.Y))))
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
                float p = (A.X - pt_1.X) / (bpt_1.X - pt_1.X);
                if ((Math.Abs(p * bpt_1.Y + (1D - p) * pt_1.Y - A.Y) < 0.01D) && (p >= 0D) && (p <= 1D))
                {
                    istrue++;
                }
                else
                {
                    istrue--;
                }
            }

            if (bpt_2.X == pt_2.X)
            {
                if ((A.X == bpt_2.X) && (((A.Y <= bpt_2.Y) && (A.Y >= pt_2.Y)) || ((A.Y >= bpt_2.Y) && (A.Y <= pt_2.Y))))
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
                float pp = (A.X - pt_2.X) / (bpt_2.X - pt_2.X);
                if ((Math.Abs(pp * bpt_2.Y + (1D - pp) * pt_2.Y - A.Y) < 0.01D) && (pp >= 0D) && (pp <= 1D))
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

        private void Count() // Обработка информации о пересечении
        {
            pictureBox1.Refresh(); // Обновить PictureBox
            PointF intersect = Intersection(bpt_1, pt_1, bpt_2, pt_2); // Создать точку пересечения
            textBoxX.Text = (Math.Round((intersect.X - 250) / 0.025)).ToString(); // Вывести Х
            textBoxZ.Text = (Math.Round((intersect.Y - 250) / 0.025)).ToString(); // Вывести Y
        }

        public Form1() // Инициализатор формы и отрисовка
        {
            InitializeComponent();
            DrawLaD();
        }

        private void timer1_Tick(object sender, EventArgs e) // Каждый тик таймера обработать информацию
        {
            Count();
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e) // Каждое изменение данных обработать информацию
        {
            Count();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e) // При перерисовке PictureBox'a отрисовать без точки пересечения
        {
            DrawLaD();
        }
    }
}