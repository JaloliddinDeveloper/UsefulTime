//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using UsefulTime.Api.Models.VideoMetadatas;

namespace UsefulTime.Api.Services.Foundations.VedioMetadatas
{
    public interface IVideoMetadataService
    {
        ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata);
    }
}
