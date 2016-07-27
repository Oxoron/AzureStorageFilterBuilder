using System;
using System.Collections.Generic;
using System.Linq;
using AzureStorageFilterBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorageFilterBuilder_UnitTests.EnvironmentDependedTests
{
    [TestClass]
    public class LocalEnvironmentTests
    {
        private static readonly LocalAzureStorage _storage = new LocalAzureStorage();

        // If you Want to to test something locally - make this method public and run it.
        [TestMethod]
        private void EmulatorTest()
        {
            string PK = 123.ToString();
            string RK = Guid.NewGuid().ToString();
            const string ETag = "*";


            DynamicTableEntity dte = new DynamicTableEntity(PK, RK, ETag, new Dictionary<string, EntityProperty>
            {
                { "doubleValue", new EntityProperty(12.34)}
            });

            new LocalAzureStorage().Insert(dte);

            string request = AzureStorageFilter.Empty
                .Not().OpenBracket()
                .Column("doubleValue").Equal().Const(Double.Epsilon)
                .CloseBracket()
                .Result;

            double actual = new LocalAzureStorage().FindBy(request)[0].Properties["doubleValue"].DoubleValue.Value;

            Assert.AreEqual(12.34, actual);
        }

        [TestMethod]
        private void TestAll()
        {
            // TODO Clear test table before test

            string request;
            Func<AzureStorageFilter> emptyFilter = () => AzureStorageFilter.Empty;

            // Const(int)
            InsertValue("int", new EntityProperty(42));
            request = emptyFilter()
                .Column("int").Equal().Const(42)
                .Result;
            if (_storage.FindBy(request).Count < 1)
            {
                Assert.Fail("const(int) fail");
            }


            // Const (double)
            InsertValue("double", new EntityProperty(42.42));
            request = emptyFilter()
                .Column("double").Equal().Const(42.42)
                .Result;
            if (_storage.FindBy(request).Count < 1)
            {
                Assert.Fail("const(double) fail");
            }

            // Const (guid)
            InsertValue("guid", new EntityProperty(Guid.NewGuid()));
            request = emptyFilter()
                .Column("guid").NotEqual().Const(Guid.NewGuid())
                .Result;
            if (_storage.FindBy(request).Count < 1)
            {
                Assert.Fail("const(guid) fail");
            }


            // Const (bool)
            InsertValue("bool", new EntityProperty(false));
            request = emptyFilter()
                .Column("bool").Equal().Const(false)
                .Result;
            if (_storage.FindBy(request).Count < 1)
            {
                Assert.Fail("const(bool) fail");
            }

            // Const (DateTime)
            InsertValue("DateTime", new EntityProperty(new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc)));
            request = emptyFilter()
                .Column("DateTime").Equal().Const(new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .Result;
            if (_storage.FindBy(request).Count < 1)
            {
                Assert.Fail("const(DateTime) fail");
            }


            // In()
            InsertValue("In", new EntityProperty(1));
            InsertValue("In", new EntityProperty(2));
            InsertValue("In", new EntityProperty(3));
            request = emptyFilter()
                .In("In", 2, 3, 4, 5)
                .Result;
            if (_storage.FindBy(request).Count != 2)
            {
                Assert.Fail("In failed");
            }

            // Between
            InsertValue("Between", new EntityProperty(1));
            InsertValue("Between", new EntityProperty(5));
            InsertValue("Between", new EntityProperty(10));
            request = emptyFilter()
                .Between("Between", 3, 7)
                .Result;
            if (_storage.FindBy(request).Count != 1)
            {
                Assert.Fail("Between failed");
            }


            // Starts with
            InsertValue("SW", new EntityProperty("Anna May"));
            InsertValue("SW", new EntityProperty("Anna Night"));
            InsertValue("SW", new EntityProperty("Anna Noise"));
            InsertValue("SW", new EntityProperty("Anna Stone"));
            InsertValue("SW", new EntityProperty("Betty Rump"));
            request = emptyFilter()
                .StartsWith("SW", "Anna N")
                .Result;
            var result = _storage.FindBy(request).Select(dte => dte.Properties["SW"].StringValue).ToArray();
            if (!result.Contains("Anna Night") || !result.Contains("Anna Noise"))
            {
                Assert.Fail("StartsWith failed");
            }
        }
        private static void InsertValue(string columnName, EntityProperty value)
        {
            string PK = Guid.NewGuid().ToString();
            string RK = Guid.NewGuid().ToString();
            const string ETag = "*";
            DynamicTableEntity dte = new DynamicTableEntity(PK, RK, ETag, new Dictionary<string, EntityProperty>
            {
                { columnName, value}
            });
            _storage.Insert(dte);
        }
    }
}