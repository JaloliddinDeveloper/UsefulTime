//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using Microsoft.EntityFrameworkCore;
using UsefulTime.Api.Models.VideoMetadatas;

namespace UsefulTime.Api.Brokers.Storages
{
    public partial class StorageBroker
    { 
        public DbSet<VideoMetadata> VideoMetadatas { get; set; }
        public async ValueTask<VideoMetadata> InsertVideoMetadataAsync(VideoMetadata videoMetadata)=>
            await InsertAsync(videoMetadata);

        public IQueryable<VideoMetadata> SelectAllVideoMetadatas()=>
            SelectAll<VideoMetadata>();

        public async ValueTask<VideoMetadata> SelectVideoMetadataByIdAsync(Guid videoMetadataId) =>
       await SelectAsync<VideoMetadata>(videoMetadataId);

        public async ValueTask<VideoMetadata> UpdateVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await UpdateAsync(videoMetadata);

        public async ValueTask<VideoMetadata> DeleteVideoMetadataAsync(VideoMetadata videoMetadata) =>
             await DeleteAsync(videoMetadata);
    }
}
