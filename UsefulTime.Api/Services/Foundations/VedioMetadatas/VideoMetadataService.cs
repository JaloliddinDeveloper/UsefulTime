//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using UsefulTime.Api.Brokers.Loggings;
using UsefulTime.Api.Brokers.Storages;
using UsefulTime.Api.Models.VideoMetadatas;

namespace UsefulTime.Api.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataService : IVideoMetadataService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public VideoMetadataService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }
        public ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata) =>
           TryCatch(async () =>
           {
               ValidatevideoMetadataOnAdd(videoMetadata);

               return await this.storageBroker.InsertVideoMetadataAsync(videoMetadata);
           });
    }
}
