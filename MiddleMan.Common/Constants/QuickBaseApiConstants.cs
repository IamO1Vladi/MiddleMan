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


    //Request Models element names

    //Field
    public const string FieldNameElement = "name";
    public const string FieldIdElement = "fid";
    public const string FieldFileNameElement= "filename";

    //QueryForDataOptionsModel
    public const string QueryOptionsSkipElement = "skip";
    public const string QueryOptionsTopElement="top";
    public const string QueryOptionsCompareWithAppLocalTimeElement = "compareWithAppLocalTime";

    //QueryForDataSortBy
    public const string QuerySortByFieldIdElement = "fid";
    public const string QuerySortByOrderElement = "order";

    //QueryForRecordsRequestModel

    public const string QueryForRecordsFromElement = "from";
    public const string QueryForRecordsSelectElement = "select";
    public const string QueryForRecordsWhereElement = "where";
    public const string QueryForRecordsSortByElement = "sortBy";
    public const string QueryForRecordsOptionsElement = "options";

    //UploadAFIleRequestModel

    public const string UploadAFileXmlType = "qbapi";
    public const string UploadAFileUserTokenElement = "usertoken";
    public const string UploadAFileRecordIdElement = "rid";
    public const string UploadAFileFieldElement = "field";

    //CreateRecordResponse

    public const string CreateRecordResponseMetadataElement = "metadata";

    //RecordMetadata

    public const string RecordCreatedRecordIdsElement = "createdRecordIds";
    public const string RecordTotalNumberOfRecordsProcessed = "totalNumberOfRecordsProcessed";
    public const string RecordUnchangedRecordIds = "unchangedRecordIds";
    public const string RecordUpdatedRecordIds = "updatedRecordIds";

    //Field Ids in QuickBase database

    public const string ThumbnailUrlFieldId = "6";
    public const string TopicFieldId = "7";
    public const string SummaryFieldId = "14";
    public const string RecordIdFieldId = "3";
    public const string CategoryFieldId = "8";
    public const string FirstParagraphFieldId = "9";
    public const string SecondParagraphFieldId= "10";
    public const string SectionImageUrlFieldId = "11";
    public const string HeaderImageUrlFieldId = "16";
    public const string PostViewFieldId = "17";
    public const string KeyWordsMetaTag = "22";

    //Metadata

    public const string MetadataNumFieldsElement = "numFields";
    public const string MetadataNumRecordsElement = "numRecords";
    public const string MetadataSkipElement = "skip";
    public const string MetadataTotalRecordsElement = "totalRecords";

    //QueryForInformationPostImages

    public const string QueryForPostImagesUrlElement= "url";
    public const string QueryForPostImagesFileNameElement = "fileName";

    //GetInTouchServiceModel

    public const string GetInTouchNameElementErrorMessage = "Name is required.";
    public const string GetInTouchEmailElementErrorMessage = "Email is required.";
    public const string GetInTouchEmailElementFormatErrorMessage = "Please enter a valid email address.";
    public const string GetInTouchPhoneNumberElementErrorMessage = "Please enter a valid phone number.";
    public const string GetInTouchServiceElementErrorMessage = "Please select a service.";
    public const string GetInTouchMessageElementErrorMessage = "Message is required.";
}