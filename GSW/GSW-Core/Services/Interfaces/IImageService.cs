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
        Task<string> AddAsync(int productId, IFormFile image);
        Task<ImageDTO> GetAsync(string fileName);
        string GetFileName(int productId);
        void Validate(IFormFile image);
    }
}
