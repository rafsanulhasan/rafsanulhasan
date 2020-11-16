
using System;
using System.Threading.Tasks;

using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RH.Apps.Web.SPA.Server.Models;

namespace RH.Apps.Web.SPA.Server.Data
{
	public class ApplicationDbContext
		: IdentityDbContext<User, Role, Guid, Models.UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
		  IPersistedGrantDbContext,
		  IConfigurationDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		    : base(options)
		{
		}

		public DbSet<PersistedGrant> PersistedGrants { get; set; }
		public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
		public DbSet<IdentityResource> IdentityResources { get; set; }
		public DbSet<ApiResource> ApiResources { get; set; }
		public DbSet<ApiScope> ApiScopes { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
			builder.Entity<UserLogin>().HasNoKey();
			builder.Entity<UserToken>().HasNoKey();

			builder.Entity<PersistedGrant>().HasNoKey();
			builder.Entity<DeviceFlowCodes>().HasNoKey();

			builder
				.Entity<ClientCorsOrigin>()
				.HasOne(cco => cco.Client)
				.WithMany(c => c.AllowedCorsOrigins)
				.HasForeignKey(cco => cco.ClientId);

			builder
				.Entity<IdentityResource>()
				.HasMany(ir => ir.UserClaims)
				.WithOne(irc => irc.IdentityResource)
				.HasForeignKey(irc => irc.IdentityResourceId);

			builder
				.Entity<ApiResource>()
				.HasMany(ar => ar.UserClaims)
				.WithOne(arc => arc.ApiResource)
				.HasForeignKey(arc => arc.ApiResourceId);

			builder
				.Entity<ApiScope>()
				.HasMany(@as => @as.UserClaims)
				.WithOne(asc => asc.Scope)
				.HasForeignKey(asc => asc.ScopeId);
		}

		public Task<int> SaveChangesAsync()
			=> SaveChangesAsync(true);
	}
}
