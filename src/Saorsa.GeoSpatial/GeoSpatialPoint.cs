using System.Numerics;

namespace Saorsa.GeoSpatial;

public class GeoSpatialPoint: IEquatable<GeoSpatialPoint>, IEquatable<Vector2>
{
    public double Longitude { get; }

    public double Latitude { get; }

    public GeoSpatialPoint(double lat, double lon)
    {
        Latitude = lat;
        Longitude = lon;
    }

    public GeoSpatialPoint(Vector2 vector)
    {
        Latitude = vector.X;
        Longitude = vector.Y;
    }

    public override bool Equals(object? other)
    {
        return other is GeoSpatialPoint otherPoint && Equals(otherPoint);
    }

    public Vector2 ToVector()
    {
        return new Vector2(
            Convert.ToSingle(Latitude), 
            Convert.ToSingle(Longitude));
    }

    public bool Equals(GeoSpatialPoint? other)
    {
        return  other is { } && Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
    }

    public bool Equals(Vector2 other)
    {
        return Latitude.Equals(other.X) && Longitude.Equals(other.Y);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude.GetHashCode(), Longitude.GetHashCode());
    }

    public static bool operator==(GeoSpatialPoint p1, GeoSpatialPoint p2)
    {
        return p1.Equals(p2);
    }

    public static bool operator !=(GeoSpatialPoint p1, GeoSpatialPoint p2)
    {
        return !p1.Equals(p2);
    }

    public static bool operator ==(GeoSpatialPoint p1, Vector2 p2)
    {
        return p1.Equals(p2);
    }
    
    public static bool operator !=(GeoSpatialPoint p1, Vector2 p2)
    {
        return !p1.Equals(p2);
    }
}
