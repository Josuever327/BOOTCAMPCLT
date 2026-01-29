namespace Api.BootCamp.Aplication.Abstractions.Common
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}