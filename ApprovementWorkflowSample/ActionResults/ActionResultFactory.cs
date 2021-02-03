namespace ApprovementWorkflowSample.ActionResults
{
    public static class ActionResultFactory
    {
        public static UploadResult GetUploadSuccess()
        {
            return new UploadResult(true, "");
        }
        public static UploadResult GetUploadFailed(string errorMessage)
        {
            return new UploadResult(false, errorMessage);
        }
    }
}