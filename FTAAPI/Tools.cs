namespace FTAAPI
{
    public static class Tools
    {
        public static string ConvertBase64ToFile(string base64, string path)
        {
            if (IsBase64String(base64))
            {
                var base64Array = Convert.FromBase64String(base64);

                string fileType = ".jpg";

                switch (base64[0])
                {
                    case 'i':
                        fileType = ".png";
                        break;
                    case 'R':
                        fileType = ".gif";
                        break;
                    case 'U':
                        fileType = ".webp";
                        break;
                    default:
                        break;
                }

                string fileName = Guid.NewGuid() + fileType;
                string filePath = path + @"\" + fileName;

                File.WriteAllBytes(filePath, base64Array);

                return fileName;

            }

            return string.Empty;
        }

        public static bool IsBase64String(string base64)
        {
            if (string.IsNullOrEmpty(base64)) return false;
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesWritten);
        }
    }
}
