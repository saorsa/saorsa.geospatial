using System;

namespace Saorsa.GeoSpatial.Tests;

public class KilometersAndStatuteMilesTests
{
    [TestCase(1, 0.621, 0.001)]
    [TestCase(1.609, 1, 0.001)]
    [TestCase(4, 2.485, 0.001)]
    [TestCase(27.35, 16.994, 0.001)]
    public void TestKmToStatuteMiles(double km, double expectedMiles, double precision)
    {
        var miles = GeoSpatial.KmToStatuteMiles(km);
        var diff = Math.Abs(miles - expectedMiles);
        Assert.True(diff < precision);
    }
    
    [TestCase(0.6213, 1, 0.001)]
    [TestCase(1, 1.609, 0.001)]
    [TestCase(2.485, 4, 0.001)]
    [TestCase(16.994, 27.35, 0.001)]
    public void TestStatuteMilesToKm(double miles, double expectedKm, double precision)
    {
        var km = GeoSpatial.StatuteMilesToKm(miles);
        var diff = Math.Abs(km - expectedKm);
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
            var derived = GeoSpatial.KmToStatuteMiles(origin);
            var originBackwards = GeoSpatial.StatuteMilesToKm(derived);
            var diff = Math.Abs(originBackwards - origin);
            Assert.True(diff < precision);
            idx++;
        }
    }
    
    [TestCase(1000u, 0.0001)]
    public void TestRandomCircularConversionsReversed(uint sampleSize, double precision)
    {
        var random = new Random();
        var idx = 0;
        while (idx < sampleSize)
        {
            var origin = random.NextDouble();
            var derived = GeoSpatial.StatuteMilesToKm(origin);
            var originBackwards = GeoSpatial.KmToStatuteMiles(derived);
            var diff = Math.Abs(originBackwards - origin);
            Assert.True(diff < precision);
            idx++;
        }
    }
}
