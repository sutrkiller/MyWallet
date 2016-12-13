namespace MyWallet.Models.Statistics
{
    public class IncomeExpenseViewModel
    {
        public string ViewName { get; set; }

        public string Header { get; set; }

        public SpentModel Today { get; set; }

        public SpentModel LastWeek { get; set; }

        public SpentModel LastMonth { get; set; }

        public SpentModel LastYear { get; set; }
    }
}