//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using UsefulTime.Api.Models.VideoMetadatas;
using UsefulTime.Api.Models.VideoMetadatas.Exceptions;
using Xeptions;

namespace UsefulTime.Api.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataService
    {
        private delegate ValueTask<VideoMetadata> ReturningVideoMetadataFunction();
        private async ValueTask<VideoMetadata> TryCatch(ReturningVideoMetadataFunction returningVideoMetadataFunction)
        {
            try
            {
                return await returningVideoMetadataFunction();
            }
            catch (NullVideoMetadataException nullVideoMetadataException)
            {
                throw CreateAndLogValidationException(nullVideoMetadataException);
            }
            catch (InvalidVideoMetadataException invalidVideoMetadataException)
            {
                throw CreateAndLogValidationException(invalidVideoMetadataException);
            }
            catch (SqlException sqlException)
            {
                var failedVideoMetadataStorageException =
                new FailedVideoMetadataStorageException(
                    message: "Failed video metadata error occured, cotact support",
                    innerException: sqlException);
                throw CreateAndLogCriticalDependencyException(failedVideoMetadataStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistVideoMetadataException =
                 new AlreadyExistVideoMetadataException(
                     message: "VideoMetadata already exist",
                      innerException: duplicateKeyException);
                throw CreateAndDependencyValidationException(alreadyExistVideoMetadataException);
            }
        }
        private VideoMetadataDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var videoMetadataDependencyException =
              new VideoMetadataDependencyException(
                  message: "Video metadata error occured, fix the errors and try again",
                  innerException: exception);
            this.loggingBroker.LogCritical(videoMetadataDependencyException);
            throw videoMetadataDependencyException;
        }
        private VideoMetadataValidationException CreateAndLogValidationException(Xeption exception)
        {
            var videoMetadataValidationException =
                new VideoMetadataValidationException(
                    message: "Video metadata Validation error occurred,fix the errors and try again",
                    innerException: exception);

            this.loggingBroker.LogError(videoMetadataValidationException);
            return videoMetadataValidationException;
        }
        public VideoMetadataDependencyValidationException CreateAndDependencyValidationException(Xeption exception)
        {
            var videoMetadataDependencyValidationException =
                 new VideoMetadataDependencyValidationException(
                     message: "Video metadata dependency error occurred,fix the errors try again",
                     innerException:exception );
            this.loggingBroker.LogError(videoMetadataDependencyValidationException);
            return videoMetadataDependencyValidationException;
        }
    }
}
