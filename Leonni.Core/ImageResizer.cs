using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Leonni.Core
{

    public class ImageResizer
    {

        /// <summary>
        /// Maximum width of resized image.
        /// </summary>
        public int MaxX { get; set; }

        /// <summary>
        /// Maximum height of resized image.
        /// </summary>
        public int MaxY { get; set; }

        /// <summary>
        /// If true, resized image is trimmed to exactly fit
        /// maximum width and height dimensions.
        /// </summary>
        public bool TrimImage { get; set; }

        /// <summary>
        /// Format used to save resized image.
        /// </summary>
        public ImageFormat SaveFormat { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImageResizer(int targetWidth, int targetHeight, bool trimImage, ImageFormat targetFormat)
        {
            MaxX = targetWidth;
            MaxY = targetHeight;
            TrimImage = trimImage;
            SaveFormat = targetFormat;
        }

        /// <summary>
        /// Resizes the image from the source file according to the
        /// current settings and saves the result to the target file.
        /// </summary>
        /// <param name="source">Image to resize</param>
        /// <param name="target">Path to save resized image</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Resize(Stream source, out MemoryStream target, out int width, out int height, bool useOriginalSize = false)
        {
            target = new MemoryStream();
            height = 0;
            width = 0;

            using (System.Drawing.Image src = System.Drawing.Image.FromStream(source, true, true))
            {
                // Check that we have an image
                if (src != null)
                {

                    int origX, origY, newX, newY;
                    int trimX = 0, trimY = 0;

                    // Default to size of source image
                    newX = origX = src.Width;
                    newY = origY = src.Height;
                    if (useOriginalSize)
                    {
                        newX = src.Width;
                        newY = src.Height;
                        trimX = trimY = 0;
                    }
                    else
                    {
                        // Does image exceed maximum dimensions?
                        if (origX > MaxX || origY > MaxY)
                        {
                            // Need to resize image
                            if (TrimImage)
                            {
                                // Trim to exactly fit maximum dimensions
                                double factor = Math.Max((double)MaxX / (double)origX, (double)MaxY / (double)origY);
                                newX = (int)Math.Ceiling((double)origX * factor);
                                newY = (int)Math.Ceiling((double)origY * factor);
                                trimX = newX - MaxX;
                                trimY = newY - MaxY;
                            }
                            else
                            {
                                // Resize (no trim) to keep within maximum dimensions
                                double factor = Math.Min((double)MaxX / (double)origX,
                                    (double)MaxY / (double)origY);
                                newX = (int)Math.Ceiling((double)origX * factor);
                                newY = (int)Math.Ceiling((double)origY * factor);
                            }
                        }
                    }
                    // Create destination image
                    using (System.Drawing.Image dest = new Bitmap(newX - trimX, newY - trimY))
                    {
                        Graphics graph = Graphics.FromImage(dest);
                        graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graph.DrawImage(src, 0, 0, newX, newY);
                        graph.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graph.TextContrast = 3;
                        graph.CompositingQuality = CompositingQuality.HighQuality;
                        graph.SmoothingMode = SmoothingMode.HighQuality;
                        graph.CompositingMode = CompositingMode.SourceCopy;

                        width = newX - trimX;
                        height = newY - trimY;

                        dest.Save(target, SaveFormat);

                        // Indicate success
                        return true;
                    }
                }
            }
            // Indicate failure
            return false;
        }

        /// <summary>
        /// Takes in a input stream and returns a resized image stream.
        /// </summary>
        /// <param name="inputStream">Input Stream</param>
        /// <param name="outputStream">Out parameter (Stream)</param>
        /// <param name="width">Required image width</param>
        /// <param name="height">Required image height</param>
        /// <returns>Boolean success flag. 0 = failure, 1 = success</returns>
        public bool Resize(Stream inputStream, out MemoryStream outputStream)
        {
            int h, w;
            return this.Resize(inputStream, out outputStream, out h, out w, false);
        }

    }

}
