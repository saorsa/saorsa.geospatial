using System;

namespace Saorsa.GeoSpatial.Tests;

public class RadiansAndDegreesTests
{
    [TestCase(90, Math.PI/2, .001)]
    [TestCase(180, Math.PI, .001)]
    [TestCase(.5, .0008726646, .01)]
    [TestCase(15, .261799, .001)]
    public void TestDegreesToRadians(double degrees, double expectedRadians, double precision)
    {
        var computedRadians = GeoSpatial.DegreesToRadians(degrees);
        
        Assert.True(Math.Abs(expectedRadians - computedRadians) < precision);
    }
    
    [TestCase(1, 57.2958, .001)]
    [TestCase(2, 114.592, .001)]
    [TestCase(3, 171.887, .001)]
    [TestCase(3.5, 200.535, .001)]
    [TestCase(Math.PI, 180.0, .001)]
    [TestCase(Math.PI/4, 45.0, .001)]
    [TestCase(Math.PI*2, 360.0, .001)]
    public void TestRadiansToDegrees(double radians, double expectedDegrees, double precision)
    {
        var computedDegrees = GeoSpatial.RadiansToDegrees(radians);
        var diff = Math.Abs(computedDegrees - expectedDegrees);
        Assert.True(diff < precision);
    }

    [TestCase(1000u, 0.0001)]
    public void TestRandomCircularConversions(uint sampleSize, double precision)
    {
        var random = new Random();
        var idx = 0;
        while (idx < sampleSize)
        {
            var origin = random.NextDouble();
            var derived = GeoSpatial.RadiansToDegrees(origin);
            var originBackwards = GeoSpatial.DegreesToRadians(derived);
            var diff = Math.Abs(originBackwards - origin);
            Assert.True(diff < precision);
            idx++;
        }
    }
}