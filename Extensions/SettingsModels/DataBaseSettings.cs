namespace Extensions.SettingsModels
{
    /// <summary>
    /// Получение строки подключения для бд
    /// </summary>
    public class DataBaseSettings
    {
        public string ConnectionString { get; private set; } = null!;
    }
}
