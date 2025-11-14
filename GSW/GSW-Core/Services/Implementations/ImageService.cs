using GSW_Core.DTOs.Image;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Constants;
using GSW_Core.Utilities.Errors.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly string imageDirectoryFilePath;

        private readonly string[] validFormats = [".png", ".jpeg", ".jpg"];
        private readonly string[] validContentTypes = ["image/png", "image/jpeg", "image/jpg"];

        private const string imageHeader = "game-";

        public ImageService(IWebHostEnvironment environment)
        {
            imageDirectoryFilePath = Path.Combine(environment.ContentRootPath, FileConstants.ImagesDirectory);
            Directory.CreateDirectory(imageDirectoryFilePath);
        }

        public async Task<string> AddAsync(int productId, IFormFile image)
        {
            Validate(image);

            var newFileName = imageHeader + productId + Path.GetExtension(image.FileName);
            var fullPath = Path.Combine(imageDirectoryFilePath, newFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return newFileName;
        }

        public async Task<ImageDTO> GetAsync(string fileName)
        {
            var fullPath = Path.Combine(imageDirectoryFilePath, fileName);
            if (File.Exists(fullPath))
            {
                return new ImageDTO(await File.ReadAllBytesAsync(fullPath), GetContentType(fileName));
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
                throw new BadRequestException("No image provided");
            }

            if (!ValidateImageFormat(image.FileName, out string extension))
            {
                throw new BadRequestException($"Invalid image format: '{extension}'");
            }

            if(!ValidateImageContentType(image.ContentType))
            {
                throw new BadRequestException("Invalid content type");
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
    }
}
