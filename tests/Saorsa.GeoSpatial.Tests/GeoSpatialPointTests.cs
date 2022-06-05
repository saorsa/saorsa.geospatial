using System;
using System.Numerics;

namespace Saorsa.GeoSpatial.Tests;

public class GeoSpatialPointTests
{
    [Test]
    public void TestConstructor()
    {
        var random = new Random();
        var lat = random.NextDouble();
        var lon = random.NextDouble();

        var point = new GeoSpatialPoint(lat, lon);
        
        Assert.AreEqual(lat, point.Latitude);
        Assert.AreEqual(lon, point.Longitude);
    }
    
    [Test]
    public void TestConstructorFromVector2()
    {
        var random = new Random();
        var lat = Convert.ToSingle(random.NextDouble());
        var lon = Convert.ToSingle(random.NextDouble());
        var vector = new Vector2(lat, lon);
        var point = new GeoSpatialPoint(vector);
        
        Assert.AreEqual(lat, point.Latitude);
        Assert.AreEqual(lon, point.Longitude);

        var vector2 = point.ToVector2();
        
        Assert.True(point.Equals(vector2));
        Assert.True(vector == vector2);
        Assert.True(vector.Equals(vector2));
    }
    
    [Test]
    public void TestConstructorFromLatLngTuple()
    {
        var random = new Random();
        var lat = Convert.ToSingle(random.NextDouble());
        var lon = Convert.ToSingle(random.NextDouble());
        var point = new GeoSpatialPoint((lat, lon));
        
        Assert.AreEqual(lat, point.Latitude);
        Assert.AreEqual(lon, point.Longitude);

        var tuple = point.ToLatLngTuple();
        
        Assert.AreEqual(tuple.Item1, lat);
        Assert.AreEqual(tuple.Item2, lon);
        Assert.True(point.Equals(tuple));
    }

    [Test]
    public void TestClone()
    {
        var random = new Random();
        var lat = random.NextDouble();
        var lon = random.NextDouble();

        var point = new GeoSpatialPoint(lat, lon);
        var clone = point.Clone() as GeoSpatialPoint;
        
        Assert.True(clone != null);
        Assert.AreEqual(clone!.Latitude, point.Latitude);
        Assert.AreEqual(clone.Longitude, point.Longitude);
    }

    [Test]
    public void TestEqualityToPoints()
    {
        var random = new Random();
        var lat = random.NextDouble();
        var lon = random.NextDouble();

        var point = new GeoSpatialPoint(lat, lon);
        var clone = point.Clone() as GeoSpatialPoint;
        
        Assert.True(clone != null);
        Assert.True(clone!.Equals(point));
        Assert.True(clone.Equals((object)point));
        Assert.True(clone == point);
    }
    
    [TestCase(null)]
    [TestCase("one")]
    [TestCase(42)]
    [TestCase(typeof(Guid))]
    public void TestEqualityToNonPoints(object? candidate)
    {
        var random = new Random();
        var lat = random.NextDouble();
        var lon = random.NextDouble();

        var point = new GeoSpatialPoint(lat, lon);
        
        Assert.False(point.Equals(candidate));
    }

    [TestCase(10000)]
    public void TestGetHashCode(int expectedHashesWithoutCollision)
    {
        var hashes = new Dictionary<int, GeoSpatialPoint>();

        for (var idx = 0; idx < expectedHashesWithoutCollision; idx++)
        {
            var random = new Random();
            var lat = random.NextDouble();
            var lon = random.NextDouble();

            var point = new GeoSpatialPoint(lat, lon);
            var hash = point.GetHashCode();
        
            Assert.False(hashes.ContainsKey(hash));
            
            hashes.Add(hash, point);
        }
    }
    
    [TestCase(.001)]
    public void TestToVector(double expectedPrecision)
    {
        var random = new Random();
        var lat = random.NextDouble();
        var lon = random.NextDouble();

        var point = new GeoSpatialPoint(lat, lon);
        var vector = point.ToVector2();

        var diffLat = Math.Abs(point.Latitude - vector.X);
        Assert.True(diffLat < expectedPrecision);
        
        var diffLon = Math.Abs(point.Longitude - vector.Y);
        Assert.True(diffLon < expectedPrecision);
    }
}
