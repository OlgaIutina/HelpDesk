using HelpDesk.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Threading.Tasks;

namespace HelpDesk.Tests.Unit
{
    public class SlaCalculatorTests
    {
        
        [Theory]
        [InlineData(Priority.Low, 72)]
        [InlineData(Priority.Normal, 24)]
        [InlineData(Priority.High, 8)]
        [InlineData(Priority.Critical, 4)]
        public void CalculateSlaDeadline_KnownPriority_ReturnsCreatedAtPlusExpectedHours(Priority priority, int expectedHours)
        {
           
            var createdAt = new DateTimeOffset(2026, 10, 20, 10, 0, 0, TimeSpan.Zero);
            var sut = new SlaCalculator();
            var deadline = sut.CalculateSlaDeadline(createdAt, priority);
            Assert.Equal(createdAt.AddHours(expectedHours), deadline);
        }
    }
}
