using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageV3.Models
{
	public class Destination
	{
		public int ID { get; set; }

		[Required]
		[Index("IX_ContinentPaysRegion", 1, IsUnique = true)]
		[StringLength(40, MinimumLength = 5, ErrorMessage = "Le nom du continent doit avoir de 5 a 40 caracteres")]
		public string Continent { get; set; }

		[Required]
		[Index("IX_ContinentPaysRegion", 2, IsUnique = true)]
		[StringLength(40, MinimumLength = 2, ErrorMessage = "Le nom du pays doit avoir de 2 a 40 caracteres")]
		public string Pays { get; set; }

		[Required]
		[Index("IX_ContinentPaysRegion", 3, IsUnique = true)]
		[StringLength(40, MinimumLength = 3, ErrorMessage = "Le nom de la region doit avoir de 3 a 60 caracteres")]
		public string Region { get; set; }

		public string Description { get; set; }

		public ICollection<Voyage> Voyages { get; set; }
	}
}