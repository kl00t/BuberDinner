﻿namespace BuberDinner.Domain.Common.Models;

public class Price : ValueObject
{
    public decimal Amount { get; private set; }

    public decimal Currency { get; private set; }

    public Price(decimal amount, decimal currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}