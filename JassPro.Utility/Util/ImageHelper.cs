using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassPro.Utility.Util
{
    public class ImageHelper
    {
        /// <summary>  
        /// 生成缩略图，不加水印  
        /// </summary>  
        /// <param name="filename">源文件</param>  
        /// <param name="nWidth">缩略图宽度</param>  
        /// <param name="nHeight">缩略图高度</param>  
        /// <param name="destfile">缩略图保存位置</param>  
        public void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile)
        {
            Image img = Image.FromFile(filename);
            ImageFormat thisFormat = img.RawFormat;

            Size CutSize = CutRegion(nWidth, nHeight, img);
            Bitmap outBmp = new Bitmap(nWidth, nHeight);
            Graphics g = Graphics.FromImage(outBmp);

            // 设置画布的描绘质量  
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            int nStartX = (img.Width - CutSize.Width) / 2;
            int nStartY = (img.Height - CutSize.Height) / 2;

            g.DrawImage(img, new Rectangle(0, 0, nWidth, nHeight),
              nStartX, nStartY, CutSize.Width, CutSize.Height, GraphicsUnit.Pixel);
            g.Dispose();

            //if (thisFormat.Equals(ImageFormat.Gif))  
            //{  
            //  Response.ContentType = "image/gif";  
            //}  
            //else  
            //{  
            //  Response.ContentType = "image/jpeg";  
            //}  

            // 以下代码为保存图片时，设置压缩质量  
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。  
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            for (int x = 0; x < arrayICI.Length; x++)
            {
                if (arrayICI[x].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[x];//设置JPEG编码  
                    break;
                }
            }

            if (jpegICI != null)
            {
                //outBmp.Save(Response.OutputStream, jpegICI, encoderParams);  
                outBmp.Save(destfile, jpegICI, encoderParams);
            }
            else
            {
                //outBmp.Save(Response.OutputStream, thisFormat);  
                outBmp.Save(destfile, thisFormat);
            }

            img.Dispose();
            outBmp.Dispose();
        }


        /// <summary>  
        /// 根据需要的图片尺寸，按比例剪裁原始图片  
        /// </summary>  
        /// <param name="nWidth">缩略图宽度</param>  
        /// <param name="nHeight">缩略图高度</param>  
        /// <param name="img">原始图片</param>  
        /// <returns>剪裁区域尺寸</returns>  
        public Size CutRegion(int nWidth, int nHeight, Image img)
        {
            double width = 0.0;
            double height = 0.0;

            double nw = (double)nWidth;
            double nh = (double)nHeight;

            double pw = (double)img.Width;
            double ph = (double)img.Height;

            if (nw / nh > pw / ph)
            {
                width = pw;
                height = pw * nh / nw;
            }
            else if (nw / nh < pw / ph)
            {
                width = ph * nw / nh;
                height = ph;
            }
            else
            {
                width = pw;
                height = ph;
            }

            return new Size(Convert.ToInt32(width), Convert.ToInt32(height));
        }

    }
}
