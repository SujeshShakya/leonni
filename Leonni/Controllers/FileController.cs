using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using Leonni.Core;
using Leonni.Interfaces;
using Leonni.Core.Controllers;

namespace Leonni.Controllers
{
    public class FileController : LeonniApplicationController
    {
        IFileRepository _repoFile;

        public FileController(IFileRepository repoFile)
        {
            _repoFile = repoFile;
        }

        public FileContentResult Picture(long id, int width, int height)
        {
            var file = _repoFile.GetSingle(x => x.Id == id);
            if (file != null)
            {
                MemoryStream output = new MemoryStream();

                if (file != null && file.Content.Length > 0 && file.ContentType.StartsWith("image/"))
                {
                    ImageResizer resizer = null;
                    if (file.Height < file.Width || file.Height == null || file.Width==null)
                    {
                         resizer = new ImageResizer(width, height, false, ImageFormat.Jpeg);
                    }
                    else
                    {
                         resizer = new ImageResizer(width, height, true, ImageFormat.Jpeg);
                    }


                    Stream stream = new MemoryStream(file.Content);
                    resizer.Resize(stream, out output);
                }
                else if (file != null && !file.ContentType.StartsWith("image/"))
                {
                    return null;
                }
                return new FileContentResult(output.ToArray(), file.ContentType);
            }
            else
                return null;

        }

    }
}
