namespace Saorsa.GeoSpatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public static class GeoSpatial
{
    private static class Constants
    {
        public const double MinutesInDegree = 60;

        public const double StatuteMilesInNauticalMile = 1.151;

        public const double KilometersInMile = 1.609344;

        public const double KilometersInNauticalMile = 1.852;

        // public const double NauticalMilesInMile = 0.868;
    }

    public static double GetDistance(
        GeoSpatialPoint p1,
        GeoSpatialPoint p2,
        GeoSpatialDistanceUnit unit = GeoSpatialDistanceUnit.NauticalMile)
    {
        return GetDistance(
            p1.Latitude,
            p1.Longitude,
            p2.Latitude,
            p2.Longitude,
            unit);
    }

    public static double GetDistance(
        double lat1,
        double lon1,
        double lat2,
        double lon2,
        GeoSpatialDistanceUnit unit = GeoSpatialDistanceUnit.NauticalMile)
    {
        var theta = lon1 - lon2;
        var dist =
            Math.Sin(DegreesToRadians(lat1)) * Math.Sin(DegreesToRadians(lat2))
            + Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) * Math.Cos(DegreesToRadians(theta));
        dist = Math.Acos(dist);
        dist = RadiansToDegrees(dist);
        // Definition of a Nautical mile
        dist *= Constants.MinutesInDegree;

        switch (unit)
        {
            case GeoSpatialDistanceUnit.Kilometer:
                dist *= Constants.KilometersInNauticalMile;
                break;
            case GeoSpatialDistanceUnit.Mile:
                dist *= Constants.StatuteMilesInNauticalMile;
                break;
            case GeoSpatialDistanceUnit.NauticalMile:
                break;
        }

        return dist;
    }

    /// <summary>
    ///     Verifies if a given vector point is contained in a given polygon of points.
    /// </summary>
    /// <param name="point">The originating point.</param>
    /// <param name="polygonPoints">The polygon points. A minimum of three is required.</param>
    public static bool IsPointInPolygon(Vector2 point, IEnumerable<Vector2> polygonPoints)
    {
        if (polygonPoints == null) throw new ArgumentNullException(nameof(polygonPoints));

        Vector2[] polygon = polygonPoints.ToArray();

        int polygonLength = polygon.Length, i = 0;
        bool inside = false;
        // x, y for tested point.
        float pointX = point.X, pointY = point.Y;
        // start / end point for the current polygon segment.
        float startX, startY, endX, endY;
        Vector2 endPoint = polygon[polygonLength - 1];
        endX = endPoint.X;
        endY = endPoint.Y;
        while (i < polygonLength)
        {
            startX = endX;
            startY = endY;
            endPoint = polygon[i++];
            endX = endPoint.X;
            endY = endPoint.Y;
            //
            inside ^= (endY > pointY ^ startY > pointY) /* ? pointY inside [startY;endY] segment ? */
                      && /* if so, test if it is under the segment */
                      ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
        }

        return inside;
    }

    /// <summary>
    ///     Verifies if a given vector point is contained in a given polygon of points.
    /// </summary>
    /// <param name="point">The originating point.</param>
    /// <param name="polygonPoints">The polygon points. A minimum of three is required.</param>
    public static bool IsPointInPolygon(GeoSpatialPoint point, IEnumerable<GeoSpatialPoint> polygonPoints)
    {
        return IsPointInPolygon(point.ToVector(), polygonPoints.Select(p => p.ToVector()));
    }

    public static double DegreesToRadians(double deg)
    {
        return deg * Math.PI / 180.0;
    }

    public static double RadiansToDegrees(double rad)
    {
        return rad / Math.PI * 180.0;
    }

    public static double KmToStatuteMiles(double km)
    {
        return km / Constants.KilometersInMile;
    }
    
    public static double KmToNauticalMiles(double km)
    {
        return km / Constants.KilometersInNauticalMile;
    }
    
    public static double StatuteMilesToKm(double miles)
    {
        return miles * Constants.KilometersInMile;
    }
    
    public static double StatuteMilesToNautical(double statuteMiles)
    {
        return statuteMiles / Constants.StatuteMilesInNauticalMile;
    }
    
    public static double NauticalMilesToStatute(double nauticalMiles)
    {
        return nauticalMiles * Constants.StatuteMilesInNauticalMile;
    }
    
    
    public static double NauticalMilesToKm(double nauticalMiles)
    {
        return nauticalMiles * Constants.KilometersInNauticalMile;
    }
}
