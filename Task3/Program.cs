using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Task3
{
    internal class Program
    {
        private static DateTime startTime;
        private static long sizeDeleteFiles;
        private static string pathDirectory;
        static void Main(string[] args)
        {
            startTime = DateTime.Now;
            sizeDeleteFiles = 0;
            pathDirectory = @"C:\\Users\\M-015\\Desktop\\123";//Это переменная - путь до папки с которой мы будем работать
            if (!Directory.Exists(pathDirectory)) // Проверим, что директория существует
            {
                Console.WriteLine("Не существует папки по указанному пути!");
                Console.ReadKey();
                return;
            }
            long size = CalculateSizeFolder(pathDirectory);
            Console.WriteLine("Исходный размер папки: " + size + "байт");
            ClearFilesAndFolders(pathDirectory, ref sizeDeleteFiles);
            Console.WriteLine("Освобождено: " + sizeDeleteFiles + "байт");
            size = CalculateSizeFolder(pathDirectory);
            Console.WriteLine("Текущий размер размер папки: " + size + "байт");
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

        public static void ClearFilesAndFolders(string dirName, ref long sizeDeleteFiles)
        {
            string[] dirs = Directory.GetDirectories(dirName);  // Получим все директории корневого каталога
            string[] files = Directory.GetFiles(dirName);// Получим все файлы корневого каталога
            foreach (string file in files)
            {
                DateTime lastWorkFile = System.IO.File.GetLastWriteTime(file);
                if ((startTime - lastWorkFile) > TimeSpan.FromMinutes(30))
                {
                    //Да, этот файл изменялся более чем 30 минут назад, значит удаляем его
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        sizeDeleteFiles += fileInfo.Length;
                        File.Delete(file);
                    }
                    catch
                    {
                        Console.WriteLine("Не удалоcь удалить файл" + file);
                    }
                }
            }
            if (dirs.Length > 0)
            {
                //Рекурсия по вложенным папкам
                foreach (string dir in dirs)
                {
                    ClearFilesAndFolders(dir, ref sizeDeleteFiles);
                }
            }
            //Делаем проверку, остались ли после удаления хоть какие то файлы в папке, если нет то удаляем папку
            files = Directory.GetFiles(dirName);
            if (files.Length == 0)
            {
                try
                {
                    //Делаем проверку чтобы не удалить верхнеуровневую папку
                    if (dirName != pathDirectory)
                    {
                        Directory.Delete(dirName, true);
                    }
                }
                catch
                {
                    Console.WriteLine("Не удалоcь удалить папку " + dirName);
                }
            }
        }
    }
}
