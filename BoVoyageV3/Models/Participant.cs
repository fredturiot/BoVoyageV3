using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageV3.Models
{
	public class Participant : Personne
	{
		[NotMapped]
		public int NumeroUnique => ID;

		[NotMapped]
		public float Reduction
		{
			get
			{
				if (Age < 12)
					return 0.6f;
				else
					return 1.0f;
			}
		}

		public int DossierReservationID { get; set; }

		[ForeignKey("DossierReservationID")]
		public DossierReservation DossierReservation { get; set; }
	}
}