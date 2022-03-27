using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace AsCII
{
    class Program
    {
        private const double WIDTH_OFFSET = 2.5;
        private const int MAX_WIGHT = 500;

        [STAThread]
        static void Main(string[] args)
        {
           

            var openFileDialog = new OpenFileDialog
            {
                Filter = "Images | *.bmp; *.png; *jpg; *.JPEG"
            };

            Console.WriteLine("Дима, нажми Enter, чтобы загрузить картинку");
            

            while(true)
            {
                Console.ReadLine();

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();

                var bitmap = new Bitmap(openFileDialog.FileName);
                bitmap = ResizeBitmap(bitmap);

                //char[] asciiTable = { '.', ',', ':', '+', '*', '?', '%', 'S', '#', '@' };
                bitmap.ToGrayscale();

                var converter = new BitmapToASCIIConverter(bitmap);
                var rows = converter.Convert();

                foreach (var row in rows)
                    Console.WriteLine(row);
                File.WriteAllLines("image.txt",rows.Select(r=> new string(r)));

                Console.SetCursorPosition(0, 0);
            }
            Console.ReadKey();
        }
        public static Bitmap ResizeBitmap(Bitmap bitmap)
        {
           
            var newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIGHT / bitmap.Width;
            if (bitmap.Width > MAX_WIGHT || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap, new Size(MAX_WIGHT, (int)newHeight));
            return bitmap;
        }

        
    }
}
