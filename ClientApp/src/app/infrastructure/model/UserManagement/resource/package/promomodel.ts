export interface PromoModel {
    id: string;
    promoCode: string;
    packageName: string;
    packageId: string;
    hasDiscountPercent: boolean;
    discount: number;
    startDate: string;
    startDateNp: string;
    endDate: string;
    endDateNp: string;
}
