using Microsoft.EntityFrameworkCore;
using NotificationService.Contracts.Constants;
using NotificationService.Domain;
using NotificationService.Infrastructure.Extensions;
using System.Security.Claims;

namespace NotificationService.Infrastructure;

public class NotificationDbContext : DbContext
{
    private readonly IConfiguration configuration;
    private readonly IHttpContextAccessor httpContextAccessor;

    public Guid CurrentUserId
    {
        get
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(CustomClaims.BIEId);

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedId))
            {
                throw new Exception("Missing user id");
            }

            return parsedId;
        }
    }

    public NotificationDbContext(
        DbContextOptions options,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor) : base(options)
    {
        this.configuration = configuration;
        this.httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.SetAuditProperties(CurrentUserId);
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ChangeTracker.SetAuditProperties(CurrentUserId);
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        ChangeTracker.SetAuditProperties(CurrentUserId);
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ChangeTracker.SetAuditProperties(CurrentUserId);
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public DbSet<Notification> Notifications => Set<Notification>();
}
