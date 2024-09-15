namespace TheatricalPlayersRefactoringKata.Application.Constants
{
    public static class StatementPrinterConstants
    {
        #region General
        public const int MINIMUM_LINES = 1000;
        public const int MAXIMUM_LINES = 4000;
        public const decimal DIVIDER_PER_LINE = 10;
        public const int CREDIT_MINIMUM_AUDIENCE = 30;
        #endregion
        #region Tragedy
        public const int TRAGEDY_MINIMUM_AUDIENCE = 30;
        public const decimal TRAGEDY_BONUS = 0m;
        public const decimal TRAGEDY_PER_AUDIENCE_ADDITIONAL = 10.00m;
        public const decimal TRAGEDY_PER_AUDIENCE = 0m;
        #endregion
        #region Comedy
        public const int COMEDY_MINIMUM_AUDIENCE = 20;
        public const decimal COMEDY_PER_AUDIENCE = 3.00m;
        public const decimal COMEDY_BONUS = 100.00m;
        public const decimal COMEDY_PER_AUDIENCE_ADDITIONAL = 5.00m;
        public const decimal COMEDY_BONUS_CREDIT_PER_ATTENDEES = 5m;
        #endregion




        //public const decimal BASE_AMOUNT_FOR_PLAY = 100.00m;
        //public const decimal BONUS_CREDITS_THRESHOLD = 30;
        
        
    }
}
