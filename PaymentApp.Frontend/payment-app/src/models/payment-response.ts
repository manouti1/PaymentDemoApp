import { Payment } from "./payment";

export interface PaymentResponse {
    pageIndex: number;
    totalPages: number;
    totalCount: number;
    items: Payment[];
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}