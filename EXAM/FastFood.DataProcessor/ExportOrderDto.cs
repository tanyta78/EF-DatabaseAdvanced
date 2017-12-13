namespace FastFood.DataProcessor
{
    internal class ExportOrderDto
    {
        public string Customer { get; set; }
        public System.Collections.Generic.IEnumerable<object> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}