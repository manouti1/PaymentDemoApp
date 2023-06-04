export interface Payment {
    id: string;
    amount: number;
    currency: string;
    cardholderNumber: string;
    holderName: string;
    expirationMonth: number;
    expirationYear: number;
    cvv: number;
    orderReference: string;
    status: string;
    anonymizedCardNumber: string;
}