using Magnetify.Services;

namespace Magnetify.Tests.Services
{
    public class SoundServiceTests
    {
        private readonly SoundService _soundService;

        public SoundServiceTests()
        {
            _soundService = new SoundService();
        }


        [Fact]
        public void Disable_SetsDisableSoundToTrue()
        {
            // Act
            _soundService.Disable();

            // Assert
            Assert.True(_soundService.DisableSound);
        }

        [Fact]
        public void Enable_SetsDisableSoundToFalse()
        {
            // Arrange
            // First disable the sound
            _soundService.Disable();

            // Act
            _soundService.Enable();

            // Assert
            Assert.False(_soundService.DisableSound);
        }
    }
}
