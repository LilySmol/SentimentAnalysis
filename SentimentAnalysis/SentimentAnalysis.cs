using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis
{
    class SentimentAnalysis
    {
        private string[] positive = new string[] { "😀", "😃", "😄", "😁", "😅", "😆", "😂", "😉", ")",
            "😊","☺","😇","😗","😙","😚","😘","😍","😋","😜","😛","😝","😏","😌","😺","😸","😹","😻","👻","😈","👍", ":)", "=)",
            " потрясающ", " да ", "спасибо", "круто", "классно"
        };

        private string[] negative = new string[] {"😶","😐","😑","😬","😒","😔","😪","😕","😟","☹","😮","😯","😲","😳","😦","😧", ":с", ":С",
            "😨","😰","😥","😢","😭","😱","😖","😣","😷","😫","😩","😓","😞","😵","😤","😠","😡","👿","🙀","😿","😾","👎", "(", ":(", "=(",
            " фу ", " не очень ", " нет " }; 
        public void setVocabulary(List<string> col, double[,] arr)
        {
            List<string> pos = new List<string>();
            List<string> neg = new List<string>();
            //yourself vocabulary
            //Console.WriteLine("pos -");
            for (int i = 0; i < positive.Length; i++)
            {
                pos.Add(positive[i]);
                //Console.Write(" " + pos[i]);
            }
            //Console.WriteLine("neg -");
            for (int i = 0; i < negative.Length; i++)
            { 
                neg.Add(negative[i]);
                //Console.Write(" "+neg[i]);
            }
            Console.WriteLine("n0 -");
            int[] column = ;// select column in Enumerable.Range(0, arr.GetLength(1));
            //pos.Add(arr[1]);
            double max = arr[0, 0];
            /*for (int j = 0; j < arr.GetLength(1); j++)
            {
                switch (maximum(arr,column))
                {
                    case arr[].GetUpperBound(1):

                        break;
                    default:
                        break;
                }

            }*/

            Console.WriteLine(" "+negative);
        }

        private int maximum(object v)
        {
            return (int)v;
        }

        public string getEmotion(string text)
        {
            text = text.ToLower();
            int positiveCount = 0;
            int negativeCount = 0;
            foreach (string positiveWord in positive)
            {
                if (text.Contains(positiveWord))
                {
                    positiveCount++;
                }
            }
            foreach (string negativeWord in negative)
            {
                if (text.Contains(negativeWord))
                {
                    negativeCount++;
                }
            }
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
