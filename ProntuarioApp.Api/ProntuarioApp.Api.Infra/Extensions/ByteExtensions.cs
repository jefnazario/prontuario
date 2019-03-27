using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace ProntuarioAppAPI.Infra.Extensions
{
    public static class ByteExtensions
    {
        public static bool IsValidImage(this byte[] bytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    Image.FromStream(ms);
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }
    }
}
