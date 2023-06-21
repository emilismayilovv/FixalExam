namespace FINALEXAM.Utilities.Extensions
{
    public static class FileExtension
    {
        public static bool CheckType(this IFormFile file, string type)
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }

        public static bool CheckSize(this IFormFile file, int size)
        {
            if (file.Length <= size * 1024)
            {
                return true;
            }
            return false;
        }

        public static async Task<string> CreateFileAsync(this IFormFile file, string root, string folder)
        {
            string filename = Guid.NewGuid().ToString()+file.FileName;
            string path = Path.Combine(root, folder, filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filename;
        }

        public static void DeleteFile(this string file, string root, string folder)
        {
            string path = Path.Combine(folder, file, root);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }
    }
}
