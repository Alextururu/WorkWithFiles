using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pathDirectory = @"C:\\Users\\M-015\\Desktop\\123";//Это переменная - путь до папки с которой мы будем работать
            if (!Directory.Exists(pathDirectory)) // Проверим, что директория существует
            {
                Console.WriteLine("Не существует папки по указанному пути!");
                Console.ReadKey();
                return;
            }
            long size = CalculateSizeFolder(pathDirectory);
            Console.WriteLine("Размер папки " + size + " байт");
            Console.ReadKey();
        }
        public static long CalculateSizeFolder(string dirName)
        {
            long size = 0;
            string[] dirs = Directory.GetDirectories(dirName);  // Получим все директории корневого каталога
            string[] files = Directory.GetFiles(dirName);// Получим все файлы корневого каталога
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                size = size + fileInfo.Length;
            }
            if (dirs.Length > 0)
            {
                //Рекурсия по вложенным папкам
                foreach (string dir in dirs)
                {
                    size = size + CalculateSizeFolder(dir);
                }
            }
            return size;
        }
    }
}
