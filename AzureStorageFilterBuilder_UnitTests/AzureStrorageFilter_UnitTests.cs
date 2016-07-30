using System;
using AzureStorageFilterBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureStorageFilterBuilder_UnitTests
{
    [TestClass]
    public class AzureStrorageFilter_UnitTests
    {
        private static readonly Func<AzureStorageFilter> EmptyFilter = () => AzureStorageFilter.Empty;

        [TestMethod]
        public void EmptyTest()
        {
            Assert.AreEqual(String.Empty, EmptyFilter());
            string filter = AzureStorageFilter.Empty
                .PartitionKey().Equal("AnyPartitionKey")
                .And()
                .RowKey().Equal("AnyRowKey");
        }

        #region Base types conversion tests (Const() method)
        [TestMethod]
        public void Const_StringArgument_CorrectValue()
        {
            //Arrange
            const string expected = "'Andrew'";

            //Act
            var actual = EmptyFilter()
                .Const("Andrew");

            //Assert
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void Const_StringArgument_EmptyValue()
        {
            //Arrange
            const string expected = "''";

            //Act
            var actual = EmptyFilter()
                .Const("");

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod,ExpectedException(typeof(FilterBuilderException))]
        public void Const_StringArgument_NullValue_ExceptionExpected()
        {
            var actual = EmptyFilter()
                .Const((string)null);

            Assert.Fail("Null string should cause en exception");
        }

        [TestMethod]
        public void Const_IntegerArgument_CorrectValue()
        {
            //Arrange
            const string expected = "30"; 

            //Act
            var actual = EmptyFilter()
                .Const(30);

            //Assert
            Assert.AreEqual(expected, actual);

        }
        
        [TestMethod]
        public void Const_DoubleArgument_CorrectValue()
        {
            // Zeros quantity after last number is not important  

            //Arrange
            const string expected = "100.25"; 

            //Act
            var actual = EmptyFilter()
                .Const(100.250);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod, ExpectedException(typeof(FilterBuilderException))]
        public void Const_DoubleArgument_NaN_ExceptionExpected()
        {
            //Arrange
            const string expected = "100.25";

            //Act
            var actual = EmptyFilter()
                .Const(Double.NaN);

            //Assert
            Assert.Fail("Conversion of Double.NaN is denied and should cause an exception");
        }

        [TestMethod, ExpectedException(typeof(FilterBuilderException))]
        public void Const_DoubleArgument_PositiveInfinity_ExceptionExpected()
        {
            //Arrange
            const string expected = "100.25";

            //Act
            var actual = EmptyFilter()
                .Const(Double.PositiveInfinity);

            //Assert
            Assert.Fail("Conversion of Double.PositiveInfinity is denied and should cause an exception");
        }

        [TestMethod, ExpectedException(typeof(FilterBuilderException))]
        public void Const_DoubleArgument_NegativeInfinity_ExceptionExpected()
        {
            //Arrange
            const string expected = "100.25";

            //Act
            var actual = EmptyFilter()
                .Const(Double.NegativeInfinity)
                ;

            //Assert
            Assert.Fail("Conversion of Double.NegativeInfinity is denied and should cause an exception");
        }

        [TestMethod]
        public void Const_BoolArgument_CorrectValue()
        {
            //Arrange
            const string expected = "true";

            //Act
            var actual = EmptyFilter()
                .Const(true)
                ;

            //Assert
            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void Const_GuidArgument_CorrectValue()
        {
            //Arrange
            const string expected = "guid'a455c695-df98-5678-aaaa-81d3367e5a34'";

            //Act
            var actual = EmptyFilter()
                .Const(Guid.Parse("a455c695-df98-5678-aaaa-81d3367e5a34"))
                ;

            //Assert
            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void Const_LongArgument_CorrectValue()
        {
            //Arrange
            const string expected = "1000000000000000";

            //Act
            var actual = EmptyFilter()
                .Const(1000000000000000)
                ;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Const_DateTimeArgument_CorrectValue()
        {
            // Arrange
            const string expected = "datetime'2008-07-10T00:00:00Z'";

            // Act
            var actual = EmptyFilter()
                .Const(new DateTime(2008, 7, 10, 0, 0, 0, DateTimeKind.Utc))
                ;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Column() tests
        [TestMethod, ExpectedException(typeof(FilterBuilderException))]
        public void Column_WhitespacesColumnNameArgument_ExceptionExpected()
        {
            // Act
            string request = EmptyFilter()
                .Column(" ");

            // Assert
            Assert.Fail("Whitespace columnName argument for Column method should cause an exception");
        }

        [TestMethod, ExpectedException(typeof(FilterBuilderException))]
        public void Column_WhitespaceSeparatedColumnNameArgument_ExceptionExpected()
        {
            // Act
            string request = EmptyFilter()
                .Column("Name With Whitespaces");

            // Assert
            Assert.Fail("columnName argument with whitespaces for Column method should cause an exception");
        }

        [TestMethod]
        public void Column_WhitespaceFramedColumnNameArgument()
        {
            // Column can be framed by whitespaces. They should be cut inside Column() method. 

            // Arrange
            const string expected = "TwoWhitespacesFrameTheName";

            // Act
            string request = EmptyFilter()
                .Column(" TwoWhitespacesFrameTheName ");

            // Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod]
        public void Column_CorrectArgument()
        {
            // Arrange
            const string expected = "Name";

            // Act
            string request = EmptyFilter()
                .Column("Name");

            // Assert
            Assert.AreEqual(expected, request);
        }
        #endregion

        #region InMethodTests
        [TestMethod]
        public void In_StringArguments()
        {
            // Arrange
            const string expected = "(Name eq 'Alexander' or Name eq 'Andrew')";

            // Act
            string request = EmptyFilter()
                .In("Name", "Alexander", "Andrew");

            // Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod, ExpectedException(typeof(FilterBuilderException))]
        public void In_EmptyArrayArgument()
        {
           // Act
            string request = EmptyFilter()
                .In("Name", new string[] {});

            // Assert
            Assert.Fail("In method should throw an exception on empty argument array");
        }

        [TestMethod, ExpectedException(typeof(FilterBuilderException))]
        public void In_NullArray()
        {
            // Act
            string request = EmptyFilter()
                .In("Name", (string[])null);

            // Assert
            Assert.Fail("In method should throw an exception on null argument array");
        }

        [TestMethod]
        public void In_SingleValueArray()
        {
            // Arrange
            const string expected = "(Name eq 'Andrew')";

            // Act
            string request = EmptyFilter()
                .In("Name", "Andrew");

            // Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod]
        public void In_IntArguments()
        {
            // Arrange
            const string expected = "(Age eq 20 or Age eq 27)";

            // Act
            string request = EmptyFilter()
                .In("Age", 20, 27);

            // Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod]
        public void In_LongArguments()
        {
            // Arrange
            const string expected = "(Time eq 200000000000000000 or Time eq 200000000000000001)";

            // Act
            string request = EmptyFilter()
                .In("Time", 200000000000000000, 200000000000000001);

            // Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod]
        public void In_DoubleArguments()
        {
            // Arrange
            const string expected = "(Weight eq 20.5 or Weight eq 27.5)";

            // Act
            string request = EmptyFilter()
                .In("Weight", 20.50, 27.50);

            // Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod]
        public void In_DateTimeArguments()
        {
            // Arrange
            const string expected = "(Time eq datetime'2008-07-10T00:00:00Z' or Time eq datetime'2007-07-10T00:00:00Z')";

            // Act
            string request = EmptyFilter()
                .In("Time", new DateTime(2008, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                            new DateTime(2007, 7, 10, 0, 0, 0, DateTimeKind.Utc));

            // Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod]
        public void In_GuidArguments()
        {
            // Arrange
            const string expected = "(GuidNumber eq guid'a455c695-df98-5678-aaaa-81d3367e5a34' or GuidNumber eq guid'a455c695-df98-5678-aaaa-81d3367e5a35')";

            // Act
            string request = EmptyFilter()
                .In("GuidNumber",   Guid.Parse("a455c695-df98-5678-aaaa-81d3367e5a34"),
                                    Guid.Parse("a455c695-df98-5678-aaaa-81d3367e5a35"));

            // Assert
            Assert.AreEqual(expected, request);
        }
        #endregion

        #region Between Tests 
        [TestMethod]
        public void Between_IntArguments()
        {
            // Arrange
            const string expected = "(Qty ge 1 and Qty le 5)";

            // Act
            string request = EmptyFilter()
                .Between("Qty", 1, 5);

            // Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod]
        public void Beetween_LongArguments()
        {
            //Arrange
            const string expected = "(Qty ge 1 and Qty le 999999)";
            const long min = 1;
            const long max = 999999;

            //Act
            string request = EmptyFilter().
                Between("Qty", min, max);

            //Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod]
        public void Beetween_DoubleArguments()
        {
            //Arrange
            const string expected = "(Qty ge 1.6 and Qty le 999999.6)";
            const double min = 1.600;
            const double max = 999999.600;

            //Act
            string request = EmptyFilter().
                Between("Qty", min, max);

            //Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod]
        public void Beetween_StringArguments()
        {
            //Arrange
            const string min = "Abcd";
            const string max = "Efgh";
            const string expected = "(Qty ge 'Abcd' and Qty le 'Efgh')";

            //Act
            string request = EmptyFilter().
                Between("Qty", min, max);

            //Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod, ExpectedException(typeof(FilterBuilderException))]
        public void Beetween_NullMinStringArgument()
        {
            //Act
            string request = EmptyFilter().
                Between("Qty", null, "abc");

            //Assert
            Assert.Fail("Null min argument for Between method should cause an exception");
        }

        [TestMethod, ExpectedException(typeof(FilterBuilderException))]
        public void Beetween_NullMaxStringArgument()
        {
            //Act
            string request = EmptyFilter().
                Between("Qty", "abc", null);

            //Assert
            Assert.Fail("Null max argument for Between method should cause an exception");
        }

        [TestMethod]
        public void Beetween_DateTimeArguments()
        {
            //Arrange
            const string expected = "(Time ge datetime'2015-07-16T00:00:00Z' and Time le datetime'2016-07-16T00:00:00Z')";
            DateTime min = new DateTime(2015, 7, 16, 0, 0, 0, DateTimeKind.Utc);
            DateTime max = new DateTime(2016, 7, 16, 0, 0, 0, DateTimeKind.Utc); 

            //Act
            string request = EmptyFilter().
                Between("Time", min, max);

            //Assert
            Assert.AreEqual(expected, request);
        }
        #endregion

        #region Logical Operators Tests
        [TestMethod]
        public void Operator_And_Test()
        {
            //Arrange
            const string expected = "Name eq 'Andrew' and Age eq 27";

            //Act
            var actual = EmptyFilter().
                Column("Name").Equal().Const("Andrew").
                And().
                Column("Age").Equal().Const(27);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Operator_Or_Test()
        {
            //Arrange
            const string expected = "Name eq 'Andrew' or Name eq 'Alex'";

            //Act
            var actual = EmptyFilter().
                Column("Name").Equal().Const("Andrew").
                Or().
                Column("Name").Equal().Const("Alex");

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Operator_Not_Test()
        {
            //Arrange
            const string expected = " not (Role eq 'Admin')";

            //Act
            var actual = EmptyFilter()
                .Not().OpenBracket()
                    .Column("Role").Equal().Const("Admin") 
                .CloseBracket();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Comparison Operators With Values Tests
        #region Equal tests
        [TestMethod]
        public void Equal_String_CorrectValue()
        {
            // Arrange
            const string expected = "Name eq 'Andrew'";

            // Act
            string actual = EmptyFilter()
                .Column("Name").Equal("Andrew");

            // Assert
            Assert.AreEqual(expected,actual);
        }
       

        [TestMethod]
        public void Equal_Double_CorrectValue()
        {
            //Arrange
            const string expected = "Weight eq 4.65";

            //Act
            string actual = EmptyFilter()
                .Column("Weight").Equal(4.65);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Equal_Int_CorrectValue()
        {
            //Arrange
            const string expected = "Age eq 25";

            //Act
            string actual = EmptyFilter()
                .Column("Age").Equal(25);

            //Assert
            Assert.AreEqual(expected,actual);

        }

        [TestMethod]
        public void Equal_Long_CorrectValue()
        {
            //Arrange
            const string expected = "Id eq 100200300400500";

            //Act
            string actual = EmptyFilter()
                .Column("Id").Equal(100200300400500);

            //Assert
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void Equal_Guid_CorrectValue()
        {
            //Arrange
            const string expected = "Guid eq guid'a455c695-df98-5678-aaaa-81d3367e5a34'";

            //Act
            string actual = EmptyFilter()
                .Column("Guid").Equal(Guid.Parse("a455c695-df98-5678-aaaa-81d3367e5a34"));

            //Assert
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void Equal_Bool_correctValue()
        {
            //Arrange
            const string expected = "IsAlive eq true";

            //Act
            string actual = EmptyFilter()
                .Column("IsAlive").Equal(true);

            //Assert
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void Equal_DateTime_CorrectValue()
        {
            //Arrange
            const string expected = "Time eq datetime'2008-07-10T00:00:00Z'";

            //Act
            string actual = EmptyFilter()
                .Column("Time").Equal(new DateTime(2008, 7, 10, 0, 0, 0, DateTimeKind.Utc));

            //Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region GreatherThan Tests

        [TestMethod]
        public void GreatherThan_String_CorrectValue()
        {
            //Arrange
            const string expected = "Name gt 'Andrew'";

            //Act
            string actual = EmptyFilter()
                .Column("Name").GreaterThan("Andrew");

            //Assert
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void GreatherThen_Double_CorrectValue()
        {
            //Arrange
            const string expected = "Weight gt 5";

            //Act
            string actual = EmptyFilter()
                .Column("Weight").GreaterThan(5);

            //Assert
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void GreatherThen_Int_CorrectValue()
        {
            //Arrange
            const string expected = "Age gt 27";

            //Act
            string actual = EmptyFilter()
                .Column("Age").GreaterThan(27);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GreatherThen_Long_CorrectValue()
        {
            //Arrange
            const string expected = "Id gt 1234567890123";

            //Act
            string actual = EmptyFilter()
                .Column("Id").GreaterThan(1234567890123);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GreatherThan_DateTime_CorrectValue()
        {
            //Arrange
            const string expected = "Time gt datetime'2008-07-10T00:00:00Z'";

            //Act
            string actual = EmptyFilter()
                .Column("Time").GreaterThan(new DateTime(2008, 7, 10, 0, 0, 0, DateTimeKind.Utc));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GreaterThenOrEqual Tests

        [TestMethod]
        public void GreaterThenOrEqual_String_CorrectValue()
        {
            //Arrange
            const string expected = "Name ge 'Andrew'";

            //Act
            string actual = EmptyFilter()
                .Column("Name").GreaterThanOrEqual("Andrew");

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GreaterThenOrEqual_Double_CorrectValue()
        {
            //Arrange
            const string expected = "Weight ge 5";

            //Act
            string actual = EmptyFilter()
                .Column("Weight").GreaterThanOrEqual(5);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GreaterThenOrEqual_Int_CorrectValue()
        {
            //Arrange
            const string expected = "Age ge 27";

            //Act
            string actual = EmptyFilter()
                .Column("Age").GreaterThanOrEqual(27);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GreaterThenOrEqual_Long_CorrectValue()
        {
            //Arrange
            const string expected = "Id ge 1234567890123";

            //Act
            string actual = EmptyFilter()
                .Column("Id").GreaterThanOrEqual(1234567890123);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GreaterThenOrEqual_DateTime_CorrectValue()
        {
            //Arrange
            const string expected = "Time ge datetime'2008-07-10T00:00:00Z'";

            //Act
            string actual = EmptyFilter()
                .Column("Time").GreaterThanOrEqual(new DateTime(2008, 7, 10, 0, 0, 0, DateTimeKind.Utc));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region LessThan Tests

        [TestMethod]
        public void LessThan_String_CorrectValue()
        {
            //Arrange
            const string expected = "Name lt 'Andrew'";

            //Act
            string actual = EmptyFilter()
                .Column("Name").LessThan("Andrew");

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LessThan_Double_CorrectValue()
        {
            //Arrange
            const string expected = "Weight lt 5";

            //Act
            string actual = EmptyFilter()
                .Column("Weight").LessThan(5);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LessThan_Int_CorrectValue()
        {
            //Arrange
            const string expected = "Age lt 27";

            //Act
            string actual = EmptyFilter()
                .Column("Age").LessThan(27);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LessThan_Long_CorrectValue()
        {
            //Arrange
            const string expected = "Id lt 1234567890123";

            //Act
            string actual = EmptyFilter()
                .Column("Id").LessThan(1234567890123);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LessThan_DateTime_CorrectValue()
        {
            //Arrange
            const string expected = "Time lt datetime'2008-07-10T00:00:00Z'";

            //Act
            string actual = EmptyFilter()
                .Column("Time").LessThan(new DateTime(2008, 7, 10, 0, 0, 0, DateTimeKind.Utc));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region LessThanOrEqual Tests

        [TestMethod]
        public void LessThanOrEqual_String_CorrectValue()
        {
            //Arrange
            const string expected = "Name le 'Andrew'";

            //Act
            string actual = EmptyFilter()
                .Column("Name").LessThanOrEqual("Andrew");

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LessThanOrEqual_Double_CorrectValue()
        {
            //Arrange
            const string expected = "Weight le 5";

            //Act
            string actual = EmptyFilter()
                .Column("Weight").LessThanOrEqual(5);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LessThanOrEqual_Int_CorrectValue()
        {
            //Arrange
            const string expected = "Age le 27";

            //Act
            string actual = EmptyFilter()
                .Column("Age").LessThanOrEqual(27);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LessThanOrEqual_Long_CorrectValue()
        {
            //Arrange
            const string expected = "Id le 1234567890123";

            //Act
            string actual = EmptyFilter()
                .Column("Id").LessThanOrEqual(1234567890123);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LessThanOrEqual_DateTime_CorrectValue()
        {
            //Arrange
            const string expected = "Time le datetime'2008-07-10T00:00:00Z'";

            //Act
            string actual = EmptyFilter()
                .Column("Time").LessThanOrEqual(new DateTime(2008, 7, 10, 0, 0, 0, DateTimeKind.Utc));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region NotEqual Tests

        [TestMethod]
        public void NotEqual_String_CorrectValue()
        {
            //Arrange
            const string expected = "Name ne 'Andrew'";

            //Act
            string actual = EmptyFilter()
                .Column("Name").NotEqual("Andrew");

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NotEqual_Double_CorrectValue()
        {
            //Arrange
            const string expected = "Weight ne 5";

            //Act
            string actual = EmptyFilter()
                .Column("Weight").NotEqual(5);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NotEqual_Int_CorrectValue()
        {
            //Arrange
            const string expected = "Age ne 27";

            //Act
            string actual = EmptyFilter()
                .Column("Age").NotEqual(27);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NotEqual_Long_CorrectValue()
        {
            //Arrange
            const string expected = "Id ne 1234567890123";

            //Act
            string actual = EmptyFilter()
                .Column("Id").NotEqual(1234567890123);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NotEqual_DateTime_CorrectValue()
        {
            //Arrange
            const string expected = "Time ne datetime'2008-07-10T00:00:00Z'";

            //Act
            string actual = EmptyFilter()
                .Column("Time").NotEqual(new DateTime(2008, 7, 10, 0, 0, 0, DateTimeKind.Utc));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion
        #endregion

        #region Comparison Operators Tests
        [TestMethod]
        public void Operator_Equal_Test()
        {
            //Arrange
            const string expected = "Name eq 'Andrew'";

            //Act
            var actual = EmptyFilter().
                Column("Name").Equal().Const("Andrew");

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Operator_GreaterThan_Test()
        {
            //Arrange
            const string expected = "Age gt 20";

            //Act
            var actual = EmptyFilter().
                Column("Age").GreaterThan().Const(20);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Operator_GreaterThanOrEqual_Test()
        {
            //Arrange
            const string expected = "Age ge 20";

            //Act
            var actual = EmptyFilter().
                Column("Age").GreaterThanOrEqual().Const(20);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Operator_LessThan_Test()
        {
            //Arrange
            const string expected = "Age lt 20";

            //Act
            var actual = EmptyFilter().
                Column("Age").LessThan().Const(20);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Operator_LessThanOrEqual_Test()
        {
            //Arrange
            const string expected = "Age le 20";

            //Act
            var actual = EmptyFilter().
                Column("Age").LessThanOrEqual().Const(20);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Operator_NotEqual_Test()
        {
            //Arrange
            const string expected = "Sex ne 'Female'";

            //Act
            var actual = EmptyFilter().
                Column("Sex").NotEqual().Const("Female");

            //Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region StartWith Testts
        [TestMethod]
        public void StartsWith_CorrectArguments()
        {
            // Arrange
            const string expected = "(Name ge 'Alex' and Name lt 'Aley')";

            // Act
            string request = EmptyFilter()
                .StartsWith("Name", "Alex");

            // Assert
            Assert.AreEqual(expected, request);
        }

        [TestMethod, ExpectedException(typeof(FilterBuilderException))]
        public void StartsWith_NullValueArgument_ExceptionExpected()
        {
            // Act
            string request = EmptyFilter()
                .StartsWith("Name", null)
                ;

            // Assert
            Assert.Fail("Whitespace argument in StartsWith method should cause an exception");
        }
        #endregion
    }
}