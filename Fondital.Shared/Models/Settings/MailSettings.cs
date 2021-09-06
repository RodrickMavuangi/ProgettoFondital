using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Shared.Models.Settings
{
	public class MailSettings
	{
		public string Mail { get; set; }
		public string DisplayName { get; set; }
		public string Password { get; set; }
		public string Host { get; set; }
		public string Port { get; set; }
	}
}
