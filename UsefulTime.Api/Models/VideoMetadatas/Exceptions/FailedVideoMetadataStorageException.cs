using Xeptions;

namespace UsefulTime.Api.Models.VideoMetadatas.Exceptions
{
    public class FailedVideoMetadataStorageException:Xeption
    {
        public FailedVideoMetadataStorageException(string message,Exception innerException)
            :base(message,innerException)
        { }
            
        
    }
}
