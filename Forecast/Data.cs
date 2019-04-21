using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Forecast
{
    /// <summary>
    /// Элемент в коллекции признаков, хранит также аналитические показатели
    /// </summary>
    public class Element
    {
        //public int Year { get; set; }

        public double Num { get; set; }
        /// <summary>
        /// Абсолютный прирост цепной(разница с предыдущим)
        /// </summary>
        public double IncreaseOrder { get; set; }
        
        /// <summary>
        /// Абсолютный прирост базисный
        /// </summary>
        public double IncreaseBase { get; set; }
        /// <summary>
        /// Темп роста цепной(отношение к предыдущему) (коэф)
        /// </summary>
        /// 
        public double RateGrowthOrder { get; set; }

        /// <summary>
        /// Темп роста базисный (коэф)
        /// </summary>
        public double RateGrowthBase { get; set; }
        /// <summary>
        /// Темп прироста цепной (коэф)
        /// </summary>
        public double RateIncreaseOrder { get; set; }

        /// <summary>
        /// Темп прироста базисный (коэф)
        /// </summary>
        public double RateIncreaseBase { get; set; }
        /// <summary>
        /// Абсолютное значение 1% прироста
        /// </summary>
        public double AbsOfPerIncrease { get; set; }
        /// <summary>
        /// Относительное ускорение
        /// </summary>
        public double RelativeAcceler { get; set; }
        /// <summary>
        /// Коэффициент опережения
        /// </summary>
        public double CoefAdvance { get; set; }

        public Element() { }
        public Element(double num)
        {
            Num = num;
            IncreaseOrder = 0;
            IncreaseBase = 0;
            RateGrowthOrder = 0;
            RateGrowthBase = 0;
        }
    }

    /// <summary>
    /// Хранит данные временного ряда и 
    /// аналитические показатели
    /// </summary>
    class Data
    {

        private Dictionary<int, Element> DataCol { get; }

        public int getKey(int i)
        {
            return DataCol.Keys.ElementAt(i);
        }
        public int Count()
        {
            return DataCol.Count;
        }
        public void setKey(int i)
        {
            //DataCol DataCol.Keys.ElementAt(i) = 
        }

        public KeyValuePair<int, Element> this[int index] => this.DataCol.ElementAt(index);


        public Data(string FileName)
        {
            DataCol = ReadFromFile(FileName);
            CalcIndicators();
        }

        private Dictionary<int, Element> ReadFromFile(string FileName) //FileName - Путь к файлу
        {
            Dictionary<int, Element> result = new Dictionary<int, Element>();

            //Диме: провести проверку на правильность данных и занести их в коллекцию с помощью метода ниже
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                //InitialDirectory = @"C:\users\qq\desktop\tabul\",
                DefaultExt = "*.csv",
                Filter = "Files (*.csv)|*.csv"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] rows = File.ReadAllLines(openFileDialog1.FileName);
                    if (rows.Length > 0)
                    {
                        //Временные переменные
                        int key;
                        double value = 0;
                        Element elem;
                        string[] row;
                        string erMes = "";
                        string misMes = "";
                        //
                        for (int i = 0; i < rows.Length; i++)//Чтение из файла
                        {
                            row = rows[i].Split(';');
                            if (!Int32.TryParse(row[0], out key))
                            {
                                if (Double.TryParse(row[0], out value))
                                {
                                    erMes += erMes += '\"' + rows[i] + "\"Дробное значение в качестве года!\n";
                                    continue;
                                }
                                if (!Int32.TryParse(TrimLettersAndSemicolon(row[0]), out key))
                                {
                                    erMes += '\"' + rows[i] + "\"\n";
                                    continue;//Пропускаю заполнение этого значения ряда
                                }
                                else
                                    misMes += '\"'+ rows[i] +"\":   " +"значение "+ " \"" + row[0] +"\" "+ " преобразовано в " +'\"'+ key.ToString() +"\"\n";
                            }

                            if (!Double.TryParse(row[1], out value))
                            {
                                if (!Double.TryParse(TrimLettersAndSemicolon(row[1]), out value))
                                {
                                    erMes += '\"' + rows[i] + "\"\n";
                                    continue;
                                }
                                else
                                    misMes += '\"' + rows[i] + "\":   " + "значение " + " \"" + row[1] + "\" " + " преобразовано в " + '\"' + value.ToString() + "\"\n";
                            }
                            
                            elem = new Element(value);
                            result.Add(key, elem);

                            //gridView.Columns.Add();
                        }
                        if (misMes != "")
                            MessageBox.Show("Автоисправление входных данных:\n" + misMes, "Внимание");
                        if (erMes != "")
                            MessageBox.Show("Значения не приняты во внимание:\n" + erMes, "Ошибки в чтении данных");
                    }
                    else MessageBox.Show("Файл пуст", "Ошибка");
                    
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return result;
        }
        /// <summary>
        /// Возвращает строку оставив только цифры
        /// </summary>
        /// <param name="str">Изменяемая строка</param>
        /// <returns></returns>
        static public string TrimLettersAndSemicolon(string str)
        {
            //string result;

            for (int i=0; i< str.Length; i++)
            {
                if (!Char.IsDigit(str[i]))
                    str = str.Remove(i, 1);
            }
            return str;
        }

        private void CalcIndicators()
        {
            for (int i = 1; i < DataCol.Count; i++)
            {
                this[i].Value.IncreaseOrder = this[i].Value.Num - this[i-1].Value.Num;//Абсолютный прирост цепной
                this[i].Value.IncreaseBase = this[i].Value.Num - this[0].Value.Num;//Абсолютный прирост базисный

                this[i].Value.RateGrowthOrder = this[i].Value.Num / this[i-1].Value.Num;//Темп роста цепной
                this[i].Value.RateGrowthBase = this[i].Value.Num / this[0].Value.Num;//Темп роста базисный

                this[i].Value.RateIncreaseOrder = this[i].Value.IncreaseOrder / this[i-1].Value.Num*100;//Темп прироста цепной
                this[i].Value.RateIncreaseBase = this[i].Value.IncreaseBase / this[0].Value.Num * 100;//Темп прироста базисный

                this[i].Value.AbsOfPerIncrease = this[i].Value.IncreaseOrder / this[i].Value.RateIncreaseOrder;//Абсолютное значение процента прироста

                {
                    /*
                    DataCol.ElementAt(i).Value.IncreaseOrder = 1;
                    DataCol.ElementAt(i).Value.IncreaseOrder = this[0].Value.Num - DataCol.ElementAt(i-1).Value.Num; ; //= DataCol[i].Num - DataCol[i- 1].Num;//Абсолютный прирост цепной
                    DataCol.ElementAt(i).Value.IncreaseBase = this.ElementAt(i).Value.Num - DataCol.ElementAt(0).Value.Num;//Абсолютный прирост базисный
                    //DataCol[i] = DataCol[i + 1];
                    DataCol.ElementAt(i).Value.RateGrowthOrder = this.ElementAt(i).Value.Num / DataCol.ElementAt(i - 1).Value.Num;//Темп роста цепной
                    DataCol.ElementAt(i).Value.RateGrowthBase = this.ElementAt(i).Value.Num / DataCol.ElementAt(0).Value.Num;//Темп роста базисный
                    */
                }//Хрень!
            }
            for (int i = 2; i< DataCol.Count; i++)
            {
                this[i].Value.RelativeAcceler = this[i].Value.RateGrowthOrder - this[i-1].Value.RateGrowthOrder;//Относительный ускорение
                this[i].Value.CoefAdvance = this[i].Value.RateGrowthOrder / this[i-1].Value.RateGrowthOrder; //Коэффициент опережения

            }
        }

    }
}
        //private void DrawGraphic();
        //private void FillTable();
