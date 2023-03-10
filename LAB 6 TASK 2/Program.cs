using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_6_TASK_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task4();
        }

        static public void Task2()
        {
            int x = 3;
            List<int> GeomProg = new List<int>();
            GeomProg.Add(x);
            for (int i = 0; i < 9; i++)
            {
                x = x * 4;
                GeomProg.Add(x);
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open(@"d:\lab_binary.dat", FileMode.OpenOrCreate)))
            {
                foreach (int a in GeomProg)
                    writer.Write(a.ToString());

            }
            GeomProg.Clear();
            using (BinaryReader reader = new BinaryReader(File.Open(@"d:\lab_binary.dat", FileMode.Open)))
            {
                int count = 0;
                while (reader.PeekChar() > -1)
                {
                    count++;
                    string row = reader.ReadString();

                    if (count == 3 || count == 7)
                    {
                        GeomProg.Add(int.Parse(row));
                    }
                }



            }
            foreach (int a in GeomProg)
            {
                Console.WriteLine(a);
            }
        }

        static public void Task3()
        {
            List<string> Text = new List<string>();
            String pathText = @"d:\lab6t2_text.dat";

            String[] str = File.ReadAllLines(pathText);
            foreach (String row in str)
            {
                Console.WriteLine($"test=>{row}");

                Text.Add(row);
            }

            List<string> NewText = new List<string>();
            for(int i = 0; i < Text.Count; i++)
            {
                string tmp = "";
                for(int j = 0; j < Text[i].Length; j++)
                {
                    if (char.IsDigit(Text[i][j]))
                    {
                        NewText.Add("*");
                    }
                    else
                    {
                        NewText.Add(Text[i][j].ToString());
                    }
                }
            }

            StringBuilder tmpText = new StringBuilder();

            foreach (string strg in NewText)
                tmpText.Append(strg);

            File.WriteAllText(@"d:\lab6t2_text2.dat", tmpText.ToString());
        }

        static public void MakeBackup(int type)
        {
            string dir = @"d:\Lab6_Temp";
            string path = dir;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }



            if (!File.Exists(@"d:\lab_text.dat"))
            {
            Console.WriteLine("Файл lab_text.dat не найден!");
            return;
            }
            

            path = path + "\\lab_text.dat";

            if (!File.Exists(path))
            File.Copy(@"d:\lab_text.dat", path);


            List<String> tmp = new List<String>();

            Console.WriteLine($"path {path}");

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {

                using (BinaryWriter writer = new BinaryWriter(File.Open(dir + "\\lab_backup.dat", FileMode.OpenOrCreate)))
                {
                    while (reader.PeekChar() != -1)
                    {
                        char name = reader.ReadChar();
                        writer.Write(name);
                    }

                }

                DateTime time = File.GetLastWriteTime(@"d:\lab_text.dat");
                FileInfo fileInfo = new FileInfo(@"d:\lab_text.dat");
                long size = fileInfo.Length;
                Console.WriteLine("Дата последнего перезаписывания "+time);
                Console.WriteLine("Размер " + size);
            }
            
            Console.WriteLine("Бэкап успешно создан!");
        }

        static public void BMP()
        {
            List<RGB> list = new List<RGB>();

            using (BinaryReader reader = new BinaryReader(File.Open("d:\\24.bmp", FileMode.Open)))
            {
                Char start1 = reader.ReadChars(1)[0];
                Char start2 = reader.ReadChars(1)[0];
                Int32 size = reader.ReadInt32();
                Int16 field1 = reader.ReadInt16();
                Int16 field2 = reader.ReadInt16();
                Int32 offset = reader.ReadInt32();

                Int32 headerBMPSize = reader.ReadInt32();
                Int32 imgWidth = reader.ReadInt32();
                Int32 imgHeight = reader.ReadInt32();
                Int16 pCount = reader.ReadInt16();

                Int16 bitPerInch = reader.ReadInt16();
                Int32 zipType = reader.ReadInt32();

                Int32 zipImgSize = reader.ReadInt32();
                Int32 hResolution = reader.ReadInt32();
                Int32 vResolution = reader.ReadInt32();
                Int32 colorCount = reader.ReadInt32();
                Int32 importantColorCount = reader.ReadInt32();

                Console.WriteLine($"Размер файла в байтах: {size}");

                Console.WriteLine($"Ширина в пикселях: {imgWidth}");
                Console.WriteLine($"Высота в пикселях: {imgHeight}");

                Console.WriteLine($"Бит/пиксел: {bitPerInch}");

                Console.WriteLine($"Горизонтальное разрешение, пиксель: {hResolution}");
                Console.WriteLine($"Вертикальное разрешение, пиксель: {vResolution}");

                Console.Write("Тип сжатия: ");
                switch (zipType)
                {
                    case 0: Console.WriteLine("BI_RGB (без сжатия)"); break;
                    case 1: Console.WriteLine("BI_RLE8 (8 bit RLE сжатие)"); break;
                    case 2: Console.WriteLine("BI_RLE4 (4 bit RLE сжатие)"); break;
                }
            }
        }
    }

    struct RGB
    {
        public byte r;
        public byte g;
        public byte b;
    }
}
