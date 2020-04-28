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
        public void Light_TurnOn_LightIsOff_OutputLinePrintsExpectedString()
        {
            _light.TurnOn();
            _output.Received().OutputLine(Arg.Is<string>(str =>str.Contains("Light is turned on")));
        }

        [Test]
        public void Light_TurnOn_AlreadyOn_OutputLineOnlyCalledOnceWithExpectedString()
        {
            _light.TurnOn();
            _light.TurnOn();
            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void Light_TurnOff_LightIsOn_OutputLinePrintsExpectedString()
        {
            _light.TurnOn();
            _light.TurnOff();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

        [Test]
        public void Light_TurnOff_LightIsOff_OutputLineOnlyCalledOnceWithExpectedString()
        {
            _light.TurnOff();
            _output.Received(0).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }
    }
}
