namespace Dgm.Common.Authorization.Claim.Resource
{
    public class AccountingClaimConstant
    {
        public const string ViewAccounting = "Claim.Accounting.Read";

        // Accounting Type
        public const string ViewAccountingType = "Claim.Accounting.AccountingType.Read";
        public const string WriteAccountingType = "Claim.Accounting.AccountingType.Write";

        // Accounting Head
        public const string ViewAccountingHead = "Claim.Accounting.AccountingHead.Read";
        public const string WriteAccountingHead = "Claim.Accounting.AccountingHead.Write";
        
        //Accounting Transaction
        public const string ViewAccountingTransactionEntry = "Claim.Accounting.TransactionEntry.Read";
        public const string WriteAccountingTransactionEntry = "Claim.Accounting.TransactionEntry.Write";

    }
}
