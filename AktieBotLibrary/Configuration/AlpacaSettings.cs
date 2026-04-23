namespace AktieBotLibrary.Configuration;

public sealed class AlpacaSettings
{
    public const string SectionName = "Alpaca";

    public string KeyId { get; init; } = string.Empty;

    public string SecretKey { get; init; } = string.Empty;

    public string BaseUrl { get; init; } = "https://paper-api.alpaca.markets";
}
