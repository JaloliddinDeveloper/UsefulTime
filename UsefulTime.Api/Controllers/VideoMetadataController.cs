//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using UsefulTime.Api.Models.VideoMetadatas.Exceptions;
using UsefulTime.Api.Models.VideoMetadatas;
using UsefulTime.Api.Services.Foundations.VideoMetadatas;

namespace UsefulTime.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoMetadataController:RESTFulController
    {
        private readonly IVideoMetadataService videoMetadataService;
        public VideoMetadataController(IVideoMetadataService videoMetadataService)
        {
            this.videoMetadataService = videoMetadataService;
        }
        [HttpPost]
        public async ValueTask<ActionResult<VideoMetadata>> PostVideoMetadataAsync(VideoMetadata videoMetadata)
        {
            try
            {
                VideoMetadata postedVideoMetadata =
                await this.videoMetadataService.AddVideoMetadataAsync(videoMetadata);

                return Created(postedVideoMetadata);
            }
            catch (VideoMetadataValidationException videoMetadataValidationException)
            {
                return BadRequest(videoMetadataValidationException.InnerException);
            }
            catch (VideoMetadataDependencyValidationException videoMetadataDependencyValidationException)
                when (videoMetadataDependencyValidationException.InnerException is
                    AlreadyExistVideoMetadataException)
            {
                return Conflict(videoMetadataDependencyValidationException.InnerException);
            }
            catch (VideoMetadataDependencyException videoMetadataDependencyException)
            {
                return InternalServerError(videoMetadataDependencyException);
            }
            catch (VideoMetadataServiceException videoMetadataServiceException)
            {
                return InternalServerError(videoMetadataServiceException);
            }
        }
    }
}
