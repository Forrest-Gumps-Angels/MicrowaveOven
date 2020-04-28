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

        [Test]
        public void Success()
        {
            int _min = 30;
            int _sec = 10;
            _display.ShowTime(_min, _sec);
            _output.Received().OutputLine(Arg.Is<string>(str =>str.Contains($"Display shows: {_min:D2}:{_sec:D2}")));
        }

    }
}
