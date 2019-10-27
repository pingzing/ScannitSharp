using OneOf;
using System;

namespace ScannitSharp.Models.BoardingAreas
{
    internal static class BoardingArea
    {
        internal static OneOf<Zone, Vehicle, ZoneCircle> Create(BoardingAreaKind kind, byte value)
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

    /// <summary>
    /// Represents a new-style ABC-zone.
    /// </summary>
    public class Zone
    {
        /// <summary>
        /// The zone letter.
        /// </summary>
        public ValidityZone Value { get; set; }
    }

    /// <summary>
    /// Represents a vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// The kind of vehicle.
        /// </summary>
        public VehicleType Value { get; set; }
    }

    /// <summary>
    /// An old-style zone region.
    /// </summary>
    public class ZoneCircle
    {
        /// <summary>
        /// An old-style zone region.
        /// </summary>
        public byte Value { get; set; }
    }
}
