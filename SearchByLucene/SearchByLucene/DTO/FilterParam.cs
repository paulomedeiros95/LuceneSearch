using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchByLucene.DTO
{
    public class FilterParam : ICloneable
    {
        private string freeText = string.Empty;

        public string FreeText { get => freeText.Trim(); set => freeText = value; }

        public string Line { get; set; } = string.Empty;

        public string Assembler { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string Year { get; set; } = string.Empty;

        public string Engine { get; set; } = string.Empty;

        public int Limite { get; set; } = 100;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public FilterParam HandlerXBar()
        {
            var newString = FreeText.ToLower().Replace('x', '/');

            var newFreeText = (FilterParam)this.Clone();
            newFreeText.FreeText = newString;
            return newFreeText;
        }

        public FilterParam HandlerNoSearchWords(List<string> noSearchWords)
        {
            var newString = FreeText.ToLower();

            foreach (var noSearchWord in noSearchWords)
            {
                newString = newString.Replace($" {noSearchWord.ToLower()} ", " ");
            }

            var newFreeText = (FilterParam)this.Clone();
            newFreeText.FreeText = newString;
            return newFreeText;
        }

        public FilterParam HandlerAroR()
        {
            var newString = FreeText.ToLower().Replace("aro", "r");

            var newFreeText = (FilterParam)this.Clone();
            newFreeText.FreeText = newString;
            return newFreeText;
        }

        public FilterParam HandlerNameWithNumber(bool isReverse = false)
        {
            var parts = FreeText.Split(' ').ToList();
            if (isReverse) parts.Reverse();
            var newString = string.Empty;
            var previourPart = string.Empty;

            foreach (var part in parts)
            {
                if (string.IsNullOrWhiteSpace(newString))
                {
                    newString = part;
                }
                else if (int.TryParse(part, out int number) && !int.TryParse(previourPart, out int previousnumber))
                {
                    newString += number;
                }
                else
                {
                    newString += $" {part}";
                }

                previourPart = part;
            }

            var newFreeText = (FilterParam)this.Clone();
            newFreeText.FreeText = newString;
            return newFreeText;
        }

        public FilterParam HandlerConcatFull(bool isReverse = false)
        {
            var parts = FreeText.Split(' ').ToList();
            if (isReverse) parts.Reverse();
            var newString = string.Empty;

            foreach (var part in parts)
            {
                if (string.IsNullOrWhiteSpace(newString))
                {
                    newString = part;
                }
                else
                {
                    newString += part;
                }
            }

            var newFreeText = (FilterParam)this.Clone();
            newFreeText.FreeText = newString;
            return newFreeText;
        }
    }
}
