using OneOf;
using System;
using System.Linq;

namespace ScannitSharp.Models.ValidityAreas
{
    internal static class ValidityArea
    {
        internal static OneOf<OldZone, NewZone, Vehicle> Create(ValidityAreaKind kind, RustBuffer value)
        {
            byte[] values = value.AsByteArray();
            switch (kind)
            {
                case ValidityAreaKind.OldZone:
                    return new OldZone { Value = values.First() };
                case ValidityAreaKind.NewZone:
                    return new NewZone { Value = values.Select(x => (ValidityZone)x).ToArray() };
                case ValidityAreaKind.VehicleType:
                    return new Vehicle { Value = (VehicleType)values.First() };
                default:
                    throw new ArgumentException($"ValidityAreaKind value '{kind}' is unsupported.", nameof(kind));
            }
        }
    }

    /// <summary>
    /// Old, regoin-style zone.
    /// </summary>
    public class OldZone
    {
        /// <summary>
        /// Old, regoin-style zone value.
        /// </summary>
        public byte Value { get; set; }
    }

    /// <summary>
    /// New, ABC-style zone.
    /// </summary>
    public class NewZone
    {
        /// <summary>
        /// An array of the valid zones.
        /// </summary>
        public ValidityZone[] Value { get; set; }
    }

    /// <summary>
    /// Type of vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Type of vehicle.
        /// </summary>
        public VehicleType Value { get; set; }
    }
}
