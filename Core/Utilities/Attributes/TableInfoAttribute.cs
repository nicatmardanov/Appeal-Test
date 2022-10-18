namespace Core.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableInfoAttribute : Attribute
    {
        public string TableName { get; set; }
        public string OrderBy { get; set; }

        public TableInfoAttribute(string tableName, string? orderBy = null)
        {
            TableName = tableName;
            OrderBy = $" ORDER BY {(!string.IsNullOrEmpty(orderBy) ? orderBy : "CreatedAt desc")}";
        }
    }
}
