using OneOf;
using System;

namespace ScannitSharp.Models.BoardingLocations
{

    internal static class BoardingLocation
    {
        internal static OneOf<NoneOrReserved, BusNumber, TrainNumber, PlatformNumber> Create(BoardingLocationKind kind, ushort value)
        {
            switch (kind)
            {
                case BoardingLocationKind.NoneOrReserved:
                    return new NoneOrReserved();
                case BoardingLocationKind.BusNumber:
                    return new BusNumber { Value = value };
                case BoardingLocationKind.TrainNumber:
                    return new TrainNumber { Value = value };
                case BoardingLocationKind.PlatformNumber:
                    return new PlatformNumber { Value = value };
                default:
                    throw new ArgumentException($"BoardingLocationKind '{kind}' is unsupported.", nameof(kind));
            }
        }
    }

    /// <summary>
    /// Indicates that there was no relevant boarding location information.
    /// </summary>
    public class NoneOrReserved { }

    /// <summary>
    /// The number of the boarded bus.
    /// </summary>
    public class BusNumber
    {
        /// <summary>
        /// The number of the boarded bus.
        /// </summary>
        public ushort Value { get; set; }
    }

    /// <summary>
    /// The number of the boarded train.
    /// </summary>
    public class TrainNumber
    {
        /// <summary>
        /// The number of the boarded train.
        /// </summary>
        public ushort Value { get; set; }
    }

    /// <summary>
    /// The number of the boarded platform.
    /// </summary>
    public class PlatformNumber
    {
        /// <summary>
        /// The number of the boarded platform.
        /// </summary>
        public ushort Value { get; set; }
    }
}
