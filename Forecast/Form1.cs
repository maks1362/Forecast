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
        public DataGridView grid;
        /// <summary>
        /// Список способов прогноза
        /// </summary>
        public static IList<string> forecasts = new List<string>
            {
                { "по средним значениям" },//cattle(скот)
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
            dataGridView1.DefaultCellStyle = cellStyle1;
            grid.Columns[0].DefaultCellStyle = new DataGridViewCellStyle() { Format = "N0" };

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
            InitializeComponent();
            
            ComboBoxMethod.DataSource = forecasts;
            var a = ComboBoxMethod.SelectedIndex; //Выбранный способ прогноза(индекс)

            CreateGrid(dataGridView1);
            grid = dataGridView1;//глобальная переменная

            openToolStripMenuItem_Click(null, null);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data data = new Data("FileName");
            grid.Rows.Clear();//очищаю таблицу
            string[] row = new string[11];
            DataGridViewRow r = new DataGridViewRow();
            for(int i=0; i< data.Count(); i++)//Заполняю DataGridView
            {
                grid.Rows.Insert(
                    grid.RowCount-1, //В какое место таблицы вставить строку
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
                //row[0] = data.getKey(i).ToString();
              /*row[0] = data[i].Key.ToString();
                row[1] = data[i].Value.Num.ToString();
                row[2] = data[i].Value.IncreaseOrder.ToString();
                row[3] = data[i].Value.IncreaseBase.ToString();
                row[4] = data[i].Value.RateGrowthOrder.ToString();
                row[5] = data[i].Value.RateGrowthBase.ToString();
                grid.Rows.Add(row);*/

            //r.SetValues();
            }
        }
    }
}
