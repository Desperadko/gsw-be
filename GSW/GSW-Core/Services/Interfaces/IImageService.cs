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
        Task AddAsync(int productId, IFormFile image);
        Task<byte[]> GetAsync(int productId);
        void Validate(IFormFile image);
    }
}
