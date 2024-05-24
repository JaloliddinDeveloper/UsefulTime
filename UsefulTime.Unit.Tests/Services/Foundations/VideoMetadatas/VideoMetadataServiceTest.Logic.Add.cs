//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using UsefulTime.Api.Models.VideoMetadatas;

namespace UsefulTime.Unit.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTest
    {
        [Fact]
        public async Task ShouldAddVideoMetadataAsync()
        {
            //give
            VideoMetadata randomVideoMetadata = CreateRandomVideoMetadata();
            VideoMetadata inputVideoMetadata = randomVideoMetadata;
            VideoMetadata returningVideoMetadata = inputVideoMetadata;
            VideoMetadata expectedVideoMetadata = returningVideoMetadata.DeepClone();

            this.storageBrokerMock.Setup(broker =>
            broker.InsertVideoMetadataAsync(inputVideoMetadata)).ReturnsAsync(expectedVideoMetadata);
            //when
            VideoMetadata actualVideoMetadata =
                await this.videoMetadataService.AddVideoMetadataAsync(inputVideoMetadata);
            //then
            actualVideoMetadata.Should().BeEquivalentTo(expectedVideoMetadata);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertVideoMetadataAsync(inputVideoMetadata), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
