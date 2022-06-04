using System;

namespace Saorsa.GeoSpatial.Tests;

public class NauticalAndStatuteMilesTests
{
    [TestCase(1, 1.151, 0.01)]
    [TestCase(2, 2.3, 0.01)]
    [TestCase(3, 3.452, 0.01)]
    [TestCase(2.7, 3.107, 0.01)]
    [TestCase(21.86, 25.156, 0.01)]
    public void TestNauticalToStatuteMiles(double nautical, double expectedStatute, double precision)
    {
        var miles = GeoSpatial.NauticalMilesToStatute(nautical);
        var diff = Math.Abs(miles - expectedStatute);
        Assert.True(diff < precision);
    }
    
    [TestCase(1.151, 1, 0.01)]
    [TestCase(2.3, 2, 0.01)]
    [TestCase(3.452, 3, 0.01)]
    [TestCase(3.107, 2.7, 0.01)]
    [TestCase(25.156, 21.86, 0.01)]
    public void TestStatuteToNauticalMiles(double statute, double expectedNautical, double precision)
    {
        var miles = GeoSpatial.StatuteMilesToNautical(statute);
        var diff = Math.Abs(miles - expectedNautical);
        Assert.True(diff < precision);
    }
    
    [TestCase(0, 100000, 1000u, 0.0001)]
    public void TestRandomCircularConversions(
        int min,
        int max,
        uint sampleSize, double precision)
    {
        var random = new Random();
        var idx = 0;
        while (idx < sampleSize)
        {
            var origin = random.NextDouble() * (max - min) + min;
            var derived = GeoSpatial.StatuteMilesToNautical(origin);
            var originBackwards = GeoSpatial.NauticalMilesToStatute(derived);
            var diff = Math.Abs(originBackwards - origin);
            Assert.True(diff < precision);
            idx++;
        }
    }
    
    [TestCase(0, 100000, 1000u, 0.0001)]
    public void TestRandomCircularConversionsReversed(
        int min,
        int max,
        uint sampleSize, double precision)
    {
        var random = new Random();
        var idx = 0;
        while (idx < sampleSize)
        {
            var origin = random.NextDouble() * (max - min) + min;
            var derived = GeoSpatial.NauticalMilesToStatute(origin);
            var originBackwards = GeoSpatial.StatuteMilesToNautical(derived);
            var diff = Math.Abs(originBackwards - origin);
            Assert.True(diff < precision);
            idx++;
        }
    }
}