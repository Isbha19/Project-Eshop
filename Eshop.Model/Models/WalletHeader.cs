using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.Models
{
	public class WalletHeader
	{
        public int Id { get; set; }
        public string UserId { get; set; }

		public double Balance { get; set; } = 0;
		public List<Wallet> wallets { get; set; }


	}
}
