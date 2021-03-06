﻿
namespace Domain.Interfaces
{
    public interface ICard
    {
        string CardNumber { get; }
        string Holder { get; }
        string ExpirationDate { get; }
        string SecurityCode { get; }
        string Brand { get; }
    }
}
