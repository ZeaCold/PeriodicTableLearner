﻿using System;
using System.Collections.Generic;

namespace ZC.PeriodicTableLearner.Resources.Extensions
{
    /// <summary>
    /// Int extension class
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// Convert an int32 to a roman number
        /// </summary>
        /// <param name="num">The number to convert</param>
        /// <returns>Returns the roman value of the specified number</returns>
        /// <exception cref="ArgumentException">Exception returned when the number is lower than 1 or higher than 3999</exception>
        public static string ToRoman(this int num)
        {
            if (num < 1 || num > 3999)
                throw new ArgumentException("Roman numerals can only represent numbers between 1 and 3999");

            string[] romanSymbols = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
            int[] romanValues = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };

            List<string> result = new List<string>();

            for (int i = romanValues.Length - 1; i >= 0; i--)
            {
                while (num >= romanValues[i])
                {
                    num -= romanValues[i];
                    result.Add(romanSymbols[i]);
                }
            }

            return string.Join("", result);
        }
    }
}
