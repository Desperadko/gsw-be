using GSW_Core.DTOs.Image;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Constants;
using GSW_Core.Utilities.Errors.Exceptions;
using GSW_Data.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GSW_Core.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly string imageDirectoryFilePath;
        private readonly string defaultImageFilePath;

        private readonly string[] validFormats = [".png", ".jpeg", ".jpg"];
        private readonly string[] validContentTypes = ["image/png", "image/jpeg", "image/jpg"];

        private const string imageHeader = "game-";

        public ImageService(IWebHostEnvironment environment)
        {
            imageDirectoryFilePath = Path.Combine(environment.ContentRootPath, FileConstants.ImagesDirectory);
            Directory.CreateDirectory(imageDirectoryFilePath);

            defaultImageFilePath = Path.Combine(environment.ContentRootPath, FileConstants.DefaultImage);
        }

        public async Task<ImageMetaDTO> AddAsync(ImageAddDTO image)
        {
            Validate(image.ImageData);

            byte[] imageBytes = [];

            using (var imageStream = image.ImageData.OpenReadStream())
            using (var stream = new MemoryStream())
            {
                await imageStream.CopyToAsync(stream);
                imageBytes = stream.ToArray();
            }

            return await ProcessImageData(image.ProductId, imageBytes, Path.GetExtension(image.ImageData.FileName));
        }

        public async Task<ImageMetaDTO> AddDefaultAsync(int productId)
        {
            var imageBytes = File.ReadAllBytes(defaultImageFilePath);

            return await ProcessImageData(productId, imageBytes, Path.GetExtension(defaultImageFilePath));
        }

        public async Task<ImageContentDTO> GetAsync(string fileName)
        {
            var fullPath = Path.Combine(imageDirectoryFilePath, fileName);
            if (File.Exists(fullPath))
            {
                return new ImageContentDTO(await File.ReadAllBytesAsync(fullPath), GetContentType(fileName));
            }

            throw new NotFoundException($"Image doesn't exist: '{fileName}'");
        }

        public string GetFileName(int productId)
        {
            foreach (var format in validFormats)
            {
                var fileName = imageHeader + productId + format;
                var fullPath = Path.Combine(imageDirectoryFilePath, fileName);
                if (File.Exists(fullPath))
                {
                    return fileName;
                }
            }

            throw new NotFoundException($"No image exists for product with id: '{productId}'");
        }

        public void Validate(IFormFile image)
        {
            if(image == null || image.Length == 0)
            {
                throw new BadRequestException(ErrorFieldConstants.IMAGE, "No image provided");
            }

            if (!ValidateImageFormat(image.FileName, out string extension))
            {
                throw new BadRequestException(ErrorFieldConstants.IMAGE, $"Invalid image format: '{extension}'");
            }

            if(!ValidateImageContentType(image.ContentType))
            {
                throw new BadRequestException(ErrorFieldConstants.IMAGE, "Invalid content type");
            }
        }

        private bool ValidateImageFormat(string filename, out string extension)
        {
            extension = Path.GetExtension(filename);

            if (string.IsNullOrEmpty(extension))
            {
                return false;
            }

            return this.validFormats.Contains(extension);
        }

        private bool ValidateImageContentType(string contentType)
        {
            if (string.IsNullOrEmpty(contentType))
            {
                return false;
            }

            return validContentTypes.Contains(contentType);
        }

        private string GetContentType(string fileName)
        {
            return Path.GetExtension(fileName).ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };
        }

        private async Task<ImageMetaDTO> ProcessImageData(
            int productId, byte[] imageBytes, string imageExtension)
        {
            var newFileName = imageHeader + productId + imageExtension;
            var fullPath = Path.Combine(imageDirectoryFilePath, newFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await stream.WriteAsync(imageBytes);
            }

            return new ImageMetaDTO(newFileName);
        }
    }
}
