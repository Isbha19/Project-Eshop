using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.Models
{
	public class Wallet
	{
		public int Id { get; set; }
		public double Amount {  get; set; }
		public DateTime TransactionDate { get; set; }
		public String TransactionType { get; set; }
		public int WalletId { get; set; }
		[ValidateNever]
		[ForeignKey("WalletId")]
		public WalletHeader? WalletHeader { get; set; }


	}
}
