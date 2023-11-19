using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using BookManager.API.Data;
using BookManager.API.Models.Domain;


namespace BookManager.API.Repositories
{
    public class S3ImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly BookManagerDbContext dbContext;

        public S3ImageRepository(IConfiguration configuration, IWebHostEnvironment webHostEnvironment, BookManagerDbContext dbContext)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
            this.dbContext = dbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var s3Client = new AmazonS3Client(configuration["AWS:AccessKeyId"], configuration["AWS:SecretKey"], RegionEndpoint.USEast1);

            using (var memoryStream = new MemoryStream())
            {
                await image.File.CopyToAsync(memoryStream);

                var fileTransferUtility = new TransferUtility(s3Client);

                await fileTransferUtility.UploadAsync(memoryStream, configuration["AWS:BucketName"], $"{image.FileName}{image.FileExtension}");
            }

            image.FilePath = $"https://{configuration["AWS:BucketName"]}.s3.amazonaws.com/{image.FileName}{image.FileExtension}";

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
