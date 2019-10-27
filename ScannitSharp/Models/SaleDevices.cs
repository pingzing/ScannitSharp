using OneOf;
using System;

namespace ScannitSharp.Models.SaleDevices
{
    internal static class SaleDevice
    {
        internal static OneOf<ServicePointSalesDevice, DriverTicketMachine, CardReader, TicketMachine, Server, HSLSmallEquipment, ExternalServiceEquipment, Reserved> Create(SaleDeviceKind kind, ushort value)
        {
            switch (kind)
            {
                case SaleDeviceKind.ServicePointSalesDevice:
                    return new ServicePointSalesDevice { Value = value };
                case SaleDeviceKind.DriverTicketMachine:
                    return new DriverTicketMachine { Value = value };
                case SaleDeviceKind.CardReader:
                    return new CardReader { Value = value };
                case SaleDeviceKind.TicketMachine:
                    return new TicketMachine { Value = value };
                case SaleDeviceKind.Server:
                    return new Server { Value = value };
                case SaleDeviceKind.HSLSmallEquipment:
                    return new HSLSmallEquipment { Value = value };
                case SaleDeviceKind.ExternalServiceEquipment:
                    return new ExternalServiceEquipment { Value = value };
                case SaleDeviceKind.Reserved:
                    return new Reserved { Value = value };
                default:
                    throw new ArgumentException($"SaleDeviceKind '{kind}' is unsupported.", nameof(kind));
            }
        }
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ServicePointSalesDevice
    {
        public ushort Value { get; set; }
    }

    public class DriverTicketMachine
    {
        public ushort Value { get; set; }
    }

    public class CardReader
    {
        public ushort Value { get; set; }
    }

    public class TicketMachine
    {
        public ushort Value { get; set; }
    }

    public class Server
    {
        public ushort Value { get; set; }
    }

    public class HSLSmallEquipment
    {
        public ushort Value { get; set; }
    }

    public class ExternalServiceEquipment
    {
        public ushort Value { get; set; }
    }

    public class Reserved
    {
        public ushort Value { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
