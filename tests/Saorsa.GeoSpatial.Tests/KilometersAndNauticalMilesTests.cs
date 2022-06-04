using System;

namespace Saorsa.GeoSpatial.Tests;

public class KilometersAndNauticalMilesTests
{
    [TestCase(80, 43.196, 0.001)]
    [TestCase(40000, 21598.272, 0.001)]
    [TestCase(7.5, 4.049, 0.001)]
    public void TestKmToNauticalMiles(double km, double expectedMiles, double precision)
    {
        var miles = GeoSpatial.KmToNauticalMiles(km);
        var diff = Math.Abs(miles - expectedMiles);
        Assert.True(diff < precision);
    }
    
    [TestCase(1, 1.852, 0.001)]
    [TestCase(2.5, 4.63, 0.001)]
    [TestCase(4, 7.408, 0.001)]
    [TestCase(80, 148.16, 0.001)]
    public void TestNauticalMilesToKm(double miles, double expectedKm, double precision)
    {
        var km = GeoSpatial.NauticalMilesToKm(miles);
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
            var derived = GeoSpatial.KmToNauticalMiles(origin);
            var originBackwards = GeoSpatial.NauticalMilesToKm(derived);
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
            var derived = GeoSpatial.KmToNauticalMiles(origin);
            var originBackwards = GeoSpatial.NauticalMilesToKm(derived);
            var diff = Math.Abs(originBackwards - origin);
            Assert.True(diff < precision);
            idx++;
        }
    }
}
