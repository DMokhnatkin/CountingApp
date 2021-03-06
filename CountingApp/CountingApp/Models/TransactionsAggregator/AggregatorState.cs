﻿using System;
using System.Collections.Generic;

namespace CountingApp.Models.TransactionsAggregator
{
    public class AggregatorState : IDebtsAggregatorState
    {
        private readonly Dictionary<(string, string), decimal> _debts;

        public AggregatorState()
        {
            _debts = new Dictionary<(string, string), decimal>();
        }

        public void IncreaseDebt(string who, string whom, decimal amountRub)
        {
            if (_debts.ContainsKey((who, whom)))
            {
                _debts[(who, whom)] += amountRub;
                return;
            }
            if (_debts.ContainsKey((whom, who)))
            {
                _debts[(whom, who)] -= amountRub;
                return;
            }
            _debts.Add((who, whom), amountRub);
        }

        public void DecreaseDebt(string who, string whom, decimal amountRub)
        {
            IncreaseDebt(who, whom, -amountRub);
        }

        public Debt[] GetAllDebts()
        {
            var result = new List<Debt>();
            foreach (var debt in _debts)
            {
                result.Add(debt.Value > 0
                    ? new Debt(debt.Key.Item2, debt.Key.Item1, debt.Value)
                    : new Debt(debt.Key.Item1, debt.Key.Item2, -debt.Value));
            }
            return result.ToArray();
        }
    }
}
