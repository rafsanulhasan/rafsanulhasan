
using System;

using Microsoft.AspNetCore.Identity;

namespace RH.Apps.Web.SPA.Server.Models
{
	public class UserToken : IdentityUserToken<Guid>
	{
		public User User { get; set; }
	}
}
