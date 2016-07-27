using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureStorageFilterBuilder
{
    public class AzureStorageFilter
    {
        #region SDK Constants
        public const string _Or = "or";
        public const string _And = "and";
        public const string _Not = "not";

        public const string _PK = "PartitionKey";
        public const string _RK = "RowKey";

        public const string _Equal = "eq";
        public const string _GreaterThan = "gt";
        public const string _GreaterThanOrEqual = "ge";
        public const string _LessThan = "lt";
        public const string _LessThanOrEqual = "le";
        public const string _NotEqual = "ne";
        #endregion


        #region Private fields
        private string _result = String.Empty;
        #endregion

        #region Constructors
        public static AzureStorageFilter Empty => new AzureStorageFilter {_result = String.Empty};
        #endregion

        #region Logical operators
        public AzureStorageFilter And()
        {
            _result += $" {_And} ";
            return this;
        }

        public AzureStorageFilter Or()
        {
            _result += $" {_Or} ";
            return this;
        }

        public AzureStorageFilter Not()
        {
            _result += $" {_Not} ";
            return this;
        }
        #endregion

        #region Equality operators
        public AzureStorageFilter Equal()
        {
            _result += $" {_Equal} ";
            return this;
        }

        public AzureStorageFilter GreaterThan()
        {
            _result += $" {_GreaterThan} ";
            return this;
        }

        public AzureStorageFilter GreaterThanOrEqual()
        {
            _result += $" {_GreaterThanOrEqual} ";
            return this;
        }

        public AzureStorageFilter LessThan()
        {
            _result += $" {_LessThan} ";
            return this;
        }

        public AzureStorageFilter LessThanOrEqual()
        {
            _result += $" {_LessThanOrEqual} ";
            return this;
        }

        public AzureStorageFilter NotEqual()
        {
            _result += $" {_NotEqual} ";
            return this;
        }
        #endregion

        #region Equality operators with arguments
        public AzureStorageFilter Equal(string value)
        {
            Equal();
            Const(value);
            return this;
        }
        public AzureStorageFilter Equal(bool value)
        {
            Equal();
            Const(value);
            return this;
        }
        public AzureStorageFilter Equal(long value)
        {
            Equal();
            Const(value);
            return this;
        }
        public AzureStorageFilter Equal(int value)
        {
            Equal();
            Const(value);
            return this;
        }
        public AzureStorageFilter Equal(double value)
        {
            Equal();
            Const(value);
            return this;
        }
        public AzureStorageFilter Equal(DateTime value)
        {
            Equal();
            Const(value);
            return this;
        }
        public AzureStorageFilter Equal(Guid value)
        {
            Equal();
            Const(value);
            return this;
        }


        public AzureStorageFilter GreaterThan(string value)
        {
            GreaterThan();
            Const(value);
            return this;
        }
        public AzureStorageFilter GreaterThan(long value)
        {
            GreaterThan();
            Const(value);
            return this;
        }
        public AzureStorageFilter GreaterThan(int value)
        {
            GreaterThan();
            Const(value);
            return this;
        }
        public AzureStorageFilter GreaterThan(double value)
        {
            GreaterThan();
            Const(value);
            return this;
        }
        public AzureStorageFilter GreaterThan(DateTime value)
        {
            GreaterThan();
            Const(value);
            return this;
        }
        

        public AzureStorageFilter GreaterThanOrEqual(string value)
        {
            GreaterThanOrEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter GreaterThanOrEqual(long value)
        {
            GreaterThanOrEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter GreaterThanOrEqual(int value)
        {
            GreaterThanOrEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter GreaterThanOrEqual(double value)
        {
            GreaterThanOrEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter GreaterThanOrEqual(DateTime value)
        {
            GreaterThanOrEqual();
            Const(value);
            return this;
        }
        

        public AzureStorageFilter LessThan(string value)
        {
            LessThan();
            Const(value);
            return this;
        }
        public AzureStorageFilter LessThan(long value)
        {
            LessThan();
            Const(value);
            return this;
        }
        public AzureStorageFilter LessThan(int value)
        {
            LessThan();
            Const(value);
            return this;
        }
        public AzureStorageFilter LessThan(double value)
        {
            LessThan();
            Const(value);
            return this;
        }
        public AzureStorageFilter LessThan(DateTime value)
        {
            LessThan();
            Const(value);
            return this;
        }

        
        public AzureStorageFilter LessThanOrEqual(string value)
        {
            LessThanOrEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter LessThanOrEqual(long value)
        {
            LessThanOrEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter LessThanOrEqual(int value)
        {
            LessThanOrEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter LessThanOrEqual(double value)
        {
            LessThanOrEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter LessThanOrEqual(DateTime value)
        {
            LessThanOrEqual();
            Const(value);
            return this;
        }

        
        public AzureStorageFilter NotEqual(string value)
        {
            NotEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter NotEqual(bool value)
        {
            NotEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter NotEqual(long value)
        {
            NotEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter NotEqual(int value)
        {
            NotEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter NotEqual(double value)
        {
            NotEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter NotEqual(DateTime value)
        {
            NotEqual();
            Const(value);
            return this;
        }
        public AzureStorageFilter NotEqual(Guid value)
        {
            NotEqual();
            Const(value);
            return this;
        }
        #endregion

        #region Common operators
        public AzureStorageFilter PartitionKey()
        {
            _result += _PK;
            return this;
        }
        public AzureStorageFilter RowKey()
        {
            _result += _RK;
            return this;
        }

        public AzureStorageFilter Const(string @const)
        {
            if (@const == null)
            {
                throw new FilterBuilderException("Can't add null string value to request");
            }
            _result += @const.AsTypedConstant();
            return this;
        }
        public AzureStorageFilter Const(int @const)
        {
            _result += @const.AsTypedConstant();
            return this;
        }
        public AzureStorageFilter Const(long @const)
        {
            _result += @const.AsTypedConstant();
            return this;
        }
        public AzureStorageFilter Const(bool @const)
        {
            _result += @const.AsTypedConstant();
            return this;
        }
        public AzureStorageFilter Const(DateTime @const)
        {
            _result += @const.AsTypedConstant();
            return this;
        }
        public AzureStorageFilter Const(Guid @const)
        {
            _result += @const.AsTypedConstant();
            return this;
        }
        public AzureStorageFilter Const(double @const)
        {
            if (double.IsNaN(@const))
            {
                throw new FilterBuilderException("Can't add Double.NaN to request: " +
                                                 $"Azure Storage does not work with this value" );
            }
            if (double.IsPositiveInfinity(@const))
            {
                throw new FilterBuilderException("Can't add Double.PositiveInfinity to request: " +
                                                 $"Azure Storage does not work with this value");
            }
            if (double.IsNegativeInfinity(@const))
            {
                throw new FilterBuilderException("Can't add Double.NegativeInfinity to request: " +
                                                 $"Azure Storage does not work with this value");
            }
            _result += @const.AsTypedConstant();
            return this;
        }
        
        public AzureStorageFilter OpenBracket()
        {
            _result += "(";
            return this;
        }
        public AzureStorageFilter CloseBracket()
        {
            _result += ")";
            return this;
        }
        public AzureStorageFilter InBrakets()
        {
            _result = $"({_result})";
            return this;
        }

        public AzureStorageFilter Column(string columnName)
        {
            // Check args
            if (String.IsNullOrWhiteSpace(columnName))
            {
                throw new FilterBuilderException("Can't create request: column name is empty.");
            }
            columnName = columnName.Trim(' ');
            if (columnName.Contains(" "))
            {
                throw new FilterBuilderException("Can't create request: column name contains whitespaces");
            }

            _result += columnName;
            return this;
        }
        #endregion

        #region In operators
        private AzureStorageFilter Const(object @const)
        {
            Type constType = @const.GetType();
            if (constType == typeof(string)) { return Const((string)@const); }
            else if (constType == typeof(int)) { return Const((int)@const); }
            else if (constType == typeof(long)) { return Const((long)@const); }
            else if (constType == typeof(bool)) { return Const((bool)@const); }
            else if (constType == typeof(DateTime)) { return Const((DateTime)@const); }
            else if (constType == typeof(Guid)) { return Const((Guid)@const); }
            else if (constType == typeof(double)) { return Const((double)@const); }

            else { throw new FilterBuilderException($"Can't convert const to request string: unknow type {constType}"); }
        }

        private AzureStorageFilter In(string column, IEnumerable<object> values)
        {
            // Check args
            if (values == null)
            {
                throw new FilterBuilderException("Can't create In request: possible values collection is null");
            }

            var valuesAsArray = values.ToArray();
            int valuesQty = valuesAsArray.Count();

            if (valuesQty == 0)
            {
                throw new FilterBuilderException("Can't create In request: values collection is empty");
            }

            int processedValuesQty = 0;

            OpenBracket();
            foreach (object value in valuesAsArray)
            {
                Column(column).Equal().Const(value);
                processedValuesQty++;
                if (processedValuesQty < valuesQty)
                {
                    Or(); // Set or statement after each comparison instead last
                }
            }
            CloseBracket();
            return this;
        }

        public AzureStorageFilter In(string column, IEnumerable<string> values)
        {
            if (values == null)
            {
                throw new FilterBuilderException("Can't form In request: null values argument");
            }
            var valuesAsObjects = values.Select(value => value as object);
            return In(column, valuesAsObjects);
        }
        public AzureStorageFilter In(string column, IEnumerable<int> values)
        {
            var valuesAsObjects = values.Select(value => value as object);
            return In(column, valuesAsObjects);
        }
        public AzureStorageFilter In(string column, IEnumerable<long> values)
        {
            var valuesAsObjects = values.Select(value => value as object);
            return In(column, valuesAsObjects);
        }
        public AzureStorageFilter In(string column, IEnumerable<DateTime> values)
        {
            var valuesAsObjects = values.Select(value => value as object);
            return In(column, valuesAsObjects);
        }
        public AzureStorageFilter In(string column, IEnumerable<Guid> values)
        {
            var valuesAsObjects = values.Select(value => value as object);
            return In(column, valuesAsObjects);
        }
        public AzureStorageFilter In(string column, IEnumerable<double> values)
        {
            var valuesAsObjects = values.Select(value => value as object);
            return In(column, valuesAsObjects);
        }

        public AzureStorageFilter In(string column, params string[] values)
        {
            return In(column, (IEnumerable<string>)values);
        }
        public AzureStorageFilter In(string column, params int[] values)
        {
            return In(column, (IEnumerable<int>)values);
        }
        public AzureStorageFilter In(string column, params long[] values)
        {
            return In(column, (IEnumerable<long>)values);
        }
        public AzureStorageFilter In(string column, params DateTime[] values)
        {
            return In(column, (IEnumerable<DateTime>)values);
        }
        public AzureStorageFilter In(string column, params Guid[] values)
        {
            return In(column, (IEnumerable<Guid>)values);
        }
        public AzureStorageFilter In(string column, params double[] values)
        {
            return In(column, (IEnumerable<double>)values);
        }
        #endregion

        #region Between operators
        private AzureStorageFilter BetweenBase(string columnName, object minInclusive, object maxInclusive)
        {
            OpenBracket()
                .Column(columnName).GreaterThanOrEqual().Const(minInclusive)
                .And()
                .Column(columnName).LessThanOrEqual().Const(maxInclusive)
            .CloseBracket();

            return this;
        }

        public AzureStorageFilter Between(string columnName, int minInclusive, int maxInclusive)
        {
            return BetweenBase(columnName, minInclusive, maxInclusive);
        }
        public AzureStorageFilter Between(string columnName, long minInclusive, long maxInclusive)
        {
            return BetweenBase(columnName, minInclusive, maxInclusive);
        }
        public AzureStorageFilter Between(string columnName, string minInclusive, string maxInclusive)
        {
            if (minInclusive == null)
            {
                throw new FilterBuilderException("Can't create Betwwen request: min argument is null");
            }
            if (maxInclusive == null)
            {
                throw new FilterBuilderException("Can't create Betwwen request: max argument is null");
            }
            return BetweenBase(columnName, minInclusive, maxInclusive);
        }
        public AzureStorageFilter Between(string columnName, double minInclusive, double maxInclusive)
        {
            return BetweenBase(columnName, minInclusive, maxInclusive);
        }
        public AzureStorageFilter Between(string columnName, DateTime minInclusive, DateTime maxInclusive)
        {
            return BetweenBase(columnName, minInclusive, maxInclusive);
        }
        #endregion

        #region StartsWith
        public AzureStorageFilter StartsWith(string columnName, string value)
        {
            // Arg check
            if (value == null)
            {
                throw new FilterBuilderException("Can't create StartsWith request: value is empty");
            }

            // Increase last symbol (for.ex. aaa => aab)
            var lastSymbol = value[value.Length - 1];
            lastSymbol++;
            var upperLimit = $"{value.Substring(0, value.Length - 1)}{lastSymbol}";

            // Create request like (columnName ge 'aaa' and columnName lt 'aab')
            OpenBracket()
                .Column(columnName).GreaterThanOrEqual().Const(value)
                .And()
                .Column(columnName).LessThan().Const(upperLimit)
            .CloseBracket();

            return this;
        }
        #endregion

        #region Conversions to String objects.
        public override string ToString()
        {
            return _result;
        }

        public string Result => _result;

        public static  implicit operator string(AzureStorageFilter filter)
        {
            return filter.Result;
        }
        #endregion
    }
}