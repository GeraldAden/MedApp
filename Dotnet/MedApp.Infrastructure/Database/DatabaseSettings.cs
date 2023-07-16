namespace MedApp.Infrastructure.Database;

public interface IDatabaseSettings
{
    string? Host { get; set; }
    string? Port { get; set; }
    string? Database { get; set; }
    string? Username { get; set; }
    string? Password { get; set; }
}

public class DatabaseSettings : IDatabaseSettings
{
    public string? Host { get; set; }
    public string? Port { get; set; }
    public string? Database { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}