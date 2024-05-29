//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedVideoMetadataException = new LockedVideoMetadataException(
                    "Video Metadata is locked, please try again.",
                        dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedVideoMetadataException);
            }
            catch (Exception exception)
            {
                var failedVideoMetadataServiceException =
                    new FailedVideoMetadataServiceException(message: "Failed guest service error occurred,contact support",innerException:exception);

                throw CreateAndLogServiseException(failedVideoMetadataServiceException);
            }
        }

        private VideoMetadataDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var videoMetadataDependencyException = new VideoMetadataDependencyException(
                message: "Failed video metadata error occured, cotact support",
                   innerException: exception);
            this.loggingBroker.LogCritical(videoMetadataDependencyException);

            return videoMetadataDependencyException;
        }
        
        private VideoMetadataServiceException CreateAndLogServiseException(Xeption exception)
        {
            var videoMetadataServiceException =
                new VideoMetadataServiceException(message: "Video metadata service error occurred,contact support",innerException:exception);
            this.loggingBroker.LogError(videoMetadataServiceException);
            return videoMetadataServiceException;
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
        private VideoMetadataDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var videoMetadataDependencyValidationException = new VideoMetadataDependencyValidationException(
                "Video Metadata dependency error occured, Fix errors and try again.",
                    exception);

            this.loggingBroker.LogError(videoMetadataDependencyValidationException);

            return videoMetadataDependencyValidationException;
        }
    }
}
