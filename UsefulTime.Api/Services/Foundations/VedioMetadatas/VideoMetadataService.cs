//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using UsefulTime.Api.Brokers.Storages;
using UsefulTime.Api.Models.VideoMetadatas;

namespace UsefulTime.Api.Services.Foundations.VedioMetadatas
{
    public class VideoMetadataService : IVideoMetadataService
    {
        private readonly IStorageBroker storageBroker;

        public VideoMetadataService(IStorageBroker storageBroker)=>
            this.storageBroker = storageBroker;
        
        public async ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata)
        {
            throw new NotImplementedException();
        }
    }
}
