//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using EFxceptions.Models.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using UsefulTime.Api.Models.VideoMetadatas;
using UsefulTime.Api.Models.VideoMetadatas.Exceptions;
using Xunit.Abstractions;

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

            var failedVideoMetadataStorageException =
                new FailedVideoMetadataStorageException(
                    message: "Failed video metadata error occured, cotact support",
                    innerException: sqlException);

            var expectedVideoMetadataDependencyException =
                new VideoMetadataDependencyException(
                    message:"Video metadata error occured, fix the errors and try again",
                    innerException: failedVideoMetadataStorageException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertVideoMetadataAsync(someVideoMetadata))
                .ThrowsAsync(sqlException);

            //when
            ValueTask<VideoMetadata> addVideoMetadata=
                 this.videoMetadataService.AddVideoMetadataAsync(someVideoMetadata);

            VideoMetadataDependencyException actualVideoMetadataDependencyException =
              await Assert.ThrowsAsync<VideoMetadataDependencyException>(addVideoMetadata.AsTask);

            //then
            actualVideoMetadataDependencyException.Should()
                .BeEquivalentTo(expectedVideoMetadataDependencyException);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertVideoMetadataAsync(someVideoMetadata), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedVideoMetadataDependencyException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDublicateKeyErrorOccursAndLogItAsync()
        {
            //given
            VideoMetadata someVideoMetadata = CreateRandomVideoMetadata();
            string someMessage = GetRandomString();

            var duplicateKeyException =
                new DuplicateKeyException(message:someMessage);

            var alreadyExistVideoMetadataException =
                new AlreadyExistVideoMetadataException(
                    message: "VideoMetadata already exist",
                     innerException:duplicateKeyException);

            var expectedVideoMetadataDependencyValidationException =
                new VideoMetadataDependencyValidationException(
                    message: "Video metadata dependency error occurred,fix the errors try again",
                    innerException:alreadyExistVideoMetadataException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertVideoMetadataAsync(someVideoMetadata))
                .ThrowsAsync(duplicateKeyException);

            //when
            ValueTask<VideoMetadata> addVideoMetadata =
                this.videoMetadataService.AddVideoMetadataAsync(someVideoMetadata);

            var actualVideoMetadataDependencyValidationException =
             await Assert.ThrowsAsync<VideoMetadataDependencyValidationException>(addVideoMetadata.AsTask);

            //then
           actualVideoMetadataDependencyValidationException.Should()
                .BeEquivalentTo(expectedVideoMetadataDependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertVideoMetadataAsync(someVideoMetadata),
            Times.Once);

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedVideoMetadataDependencyValidationException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfDbCurrencyErrorOccursAndLogItAsync()
        {
            //given
            VideoMetadata someVideoMetadata = CreateRandomVideoMetadata();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedVideoMetadataException =
                new LockedVideoMetadataException("Video Metadata is locked, please try again.",
                    dbUpdateConcurrencyException);

            var expectedVideoMetadataDependencyValidationException =
                new VideoMetadataDependencyValidationException(
                    "Video Metadata dependency error occured, Fix errors and try again.",
                        lockedVideoMetadataException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(dbUpdateConcurrencyException);

            //when
            ValueTask<VideoMetadata> addVideoMetadataTask =
                this.videoMetadataService.AddVideoMetadataAsync(someVideoMetadata);

            var actualVideoMetadataDependencyValidationException =
                await Assert.ThrowsAsync<VideoMetadataDependencyValidationException>(addVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataDependencyValidationException.Should()
                .BeEquivalentTo(expectedVideoMetadataDependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataDependencyValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertVideoMetadataAsync(someVideoMetadata), Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
