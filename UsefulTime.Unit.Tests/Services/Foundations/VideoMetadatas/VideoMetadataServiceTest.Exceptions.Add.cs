//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using Microsoft.Data.SqlClient;
using Moq;
using UsefulTime.Api.Models.VideoMetadatas;

namespace UsefulTime.Unit.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTest
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            VideoMetadata someVideoMetadata=CreateRandomVideoMetadata();
            SqlException sqlException = GetSqlException();

            this.storageBrokerMock.Setup(broker =>
            broker.InsertVideoMetadataAsync(someVideoMetadata))
                .ThrowsAsync(sqlException);
            //when

            //then
        }
    }
}
