using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab6Charp
{
    internal class Program
    {

        static void task1()
        {
            string inputPath = "input_1.txt";
            string outputPath = "output_1.txt";

            string text = File.ReadAllText(inputPath);

            string pattern = @"\b(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.(19[0-9]{2}|20[0-9]{2})\b";

            MatchCollection matches = Regex.Matches(text, pattern);

            string[] dates = new string[matches.Count];
            int count = 0;

            foreach (Match match in matches)
            {
                dates[count++] = match.Value;
            }

            Console.WriteLine("Find date in {0}: " + count,inputPath);

            File.WriteAllLines(outputPath, dates);

            Console.WriteLine("Result in {0}",outputPath);
        }

        static void task2()
        {
            string inputPath = "input_2.txt";
            string outputPath = "output_2.txt";
            Console.Write("Enter word:");
            string searchWord = Console.ReadLine();

            string text = File.ReadAllText(inputPath);

            if (text.Contains(searchWord))
                File.WriteAllText(outputPath, "Text has word.");
            else
                File.WriteAllText(outputPath, "Text hasnd word.");

        }

        static void task3()
        {
            string inputPath = "input_3.txt";
            string outputPath = "output_3.txt";

            string text = File.ReadAllText(inputPath);

            string[] words = Regex.Split(text, @"[\s,.;:!?()\[\]\""\-]+");

            string doubledWords = "";
            string cleanedText = text;

            foreach (string word in words)
            {
                if (word == "") continue;

                if (Regex.IsMatch(word, @"(.)\1"))
                {
                    doubledWords += word + " ";

                    string[] tokens = cleanedText.Split(' ');
                    string newText = "";

                    foreach (string token in tokens)
                    {
                        if (token != word)
                            newText += token + " ";
                    }

                    cleanedText = newText.Trim();
                }
            }

            File.WriteAllText(outputPath,
                "Word with 2 same latters:\n" + doubledWords.Trim() +
                "\n\nText wiyout these word:\n" + cleanedText);

        }

        static void task4()
        {
            string path = "powers_of_3.dat";

            int size = Convert.ToInt32(Console.ReadLine());

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i <= size; i++)
                {
                    int power = (int)Math.Pow(3, i);
                    writer.Write(power);
                }
            }

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                int index = 0;
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32();
                    if (index % 2 == 0)
                    {
                        Console.WriteLine($"Індекс {index}: {number}");
                    }
                    index++;
                }
            }
        }

        static void task5()
        {
            string basePath = @"d:\temp";
            string folder1 = Path.Combine(basePath, "Андрійчук1");
            string folder2 = Path.Combine(basePath, "Андрійчук2");

            Directory.CreateDirectory(folder1);
            Directory.CreateDirectory(folder2);

            string t1Path = Path.Combine(folder1, "t1.txt");
            string t2Path = Path.Combine(folder1, "t2.txt");

            File.WriteAllText(t1Path, "Певна персона номер 1, 2001 року народження, місце проживання м. Суми");
            File.WriteAllText(t2Path, "Певна персона номер 2 з кращим іменем, 2000 року народження, місце проживання м. Київ");

            string t3Path = Path.Combine(folder2, "t3.txt");
            string combinedText = File.ReadAllText(t1Path) + Environment.NewLine + File.ReadAllText(t2Path);
            File.WriteAllText(t3Path, combinedText);

            string t2NewPath = Path.Combine(folder2, "t2.txt");
            File.Move(t2Path, t2NewPath);

            string t1CopyPath = Path.Combine(folder2, "t1.txt");
            File.Copy(t1Path, t1CopyPath, overwrite: true);

            string allPath = Path.Combine(basePath, "All");
            if (Directory.Exists(allPath)) Directory.Delete(allPath, true);
            Directory.Move(folder2, allPath);
            Directory.Delete(folder1, true);

            Console.WriteLine("\nІнформація про файли в папці ALL:");
            string[] allFiles = Directory.GetFiles(allPath);
            foreach (string file in allFiles)
            {
                FileInfo fi = new FileInfo(file);
                Console.WriteLine($"Файл: {fi.Name}");
                Console.WriteLine($"  Розмір: {fi.Length} байт");
                Console.WriteLine($"  Створено: {fi.CreationTime}");
                Console.WriteLine($"  Повний шлях: {fi.FullName}\n");
            }
        }



        static void choose_task()
        {
            Console.Write("1.Зчитування дати \n2.Чи присутнє слово в тесті\n3.Вилучити слова з подвоєнням літер\n4.Степені 3(Бінарний файл)\n5.Робота з ствренням дерикторій та файлів\n");
            int answer = Convert.ToInt16(Console.ReadLine());

            switch (answer)
            {
                case 1:
                    {
                        task1();
                        Console.Write("\n\n\n\n\n\n\n");
                        choose_task();
                        break;
                    }
                case 2:
                    {
                        task2();
                        Console.Write("\n\n\n\n\n\n\n");
                        choose_task();
                        break;
                    }
                case 3:
                    {
                        task3();
                        Console.Write("\n\n\n\n\n\n\n");
                        choose_task();
                        break;
                    }
                case 4:
                    {
                        task4();
                        Console.Write("\n\n\n\n\n\n\n");
                        choose_task();
                        break;
                    }
                case 5:
                    {
                        task5();
                        Console.Write("\n\n\n\n\n\n\n");
                        choose_task();
                        break;
                    }
                case 6:
                default:
                    {
                        choose_task();
                        break;
                    }

            }
        }
        public static void Main(string[] args)
        {
            choose_task();
        }

    }

}