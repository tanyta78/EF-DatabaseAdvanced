namespace FastFood.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using Dto.Import;
    using Models;
    using Models.Enums;
    using Newtonsoft.Json;
    using ValidationContext = AutoMapper.ValidationContext;

    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var employeeDtos = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);

            var validEmployees = new List<Employee>();
            var validPositions = new List<Position>();
            foreach (var employeeDto in employeeDtos)
            {
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var validPosition = validPositions.FirstOrDefault(p => p.Name == employeeDto.Position);

                if (validPosition == null)
                {
                    validPosition = new Position()
                    {
                        Name = employeeDto.Position
                    };

                    validPositions.Add(validPosition);

                }

                var employee = new Employee()
                {
                    Age = employeeDto.Age,
                    Position = validPosition,
                    Name = employeeDto.Name
                };

                validEmployees.Add(employee);

                sb.AppendLine(string.Format(SuccessMessage, employee.Name));
            }

            context.Positions.AddRange(validPositions);

            context.Employees.AddRange(validEmployees);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var itemsDtos = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);

            var validItems = new List<Item>();
            var validCategories = new List<Category>();
            foreach (var itemDto in itemsDtos)
            {
                if (!IsValid(itemDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var itemAlreadyExists = validItems.Any(i => i.Name == itemDto.Name) || context.Items.Any(i => i.Name == itemDto.Name);

                if (itemAlreadyExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var validCategory = validCategories.FirstOrDefault(c => c.Name == itemDto.Category);

                if (validCategory == null)
                {
                    validCategory = new Category()
                    {
                        Name = itemDto.Category
                    };

                    validCategories.Add(validCategory);

                }

                var item = new Item()
                {
                    Name = itemDto.Name,
                    Price = itemDto.Price,
                    Category = validCategory
                };


                validItems.Add(item);

                sb.AppendLine(string.Format(SuccessMessage, itemDto.Name));
            }
            context.Categories.AddRange(validCategories);
            context.Items.AddRange(validItems);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));
            var ordersDtos = (OrderDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var sb = new StringBuilder();

            var validOrders = new List<Order>();
            var validOrderItems = new List<OrderItem>();
            foreach (var orderDto in ordersDtos)
            {
                if (!IsValid(orderDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var orderDateTime = DateTime.ParseExact(orderDto.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                var employee = context.Employees.FirstOrDefault(e => e.Name == orderDto.Employee);

                if (employee == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var orderType = Enum.TryParse<OrderType>(orderDto.Type, out var type) ? type : OrderType.ForHere;

                var orderItems = orderDto.Items;

                var isOrderItemsExist = orderDto.Items.All(e => context.Items.Any(i => i.Name == e.Name));

                var isQuantitiesValid = orderDto.Items.All(i => i.Quantity > 0);

                if (!isOrderItemsExist )
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!isQuantitiesValid)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                
                var validOrder = new Order()
                {
                    Customer = orderDto.Customer,
                    Employee = employee,
                    DateTime = orderDateTime,
                    Type = type
                   
                };
                foreach (var orderItemDto in orderItems)
                {
                    var item = context.Items.FirstOrDefault(i => i.Name == orderItemDto.Name);
                    var quantity = orderItemDto.Quantity;
                    var validOrderItem = new OrderItem()
                    {
                        Item = item,
                        Order = validOrder,
                        Quantity = quantity
                    };
                    
                    validOrder.OrderItems.Add(validOrderItem);
                    validOrderItems.Add(validOrderItem);
                }
                

                validOrders.Add(validOrder);
                sb.AppendLine(string.Format($"Order for {validOrder.Customer} on {orderDateTime:g} added"));
            }

            context.Orders.AddRange(validOrders);
            context.OrderItems.AddRange(validOrderItems);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }
    }
}