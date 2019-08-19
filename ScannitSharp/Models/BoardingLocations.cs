using OneOf;
using System;

namespace ScannitSharp.Models.BoardingLocations
{

    public static class BoardingLocation
    {
        public static OneOf<NoneOrReserved, BusNumber, TrainNumber, PlatformNumber> Create(BoardingLocationKind kind, ushort value)
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
    public class NoneOrReserved
    {
        public BoardingLocationKind Kind => BoardingLocationKind.NoneOrReserved;
    }

    public class BusNumber
    {
        public BoardingLocationKind Kind => BoardingLocationKind.BusNumber;
        public ushort Value { get; set; }
    }

    public class TrainNumber
    {
        public BoardingLocationKind Kind => BoardingLocationKind.TrainNumber;
        public ushort Value { get; set; }
    }

    public class PlatformNumber
    {
        public BoardingLocationKind Kind => BoardingLocationKind.PlatformNumber;
        public ushort Value { get; set; }
    }
}
