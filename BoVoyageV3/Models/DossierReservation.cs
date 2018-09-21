using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageV3.Models
{
	public enum EtatDossierReservation : byte
	{
		EnAttente,
		EnCours,
		Refusee,
		Acceptee
	}

	public enum RaisonAnnulationDossier : byte
	{
		Client = 1,
		PlacesInsuffisantes
	}

	[Table("DossiersReservations")]
	public class DossierReservation
	{
		public int ID { get; set; }

		[NotMapped]
		public int NumeroUnique
		{
			get
			{
				Guid guid = new Guid();
				return guid.GetHashCode();
			}
		}

		[Required]
		[StringLength(20, MinimumLength = 5, ErrorMessage = "La numero de CB doit avoir de 5 a 20 caracteres")]
		public string NumeroCarteBancaire { get; set; }

		[NotMapped]
		public decimal PrixParPersonne
		{
			get
			{
				//const decimal VAPrix = 1.25m;
				//decimal totalAssurances = 0.0m;
				//foreach (Assurance assurance in Assurances)
				//	totalAssurances += assurance.Montant;

				//return Voyage.PrixParPersonne * VAPrix + totalAssurances;
				return 100.0m;
			}
		}

		[NotMapped]
		public decimal PrixTotal => 100.0m;

		[Required]
		[EnumDataType(typeof(EtatDossierReservation))]
		public EtatDossierReservation Etat { get; set; }

		public int ClientID { get; set; }

		[ForeignKey("ClientID")]
		public Client Client { get; set; }

		public int VoyageID { get; set; }

		[ForeignKey("VoyageID")]
		public Voyage Voyage { get; set; }

		public ICollection<Assurance> Assurances { get; set; }

		public ICollection<Participant> Participants { get; set; }
	}
}