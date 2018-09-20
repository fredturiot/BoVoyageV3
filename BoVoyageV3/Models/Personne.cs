using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageV3.Models
{
	public abstract class Personne
	{
		public int ID { get; set; }

		[Required]
		[StringLength(20, MinimumLength = 1, ErrorMessage = "La civilite doit avoir de 1 a 20 caracteres")]
		[Index("IX_PersonneUnique", 1, IsUnique = true)]
		public string Civilite { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "Le nom doit avoir de 2 a 30 caracteres")]
		[Index("IX_PersonneUnique", 2, IsUnique = true)]
		public string Nom { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "La prenom doit avoir de 2 a 30 caracteres")]
		[Index("IX_PersonneUnique", 3, IsUnique = true)]
		public string Prenom { get; set; }

		[Required]
		[StringLength(60, MinimumLength = 5, ErrorMessage = "L'adresse doit avoir de 5 a 60 caracteres")]
		[Index("IX_PersonneUnique", 4, IsUnique = true)]
		public string Adresse { get; set; }

		[Required]
		[StringLength(20, MinimumLength = 3, ErrorMessage = "Lu numero de telephone doit avoir de 3 a 20 caracteres")]
		public string Telephone { get; set; }

		[Required]
		public DateTime DateNaissance { get; set; }

		[NotMapped]
		public int Age
		{
			get
			{
				DateTime today = DateTime.Today;
				int age = today.Year - DateNaissance.Year;
				return age;
			}
		}
	}
}