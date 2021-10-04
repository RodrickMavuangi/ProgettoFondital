using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fondital.Shared.Models.Auth
{
	[NotMapped]
	public class UserRole : IdentityUserRole<int>
	{
		//public int UserId { get; set; }
		//public int RoleId { get; set; }
	}
}
