namespace MiddleMan.Common.Constants;

public static class QuickBaseApiConstants
{

    public const int MaxApiRetries = 10;
    public const int ApiRetryDelayMilliseconds = 2000;

    public const string CustomersTableId = "buj25wk4r";
    public const string QbRealmHostName = "vladimirbuilder.quickbase.com";
    public const string InformationTableId = "bukcr8zp3";
    public const string InformationImagesTableId = "bukcsfwf9";
    public const string SubscribedUsersTableId = "bukpabubn";

    public const string InsertOrUpdateRecordsEndpoint = "https://api.quickbase.com/v1/records";

    public const string QueryForRecordsEndpoint = "https://api.quickbase.com/v1/records/query";

    public const string QueryForRecordsErrorMessage = "Failed to retrieve records: {0}";
    public const string ErrorSubscribingMessage = "Error subscribing: {0}";
}