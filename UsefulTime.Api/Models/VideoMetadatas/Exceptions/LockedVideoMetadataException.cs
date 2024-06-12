//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using Xeptions;

namespace UsefulTime.Api.Models.VideoMetadatas.Exceptions
{
    public class LockedVideoMetadataException:Xeption
    {
        public LockedVideoMetadataException(string message, Exception innerException)
           : base(message, innerException)
        { }
    }
}
