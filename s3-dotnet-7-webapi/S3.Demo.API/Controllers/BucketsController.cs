using Amazon.S3;
using Microsoft.AspNetCore.Mvc;

namespace S3.Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketsController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;

        public BucketsController(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBucketAsync(string bucketName)
        {
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (bucketExists) return BadRequest($"Bucket {bucketName} already exists.");
            await _s3Client.PutBucketAsync(bucketName);
            return Created("buckets", $"Bucket {bucketName} created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBucketAsync()
        {
            var data = await _s3Client.ListBucketsAsync();
            var buckets = data.Buckets.Select(b => { return b.BucketName; });
            return Ok(buckets);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBucketAsync(string bucketName)
        {
            await _s3Client.DeleteBucketAsync(bucketName);
            return NoContent();
        }
    }
}
