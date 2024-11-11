namespace MiddleMan.Common.Constants;

public static class ValidationConstants
{
        //Information section
        public const int MaxTopicLength = 46;
        public const int MinTopicLength = 28;
        public const int MaxSummaryLength = 112;
        public const int MinSummaryLength = 56;

        //GetInTouchServiceModel

        public const int MaxGetInTouchNameLength = 50;
        public const int MinGetInTOuchNameLength = 2;
        public const int MaxGetInTouchIndustryLength = 50;
        public const int MinGetInTOuchIndustryLength = 2;
        public const int MaxGetInTouchMessageLength = 5000;
        public const int MinGetInTouchMessageLength = 10;
        public const int MaxFileAttachmentSize = 99 * 1024 * 1024;
}