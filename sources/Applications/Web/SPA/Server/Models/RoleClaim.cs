
using System;

using Microsoft.AspNetCore.Identity;

namespace RH.Apps.Web.SPA.Server.Models
{
	public class RoleClaim : IdentityRoleClaim<Guid>
	{
		public Role Role { get; set; }
	}
}
