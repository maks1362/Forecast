using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

                OpenFile("D:\\Учёба(!)\\Инструменталки(Ивашк)\\Лаб_6 (Проект)\\Исходные данные\\врем_ряд_1.csv");

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
                    forecastAbs();
                    break;
                case 1://геометр
                    forecastGeom();
                    break;
                case 2:

                    break;
                case 3:

                    break;
            }


        }//Прогноз

        private void forecastAbs()
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

        private void forecastGeom()
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
    }
}
