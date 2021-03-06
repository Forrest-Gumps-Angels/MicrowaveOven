using System;
using System.IO;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IntegrationStep2
    {
        private StringWriter str;
        private Display _display;
        private Output _output;

        [SetUp]
        public void Setup()
        {
            str = new StringWriter();
            Console.SetOut(str);
            _output = new Output();
            _display = new Display(_output);
        }

        [TestCase(1, 1)]
        [TestCase(10, 10)]
        [TestCase(30, 30)]
        [TestCase(60, 60)]
        public void Display_ShowTime_ValidValues_CorrectOutput(int _min, int _sec)
        {
            _display.ShowTime(_min, _sec);
            Assert.That(str.ToString().Contains($"Display shows: {_min:D2}:{_sec:D2}"));
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(60)]
        [TestCase(99)]
        public void Display_ShowPower_ValidValues_CorrectOutput(int _power)
        {
            _display.ShowPower(_power);
            Assert.That(str.ToString().Contains($"Display shows: {_power} W"));
        }

        [Test]
        public void Display_Clear_CorrectOutput()
        {
            _display.Clear();
            Assert.That(str.ToString().Contains("Display cleared"));
        }
    }
}
