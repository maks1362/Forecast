using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Diagnostics;

namespace Forecast
{
    /*static class ForecastMethod
    {
        //static 
    }*/

    public partial class Form1 : Form
    {
        private Data data;
        private string fileName;
        //private DataGridView grid;
        /// <summary>
        /// Список способов прогноза
        /// </summary>
        public static IList<string> forecasts = new List<string>
            {
                { "по среднему абсолютному приросту" },
                { "по среднему коэффициенту роста" },
                { "стационарного ряда" },
                { "по уравнению тренда" }
               //     new Element() { Symbol="Sc", Name="Scandium", AtomicNumber=21}},
            };

        public static IList<string> methods = new List<string>
            {
                { "линейная" },
                { "показательная" },
                { "парабола" }
               //     new Element() { Symbol="Sc", Name="Scandium", AtomicNumber=21}},
            };

        private void CreateGrid(DataGridView grid)
        {
            grid.ColumnCount = 11;
            grid.Columns[0].Name = "Год";
            grid.Columns[1].Name = "Уровень ряда";
            grid.Columns[2].Name = "Абсолютный прирост цепной";
            grid.Columns[3].Name = "Абсолютный прирост базисный";
            grid.Columns[4].Name = "Темп роста цепной";
            grid.Columns[5].Name = "Темп роста базисный";
            grid.Columns[6].Name = "Темп прироста цепной";
            grid.Columns[7].Name = "Темп прироста базисный";
            grid.Columns[8].Name = "Абсолютное значение 1% прироста";
            grid.Columns[9].Name = "Относительное ускорение";
            grid.Columns[10].Name = "Коэффициент опережения";

            DataGridViewCellStyle cellStyle1 = new DataGridViewCellStyle
            {
                Format = "N4"
            };
            dataGridView1.DefaultCellStyle = cellStyle1;//Базовый стиль всех ячеек 4 знака после запятой
            grid.Columns[0].DefaultCellStyle = new DataGridViewCellStyle() { Format = "N0" };//Стиль года

            dataGridView1.RowHeadersWidth = 70;
            grid.Columns[0].Width = 50;
            /*for(int i=1; i<grid.ColumnCount; i++)
            {
                //grid.Columns[i].Width = 70;
                //grid.Columns[i].DefaultCellStyle = cellStyle1;
            }*/
        }

        private void ClearGrid(DataGridView dataGrid)
        {
            dataGrid.Rows.Clear();//очищаю таблицу
            
        }
        private void ClearChart(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            chart.Series[0].Points.Clear();//Очищаю графики
            if ((chart.Series.Count == 2))
            {
                chart.Series[1].Points.Clear();

            }//Очищаю графики
        }

        public Form1()
        {
            try
            {
                InitializeComponent();

                ComboBoxMethod.DataSource = forecasts;
                comboBoxMethods.DataSource = methods;
                CreateGrid(dataGridView1);
                //grid = dataGridView1;//глобальная переменная

                OpenFile("..\\..\\Исходные данные\\Проверка линейн тренда — копия.csv");
                ComboBoxMethod.SelectedIndex = 3;

                //forecast_Click(null, null);

                //chart1.Series[0].X
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog  myOpenFileDialog = new OpenFileDialog
            {
                //InitialDirectory = @"C:\users\qq\desktop\tabul\",
                DefaultExt = "*.csv",
                Filter = "Files (*.csv)|*.csv"
            };

            if (!(myOpenFileDialog.ShowDialog() == DialogResult.OK))
                return;
            fileName = myOpenFileDialog.FileName;
            OpenFile(fileName);

        }//Диалог открытия файла и заполнение DataGridView

        private void OpenFile(string FileName)
        {

            data = new Data(FileName);
            ClearGrid(dataGridView1);//Очистка
            ClearChart(chart1);//Очистка
            for (int i = 0; i < data.Count(); i++)//Заполняю DataGridView
            {
                dataGridView1.Rows.Insert(
                    dataGridView1.RowCount - 1, //В какое место таблицы вставить строку
                    data[i].Key,
                    data[i].Value.Num,
                    data[i].Value.IncreaseOrder,
                    data[i].Value.IncreaseBase,
                    data[i].Value.RateGrowthOrder,
                    data[i].Value.RateGrowthBase,
                    data[i].Value.RateIncreaseOrder,
                    data[i].Value.RateIncreaseBase,
                    data[i].Value.AbsOfPerIncrease,
                    data[i].Value.RelativeAcceler,
                    data[i].Value.CoefAdvance
                );
            }
            chart1.Series[0].Points.DataBindXY(data.Keys, data.GetNums());//Рисую график
        }//Открыть файл и заполнить DataGridView

        private void forecast_Click(object sender, EventArgs e)
        {
            switch (ComboBoxMethod.SelectedIndex)
            {
                case 0://абсолютн
                    ForecastAbs();
                    break;
                case 1://геометр
                    ForecastGeom();
                    break;
                case 2://Стационарный
                    ForecastStat();
                    break;
                case 3://Уровень тренда
                    ForecastTrend();
                    break;
            }


        }//Прогноз

        private void ForecastAbs()
        {
            if (data != null)
            {
                chart1.Series[0].Points.DataBindXY(data.Keys, data.GetNums());
                int periodYprezdenia = (Int32)PeriodNumeric.Value;
                double sredAbsIncrease = 0;
                double[] newNums = new double[periodYprezdenia];
                if (data.Count() + 1 != dataGridView1.Rows.Count)
                    //for (int i = data.Count(); i < dataGridView1.Rows.Count; i++)
                    while (data.Count() + 1 != dataGridView1.Rows.Count)
                    {
                        dataGridView1.Rows.RemoveAt(data.Count());
                    }//Очистка лишних строк в таблице
                for (int i = 0; i < data.Count(); i++)
                {
                    sredAbsIncrease += data[i].Value.IncreaseOrder;
                }//Средний абсолютный прирост цепной

                sredAbsIncrease = (sredAbsIncrease / data.Count()) - 1;
                for (int i = 0, k = 1; i < periodYprezdenia; i++)
                {
                    newNums[i] = data[data.Count() - 1].Value.Num + sredAbsIncrease * k;
                    dataGridView1.Rows.Add(data[data.Count() - 1].Key + k, newNums[i]);
                    chart1.Series[0].Points.AddXY(data[data.Count() - 1].Key + k, newNums[i]);
                    k++;
                }
            }
        }

        private void ForecastGeom()
        {
            if (data != null)
            {
                chart1.Series[0].Points.DataBindXY(data.Keys, data.GetNums());

                int periodYprezdenia = (Int32)PeriodNumeric.Value;
                double sredGrows = 1;
                double[] newNums = new double[periodYprezdenia];
                if (data.Count() + 1 != dataGridView1.Rows.Count)
                    //for (int i = data.Count(); i < dataGridView1.Rows.Count; i++)
                    while (data.Count() + 1 != dataGridView1.Rows.Count)
                    {
                        dataGridView1.Rows.RemoveAt(data.Count());
                    }//Очистка лишних строк в таблице
                for (int i = 1; i < data.Count(); i++)
                {
                    sredGrows *= data[i].Value.RateGrowthOrder;
                    //sredAbsGrows += data[i].Value.IncreaseOrder;
                }//Средний абсолютный прирост цепной
                sredGrows = Math.Pow(sredGrows, 1.0 / data.Count()); //
                for (int i = 0, k = 1; i < periodYprezdenia; i++)
                {
                    newNums[i] = data[data.Count() - 1].Value.Num * Math.Pow(sredGrows, k); //
                    dataGridView1.Rows.Add(data[data.Count() - 1].Key + k, newNums[i]);
                    chart1.Series[0].Points.AddXY(data[data.Count() - 1].Key + k, newNums[i]);
                    k++;
                }
            }
        }

        private void ForecastStat()
        {
            if (data != null)
            {
                if (dataGridView1.Rows.Count != data.Count() + 1)
                    dataGridView1.Rows.RemoveAt(data.Count());

                double avarLvl1 = 0;//Средний уровень половины ряда
                    double avarLvl2 = 0;
                    int countNum1 = (data.Count() / 2);
                    int countNum2 = (data.Count() - data.Count() / 2);
                    //Chart chart2 = new Chart();//Нужен для формул статистики
                    double tStudentTable;
                { 
                    {/*if (data.Count() % 2 == 0)
                    {
                        for (int i = 0; i < data.Count() / 2; i++)
                        {
                            avarLvl1 += data[i].Value.Num;
                        }
                        for (int i = data.Count() / 2; i < data.Count(); i++)
                        {
                            avarLvl2 += data[i].Value.Num;
                        }
                        avarLvl1 = avarLvl1 / (data.Count() / 2);
                    }
                    else
                    {
                        for (int i = 0; i < data.Count() / 2 + 1; i++)
                        {
                            avarLvl1 += data[i].Value.Num;
                        }
                        for (int i = data.Count() / 2 + 1; i < data.Count(); i++)
                        {
                            avarLvl2 += data[i].Value.Num;
                        }
                        avarLvl1 = avarLvl1 / (data.Count() / 2+1);
                    }
                    avarLvl2 = avarLvl2 / (data.Count() / 2);*/
                    }//Старый способ
                    for (int i = 0; i < data.Count() / 2; i++)
                    {
                        avarLvl1 += data[i].Value.Num;
                    }
                    for (int i = data.Count() / 2; i < data.Count(); i++)
                    {
                        avarLvl2 += data[i].Value.Num;
                    }
                    avarLvl1 = avarLvl1 / countNum1;
                    avarLvl2 = avarLvl2 / countNum2;


                    double sigma1 = 0;//Средне квадр отклонение для половины ряда
                    double sigma2 = 0;
                    for (int i = 0; i < data.Count() / 2; i++)
                    {
                        sigma1 += Math.Pow(data[i].Value.Num - avarLvl1, 2);
                    }
                    for (int i = data.Count() / 2; i < data.Count(); i++)
                    {
                        sigma2 += Math.Pow(data[i].Value.Num - avarLvl2, 2);
                    }
                    sigma1 =(sigma1 / (countNum1-1));
                    sigma2 =(sigma2 / (countNum2-1));

                    {
                    /*if (sigma1 > sigma2)
                    {
                        fisher = sigma1 / sigma2;
                    }
                    else
                    {
                        fisher = sigma2 / sigma1;
                    }*/
                    }//Старый способ вычисления критерия фишера 
                    double fisherMy = (sigma1 > sigma2) ? ((sigma1) / (sigma2)) : ((sigma2)/ (sigma1));//Критерий Фишера


                    
                    var fisherTable = chart1.DataManipulator.Statistics.InverseFDistribution(0.05, countNum1-1, countNum2-1);//Формула табличного значения Фишера
                    if (fisherMy > fisherTable)
                    {
                        /*MessageBox.Show("Фактическое значение F-критерия больше или табличного!");
                        return;*/

                        double tStudent = (Math.Abs(avarLvl1 - avarLvl2)) / Math.Sqrt( (sigma1 / countNum1) + ((sigma2) / countNum2) );
                        double part1 = (sigma1 / countNum1) + (sigma2 / countNum2);
                        double part2 = (Math.Pow((sigma1 / countNum1), 2) / (countNum1 + 1)) + (Math.Pow((sigma2 / countNum2), 2) / (countNum2 + 1));
                        double f = part1 / Math.Sqrt(part2);
                        tStudentTable = chart1.DataManipulator.Statistics.InverseTDistribution(0.05, (int)Math.Round(f-2));
                        if (tStudent > tStudentTable)
                        {
                            MessageBox.Show("Фактическое значение T-критерия больше или табличного. Гипотеза о стационарности ряда опровергнута!", "Прогнозирование");
                            return;
                        }//Проверка критерия
                    }//Проверка Фишера
                    else
                    { 
                        double tStudent; //Коэф студента
                        double sigma; //Средне квдр отклонение разности двух средних
                        sigma = Math.Sqrt(((sigma1 * (countNum1 - 1)) + (sigma2 * (countNum2 - 1))) / (countNum1 + countNum2 - 2));
                        tStudent = (Math.Abs(avarLvl1 - avarLvl2)) / (sigma * Math.Sqrt(1.0 / countNum1 + 1.0 / countNum2));
                        tStudentTable = chart1.DataManipulator.Statistics.InverseTDistribution(0.05, countNum1 + countNum2 - 2);
                        if (tStudent > tStudentTable)
                    {
                        MessageBox.Show("Фактическое значение T-критерия больше или табличного. Гипотеза о стационарности ряда опровергнута!", "Прогнозирование");
                        return;
                    }//Проверка Студента
                    }
                }

                double avarLvl = (avarLvl1 + avarLvl2) / 2;//Средний уровень ряда
                double sigmaForecast = 0; //среднее квадратическое отклонение для прогноза
                for (int i=0; i<data.Count(); i++)
                {
                    sigmaForecast += Math.Pow(data[i].Value.Num - avarLvl, 2);
                }
                sigmaForecast = Math.Sqrt(sigmaForecast / (data.Count()-1));
                tStudentTable = chart1.DataManipulator.Statistics.InverseTDistribution(0.05, data.Count());//Новое значение коэф студента
                double forecastError = sigmaForecast * tStudentTable * Math.Sqrt(1.0 + (1.0 / data.Count()));

                /*DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = data[data.Count()].Key;
                row.Cells[1].Value = avarLvl;*/

                textBoxErrorIndic.Text = Math.Round(sigmaForecast, 4).ToString();
                textBoxErrorForecast.Text = Math.Round(forecastError, 4).ToString();

                dataGridView1.Rows.Insert(data.Count(), data[data.Count()-1].Key+1, avarLvl);
                chart1.Series[0].Points.DataBindXY(data.Keys, data.GetNums());
                chart1.Series[0].Points.AddXY(data[data.Count() - 1].Key + 1, avarLvl);
            }

        }

        private void ForecastTrend()
        {
            if (data != null)
            {
                double newNum=0;
                int n1=0;
                int yravCount=0;
                double er = 0;
                chart1.Series[0].Points.DataBindXY(data.Keys, data.GetNums());
                if (dataGridView1.Rows.Count != data.Count()+1)
                    dataGridView1.Rows.RemoveAt(data.Count());
                switch (comboBoxMethods.SelectedIndex)
                {
                    case 0://абсолютн
                        TrendLine();
                        break;
                    case 1://геометр
                        TrendIndecative();
                        break;
                    case 2://Стационарный
                        TrendParabola();
                        break;
                    case 3://Уровень тренда
                        ForecastTrend();
                        break;
                    default:
                        return;
                    
                }

                textBoxErrorIndic.Text = Math.Round(er, 4).ToString();
                

                void TrendLine()
                {
                    yravCount = 2;
                    n1 = data.Count();
                    double sumT = 0;
                    double sumT2 = 0;
                    double sumLnNum = 0;
                    double sumLnNumT = 0;
                    for (int i = 1; i < n1 + 1; i++)
                    {
                        sumT += i;
                        sumT2 += i * i;
                        sumLnNum += data[i - 1].Value.Num;//
                        sumLnNumT += i * data[i - 1].Value.Num;//
                    }



                    double[,] a1 = new double[,] { { n1, sumT }, { sumT, sumT2 } };
                    double[] b1 = new double[] { sumLnNum, sumLnNumT };
                    double[] x1 = GausSolver(yravCount, a1, b1);

                    //

                    newNum = x1[0] + (x1[1] * (n1 + 1));
                    dataGridView1.Rows.Insert(data.Count(), data[data.Count() - 1].Key + 1, newNum);
                    chart1.Series[0].Points.DataBindXY(data.Keys, data.GetNums());
                    chart1.Series[0].Points.AddXY(data[data.Count() - 1].Key + 1, newNum);

                    double sum = 0;
                    for (int i = 0; i < data.Count(); i++)
                    {
                        sum += Math.Pow(data[i].Value.Num - (x1[0] + (x1[1] * (i+1))), 2);
                    }

                    er = Math.Sqrt(sum / (data.Count() - yravCount));
                }

                void TrendIndecative()
                {
                    yravCount = 2;
                    n1 = data.Count();
                    double sumT = 0;
                    double sumT2 = 0;
                    double sumLnNum = 0;
                    double sumLnNumT = 0;
                    for (int i = 1; i < n1 + 1; i++)
                    {
                        sumT += i;
                        sumT2 += i * i;
                        sumLnNum += Math.Log(data[i - 1].Value.Num);
                        sumLnNumT += i * Math.Log(data[i - 1].Value.Num);
                    }



                    double[,] a1 = new double[,] { { n1, sumT }, { sumT, sumT2 } };
                    double[] b1 = new double[] { sumLnNum, sumLnNumT };
                    double[] x1 = GausSolver(yravCount, a1, b1);

                    for (int i = 0; i < x1.Count(); i++)
                    {
                        x1[i] = Math.Exp(x1[i]);
                    }//Извлекаем логарифм

                    newNum = x1[0] * Math.Pow(x1[1], n1 + 1);
                    dataGridView1.Rows.Insert(data.Count(), data[data.Count() - 1].Key + 1, newNum);
                    chart1.Series[0].Points.DataBindXY(data.Keys, data.GetNums());
                    chart1.Series[0].Points.AddXY(data[data.Count() - 1].Key + 1, newNum);

                    double sum = 0;
                    for (int i = 0; i < data.Count(); i++)
                    {
                        sum += Math.Pow(data[i].Value.Num - (x1[0] * Math.Pow(x1[1], i + 1)), 2);
                    }

                    er = Math.Sqrt(sum / (data.Count() - yravCount));
                }

                void TrendParabola()
                {
                    yravCount = 3;
                    //Коэффициенты
                    n1 = data.Count();
                    double sumT = 0;
                    double sumT2 = 0;
                    double sumT3 = 0;
                    double sumT4 = 0;
                    double sumNum = 0;
                    double sumNumT = 0;
                    double sumNumT2 = 0;
                    for (int i = 1; i < n1 + 1; i++)
                    {
                        sumT += i;
                        sumT2 += i * i;
                        sumT3 += i * i * i;
                        sumT4 += i * i * i * i;
                        sumNum += data[i - 1].Value.Num;//
                        sumNumT += i * data[i - 1].Value.Num;//
                        sumNumT2 += i * i * data[i - 1].Value.Num;//
                    }



                    double[,] a1 = new double[,] { { n1, sumT, sumT2 }, { sumT, sumT2, sumT3 }, { sumT2, sumT3, sumT4} };
                    double[] b1 = new double[] { sumNum, sumNumT, sumNumT2 };
                    double[] x1 = GausSolver(yravCount, a1, b1);

                    //

                    newNum = x1[0] + (x1[1] * (n1 + 1)) + (x1[2] * Math.Pow(n1+1, 2));
                    dataGridView1.Rows.Insert(data.Count(), data[data.Count() - 1].Key + 1, newNum);
                    chart1.Series[0].Points.DataBindXY(data.Keys, data.GetNums());
                    chart1.Series[0].Points.AddXY(data[data.Count() - 1].Key + 1, newNum);

                    double sum = 0;
                    for (int i = 0; i < data.Count(); i++)
                    {
                        sum += Math.Pow(data[i].Value.Num - (x1[0] + (x1[1] * (i + 1)) + (x1[2] * Math.Pow(i + 1, 2))), 2);
                    }

                    er = Math.Sqrt(sum / (data.Count() - yravCount));
                }

                double[] GausSolver(int n, double[,] a, double[] b)
                {
                    double[] x = new double[n];

                    for (int k = 0; k < n - 1; k++)
                    {
                        for (int i = k + 1; i < n; i++)
                        {
                            for (int j = k + 1; j < n; j++)
                            {
                                a[i, j] = a[i, j] - a[k, j] * (a[i, k] / a[k, k]);
                            }
                            b[i] = b[i] - b[k] * a[i, k] / a[k, k];
                        }
                    }
                    double s;//Временная переменная
                    for (int k = n - 1; k >= 0; k--)
                    {
                        s = 0;
                        for (int j = k + 1; j < n; j++)
                            s = s + a[k, j] * x[j];
                        x[k] = (b[k] - s) / a[k, k];
                    }
                    return x;

                }//функция решения СЛАУ
            }
        }
        /// <summary>
        /// Метод скользящей средней
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AvarageChart(object sender, EventArgs e)
        {
            double lvlSkolz;

            if ((chart1.Series.Count == 1))
            {

                System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    ChartArea = "ChartArea1",
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                    Color = System.Drawing.Color.Green,
                    Legend = "Legend1",
                    Name = "Метод скользящей средней",
                    YValuesPerPoint = 7
                };
                this.chart1.Series.Add(series2);
            }
            else
            {
                chart1.Series[1].Points.Clear();
            }

            for (int i=1; i< data.Count()-1; i++)
            {
                lvlSkolz = (data[i - 1].Value.Num + data[i].Value.Num + data[i + 1].Value.Num) / 3;
                chart1.Series[1].Points.AddXY(data[i].Key, lvlSkolz);
            }
        }

        private void ComboBoxMethod_SelectedIndexChanged(object sender, EventArgs e)//При изменении метода прогноза
        {
            if (!(fileName == null))
                OpenFile(fileName);
            if (!(chart1.Series.Count == 1) && ComboBoxMethod.SelectedIndex != 3)
            {
                    chart1.Series.RemoveAt(1);
                //chart1.Series[1].Points.Clear();
            }
            switch (ComboBoxMethod.SelectedIndex)
            {
                case 0://абсолютн
                    HideAllMethods();
                    PeriodNumeric.Visible = true;
                    LabelDalnPrognoza.Visible = true;
                    break;
                case 1://геометр
                    HideAllMethods();
                    PeriodNumeric.Visible = true;
                    LabelDalnPrognoza.Visible = true;
                    break;
                case 2://Стационарный
                    HideAllMethods();
                    labelErrorIndc.Visible = true;
                    textBoxErrorIndic.Visible = true;
                    labelErrorForecast.Visible = true;
                    textBoxErrorForecast.Visible = true;
                    break;
                case 3://Уровень тренда
                    HideAllMethods();
                    labelErrorIndc.Visible = true;
                    textBoxErrorIndic.Visible = true;
                    buttonSgladitb.Visible = true;
                    labelMethodTrend.Visible = true;
                    comboBoxMethods.Visible = true;
                    break;
            }
        }
        private void HideAllMethods()
    {
        labelErrorIndc.Visible = false;
        textBoxErrorIndic.Visible = false;
        PeriodNumeric.Visible = false;
        LabelDalnPrognoza.Visible = false;
        labelMethodTrend.Visible = false;
        comboBoxMethods.Visible = false;
        buttonSgladitb.Visible = false;
        labelErrorForecast.Visible = false;
        textBoxErrorForecast.Visible = false;
        if (!(chart1.Series.Count == 1))
        {
                chart1.Series.RemoveAt(1);
            //chart1.Series[1].Points.Clear();
        }
        }//Скрывает интерфейс разных методов прогноза

        private void PdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Объект документа пдф
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetPageSize(PageSize.A4.Rotate());
            try
            {
            //Создаем объект записи пдф-документа в файл
            PdfWriter.GetInstance(doc, new FileStream("pdfTables.pdf", FileMode.Create));

            }
            catch (IOException)
            {

                    MessageBox.Show("Пожалуйста, для сохранения файла, закройте файл pdf и нажмите ОК");
                try
                {
                    PdfWriter.GetInstance(doc, new FileStream("pdfTables.pdf", FileMode.Create));
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалост вывести в pdf файл!");
                    return;
                }
                
            }

            //Открываем документ
            doc.Open();

            //Определение шрифта необходимо для сохранения кириллического текста
            //Иначе мы не увидим кириллический текст
            //Если мы работаем только с англоязычными текстами, то шрифт можно не указывать
            string FONT_LOCATION = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.TTF"); //определяем В СИСТЕМЕ(чтобы не копировать файл) расположение шрифта arial.ttf
            BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED); //создаем шрифт
            iTextSharp.text.Font fontParagraph = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL); //регистрируем + можно задать параметры для него(17 - размер, последний параметр - стиль)
            //Создаем объект таблицы и передаем в нее число столбцов таблицы из нашего датасета
            PdfPTable table = new PdfPTable(dataGridView1.ColumnCount);//MyDataSet.Tables[i].Columns.Count);
            //Добавим в таблицу общий заголовок
            
            PdfPCell cell = new PdfPCell(new Phrase(""));
            //cell.Colspan = dataGridView1.ColumnCount;
            //cell.HorizontalAlignment = 1;
            ////Убираем границу первой ячейки, чтобы был как заголовок
            //cell.Border = 0;
            //table.AddCell(cell);

            //Сначала добавляем заголовки таблицы
            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            {
                cell = new PdfPCell(new Phrase(new Phrase(dataGridView1.Columns[j].HeaderText, fontParagraph)));
                //Фоновый цвет (необязательно, просто сделаем по красивее)
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(cell);
            }
            //Добавляем все остальные ячейки
            for (int j = 0; j < dataGridView1.RowCount - 1; j++)
            {
                for (int k = 0; k < dataGridView1.ColumnCount; k++)
                {
                    if (dataGridView1.Rows[j].Cells[k].Value != null)
                        table.AddCell(new Phrase(Math.Round(Convert.ToDouble(dataGridView1.Rows[j].Cells[k].Value), 2).ToString(), fontParagraph));
                }
            }
            //if (ComboBoxMethod.SelectedIndex != 1 && textBoxErrorIndic.Text != "")
                //fileCSV += "Среднеквадратическая ошибка расчёта ;" + textBoxErrorIndic.Text + ";";
            //table.AddCell(new Phrase(Math.Round(Convert.ToDouble(textBoxErrorIndic.Text), 2).ToString(), fontParagraph));
            // table.AddCell(new Phrase("123", fontParagraph));
            /*if (ComboBoxMethod.SelectedIndex == 2 && textBoxErrorForecast.Text != "")
                fileCSV += "Ошибка прогноза ;" + textBoxErrorForecast.Text + ";";*/

            chart1.SaveImage(Application.StartupPath + @"\1.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Application.StartupPath + @"/1.bmp");
            jpg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
            //doc.NewPage();
            doc.Add(jpg);

            //Добавляем таблицу в документ
            doc.Add(table);
            //Закрываем документ
            doc.Close();

            Process.Start("pdfTables.pdf");
        }

        private void CsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileCSV = "";
            string name = "Forecast";
            for (int f = 0; f < dataGridView1.ColumnCount; f++)
            {
                fileCSV += (dataGridView1.Columns[f].HeaderText + ";");

            }
            fileCSV += "\t\n"; //тут была загвоздка
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {

                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1[j, i].Value != null)
                        fileCSV += (dataGridView1[j, i].Value).ToString() + ";";
                }

                fileCSV += "\t\n";
            }
            if (ComboBoxMethod.SelectedIndex != 1 && textBoxErrorIndic.Text != "")
                fileCSV += "Среднеквадратическая ошибка расчёта ;" + textBoxErrorIndic.Text + ";";
            if (ComboBoxMethod.SelectedIndex == 2 && textBoxErrorForecast.Text != "")
                fileCSV += "Ошибка прогноза ;" + textBoxErrorForecast.Text + ";";
            StreamWriter wr = new StreamWriter(name + ".csv", false, Encoding.GetEncoding("windows-1251"));
            wr.Write(fileCSV);
            wr.Close();
            Process.Start("Forecast.csv");
        }
    }
}
