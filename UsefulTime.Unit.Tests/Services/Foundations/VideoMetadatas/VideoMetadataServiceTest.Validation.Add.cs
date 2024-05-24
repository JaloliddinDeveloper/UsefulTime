//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using Moq;
using UsefulTime.Api.Models.VideoMetadatas.Exceptions;
using UsefulTime.Api.Models.VideoMetadatas;

namespace UsefulTime.Unit.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTest
    {
        [Fact]
        public async Task ShouldThrowValidationExeptionOnAddIfVideoMetadataIsNullAndLogItAsync()
        {
            //given
            VideoMetadata nullVideoMetadata = null;

            var nullVideoMetadataException =
                new NullVideoMetadataException(message: "Video metadata is null");

            var expectedVideoMetadataValidationException =
                new VideoMetadataValidationException(
                    message: "Video metadata Validation error occurred,fix the errors and try again",
                    innerException: nullVideoMetadataException);

            //when
            ValueTask<VideoMetadata> addVideoMetadataTask =
                this.videoMetadataService.AddVideoMetadataAsync(nullVideoMetadata);

            //then
            await Assert.ThrowsAsync<VideoMetadataValidationException>(() =>
            addVideoMetadataTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataValidationException))),
            Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertVideoMetadataAsync(It.IsAny<VideoMetadata>()), Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
