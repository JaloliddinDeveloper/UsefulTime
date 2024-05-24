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
    }
}
