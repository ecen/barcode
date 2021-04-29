﻿using barcode;
using NUnit.Framework;
using System;

namespace barcode_test
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToBinaryStringSimple()
        {
            string s = "▍  ▍ ";
            string expected = "10010";
            Assert.AreEqual(BarcodeProgram.ToBinaryString(s), expected);
        }

        [Test]
        public void ToBinaryStringNewline()
        {
            string s = "▍  ▍ \n";
            string expected = "10010";
            Assert.AreEqual(BarcodeProgram.ToBinaryString(s), expected);
        }

        [Test]
        public void ToBinaryStringTestdata()
        {
            string s = "▍ ▍   ▍▍ ▍ ▍▍   ▍  ▍▍  ▍   ▍▍ ▍   ▍▍ ▍   ▍▍ ▍ ▍ ▍ ▍▍▍  ▍ ▍▍  ▍▍ ▍▍ ▍▍  ▍  ▍▍▍ ▍▍  ▍▍ ▍   ▍  ▍ ▍";
            string expected = "10100011010110001001100100011010001101000110101010111001011001101101100100111011001101000100101";
            Assert.AreEqual(BarcodeProgram.ToBinaryString(s), expected);
        }

        [Test]
        public void StripEndGuardsSucceeds()
        {
            string barcode = "1010101101";
            string expected = "0101";
            Assert.AreEqual(BarcodeProgram.StripEndGuards(barcode), expected);
        }

        [Test]
        public void StripEndGuardsFails()
        {
            string barcode = "101010110";
            Assert.That(
                () => BarcodeProgram.StripEndGuards(barcode), 
                Throws.Exception.TypeOf<FormatException>());
        }

        [Test]
        public void SplitOnCenterGuardSucceeds()
        {
            string barcode = "00000001111111000000011111110000000111111101010000000011111110000000111111100000001111111";
            string[] expected = new string[] {
                "000000011111110000000111111100000001111111",
                "000000011111110000000111111100000001111111"
            };
            Assert.AreEqual(BarcodeProgram.SplitOnCenterGuard(barcode), expected);
        }

        [Test]
        public void SplitOnCenterGuardSucceedsOnTrickyGuardPattern()
        {
            string barcode = "00000001111111000000011111110000000111110101010100000011111110000000111111100000001111111";
            string[] expected = new string[] {
                "000000011111110000000111111100000001111101",
                "100000011111110000000111111100000001111111"
            };
            Assert.AreEqual(BarcodeProgram.SplitOnCenterGuard(barcode), expected);
        }

        [Test]
        public void SplitOnWordsSuceeds()
        {
            string barcode = "000000011111110001111";
            string[] expected = new string[] { "0000000", "1111111", "0001111" };
            Assert.AreEqual(BarcodeProgram.SplitIntoWords(barcode), expected);
        }

        [Test]
        public void SplitOnWordsThrowsOnBadLength()
        {
            string barcode = "101000000011111110001111101";
            Assert.That(
                () => BarcodeProgram.SplitIntoWords(barcode),
                Throws.Exception.TypeOf<FormatException>());
        }

        [Test]
        public void TranslationSucceeds()
        {
            string[] leftWords = new string[] { "0001101", "0111101", "0001011" };
            string[] rightWords = new string[] { "1110010", "1000010", "1110100" };
            int[] nrs = new int[] { 0, 3, 9 };

            Assert.AreEqual(BarcodeProgram.translateWords(leftWords, BarcodeProgram.LEFT_HAND), nrs);
            Assert.AreEqual(BarcodeProgram.translateWords(rightWords, BarcodeProgram.RIGHT_HAND), nrs);
        }
    }
}