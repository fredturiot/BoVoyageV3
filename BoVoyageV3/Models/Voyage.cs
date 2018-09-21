using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageV3.Models
{
	public class Voyage
	{
		public int ID { get; set; }

		[Required]
		[Index("IX_DatesAgenceDestination", 1, IsUnique = true)]
		public DateTime DateAller { get; set; }

		[Required]
		[Index("IX_DatesAgenceDestination", 2, IsUnique = true)]
		public DateTime DateRetour { get; set; }

		[Required]
		public int PlacesDisponibles { get; set; }

		[Required]
		[Column(TypeName = "Money")]
		public decimal PrixParPersonne { get; set; }

		[Index("IX_DatesAgenceDestination", 3, IsUnique = true)]
		public int AgenceVoyageID { get; set; }

		[ForeignKey("AgenceVoyageID")]
		public AgenceVoyage AgenceVoyage { get; set; }

		[Index("IX_DatesAgenceDestination", 4, IsUnique = true)]
		public int DestinationID { get; set; }

		[ForeignKey("DestinationID")]
		public Destination Destination { get; set; }

		public ICollection<DossierReservation> DossiersReservations { get; set; }
	}
}