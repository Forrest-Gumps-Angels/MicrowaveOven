using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IntegrationStep1
    {
        private Light _light;
        private  IOutput _output;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
        }

        [Test]
        public void TurnLightOn_LightIsAlreadyOff_OutputIsExped()
        {
            _light.TurnOn();
            _output.Received().OutputLine(Arg.Is<string>(str =>str.Contains("Light is turned on")));
        }

        [Test]
        public void TurnLightOn_LightIsOn_Failed()
        {
            _light.TurnOn();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void TurnLightOff_LightIsOn_OutputIsValid()
        {
            _light.TurnOff();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

    }
}
