using OneOf;
using System;

namespace ScannitSharp.Models.BoardingAreas
{
    public static class BoardingArea
    {
        public static OneOf<Zone, Vehicle, ZoneCircle> Create(BoardingAreaKind kind, byte value)
        {
            switch (kind)
            {
                case BoardingAreaKind.Zone:
                    return new Zone { Value = (ValidityZone)value };
                case BoardingAreaKind.Vehicle:
                    return new Vehicle { Value = (VehicleType)value };
                case BoardingAreaKind.ZoneCircle:
                    return new ZoneCircle { Value = value };
                default:
                    throw new ArgumentException($"BoardingAreaKind '{kind}' is unsupported.", nameof(kind));
            }
        }
    }

    public class Zone
    {
        public BoardingAreaKind Kind => BoardingAreaKind.Zone;
        public ValidityZone Value { get; set; }
    }

    public class Vehicle
    {
        public BoardingAreaKind Kind => BoardingAreaKind.Vehicle;
        public VehicleType Value { get; set; }
    }

    public class ZoneCircle
    {
        public BoardingAreaKind Kind => BoardingAreaKind.ZoneCircle;
        public byte Value { get; set; }
    }
}
