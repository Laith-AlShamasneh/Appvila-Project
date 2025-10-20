using Microsoft.EntityFrameworkCore;

namespace AppvilaAPI.Helpers.Logger;

public class LoggerDbContext(DbContextOptions<LoggerDbContext> options) : DbContext(options)
{
}
