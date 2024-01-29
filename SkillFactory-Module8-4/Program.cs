using System;
using System.Collections.Generic;
using System.IO;


namespace SkillFactory_Module8_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"\***\students.dat"; // путь до файла, содержащего, считываемые бинарные данные
            string directoryPath = @"\***\Students"; //путь до создаваемой по заданию папки students
            Student[] students = ReadDataFromBin(path).ToArray();
            foreach (Student student in students)
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string filePath = @$"\***\Students\{student.Group}.txt"; // путь до создаваемого по заданию текстового файла группы
                if (!File.Exists(filePath))
                {
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        sw.Write(student.Name);
                        sw.Write($" {student.DateOfBirth}");
                        sw.Write($" {student.AvgPoint}\n");

                    }

                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.Write(student.Name);
                        sw.Write($" {student.DateOfBirth}");
                        sw.Write($" {student.AvgPoint}\n");

                    }
                }
            }
        }

        static List<Student> ReadDataFromBin(string path)
        {
            List<Student> students = new();
            if (File.Exists(path))
            {
                using (BinaryReader read = new BinaryReader(File.OpenRead(path)))
                {
                    while (read.PeekChar() > -1)
                    {
                        Student student = new Student();
                        student.Name = read.ReadString();
                        student.Group = read.ReadString();
                        long DateOfBirth = read.ReadInt64();
                        student.DateOfBirth = DateTime.FromBinary(DateOfBirth);
                        student.AvgPoint = read.ReadDecimal();
                        students.Add(student);
                    }
                    return students;
                }
            }
            else
            {
                Console.WriteLine("Неверный путь");
                return null;
            }

        }
    }
}
