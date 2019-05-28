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
    public class Data
    {
        /// <summary>
        /// Возвращает строку оставив только цифры
        /// </summary>
        /// <param name="str">Изменяемая строка</param>
        /// <returns></returns>
        public static string TrimLettersAndSemicolon(string str)
        {
            //string result;

            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsDigit(str[i]))
                    str = str.Remove(i, 1);
            }
            return str;
        }

        private Dictionary<int, Element> DataCol { get; }

        //public int getKey(int i)
        //{
        //    return DataCol.Keys.ElementAt(i);
        //}
        //public void setKey(int i)
        //{
        //    //DataCol DataCol.Keys.ElementAt(i) = 
        //}
        /// <summary>
        /// Возвращает количество элементов в коллекции
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return DataCol.Count;
        }
        public double[] GetNums()
        {
            double[] result = new double[this.DataCol.Count];
            for (int i=0; i < result.Length; i++)
            {
                result[i] = this[i].Value.Num;
            }
            return result;
        }
        public KeyValuePair<int, Element> this[int index] => this.DataCol.ElementAt(index);
        public Dictionary<int, Element>.KeyCollection Keys => this.DataCol.Keys;
        public Dictionary<int, Element>.ValueCollection Values => this.DataCol.Values;

        public Data(string FileName)
        {
            DataCol = ReadFromFile(FileName);
            CalcIndicators();
        }
        /// <summary>
        /// Чтение данных из файла
        /// </summary>
        /// <param name="FileName">Путь к файлу для чтения</param>
        private Dictionary<int, Element> ReadFromFile(string FileName) //FileName - Путь к файлу
        {
            Dictionary<int, Element> result = new Dictionary<int, Element>();

            try
            {
                string[] rows = File.ReadAllLines(FileName, Encoding.Default);//openFileDialog1.FileName);
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
                        if (!Int32.TryParse(row[0], out key))//Проверка ключа
                        {
                            if (Double.TryParse(row[0], out value))//Проверка на дробное число вместо целого
                            {
                                erMes += erMes += '\"' + rows[i] + "\"Дробное или слишком большое значение в качестве года!\n";
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

                        if (!Double.TryParse(row[1], out value))//Проверка значения
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

            return result;
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
