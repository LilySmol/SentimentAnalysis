using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis
{
    class SentimentAnalysis
    {

        private static List<string> pos = new List<string>();
        private static List<string> neg = new List<string>();

        private static string[] positive = new string[] { "😀", "😃", "😄", "😁", "😅", "😆", "😂", "😉", ")",
            "😊","☺","😇","😗","😙","😚","😘","😍","😋","😜","😛","😝","😏","😌","😺","😸","😹","😻","👻","😈","👍", "💛", ":)", "=)",
            " потрясающ", " да ", "спасибо", "круто", "классно"
        };

        private static string[] negative = new string[] {"😶","😐","😑","😬","😒","😔","😪","😕","😟","☹","😮","😯","😲","😳","😦","😧", ":с", ":С",
            "😨","😰","😥","😢","😭","😱","😖","😣","😷","😫","😩","😓","😞","😵","😤","😠","😡","👿","🙀","😿","😾","👎", "(", ":(", "=(",
            " фу ", " не очень ", " нет " }; 
        public static void setVocabulary(List<string> col, double[,] arr)
        {
            //yourself vocabulary
            for (int i = 0; i < positive.Length; i++)
            {
                pos.Add(positive[i]);
            }
            for (int i = 0; i < negative.Length; i++)
            { 
                neg.Add(negative[i]);
                //Console.Write(" "+neg[i]);
            }
            
            //Разделение слов по тональности
            for (int iCol = 0; iCol < arr.GetLength(1); iCol++)
            {
                switch (LargestIndexInColumn(iCol, arr))
                {
                    case 0://радость
                        pos.Add(col[iCol]);
                        break;
                    case 1://грусть
                    case 2://злость
                    case 3://страх
                    case 4://отвращение
                        neg.Add(col[iCol]);
                        break;
                    default:
                        break;
                }
            }
        }

        private static int LargestIndexInColumn(int i, double[,] arr)//индекс максимального значения в столбце
        {
            //double[] largeCol = new double[cols];//массив из максимумов столбцов по всем строкам
            /*for (int i = 0; i < cols; i++)
            {*/
            // Функция для поиска максимального элемента каждого столбца.
            int imaxm = 0;
            double maxm = arr[0, i];
            for (int j = 0; j < arr.GetLength(0); j++)
                if (arr[j, i] > maxm)
                {
                    maxm = arr[j, i];
                    imaxm = j;
                }
                //largeCol[i] = maxm;
                Console.WriteLine(maxm);
            return imaxm;
            /*}
            return largeCol;
            */
        }

        public string getEmotion(string text)
        {
            text = text.ToLower();
            int positiveCount = 0;
            int negativeCount = 0;
            foreach (string positiveWord in pos)
            {
                if (text.Contains(positiveWord.ToLower()))
                {
                    positiveCount++;
                }
            }
            Console.WriteLine("Npos {0}", positiveCount);
            foreach (string negativeWord in neg)
            {
                if (text.Contains(negativeWord.ToLower()))
                {
                    negativeCount++;
                }
            }
            Console.WriteLine("Nneg {0}", negativeCount);
            if (positiveCount == 0 && negativeCount == 0)
            {
                return "neutral";
            }
            if (positiveCount >= negativeCount)
            {
                return "positive";
            }           
            return "negative";
        }
    }
}
