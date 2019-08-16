using OneOf;
using System;
using System.Linq;

namespace ScannitSharp.Bindings.Models.ValidityAreas
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

    public class OldZone
    {
        public ValidityAreaKind Kind => ValidityAreaKind.OldZone;
        public byte Value { get; set; }
    }

    public class NewZone
    {
        public ValidityAreaKind Kind => ValidityAreaKind.NewZone;
        public ValidityZone[] Value { get; set; }
    }

    public class Vehicle
    {
        public ValidityAreaKind Kind => ValidityAreaKind.VehicleType;
        public VehicleType Value { get; set; }
    }
}
