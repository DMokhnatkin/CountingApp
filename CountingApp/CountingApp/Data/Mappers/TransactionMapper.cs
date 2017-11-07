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
                    var contributors = transactionData
                        .SelectTokens("$.Contributions")
                        .Values<JObject>()
                        .Select(x => new Contribution
                        {
                            PersonId = x["PersonId"].Value<string>(),
                            AmountRub = x["Value"].Value<decimal>(),
                        })
                        .ToArray();
                    var personList = transactionData
                        .SelectTokens("$.PersonList")
                        .Values<string>()
                        .ToArray();

                    return new Purchase
                    {
                        Id = dto.Id,
                        Timestamp = dto.Timestamp,
                        Contributions = contributors,
                        PersonList = personList
                    };
            }
            return null;
        }
    }
}
