using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infint_დავალება2_C__
{
    internal class Infint
    {
        public List<int> Digits = new List<int>();
        public bool IsNegative = false;
        //constructor
        public Infint(string num)
        {
            if (num.StartsWith('-'))
            {
                IsNegative = true;
                num = num.Substring(1);
            }
            for (int i = num.Length - 1; i >= 0; i--)
            {
                Digits.Add(int.Parse(num[i].ToString()));
            }
            RemoveZeroes(); //removing Extra zeros
        }

        //function that removes zeroes
        public void RemoveZeroes()
        {
            while (Digits.Count > 1 && Digits.Last() == 0)
                Digits.RemoveAt(Digits.Count - 1);
            if (Digits.Count == 1 && Digits[0] == 0)
                IsNegative = false;
        }

        //CompareTo function
        public int CompareTo(Infint other)
        {
            if (this.IsNegative != other.IsNegative)
                return this.IsNegative ? -1 : 1;
            if (this.Digits.Count != other.Digits.Count)
            {
                if (this.IsNegative)
                    return this.Digits.Count > other.Digits.Count ? -1 : 1;
                else
                    return this.Digits.Count > other.Digits.Count ? 1 : -1;
            }

            for (int i = Digits.Count - 1; i >= 0; i--)
            {
                if (this.Digits[i] != other.Digits[i])
                {
                    if (this.IsNegative)
                        return this.Digits[i] > other.Digits[i] ? -1 : 1;
                    else
                        return this.Digits[i] > other.Digits[i] ? 1 : -1;
                }
            }
            return 0;
        }

        //Plus function
        public Infint Plus(Infint other)
        {
            if (this.IsNegative == other.IsNegative)
            {
                Infint result = new Infint("0");
                result.Digits.Clear();
                int carry = 0;
                int max = Math.Max(this.Digits.Count, other.Digits.Count);

                for (int i = 0; i < max || carry > 0; i++)
                {
                    int sum = carry;
                    if (i < this.Digits.Count) sum += this.Digits[i];
                    if (i < other.Digits.Count) sum += other.Digits[i];
                    carry = sum / 10;
                    result.Digits.Add(sum % 10);
                }
                result.IsNegative = this.IsNegative;
                result.RemoveZeroes();
                return result;
            }
            else
            {
                if (this.IsNegative)
                {
                    Infint temp = new Infint(this.ToString());
                    temp.IsNegative = false;
                    return other.Minus(temp);
                }
                else
                {
                    Infint temp = new Infint(other.ToString());
                    temp.IsNegative = false;
                    return this.Minus(temp);
                }
            }
        }

        //Minus Method
        public Infint Minus(Infint other)
        {
            if (this.IsNegative != other.IsNegative)
            {
                Infint temp = new Infint(other.ToString());
                temp.IsNegative = !other.IsNegative;
                return this.Plus(temp);
            }
            if (this.CompareTo(other) < 0)
            {
                var val = other.Minus(this);
                val.IsNegative = !this.IsNegative;
                return val;
            }

            List<int> tempDigits = new List<int>(this.Digits);
            Infint result = new Infint("0");
            result.Digits.Clear();
            int max = Math.Max(this.Digits.Count, other.Digits.Count);

            for (int i = 0; i < max; i++)
            {
                int sub = 0;
                if (i < tempDigits.Count) sub += tempDigits[i];
                if (i < other.Digits.Count)
                {
                    if (sub < other.Digits[i])
                    {
                        int j = i + 1;
                        while (j < tempDigits.Count && tempDigits[j] == 0)
                        {
                            tempDigits[j] = 9;
                            j++;
                        }
                        if (j < tempDigits.Count)
                        {
                            tempDigits[j] -= 1;
                            sub += 10;
                        }
                    }
                    sub -= other.Digits[i];
                }
                result.Digits.Add(sub);
            }

            result.RemoveZeroes();
            return result;
        }

        public override string ToString()
        {
            if (Digits.Count == 0) return "0";
            string s = IsNegative ? "-" : "";
            for (int i = Digits.Count - 1; i >= 0; i--)
            {
                s += Digits[i];
            }
            return s;
        }
    }

    //Program Main
    internal class Program
    {
        static void Main(string[] args)
        {
            Infint num1 = new Infint("-120");
            Infint num2 = new Infint("20");
            Console.WriteLine(num1.Minus(num2).ToString()); //it will print -140 in this situation
        }
    }
}