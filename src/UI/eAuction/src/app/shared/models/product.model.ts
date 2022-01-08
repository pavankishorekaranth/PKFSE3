import { IBid } from "./bid.model";

export interface IProduct {
    Id: string;
    ProductName: string;
    ShortDescription: string;
    DetailedDescription: string;
    Category: string;
    StartingPrice: number;
    BidEndDate: string;
    FirstName: string;
    LastName: string;
    Address: string;
    City: string;
    State: string;
    Pin: number;
    Phone: string;
    Email: string;
    Bids: IBid[];
}
