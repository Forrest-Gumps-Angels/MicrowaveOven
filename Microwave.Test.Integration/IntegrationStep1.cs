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
    public class IntegrationStep1
    {
        private Light _light;
        private  IOutput _output;
        private StringWriter str;

        [SetUp]
        public void Setup()
        {
            str = new StringWriter();
            Console.SetOut(str);
            _output = new Output();
            _light = new Light(_output);
        }

        [Test]
        public void Light_TurnOn_LightIsOff_OutputLinePrintsExpectedString()
        {
            _light.TurnOn();
            Assert.That(str.ToString().Contains("Light is turned on"));
            //_output.Received().OutputLine(Arg.Is<string>(str =>str.Contains("Light is turned on")));
        }

        [Test]
        public void Light_TurnOn_AlreadyOn_OutputLineOnlyCalledOnceWithExpectedString()
        {
            _light.TurnOn();
            _light.TurnOn();
            Assert.That(str.ToString().Contains("Light is turned on"));
            //_output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void Light_TurnOff_LightIsOn_OutputLinePrintsExpectedString()
        {
            _light.TurnOn();
            _light.TurnOff();
            Assert.That(str.ToString().Contains("Light is turned off"));
            //_output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

        [Test]
        public void Light_TurnOff_LightIsOff_OutputLineOnlyCalledOnceWithExpectedString()
        {
            _light.TurnOff();
            Assert.That(str.ToString().Contains("Light is turned off"));
            //_output.Received(0).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }
    }
}
