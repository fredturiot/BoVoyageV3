using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageV3.Models
{
	public enum TypeAssurance : byte
	{
		Annulation = 1
	}

	public class Assurance
	{
		public int ID { get; set; }

		[Required]
		[Column(TypeName = "Money")]
		[Index("IX_MontantType", 1, IsUnique = true)]
		public decimal Montant { get; set; }

		[Required]
		[EnumDataType(typeof(TypeAssurance))]
		[Index("IX_MontantType", 2, IsUnique = true)]
		public TypeAssurance Type { get; set; }

		public ICollection<DossierReservation> DossiersReservations { get; set; }
	}
}