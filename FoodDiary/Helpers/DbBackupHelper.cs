using Microsoft.Data.Sqlite;

namespace FoodDiary.Helpers
{
    public class DbBackupHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DbBackupHelper> _logger;
        private readonly string _connectionString;
        private readonly string _backupDirectory;

        public DbBackupHelper(IConfiguration configuration, ILogger<DbBackupHelper> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = ConfigHelper.GetRequiredConfig(configuration, "ConnectionStrings:DefaultConnection", "DATABASE_CONNECTION");
            
            // Get backup directory from config or use default in app root
            _backupDirectory = configuration["BackupSettings:Directory"] ?? 
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
            
            // Ensure backup directory exists
            if (!Directory.Exists(_backupDirectory))
            {
                Directory.CreateDirectory(_backupDirectory);
            }
        }

        /// <summary>
        /// Creates a backup of the SQLite database with a timestamp in the filename
        /// </summary>
        /// <returns>The path to the created backup file</returns>
        public async Task<string> CreateBackupAsync()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var backupFileName = $"FoodDiary_Backup_{timestamp}.db";
            var backupFilePath = Path.Combine(_backupDirectory, backupFileName);

            try
            {
                // Create a connection to the source database
                using var sourceConnection = new SqliteConnection(_connectionString);
                await sourceConnection.OpenAsync();

                // Create a connection for the backup file
                using var backupConnection = new SqliteConnection($"Data Source={backupFilePath}");
                
                // Perform the backup
                sourceConnection.BackupDatabase(backupConnection);
                
                _logger.LogInformation("Database backup created successfully at {BackupPath}", backupFilePath);
                return backupFilePath;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create database backup");
                throw;
            }
        }
    }
}
