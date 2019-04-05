using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast
{
    abstract class ForecastMethod
    {
        //abstract Data DataAr {get; set;};

        abstract protected void CalcForecast();
        abstract protected void DrawGraphic();
        abstract protected void FillTable();
    }
}
