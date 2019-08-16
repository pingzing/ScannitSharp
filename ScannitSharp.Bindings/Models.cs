namespace ScannitSharp.Bindings
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
}
