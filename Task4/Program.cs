using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    internal class Program
    {
        public static string path = @"C:\Users\M-015\Desktop\Students.dat";
        static Student[] students;
        static void Main(string[] args)
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    students = (Student[])formatter.Deserialize(fs);
                }
                //Создаем папку на рабочем столе
                string newPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Students";
                DirectoryInfo dirInfo = new DirectoryInfo(newPath);
                if (!dirInfo.Exists)
                    dirInfo.Create();
                //Создаем текстовые файлы с группами и записываем в них студентов
                foreach (Student student in students)
                {
                    string group = student.Group;
                    string filePath = newPath + "\\" + group + ".txt";
                    if (!File.Exists(filePath))
                    {
                        using (StreamWriter sw = File.CreateText(filePath))
                        {
                            sw.WriteLine(student.Name + ", " + student.DateOfBirth.Date.ToShortDateString());
                        }
                    }
                    else
                    {
                        System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath, true);
                        writer.WriteLine(student.Name + ", " + student.DateOfBirth.Date.ToShortDateString());
                        writer.Close();
                    }
                }
            }
        }
    }
}
