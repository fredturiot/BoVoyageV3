using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BoVoyageV3.Data
{
	public class BoVoyageContext : DbContext
	{
		public BoVoyageContext() : base("BoVoyageV3ConnectionString") {}
	}
}