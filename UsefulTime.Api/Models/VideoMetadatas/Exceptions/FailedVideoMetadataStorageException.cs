//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
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
