using System;

namespace ScannitSharp.Bindings
{
    public class TravelCard
    {
        public byte ApplicationVersion { get; set; }
        public byte ApplicationKeyVersion { get; set; }
        public string ApplicationInstanceId { get; set; }
        public byte PlatformType { get; set; }
        public bool IsMacProtected { get; set; }

        public DateTimeOffset ApplicationIssuingDate { get; set; }
        public bool ApplicationStatus { get; set; }
        public byte ApplicationUnblockingNumber { get; set; }
        public uint ApplicationTransactionCounter { get; set; }
        public uint ActionListCounter { get; set; }

        public PeriodPass PeriodPass { get; set; }

        public uint StoredValueCents { get; set; }
        public DateTimeOffset LastLoadDateTime { get; set; }
        public uint LastLoadValue { get; set; }
        public ushort LastLoadOrganization { get; set; }
        public ushort LastLoadDeviceNum { get; set; }

        public ETicket ETicket { get; set; }

        public History[] History { get; set; }
    }

    public class PeriodPass
    {

    }

    public class ETicket
    {

    }

    public class History
    {

    }
}
