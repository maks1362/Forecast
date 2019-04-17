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
    {   /// <summary>
        /// Список способов прогноза
        /// </summary>
        public static IList<string> forecasts = new List<string>
            {
                { "по средним значениям" },//cattle(скот)
                { "стационарного ряда" },
                { "по уравнению тренда" }
               //     new Element() { Symbol="Sc", Name="Scandium", AtomicNumber=21}},
            };

        public Form1()
        {
            InitializeComponent();
            ComboBoxMethod.DataSource = forecasts;
            var a = ComboBoxMethod.SelectedIndex; //Выбранный способ прогноза(индекс)

            Data data = new Data("FileName");
        }
    }
}
