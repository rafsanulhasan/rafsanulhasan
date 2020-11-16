
using System;

using Microsoft.AspNetCore.Identity;

namespace RH.Apps.Web.SPA.Server.Models
{
	public class UserClaim : IdentityUserClaim<Guid>
	{
		public User User { get; set; }
	}
}
