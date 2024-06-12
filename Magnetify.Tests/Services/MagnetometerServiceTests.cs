using Magnetify.Interfaces;
using Magnetify.Services;
using Moq;

namespace Magnetify.Tests.Services
{
    public class MagnetometerServiceTests
    {
        private readonly MagnetometerService _service;
        private readonly Mock<IAlertService> _alertServiceMock;

        public MagnetometerServiceTests()
        {
            _alertServiceMock = new Mock<IAlertService>();
            _service = new MagnetometerService(_alertServiceMock.Object);
        }

        [Fact]
        public void Stop_ShouldSetRunningToFalse()
        {
            _service.Start();
            _service.Stop();
            Assert.False(_service.Running);
        }

        [Fact]
        public void AddValue_ShouldUpdateValuesCorrectly()
        {
            double value = 100.0;
            _service.AddValue(value);

            Assert.Equal(value, _service.CurrentValue);
            Assert.Equal(value, _service.Values.Last());
            Assert.Equal(value, _service.CurrentAverage);
            Assert.Equal(value, _service.CurrentMax);
            Assert.Equal(value, _service.CurrentMin);
        }
    }

}
