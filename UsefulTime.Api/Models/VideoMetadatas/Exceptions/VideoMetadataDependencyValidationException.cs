using Xeptions;

namespace UsefulTime.Api.Models.VideoMetadatas.Exceptions
{
    public class VideoMetadataDependencyValidationException:Xeption
    {
        public VideoMetadataDependencyValidationException(string message, Xeption innerException)
            :base(message, innerException)
        { }   
    }
}
