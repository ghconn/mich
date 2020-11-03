namespace CTest
{
    internal class UploadCompletedNotice
    {
        public UploadCompletedNotice()
        {
        }

        public object bucketName { get; set; }
        public object fileName { get; set; }
        public object keyName { get; set; }
        public object md5 { get; set; }
        public object sha512 { get; set; }
        public string mimeType { get; set; }
        public long movieId { get; set; }
        public long userId { get; set; }
        public bool video { get; set; }
    }
}