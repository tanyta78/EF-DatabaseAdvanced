namespace FastFood.DataProcessor.Dto.Export
{
    using System.Collections.Generic;
    using System.Linq;

    internal class ExportEmployeeDto
    {
        public string Name { get; set; }
        
        public IEnumerable<ExportOrderDto> Orders { get; set; }

        public decimal TotalMade => this.Orders.Sum(o => o.TotalPrice);
    }
}