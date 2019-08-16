namespace ScannitSharp.Bindings.Models
{
    public enum BoardingDirection : uint
    {
        TowardEnd = 0,
        TowardStart = 1,
    }

    public enum Language : uint
    {
        Finnish = 0,
        Swedish = 1,
        English = 2,
    }

    public enum TransactionType : uint
    {
        SeasonPass = 0,
        ValueTicket = 1,
    }

    /// <summary>
    /// The HSL fare zone(s) in which a ticket is valid.
    /// </summary>
    public enum ValidityZone
    {
        ZoneA = 0,
        ZoneB = 1,
        ZoneC = 2,
        ZoneD = 3,
        ZoneE = 4,
        ZoneF = 5,
        ZoneG = 6,
        ZoneH = 7,
    }

    /// <summary>
    /// The vehicle type on which this ticket is valid.
    /// </summary>
    public enum VehicleType
    {
        Undefined = 0,
        Bus = 1,
        Tram = 5,
        Metro = 6,
        Train = 7,
        Ferry = 8,
        ULine = 9,
    }
}
