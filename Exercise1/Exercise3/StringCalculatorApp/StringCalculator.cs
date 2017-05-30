using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculatorApp
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            int result = 0;
            if (string.IsNullOrWhiteSpace(numbers))
            {
                return result;
            }
            var stringDelimiters = GetStringDelimiters(numbers).ToList();
            var charDelimiters = GetDelimiters(numbers);


            string[] numberArray;
            if (stringDelimiters.Count > 0 || charDelimiters.Length > 0)
            {
                foreach (var cD in charDelimiters)
                {
                    stringDelimiters.Add(cD.ToString());
                }
                var delimiters = stringDelimiters.ToArray();
                numberArray = numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                numberArray = numbers.Split(new char[2] { ',', '\n' });
            }

            var negativeNumbers = new List<int>();

            foreach (var numberString in numberArray)
            {
                int nr;
                if (int.TryParse(numberString, out nr))
                {
                    if(nr < 0)
                    {
                        negativeNumbers.Add(nr);
                    }
                    else if (nr < 1000)
                    {
                        result += nr;
                    }
                }
            }

            if(negativeNumbers.Count > 0)
            {
                var message = string.Empty;
                negativeNumbers.ForEach(nr => message += $"{nr} ");

                throw new NegativeNumberNotAllowedException("Negatives not allowed!" + message, negativeNumbers);
            }

            return result;
        }

        private char[] GetDelimiters(string numbers)
        {

            var indexOfNL = numbers.IndexOf('\n');
            var delimiterArray = numbers.Take(indexOfNL + 1).ToArray();
            foreach (var d in delimiterArray)
            {
                if (char.IsDigit(d))
                {
                    return new char[0];
                }
            }

            return delimiterArray;


        }

        public IEnumerable<String> GetStringDelimiters(string numbers)
        {
            
            var pattern = @"\[([^]]*)\]";
            var result = Regex.Matches(numbers, pattern);
            var list = new List<string>();
            foreach (Match m in result)
            {
                var delimiter = m.Value.Replace("[", "").Replace("]", "");
                list.Add(delimiter);
            }
            return list;
        }
    }
}
