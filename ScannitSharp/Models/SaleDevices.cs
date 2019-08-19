using OneOf;
using System;

namespace ScannitSharp.Models.SaleDevices
{
    public static class SaleDevice
    {
        public static OneOf<ServicePointSalesDevice, DriverTicketMachine, CardReader, TicketMachine, Server, HSLSmallEquipment, ExternalServiceEquipment, Reserved> Create(SaleDeviceKind kind, ushort value)
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

    public class ServicePointSalesDevice
    {
        public SaleDeviceKind Kind => SaleDeviceKind.ServicePointSalesDevice;
        public ushort Value { get; set; }
    }

    public class DriverTicketMachine
    {
        public SaleDeviceKind Kind => SaleDeviceKind.DriverTicketMachine;
        public ushort Value { get; set; }
    }

    public class CardReader
    {
        public SaleDeviceKind Kind => SaleDeviceKind.CardReader;
        public ushort Value { get; set; }
    }

    public class TicketMachine
    {
        public SaleDeviceKind Kind => SaleDeviceKind.TicketMachine;
        public ushort Value { get; set; }
    }

    public class Server
    {
        public SaleDeviceKind Kind => SaleDeviceKind.Server;
        public ushort Value { get; set; }
    }

    public class HSLSmallEquipment
    {
        public SaleDeviceKind Kind => SaleDeviceKind.HSLSmallEquipment;
        public ushort Value { get; set; }
    }

    public class ExternalServiceEquipment
    {
        public SaleDeviceKind Kind => SaleDeviceKind.ExternalServiceEquipment;
        public ushort Value { get; set; }
    }

    public class Reserved
    {
        public SaleDeviceKind Kind => SaleDeviceKind.Reserved;
        public ushort Value { get; set; }
    }
}
