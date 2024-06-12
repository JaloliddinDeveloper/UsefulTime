//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using Microsoft.Data.SqlClient;
using Moq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using UsefulTime.Api.Brokers.DateTimes;
using UsefulTime.Api.Brokers.Loggings;
using UsefulTime.Api.Brokers.Storages;
using UsefulTime.Api.Models.VideoMetadatas;
using UsefulTime.Api.Services.Foundations.VideoMetadatas;
using Xeptions;

namespace UsefulTime.Unit.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTest
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly IVideoMetadataService videoMetadataService;

        public VideoMetadataServiceTest()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();

            this.videoMetadataService = new VideoMetadataService
                (storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object,
                dateTimeBroker:this.dateTimeBrokerMock.Object);
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
         actualException => actualException.SameExceptionAs(expectedException);

        private static DateTimeOffset GetRandomDateTime() =>
           new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static VideoMetadata CreateRandomVideoMetadata() =>
         CreateVideoMetadataFiller(GetRandomDateTime()).Create();

        public static VideoMetadata CreateRandomVideoMetadata(DateTimeOffset date) =>
          CreateVideoMetadataFiller(date).Create();
        
        private static Filler<VideoMetadata> CreateVideoMetadataFiller(DateTimeOffset dates)
        {
            var filler = new Filler<VideoMetadata>();

            filler.Setup()
               .OnType<DateTimeOffset>().Use(dates);
            return filler;
        }
        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));
        private static string GetRandomString()
        {
            return new MnemonicString().GetValue();
        }
    }
}
