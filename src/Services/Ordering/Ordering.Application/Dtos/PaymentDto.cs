﻿namespace Ordering.Application.Dtos;

public record PaymentDto(
    string CardNumber,
    string CardHolderName,
    DateTime Expiration,
    string Cvv,
    int PaymentMethod);