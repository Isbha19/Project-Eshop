using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Eshop.Model.Models
{
	public class SalesReport
	{
	

		public string? MonthName { get; set; }
		[JsonConverter(typeof(JsonDateConverter))]
		public DateTime Date { get; set; }
		public DateTime Year { get; set; }
        public int SalesCount { get; set; }
        public double OrderAmount { get; set; }
        public double DiscountAmount {  get; set; }
        public double DiscountDeduction { get; set; }
    }
	public class JsonDateConverter : JsonConverter<DateTime>
	{
		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// Implement if needed
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
		}
	}
}
