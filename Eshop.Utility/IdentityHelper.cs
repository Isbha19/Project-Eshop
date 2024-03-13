using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Utility
{
	public static class IdentityHelper
	{

		public static string? GetUserId(ClaimsPrincipal user)
		{
			var claimsIdentity = (ClaimsIdentity?)user.Identity;
			return claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}
	}

}
