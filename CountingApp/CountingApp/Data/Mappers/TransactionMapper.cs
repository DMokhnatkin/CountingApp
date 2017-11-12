using System;
using System.Linq;
using CountingApp.Core.Dto;
using CountingApp.Models;
using CountingApp.Models.Transactions;
using Newtonsoft.Json;
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
                    var contributions = new JArray();
                    if (purchase.Contributions != null && purchase.Contributions.Length != 0)
                    {
                        contributions = new JArray(purchase.Contributions.Select(x =>
                        {
                            dynamic obj = new JObject();
                            obj.PersonId = x.Person.Id;
                            obj.Value = x.AmountRub;
                            return obj;
                        }));
                    }
                    var personList = new JArray();
                    if (purchase.PersonList != null && purchase.PersonList.Length != 0)
                    {
                        personList = new JArray(purchase.PersonList.Select(x => new JValue(x)));
                    }

                    transactionData.Add("Contributions", contributions);
                    transactionData.Add("PersonList", personList);
                    break;
            }
            return new TransactionDto
            {
                TransactionType = "purchase",
                Id = transaction.Id,
                Timestamp = transaction.Timestamp,
                TotalAmount = transaction.TotalAmountRub,
                TransactionData = transactionData.ToString(Formatting.None)
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
                            Person = new Person(x["PersonId"].Value<string>(), "disp " + x),//todo
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
                        PersonList = personList,
                        People = personList.Select(x => new Person(x, "disp " + x)).ToArray() // TODO: remove
                    };
            }
            return null;
        }
    }
}
