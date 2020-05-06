using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Test.Integration
{
    class IntegrationStep4
    {
        private ITimer _timer;
        private IUserInterface _userInterface;
        private IOutput _output;
        private CookController _uut;
        private PowerTube _powerTube;
        private Display _display;

        [SetUp]
        public void Setup()
        {
            _timer = Substitute.For<ITimer>();
            _userInterface = Substitute.For<IUserInterface>();
            _output = Substitute.For<IOutput>();

            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _uut = new CookController(_timer, _display, _powerTube);
        }

        // Integrationtest CookController - Powertube //
        [TestCase(1, 1)]
        [TestCase(10, 10)]
        [TestCase(50, 50)]
        [TestCase(100, 100)]
        public void Cookcontroller_StartCooking_ValidValues_CorrectOutput(int power, int time)
        {
            _uut.StartCooking(power, time);

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"PowerTube works with {power}")));
        }


        [Test]
        public void Cookcontroller_Stop_CorrectOutput()
        {
            _uut.Stop();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"PowerTube turned off")));
        }

        [TestCase(50)]
        public void Cookcontroller_OnTimerTick_CorrectOutput(int time)
        {
            _timer.TimerTick += Raise.Event();
            

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"PowerTube turned off")));
        }
    }
}
