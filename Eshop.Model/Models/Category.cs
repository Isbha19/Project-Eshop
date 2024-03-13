using Eshop.Model.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Models;

public class Category
{
	public int Id { get; set; }
	[Required(ErrorMessage = "Enter a category name")]
	public String? CategoryName { get; set; }
	[Required(ErrorMessage = "Enter a category Description")]
	public string? CategoryDescription { get; set; }
	public string? ImageUrl { get; set; }
	[ValidateNever]

	public List<Product> products { get; set; }
	public bool IsDiscount { get; set; }
}
