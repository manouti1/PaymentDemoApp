﻿using MediatR;
using PaymentAPI.Domain;

namespace PaymentAPI.Application.Payments.VoidPaymentCommand
{
    public class VoidPaymentCommand : IRequest<Payment>
    {
        public Guid Id { get; set; }
        public string OrderReference { get; set; }
    }
}
