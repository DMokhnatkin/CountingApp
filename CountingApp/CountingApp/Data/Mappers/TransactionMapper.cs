using System;
using System.Linq;
using CountingApp.Core.Dto;
using CountingApp.Models;
using CountingApp.Models.Transactions;
using Newtonsoft.Json.Linq;

namespace CountingApp.Data.Mappers
{
    static class TransactionMapper
    {
        public static TransactionDto Map(this Transaction transaction)
        {
            var transactionData = new JObject();
            // TODO : remove hardcode
            switch (transaction)
            {
                case Purchase purchase:
                    throw new NotImplementedException();
                    break;
            }
            return new TransactionDto
            {
                TransactionType = "purchase",
                Id = transaction.Id,
                Timestamp = transaction.Timestamp
            };
        }

        public static Transaction Unmap(this TransactionDto dto)
        {
            var transactionData = JObject.Parse(dto.TransactionData);
            // TODO : remove hardcode
            switch (dto.TransactionType)
            {
                case "purchase":
                    return new Purchase
                    {
                        Id = dto.Id,
                        Timestamp = dto.Timestamp,
                        Contributions = 
                            transactionData
                            .SelectTokens("$.Contributions")
                            .Values<JProperty>()
                            .Select(x => new Contribution
                                {
                                    AmountRub = x.Value<decimal>(),
                                    PersonId = new Guid(x.Name)
                                })
                            .ToArray(),
                        PersonList = 
                            transactionData
                            .SelectTokens("$.PersonList")
                            .Values<string>()
                            .ToArray()
                    };
            }
            return null;
        }
    }
}
