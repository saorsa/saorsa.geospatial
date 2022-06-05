using System;

namespace Saorsa.GeoSpatial.Tests;

public class GeoDistanceTests
{
    [TestCase(
        "Sofia, Eagles Bridge - Sofia, Yavorov Lane",
        42.690573522, 23.337522419,
        42.683699178, 23.347328398,
        GeoSpatialDistanceUnit.Kilometer, 
        1.11, 
        .1)]
    [TestCase(
        "Sofia, Eagles Bridge - Plovdiv, Downtown",
        42.690783301, 23.337254088,
        42.143466992, 24.751962024,
        GeoSpatialDistanceUnit.Kilometer, 
        131.35, 
        .5)]
    [TestCase(
        "Sofia, Eagles Bridge - London, Downtown",
        42.690783301, 23.337254088,
        51.506263484, -0.124484262,
        GeoSpatialDistanceUnit.Kilometer, 
        2020.14, 
        5.8)]
    [TestCase(
        "Sofia, Eagles Bridge - South Africa",
        42.690783301, 23.337254088,
        -31.171100964, 23.709847131,
        GeoSpatialDistanceUnit.Kilometer, 
        8178.4, 
        31.0)]
    [TestCase(
        "Calais, France - Dover, UK",
        50.970541359, 1.855669305,
        51.124853585, 1.331054893,
        GeoSpatialDistanceUnit.NauticalMile, 
        21.92225,
        0.1)]
    [TestCase(
        "London - Leeds",
        51.511166949, -0.090245415,
        53.798939970, -1.547843151,
        GeoSpatialDistanceUnit.Mile, 
        169.640,
        0.5)]
    public void TestSimplifiedDistance(
        string alias,
        double latitude1, double longitude1,
        double latitude2, double longitude2,
        GeoSpatialDistanceUnit distanceMeasure,
        double expectedDistance,
        double precision)
    {
        var distance = GeoSpatial.GetDistance(
            latitude1, longitude1, latitude2, longitude2,
            distanceMeasure);

        var diff = Math.Abs(distance - expectedDistance);
        Assert.True(diff < precision);
    }
    
    [TestCase(
        "Sofia, Eagles Bridge - Sofia, Yavorov Lane",
        42.690573522, 23.337522419,
        42.683699178, 23.347328398,
        GeoSpatialDistanceUnit.Kilometer, 
        1.11, 
        .005)]
    [TestCase(
        "Sofia, Eagles Bridge - Plovdiv, Downtown",
        42.690783301, 23.337254088,
        42.143466992, 24.751962024,
        GeoSpatialDistanceUnit.Kilometer, 
        131.35, 
        .24)]
    [TestCase(
        "Sofia, Eagles Bridge - London, Downtown",
        42.690783301, 23.337254088,
        51.506263484, -0.124484262,
        GeoSpatialDistanceUnit.Kilometer, 
        2020.14, 
        4.5)]
    [TestCase(
        "Sofia, Eagles Bridge - South Africa",
        42.690783301, 23.337254088,
        -31.171100964, 23.709847131,
        GeoSpatialDistanceUnit.Kilometer, 
        8178.4, 
        35.0)]
    [TestCase(
        "Calais, France - Dover, UK",
        50.970541359, 1.855669305,
        51.124853585, 1.331054893,
        GeoSpatialDistanceUnit.NauticalMile, 
        21.92225,
        0.07)]
    [TestCase(
        "London - Leeds",
        51.511166949, -0.090245415,
        53.798939970, -1.547843151,
        GeoSpatialDistanceUnit.Mile, 
        169.640,
        0.2)]
    public void TestHaversineDistance(
        string alias,
        double latitude1, double longitude1,
        double latitude2, double longitude2,
        GeoSpatialDistanceUnit distanceMeasure,
        double expectedDistance,
        double precision)
    {
        var distance = GeoSpatial.GetHaversineDistance(
            latitude1, longitude1, latitude2, longitude2,
            distanceMeasure);

        var diff = Math.Abs(distance - expectedDistance);
        Assert.True(diff < precision);
    }
}