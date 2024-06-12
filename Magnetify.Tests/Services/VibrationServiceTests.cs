using Magnetify.Services;

namespace Magnetify.Tests.Services
{
    public class VibrationServiceTests
    {
        [Fact]
        public void VibrationService_Initialization_DisableVibrationIsFalse()
        {
            // Arrange & Act
            var vibrationService = new VibrationService();

            // Assert
            Assert.False(vibrationService.DisableVibration);
        }

        [Fact]
        public void Disable_SetsDisableVibrationToTrue()
        {
            // Arrange
            var vibrationService = new VibrationService();

            // Act
            vibrationService.Disable();

            // Assert
            Assert.True(vibrationService.DisableVibration);
        }

        [Fact]
        public void Enable_SetsDisableVibrationToFalse()
        {
            // Arrange
            var vibrationService = new VibrationService();
            // First disable it
            vibrationService.Disable();

            // Act
            vibrationService.Enable();

            // Assert
            Assert.False(vibrationService.DisableVibration);
        }

    }
}
