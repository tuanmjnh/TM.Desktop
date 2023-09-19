using System;
namespace TM.Desktop
{
    public class ResizeImage
    {
        public static System.Drawing.Image RezizeImage(System.Drawing.Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                System.Drawing.Bitmap cpy = new System.Drawing.Bitmap(nnx, nny, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(cpy))
                {
                    gr.Clear(System.Drawing.Color.Transparent);
                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    gr.DrawImage(img,
                        new System.Drawing.Rectangle(0, 0, nnx, nny),
                        new System.Drawing.Rectangle(0, 0, img.Width, img.Height),
                        System.Drawing.GraphicsUnit.Pixel);
                }
                return cpy;
            }
        }
        public static System.IO.MemoryStream ByteArrayToStream(byte[] arr)
        {
            return new System.IO.MemoryStream(arr, 0, arr.Length);
        }
        public static string Upload(byte[] fileupload, string savePath)
        {
            string s = Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                //System.Web.HttpFileCollection fileCol = Request.Files;
                //System.IO.BinaryReader b = new System.IO.BinaryReader(fileupload.InputStream);
                //byte[] buffer = fileupload.FileBytes;
                var buffer = fileupload;
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(byte[] fileupload,string savePath, int MW, int MH)
        {
            string s = Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                //System.Web.HttpFileCollection fileCol = Request.Files;
                //System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                //byte[] buffer = fileupload.FileBytes;
                var buffer = fileupload;
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), MW, MH);
                img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(byte[] fileupload, string savePath, string path)
        {
            string s = path + Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.CreateDirectory(path);
                //System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                //byte[] buffer = fileupload.FileBytes;
                var buffer = fileupload;
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(byte[] fileupload, string savePath, string path, string fileName)
        {
            string s = path + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.CreateDirectory(path);;
                //System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                //byte[] buffer = fileupload.FileBytes;
                var buffer = fileupload;
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
    }
}