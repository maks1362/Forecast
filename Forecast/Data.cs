using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast
{
    /// <summary>
    /// Элемент в коллекции признаков, хранит также аналитические показатели
    /// </summary>
    public class Element
    {
        //public int Year { get; set; }

        public double Num { get; set; }
        public double IncreaseOrder { get; set; }//Абсолютный прирост цепной(разница с предыдущим
        public double IncreaseBase { get; set; }//Абсолютный прирост базисный

        public double RateGrowthOrder { get; set; }//Темп роста цепной(отношение к предыдущему)
        public double RateGrowthBase { get; set; }//Темп роста базисный

        public Element() { }
        public Element(double num)
        {
            Num = num;
        }
    }

    /// <summary>
    /// Хранит данные временного ряда и 
    /// аналитические показатели
    /// </summary>
    class Data
    {   

        private Dictionary<int, Element> DataCol { get; }

        public Data(string FileName)
        {
            DataCol = ReadFromFile(FileName);
        }

        private Dictionary<int, Element> ReadFromFile(string FileName) //FileName - Путь к файлу
        {
            Dictionary<int, Element> result = new Dictionary<int, Element>();
            
            //провести проверку на правильность данных и занести их в коллекцию с помощью метода ниже

            result.Add(1998, new Element(2.8));//Пример для добавления точки, где 1998 - год, 2.8 - значение признака

            return result;
        }
            
        private void CalcIndicators()
        {
            for (int i = 1; i < DataCol.Count; i++)
            {
                DataCol.ElementAt(i).Value.IncreaseOrder = DataCol.ElementAt(i).Value.Num - DataCol.ElementAt(i-1).Value.Num;//Абсолютный прирост цепной
                DataCol.ElementAt(i).Value.IncreaseBase = DataCol.ElementAt(i).Value.Num - DataCol.ElementAt(0).Value.Num;//Абсолютный прирост базисный

                DataCol.ElementAt(i).Value.RateGrowthOrder = DataCol.ElementAt(i).Value.Num / DataCol.ElementAt(i - 1).Value.Num;//Темп роста цепной
                DataCol.ElementAt(i).Value.RateGrowthBase = DataCol.ElementAt(i).Value.Num / DataCol.ElementAt(0).Value.Num;//Темп роста базисный
            }

        }
        }

        //private void DrawGraphic();
        //private void FillTable();

    }
}
