using System;
using System.Collections.Generic;
using System.Text;

namespace barcode
{
    public class BarcodeProgram
    {
        public static Dictionary<string, int> LEFT_HAND = new Dictionary<string, int>
        {
            { "0001101", 0 },
            { "0011001", 1 },
            { "0010011", 2 },
            { "0111101", 3 },
            { "0100011", 4 },
            { "0110001", 5 },
            { "0101111", 6 },
            { "0111011", 7 },
            { "0110111", 8 },
            { "0001011", 9 },
        };

        // Just the left hand dictionary but each bit is inverted.
        public static Dictionary<string, int> RIGHT_HAND = new Dictionary<string, int>
        {
            { "1110010", 0 },
            { "1100110", 1 },
            { "1101100", 2 },
            { "1000010", 3 },
            { "1011100", 4 },
            { "1001110", 5 },
            { "1010000", 6 },
            { "1000100", 7 },
            { "1001000", 8 },
            { "1110100", 9 },
        };

        static void Main(string[] args)
        {
            string[] lines = FileToBinaryLines(args[0]);
            foreach (string line in lines)
            {
                string noEndGuards = StripEndGuards(line);
                string[] both = SplitOnCenterGuard(noEndGuards);
                string left = both[0];
                string right = both[1];
                string[] lefts = SplitIntoWords(left);
                string[] rights = SplitIntoWords(right);
                int[] leftNrs = translateWords(lefts, LEFT_HAND);
                int[] rightNrs = translateWords(rights, RIGHT_HAND);

                string numberSystem = leftNrs[0].ToString();
                string leftPart = string.Join("", leftNrs[1..leftNrs.Length]);
                string rightPart = string.Join("", leftNrs[1..rightNrs.Length]);
                string moduloCheck = rightNrs[rightNrs.Length].ToString();

                string output = $"{numberSystem} {leftPart} {rightPart} {moduloCheck}";
                Console.WriteLine($"{line}\n{output}");
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
            int lengthMinusEndGuards = 7 * 6 * 2 + 5;
            if (!barcode.Contains("01010")) 
            {
                throw new FormatException("Barcode does not contain center guard.");
            }
            if (barcode.Length != lengthMinusEndGuards)
            {
                throw new FormatException($"Barcode was {barcode.Length} characters but should be {lengthMinusEndGuards}.");
            }
            
            string[] split = new string[2] { 
                barcode.Substring(0, 7 * 6), // Left side
                barcode.Substring(7 * 6 + 5, 7 * 6) // Right side
            };
            return split;
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

        public static int[] translateWords(string[] words, Dictionary<string, int> dict)
        {
            int[] numbers = new int[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                numbers[i] = dict[words[i]];
            }
            return numbers;
        }
    }
}
