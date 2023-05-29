namespace PaymentAPI.Domain
{
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardholderNumber { get; set; }
        public string HolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int CVV { get; set; }
        public string OrderReference { get; set; }
        public PaymentStatus Status { get; set; }

        private Payment() { } // Parameterless constructor for EF

        public Payment(decimal amount, string currency, string cardholderNumber, string holderName, int expirationMonth, int expirationYear, int cvv, string orderReference)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            Currency = currency;
            CardholderNumber = cardholderNumber;
            HolderName = holderName;
            ExpirationMonth = expirationMonth;
            ExpirationYear = expirationYear;
            CVV = cvv;
            OrderReference = orderReference;
            Status = PaymentStatus.Authorized;
        }
        public string AnonymizedCardNumber
        {
            get
            {
                if (CardholderNumber.Length > 5)
                {
                    return CardholderNumber.Substring(0, 6) + new string('*', CardholderNumber.Length - 10) + CardholderNumber.Substring(CardholderNumber.Length - 4);
                }
                else
                {
                    return CardholderNumber;
                }
            }
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(CardholderNumber) ||
                string.IsNullOrEmpty(HolderName) ||
                ExpirationMonth < 1 || ExpirationMonth > 12 ||
                ExpirationYear < DateTime.Now.Year ||
                (ExpirationYear == DateTime.Now.Year && ExpirationMonth < DateTime.Now.Month) ||
                CVV < 100 || CVV > 999 ||
                string.IsNullOrEmpty(Currency) ||
                Amount <= 0)
            {
                throw new Exception("Invalid payment details");
            }
        }
    }
}
