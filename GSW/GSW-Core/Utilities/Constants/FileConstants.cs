using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Utilities.Constants
{
    public static class FileConstants
    {
        public const string StaticAssetsDirectory = "Assets";
        public static readonly string DefaultImage = Path.Combine(StaticAssetsDirectory, "default.png");

        public const string ImagesDirectory = "Images";
        public const string ImagesRequestPath = "/" + ImagesDirectory;
    }
}
