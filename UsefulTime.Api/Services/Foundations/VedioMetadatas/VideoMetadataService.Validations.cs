﻿//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using UsefulTime.Api.Models.VideoMetadatas;
using UsefulTime.Api.Models.VideoMetadatas.Exceptions;

namespace UsefulTime.Api.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataService
    {
        private void ValidatevideoMetadataOnAdd(VideoMetadata videoMetadata)
        {
            ValidationVideoMetadataNotNull(videoMetadata);

            Validate(
              (Rule: IsInvalid(videoMetadata.Id), Parameter: nameof(videoMetadata.Id)),
              (Rule: IsInvalid(videoMetadata.Title), Parameter: nameof(videoMetadata.Title)),
              (Rule: IsInvalid(videoMetadata.BlobPath), Parameter: nameof(videoMetadata.BlobPath)),
              (Rule: IsInvalid(videoMetadata.CreatedDate), Parameter: nameof(videoMetadata.CreatedDate)),
              (Rule: IsInvalid(videoMetadata.UpdatedDate), Parameter: nameof(videoMetadata.UpdatedDate)),
               (Rule: IsNotRecent(videoMetadata.CreatedDate), Parameter: nameof(VideoMetadata.CreatedDate)),
                (Rule: IsNotSame(
                    firstDate: videoMetadata.CreatedDate,
                    secondDate: videoMetadata.UpdatedDate,
                    secondDateName: nameof(VideoMetadata.UpdatedDate)),

                Parameter: nameof(VideoMetadata.CreatedDate)));
        }
        private void ValidationVideoMetadataNotNull(VideoMetadata videoMetadata)
        {
            if (videoMetadata is null)
            {
                throw new NullVideoMetadataException(message: "Video metadata is null");
            }
        }
        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };
        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };
        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Data is required"
        };
        private static dynamic IsNotSame(
           DateTimeOffset firstDate,
           DateTimeOffset secondDate,
           string secondDateName) => new
           {
               Condition = firstDate != secondDate,
               Message = $"Date is not same as {secondDateName}"
           };

        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime = this.dateTimeBroker.GetCurrentDateTimeOffset();
            TimeSpan timeDifference = currentDateTime.Subtract(date);

            return timeDifference.TotalSeconds is > 60 or < 0;
        }
        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidVideoMetadataException = 
                new InvalidVideoMetadataException(message: "Video metadata is invalid");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidVideoMetadataException.UpsertDataList(parameter, rule.Message);
                }
            }
            invalidVideoMetadataException.ThrowIfContainsErrors();
        }
    }
}
