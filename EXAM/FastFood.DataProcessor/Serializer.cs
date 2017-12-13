using System;
using System.IO;
using FastFood.Data;

namespace FastFood.DataProcessor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Dto.Export;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
	{
		public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
		{

		    var emplOrders = context.Employees
		        .Where(e => e.Name == employeeName)
		        .Include(e => e.Orders)
		        .ThenInclude(o=>o.OrderItems)
		        .ThenInclude(oi=>oi.Order)
		        .Select(e => new ExportEmployeeDto()
		        {
		            Name = e.Name,
		            Orders = e.Orders.Select(o => new ExportOrderDto()
		            {
		                Customer = o.Customer,
		                Items = o.OrderItems.Select(oi => new
		                {
		                    Name = oi.Item.Name,
		                    Price = oi.Item.Price,
		                    Quantity = oi.Quantity
		                }),
		                TotalPrice = o.OrderItems.Sum(oi=>oi.Item.Price*oi.Quantity)
		            })
		           
		        });
		    
		

		    var json = JsonConvert.SerializeObject(emplOrders.First(), Formatting.Indented);

		    return json;
        }

	    public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
	    {

	        var categoriesNames = categoriesString.Split(new char[] {','});
	        var categoriesResult = new List<CategoryDto>();
	        

            foreach (var nameCategory in categoriesNames)
            {
                var category = context.Categories
                    .Include(c => c.Items)
                    .ThenInclude(i => i.OrderItems)
                    .First(c => c.Name == nameCategory);

                var mostPopulatedItem = category.Items
	                .Select(i => new ExportItemDto()
	                {
	                    Name = i.Name,
	                    TotalMade = i.OrderItems.Sum(oi => oi.Quantity * oi.Item.Price),
	                    TimesSold = i.OrderItems.Sum(oi=>oi.Quantity)

	                })
	            .OrderByDescending(e=>e.TotalMade)
	                .First();
	            
	            var categoryRes =new CategoryDto()
	            {
	                Name = category.Name,
	                MostPopulatarItem = mostPopulatedItem
	            };
	            
	            categoriesResult.Add(categoryRes);
	        }

	      var res=  categoriesResult.OrderByDescending(c => c.MostPopulatarItem.TotalMade)
	            .ThenByDescending(c => c.MostPopulatarItem.TimesSold).ToArray();
            
            var sb = new StringBuilder();

	        var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("Categories"));
	        serializer.Serialize(new StringWriter(sb), res, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

	        var result = sb.ToString();
	        return result;

        }
	    
	}

    
}