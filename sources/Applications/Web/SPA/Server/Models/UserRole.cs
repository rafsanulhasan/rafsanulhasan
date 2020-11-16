
using System;

using Microsoft.AspNetCore.Identity;

namespace RH.Apps.Web.SPA.Server.Models
{
	public class UserRole : IdentityUserRole<Guid>
	{
		public User User { get; set; }
		public Role Role { get; set; }
	}
}
