using GSW_Core.DTOs.Image;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageMetaDTO> AddAsync(ImageAddDTO image);
        Task<ImageContentDTO> GetAsync(string fileName);
        string GetFileName(int productId);
        void Validate(IFormFile image);
    }
}
