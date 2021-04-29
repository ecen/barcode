using System;
using System.Collections.Generic;
using System.Text;

namespace barcode
{
    public class BarcodeProgram
    {
        static void Main(string[] args)
        {
            string[] lines = FileToBinaryLines(args[0]);
            foreach (string line in lines)
            {
                Console.WriteLine($"{line}");
            }
        }

        public static string[] FileToBinaryLines(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            string[] outputLines = new string[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                outputLines[i] = ToBinaryString(lines[i]);
            }
            return outputLines;
        }

        public static string ToBinaryString(string barcode)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in barcode)
            {
                if (c == '▍') sb.Append('1');
                else if (c == ' ') sb.Append('0');
            }
            return sb.ToString();
        }

        public static string StripEndGuards(string barcode)
        {
            if (barcode.StartsWith("101") && barcode.EndsWith("101"))
            {
                return barcode.Substring(3, barcode.Length - 6);
            } else
            {
                throw new FormatException("Barcode does not contain end guard patterns.");
            }
        }

        public static string[] SplitOnCenterGuard(string barcode)
        {
            if (barcode.Contains("01010"))
            {
                string[] split = barcode.Split("01010");
                return split;
            }
            else
            {
                throw new FormatException("Barcode does not contain center guard pattern.");
            }
        }

        public static string[] SplitIntoWords(string barcode)
        {
            if (barcode.Length % 7 != 0)
            {
                throw new FormatException("Barcode does not contain full words.");
            }

            string[] words = new string[barcode.Length / 7];
            for (int i = 0;  i < words.Length; i++)
            {
                words[i] = barcode.Substring(i * 7, 7);
            }
            return words;
        }
    }
}
