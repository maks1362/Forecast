using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Web.UI.DataVisualization.Charting; //Критерий фишера

namespace Forecast
{
    /*static class ForecastMethod
    {
        //static 
    }*/

    public partial class Form1 : Form
    {
        private Data data;
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

        public void CreateGrid(DataGridView grid)
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


        public Form1()
        {
            try
            {
                InitializeComponent();

                ComboBoxMethod.DataSource = forecasts;

                CreateGrid(dataGridView1);
                //grid = dataGridView1;//глобальная переменная

                OpenFile("D:\\kl\\вавкп.csv");
                ComboBoxMethod.SelectedIndex = 3;

                forecast_Click(null, null);

                //OpenToolStripMenuItem_Click(null, null);
                //chart1.Series.Add("ser");
                /*chartForm = new ChartForm(data);
                chartForm.Show();*/
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
            data = new Data(myOpenFileDialog.FileName);
            dataGridView1.Rows.Clear();//очищаю таблицу
            string[] row = new string[11];
            DataGridViewRow r = new DataGridViewRow();
            for(int i=0; i< data.Count(); i++)
            {
                dataGridView1.Rows.Insert(
                    dataGridView1.RowCount-1, //В какое место таблицы вставить строку
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
            }//Заполняю DataGridView
        }//Диалог открытия файла и заполнение DataGridView

        private void OpenFile(string FileName)
        {

            data = new Data(FileName);
            dataGridView1.Rows.Clear();//очищаю таблицу
            string[] row = new string[11];
            DataGridViewRow r = new DataGridViewRow();
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
        }//Открыть файл и заполнить DataGridView

        private void forecast_Click(object sender, EventArgs e)
        {
            //var a = ComboBoxMethod.SelectedIndex; //Выбранный способ прогноза(индекс)
            switch (ComboBoxMethod.SelectedIndex)
            {
                case 0://абсолютн
                    ForecastAbs();
                    break;
                case 1://геометр
                    ForecastGeom();
                    break;
                case 2:
                    ForecastStat();
                    break;
                case 3:

                    break;
            }


        }//Прогноз

        private void ForecastAbs()
        {
            if (data != null)
            {
                chart1.Series["Serie"].Points.DataBindXY(data.Keys, data.GetNums());
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
                chart1.Series["Serie"].Points.DataBindXY(data.Keys, data.GetNums());

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
                { 
                    double avarLvl1 = 0;//Средний уровень половины ряда
                    double avarLvl2 = 0;
                    int countNum1 = (data.Count() / 2);
                    int countNum2 = (data.Count() - data.Count() / 2);
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
                    sigma1 = Math.Sqrt(sigma1 / (countNum1-1));
                    sigma2 = Math.Sqrt(sigma2 / (countNum2-1));

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
                    double fisherMy = (sigma1 > sigma2) ? (sigma1/sigma2) : (sigma2/sigma1);//Критерий Фишера


                    Chart chart1 = new Chart();//Нужен для формул статистики
                    var fisherTable = chart1.DataManipulator.Statistics.InverseFDistribution(0.05, countNum1-1, countNum2-1);//Формула табличного значения Фишера
                    if (fisherMy > fisherTable)
                    {
                        /*MessageBox.Show("Фактическое значение F-критерия больше или табличного!");
                        return;*/

                        double tStudent = (Math.Abs(avarLvl1 - avarLvl2)) / Math.Sqrt( ((sigma1 * sigma1) / countNum1) + ((sigma2 * sigma2) / countNum2) );
                        double part1 = ((sigma1 * sigma1) / countNum1) + ((sigma2 * sigma2) / countNum2);
                        double part2 = (Math.Pow(((sigma1 * sigma1) / countNum1), 2) / (countNum1 + 1)) + (Math.Pow(((sigma2 * sigma2) / countNum2), 2) / (countNum2 + 1));
                        double f = part1 / Math.Sqrt(part2);
                        double tStudentTable = chart1.DataManipulator.Statistics.InverseTDistribution(0.05, (int)Math.Round(f-2));
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
                        sigma = Math.Sqrt((((sigma1 * sigma1) * (countNum1 - 1)) + (sigma2 * sigma2 * (countNum2 - 1))) / (countNum1 + countNum2 - 2));
                        tStudent = (Math.Abs(avarLvl1 - avarLvl2)) / (sigma * Math.Sqrt(1.0 / countNum1 + 1.0 / countNum2));
                        double tStudentTabl = chart1.DataManipulator.Statistics.InverseTDistribution(0.05, countNum1 + countNum2 - 2);
                        if (tStudent > tStudentTabl)
                    {
                        MessageBox.Show("Фактическое значение T-критерия больше или табличного. Гипотеза о стационарности ряда опровергнута!", "Прогнозирование");
                        return;
                    }//Проверка Студента
                    }
                }


            }

        }
    }
}
