using MainTz.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using System.ComponentModel;
using System.Reflection;

namespace MainTz.Web
{
    //public partial class KkdtsDbContext : DbContext
    //{
    //    public const string DbScheme = "kkdts";
    //    protected readonly DataBaseOptions _options;

    //    static KkdtsDbContext()
    //    {
    //        NpgsqlConnection.GlobalTypeMapper.MapEnum<AccessType>();
    //        NpgsqlConnection.GlobalTypeMapper.MapEnum<CtpAccessMode>();
    //        NpgsqlConnection.GlobalTypeMapper.MapEnum<LaneDirection>();
    //    }

    //    public KkdtsDbContext(IOptions<DataBaseOptions> options)
    //    {
    //        _options = options.Value;
    //    }

    //    public KkdtsDbContext(DataBaseOptions options)
    //    {
    //        _options = options;
    //    }

    //    #region [ DbSets ]

    //    public DbSet<BlobEntity> Blobs { get; set; }

    //    /// <summary>
    //    /// Роли
    //    /// </summary>
    //    public DbSet<RoleEntity> Roles { get; set; }

    //    #endregion

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);

    //        modelBuilder.UseValueConverter(new DateTimeConverter());
    //        modelBuilder.UseValueConverter(new NullableDateTimeConverter());
    //        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(KkdtsDbContext))!);
    //    }
    //}
}
