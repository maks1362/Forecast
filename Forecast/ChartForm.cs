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
    public partial class ChartForm : Form
    {
        public ChartForm(Data data)
        {
            InitializeComponent();

            FillData(data);
        }

        public void FillData(Data data)
        {
            chart1.Series["Serie"].Points.DataBindXY(data.Keys, data.GetNums());
        }
    }
}
