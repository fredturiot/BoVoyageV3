using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageV3.Models
{
	[Table("AgencesVoyages")]
	public class AgenceVoyage
	{
		public int ID { get; set; }

		[Required]
		[Index(IsUnique = true)]
		[StringLength(60, MinimumLength = 5, ErrorMessage = "Le nom de l'agence doit avoir de 5 a 60 caracteres")]
		public string Nom { get; set; }

		public ICollection<Voyage> Voyages { get; set; }
	}
}