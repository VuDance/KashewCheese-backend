using Amazon.S3;
using Amazon.S3.Transfer;
using KashewCheese.Application.Common.Interfaces.File;
using KashewCheese.Application.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;

namespace KashewCheese.Infrastructure.File
{
    public class S3FileStorageService : IFileStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3FileStorageService(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:BucketName"];
        }
        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType="image/jpeg",string sizeLabel=null)
        {
            var fileTransferUtility = new TransferUtility(_s3Client);
            string key;
            if (sizeLabel.IsNullOrEmpty())
            {
                key = fileName;
            }
            else
            {
                key = $"{sizeLabel}/{fileName}";
            }
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileStream,
                Key = key,
                BucketName = _bucketName,
                ContentType = contentType
            };

            await fileTransferUtility.UploadAsync(uploadRequest);
            return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
        }


        public async Task<string> UploadFilesAsync(ICollection<FileUploadDto> fileUploadDtos)
        {
            var uploadTasks = new List<Task>();
            foreach (var file in fileUploadDtos)
            {
                var resize70x70Stream = new MemoryStream();
                file.FileStream.Position = 0;
                await file.FileStream.CopyToAsync(resize70x70Stream);
                resize70x70Stream.Position = 0;

                var resize400x400Stream = new MemoryStream();
                file.FileStream.Position = 0;
                await file.FileStream.CopyToAsync(resize400x400Stream);
                resize400x400Stream.Position = 0;

                file.FileStream.Position = 0;

                var resize70x70 = ResizeImage(resize70x70Stream, 70, 70);
                var resize400x400 = ResizeImage(resize400x400Stream, 400, 400);

                uploadTasks.Add(UploadFileAsync(file.FileStream, file.FileName, file.ContentType, "original"));
                uploadTasks.Add(UploadFileAsync(resize70x70, file.FileName, file.ContentType, "70x70"));
                uploadTasks.Add(UploadFileAsync(resize400x400, file.FileName, file.ContentType, "400x400"));
            }
            await Task.WhenAll(uploadTasks);
            return "Upload successfully";
        }

        private static Stream ResizeImage(Stream imageStream, int width, int height)
        {
            using var image = Image.Load(imageStream);
            IImageFormat format = image.Metadata.DecodedImageFormat;
            image.Mutate(x => x.Resize(width, height));
            var output = new MemoryStream();
            image.Save(output, format);
            output.Position = 0;
            return output;
        }


    }
}
