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

        public static string StripGuardPattern(string barcode)
        {
            if (barcode.StartsWith("101") && barcode.EndsWith("101"))
            {
                return barcode.Substring(3, barcode.Length - 6);
            } else
            {
                throw new FormatException("Barcode does not contain guard pattern.");
            }
        }
    }
}
