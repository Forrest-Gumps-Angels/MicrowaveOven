using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Test.Integration
{
    public class IntegrationStep6
    {
        private IUserInterface _uut;
        private ICookController _cookcontroller;
        private ITimer _timer;
        private IPowerTube _powertube;


        private IButton _powerbutton,  _timebutton,  _startcancelbutton;
        private IDoor _door;
        private IDisplay _display;
        private ILight _light;

        [SetUp]
        public void Setup()
        {
            _timer = Substitute.For<ITimer>();
            _powertube = Substitute.For<IPowerTube>();
            _powerbutton = Substitute.For<IButton>();
            _timebutton = Substitute.For<IButton>();
            _startcancelbutton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _display = Substitute.For<IDisplay>();
            _light = Substitute.For<ILight>();

            _cookcontroller = new CookController(_timer, _display, _powertube, _uut);
            _uut = new UserInterface(_powerbutton, _timebutton, _startcancelbutton, _door, _display, _light, _cookcontroller);
       
        }

        [TestCase(700, 99)]
        [TestCase(500, 5)]
        [TestCase(50, 1)]
        public void UserInterface_StartCooking_When_StartPressed(int power, int time)
        {
            for (int i = 0; i < power / 50; i++)
            {
                _powerbutton.Pressed += Raise.Event();
            }
            for (int i = 0; i < time; i++)
            {
                _timebutton.Pressed += Raise.Event();
            }

            _startcancelbutton.Pressed += Raise.Event();

            _powertube.Received(1).TurnOn(power);
            _timer.Received(1).Start(time * 60);

            //Assert.AreEqual(_cookcontroller.isCooking, true);
        }


        // This test fails because the UserInterface is able to set timer above 2 digits.
        [TestCase(700, 200)]
        [TestCase(500, 300)]
        [TestCase(50, 100000)]
        public void UserInterface_NotSetTimeOver2Digits(int power, int time)
        {
            for (int i = 0; i < power / 50; i++)
            {
                _powerbutton.Pressed += Raise.Event();
            }
            for (int i = 0; i < time; i++)
            {
                _timebutton.Pressed += Raise.Event();
            }

            _startcancelbutton.Pressed += Raise.Event();

            _timer.DidNotReceive().Start(time * 60);
        }

        [Test]
        public void UserInterface_ClearAndTurnoff_WhenCookingIsDone()
        {
            _powerbutton.Pressed += Raise.Event();
            _timebutton.Pressed += Raise.Event();
            _startcancelbutton.Pressed += Raise.Event();

            _timer.Expired += Raise.Event();

            _display.Received().Clear();    
            _light.Received().TurnOff();
        }





    }
}
