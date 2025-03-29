using FoodDiary.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoodDiary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly DbBackupHelper _backupHelper;

        public DatabaseController(DbBackupHelper backupHelper)
        {
            _backupHelper = backupHelper;
        }

        /// <summary>
        /// Creates a backup of the current database
        /// </summary>
        /// <returns>Path to the created backup file</returns>
        [HttpPost("backup")]
        public async Task<IActionResult> CreateBackup()
        {
            try
            {
                var backupPath = await _backupHelper.CreateBackupAsync();
                return Ok(new { message = "Backup created successfully", path = backupPath });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "Failed to create backup", message = ex.Message });
            }
        }
    }
}
