using Android.Graphics;

namespace FitnessApp.Resources.Extensions
{
    public static class IoExtenstions
    {
        public static Bitmap ToBitmap(this byte[] file)
        {
            var bitmap = BitmapFactory.DecodeByteArray(file, 0, file.Length);
            return bitmap;
        }
    }
}