//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using Moq;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using UsefulTime.Api.Brokers.Loggings;
using UsefulTime.Api.Brokers.Storages;
using UsefulTime.Api.Models.VideoMetadatas;
using UsefulTime.Api.Services.Foundations.VedioMetadatas;
using Xeptions;

namespace UsefulTime.Unit.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTest
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IVideoMetadataService videoMetadataService;

        public VideoMetadataServiceTest()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.videoMetadataService = new VideoMetadataService
                (storageBroker: this.storageBrokerMock.Object,
                loggingBroker: loggingBrokerMock.Object);
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
         actualException => actualException.SameExceptionAs(expectedException);

        private static VideoMetadata CreateRandomVideoMetadata() =>
           CreateVideoMetadataFiller(date: GetRandomDateTimeOffset()).Create();
        public static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<VideoMetadata> CreateVideoMetadataFiller(DateTimeOffset date)
        {
            var filler = new Filler<VideoMetadata>();

            filler.Setup()
               .OnType<DateTimeOffset>().Use(date);
            return filler;
        }
    }
}
