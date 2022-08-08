using MSA.Phase2.Weatherman.Models;
using MSA.Phase2.Weatherman.Services;

namespace UniTest
{
    [TestFixture]
    public class WeatherManServiceTest
    {
        private readonly WeathermanServices _service = new WeathermanServices();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void tempWarningTest()
        {
            Main main = new Main();
            main.temp = 400;
            string output = _service.tempWarning(main);
            Assert.AreEqual("Warning: Extremely hot weather today. Beware of heat strokes.", output);
            main.temp = 0;
            output = _service.tempWarning(main);
            Assert.AreEqual("Warning: Extremely cold weather today, encouraged to stay home or make sure to dress with multiple layers of clothing.", output);
            main.temp = (float)303.15;
            output = _service.tempWarning(main);
            Assert.AreEqual("Warm weather today.", output);
        }

        [Test]
        public void weatherWarningTestNull()
        {
            //testing for empty weather id
            Weather weather = new Weather();
            string output = _service.weatherWarning(weather);
            Assert.AreEqual(null, output);
            
            //testing for invalid weather id
            weather.id = -1;
            output = _service.weatherWarning(weather);
            Assert.AreEqual(null, output);



        }

        //testing for thunder with rain weather id
        [Test]
        [TestCase(200)]
        [TestCase(201)]
        [TestCase(202)]
        public void weatherWarningTestThunderRain(int value)
        {
            Weather weather = new Weather();
            weather.id = value;
            string output = _service.weatherWarning(weather);
            Assert.AreEqual("Warning: Thunderstorm with rain expected today, stay home if you can.", output);
        }

        //testing for thunder weather id
        [Test]
        [TestCase(210)]
        [TestCase(215)]
        [TestCase(221)]
        public void weatherWarningTestThunder(int value)
        {
            Weather weather = new Weather();
            weather.id = value;
            string output = _service.weatherWarning(weather);
            Assert.AreEqual("Warning: Thunderstorm expected today.", output);
        }

        //testing for drizzle weather id
        [Test]
        [TestCase(300)]
        [TestCase(315)]
        [TestCase(321)]
        public void weatherWarningTestDrizzle(int value)
        {
            Weather weather = new Weather();
            weather.id = value;
            string output = _service.weatherWarning(weather);
            Assert.AreEqual("Warning: Drizzle expected today, make sure to bring an umbrella or a raincoat.", output);

        }

        //testing for rain weather id
        [Test]
        [TestCase(500)]
        [TestCase(520)]
        [TestCase(531)]
        public void weatherWarningTestRain(int value)
        {
            Weather weather = new Weather();
            weather.id = value;
            string output = _service.weatherWarning(weather);
            Assert.AreEqual("Warning: Rain expected today, make sure to bring an umbrella or a raincoat.", output);

        }

        //testing for snow weather id
        [Test]
        [TestCase(600)]
        [TestCase(610)]
        [TestCase(622)]
        public void weatherWarningTestSnow(int value)
        {
            Weather weather = new Weather();
            weather.id = value;
            string output = _service.weatherWarning(weather);
            Assert.AreEqual("Warning: Snow expected today, make sure to dress warm.", output);
        }

        //testing for atmosphere weather id
        [Test]
        [TestCase(701)]
        [TestCase(740)]
        [TestCase(781)]
        public void weatherWarningTestAtmosphere(int value)
        {
            Weather weather = new Weather();
            weather.id = value;
            string output = _service.weatherWarning(weather);
            Assert.AreEqual("Warning: Unclear atmosphere expected today, beware of unclear sights when driving.", output);
        }

        //testing for clear weather id
        [Test]
        [TestCase(800)]
        public void weatherWarningTestClear(int value)
        {
            Weather weather = new Weather();
            weather.id = value;
            string output = _service.weatherWarning(weather);
            Assert.AreEqual("Clear weather today, enjoy the great weater!", output);
        }

        //testing for cloud id
        [Test]
        [TestCase(801)]
        [TestCase(803)]
        [TestCase(804)]
        public void weatherWarningTestCloudy(int value)
        {
            Weather weather = new Weather();
            weather.id = value;
            string output = _service.weatherWarning(weather);
            Assert.AreEqual("Clouds expected today", output);
        }


    }
}