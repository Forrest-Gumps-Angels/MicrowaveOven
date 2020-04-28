using System;
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
        private Display _display;
        private IOutput _output;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
        }

        [TestCase(1, 1)]
        [TestCase(10, 10)]
        [TestCase(30, 30)]
        [TestCase(60, 60)]
        public void Display_ShowTime_ValidValues_Success(int _min, int _sec)
        {
            _display.ShowTime(_min, _sec);
            _output.Received().OutputLine(Arg.Is<string>(str =>str.Contains($"Display shows: {_min:D2}:{_sec:D2}")));
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(30)]
        [TestCase(60)]
        [TestCase(99)]
        public void Display_ShowPower_ValidValues_Success(int _power)
        {
            _display.ShowPower(_power);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"Display shows: {_power} W")));
        }

        [Test]
        public void Display_Clear_Success()
        {
            _display.Clear();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }
    }
}
