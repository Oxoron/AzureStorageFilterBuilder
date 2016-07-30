using System;
using AzureStorageFilterBuilder;

namespace Examples
{
    class Program
    {
        // This property always returns new filter
        private static AzureStorageFilter EmptyFilter => AzureStorageFilter.Empty;

        static void Main(string[] args)
        {
            // Filter partitionKey
            string filter = EmptyFilter
                .PartitionKey().Equal("MyPartitionKey");
            
            // Or row key 
            filter = EmptyFilter
                .RowKey().Equal(92001232);
            

            // Or full key
            filter = EmptyFilter
                .PartitionKey().Equal("Italy")
                .And()
                .RowKey().Equal("Vito@Corleone.it");




            // You can filter teenagers used column named "Age" 
            filter = EmptyFilter
                .Column("Age").GreaterThanOrEqual(18);

            // Or filter "BirthDay" column with the same purposes
            var eightteenYearsAgo = DateTime.UtcNow.Subtract(new TimeSpan(18*365, 0, 0, 0));
            filter = EmptyFilter
                .Column("BirthDay").GreaterThan(eightteenYearsAgo);




            // Use standard logical operators
            filter = EmptyFilter
                .PartitionKey().Equal(1234)
                .Or()
                .PartitionKey().Equal(5678)
                .And().Not().OpenBracket()
                .RowKey().GreaterThan(90)
                .CloseBracket();



            // If you want to select one of some values - use In
            filter = EmptyFilter
                .In("WarStartYear", 1914, 1939, 2001);

            // Other way:
            var warYears = new[] {1914, 1939, 2001};
            filter = EmptyFilter
                .In("WarStartYear", warYears);





            // Wanna take a diapason - you're welcome
            var now = DateTime.UtcNow;
            var monthStart = new DateTime(now.Year, now.Month, 1, 0,0,0,DateTimeKind.Utc);
            filter = EmptyFilter
                .Between("Date", monthStart, now); 



            // And finally, if you want select all string values started form  "A" in your Name column
            filter = EmptyFilter
                .StartsWith("Name", "A");

        }
    }
}
