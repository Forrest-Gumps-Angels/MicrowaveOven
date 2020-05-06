using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
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

        [TestCase(1, 1)]
        [TestCase(10, 10)]
        [TestCase(50, 50)]
        [TestCase(100, 100)]
        public void CookController_StartCooking_ValidValues_CorrectOutput(int power, int time)
        {
            _uut.StartCooking(power, time);

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"PowerTube works with {power}")));
        }

        [TestCase(0)]
        [TestCase(-100)]
        [TestCase(101)]
        [TestCase(200)]
        public void CookController_StartCooking_InvalidValues_ExceptionThrown(int power)
        {
            Assert.That(() => _uut.StartCooking(power, 1), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Cookcontroller_StartCooking_AlreadyOn_ApplicationException()
        {
            _uut.StartCooking(50, 30);
            Assert.That(() => _uut.StartCooking(50, 30), Throws.TypeOf<ApplicationException>());
        }

        [TestCase(1, 1)]
        [TestCase(10, 10)]
        [TestCase(50, 50)]
        [TestCase(100, 100)]
        public void CookController_Stop_CorrectOutput(int power, int time)
        {
            _uut.StartCooking(power, time);
            _uut.Stop();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("PowerTube turned off")));
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(30)]
        [TestCase(59)]
        public void CookController_OnTimerTick_CorrectOutput(int timeRemaining)
        {
            _timer.TimeRemaining.Returns(timeRemaining);
            _timer.TimerTick += Raise.Event();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"Display shows: 00:{timeRemaining:D2}")));
        }

        [Test]
        public void CookController_OnTimerËxpired_CorrectOutput()
        {
            //_uut.StartCooking(50, 20);
            //_timer.TimeRemaining.Returns(1);
            _timer.Expired += Raise.Event();
            _output.Received().OutputLine(Arg.Is<string>("PowerTube turned off"));
        }
    }
}
