using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON
{
    public enum FileExtension
    {
        JPG = 255216,
        GIF = 7173,
        PNG = 13780,
   
        VALIDFILE = 9999999
        // 255216 jpg; 

        // 7173 gif; 

        // 6677 bmp, 

        // 13780 png; 

        // 6787 swf 

        // 7790 exe dll, 

        // 8297 rar 

        // 8075 zip 

        // 55122 7z 

        // 6063 xml 

        // 6033 html 

        // 239187 aspx 

        // 117115 cs 

        // 119105 js 

        // 102100 txt 

        // 255254 sql  

    }
    public class FileHelper
    {


        public static FileExtension CheckTextFile(byte[] bytes)
        {
            //FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            //System.IO.BinaryReader br = new System.IO.BinaryReader(fileStream);
            string fileType = string.Empty; ;
            try
            {
                byte data = bytes[0];
                fileType += data.ToString();
                data = bytes[1];
                fileType += data.ToString();
                FileExtension extension;
                try
                {
                    extension = (FileExtension)Enum.Parse(typeof(FileExtension), fileType);
                }
                catch
                {

                    extension = FileExtension.VALIDFILE;
                }
                return extension;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
    
}

