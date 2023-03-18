namespace PrizeBondChecker.Domain.Constants
{
    public class ApplicationMessages
    {
        public const string HttpStatusCodeDescriptionOk = "OK";
        public const string HttpStatusCodeDescriptionCreated = "Created";
        public const string HttpStatusCodeDescriptionAccepted = "Accepted";
        public const string HttpStatusCodeDescriptionInternalError = "Internal Server Error";

        public const string UserNotFound = "User not found.";
        public const string InvalidUserNameOrPassword = "Username or password is invalid.";

        public const string DrawNumberExist = "Same draw number already exist";
        public const string DrawMonthYearExist = "Same month year already exist";
        public const string DrawNumberNotFound = "Draw number not found";

        public const string DataRetriveSuccessfull = "Data has been retrived successfully.";
        public const string DataCreatedSuccessfull = "Data has been created successfully.";
        public const string DataUpdatedSuccessfull = "Data has been updated successfully.";
        public const string DataDeletedSuccessfull = "Data has been deleted successfully.";
        public const string DataAddedSuccessfull = "Data has been added successfully.";
    }
}
