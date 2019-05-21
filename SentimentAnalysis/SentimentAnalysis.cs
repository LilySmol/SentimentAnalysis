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
            " потрясающ", " да "
        };

        private string[] negative = new string[] {"😶","😐","😑","😬","😒","😔","😪","😕","😟","☹","😮","😯","😲","😳","😦","😧", ":с", ":С",
            "😨","😰","😥","😢","😭","😱","😖","😣","😷","😫","😩","😓","😞","😵","😤","😠","😡","👿","🙀","😿","😾","👎", "(", ":(", "=(",
            " фу ", " не очень ", " нет " }; 

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
