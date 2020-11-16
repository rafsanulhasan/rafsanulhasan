
using System;

using Microsoft.AspNetCore.Identity;

namespace RH.Apps.Web.SPA.Server.Models
{
	public class UserLogin : IdentityUserLogin<Guid>
	{
		public User User { get; set; }
	}
}
