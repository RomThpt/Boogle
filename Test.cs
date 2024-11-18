using System;
using System.IO;
using Xunit;

namespace Boogle.Tests
{
    public class ProgramTest
    {
        [Fact]
        public void Main_PrintsWelcomeMessage()
        {
            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            // Act
            Program.Main(new string[] { });

            // Assert
            var output = writer.GetStringBuilder().ToString().Trim();
            Assert.Equal("Welcome to Boogle!", output);
        }
    }
}