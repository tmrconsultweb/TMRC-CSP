using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TMRC_CSP.ViewModel.Common
{
    public class Images
    {
        public static string[] SaveImages(string ImagePath = "~/Images/Logo", string DbSaveImagePath = "../../../../images/Logo/")
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpFileCollection files = HttpContext.Current.Request.Files;
                string[] imgPath = new string[files.Count];
                for (int i = 0; i < files.Count; i++)
                {
                    var dateTime = DateTime.Now.ToString("ddMMyyyyHHmmssffff");
                    HttpPostedFile file = files[i];
                    if (!Images.IsImage(file)) return new[] { "Invalid image formate" };   //Check that it's a image or not
                    string fname;
                    if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = dateTime + testfiles[testfiles.Length - 1];
                    }
                    else
                        fname = dateTime + file.FileName;

                    imgPath[i] = DbSaveImagePath + fname;
                    fname = Path.Combine(HttpContext.Current.Server.MapPath(ImagePath), fname);
                    try
                    {
                        file.SaveAs(fname);
                    }
                    catch (Exception ex)
                    {
                        return new[] { "Invalid image" };
                    }
                }
                return imgPath;
            }
            return new[] { "Please select an image" };
        }

        public static bool IsImage(HttpPostedFile postedFile)
        {
            const int ImageMinimumBytes = 512;


            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return false;
                }

                if (postedFile.ContentLength < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[512];
                postedFile.InputStream.Read(buffer, 0, 512);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                postedFile.InputStream.Position = 0;
            }

            return true;
        }
    }
}