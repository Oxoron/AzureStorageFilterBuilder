using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorageFilterBuilder_UnitTests
{
    internal class LocalAzureStorage
    {
        public const string ConnectionString = "UseDevelopmentStorage=true";

        private readonly CloudTable _table;

        public LocalAzureStorage()
        {
            const string tableName = "aTestTable";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            _table = tableClient.GetTableReference(tableName);
            _table.CreateIfNotExists();
        }

        public void Insert(DynamicTableEntity entity)
        {
            var insertOperation = TableOperation.Insert(entity);
            _table.Execute(insertOperation);
        }


        public List<DynamicTableEntity> FindBy(string filter)
        {
            TableContinuationToken token = null;
            var query = new TableQuery().Where(filter);
            var entities = new List<DynamicTableEntity>();
            do
            {
                var queryResult = _table.ExecuteQuerySegmented(query, token);
                entities.AddRange(queryResult.Results.ToList());
                token = queryResult.ContinuationToken;
            } while (token != null);

            return entities;
        }
    }
}