//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
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
    }
}
