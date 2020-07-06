using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpora
{
    class Base
    {
        struct Line
        {
            public string Q { get; set; }
            public string A { get; set; }

            static public Line Parse(string line, string splitter)
            {
                char ch = (char)1500;
                string ns = "" + ch;
                line = line.Replace(splitter, ns);
                Line temp = new Line();
                temp.Q = line.Split(ch)[0].Trim(' ');
                temp.A = line.Split(ch)[1].Trim(' ');
                return temp;
            }

            public override string ToString()
            {
                return string.Format("{0} {1}", Q, A);
            }
        }

        static string Splitter = "@@@";

        static List<Line> list = new List<Line>();

        static public void SetSplitter(string splitter)
        {
            Splitter = splitter;
        }

        static public string FileImport()
        {
            try
            {
                Directory.CreateDirectory("bases");
                string[] files = Directory.GetFiles("bases");
                if (files.Length == 0)
                    throw new IOException();
                return CreateList(files);
            }
            catch (IOException)
            {
                return "Файлы не найдены.";
            }
        }

        static private string CreateList(string[] files)
        {
            list.Clear();
            int counter = 0;
            foreach (string file in files)
            {
                try
                {
                    StreamReader sr = new StreamReader(file);
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (line.Length > 0 && !list.Contains(Line.Parse(line, Splitter)))
                            list.Add(Line.Parse(line, Splitter));
                    }
                    sr.Close();
                    counter++;
                }
                catch { continue; }
            }
            return string.Format("Обработанно файлов: {0} из {1} ({2} вопросов)", counter, files.Length, list.Count);
        }

        static public string FindAnsver(string question)
        {
            string ansvers = "";
            int num = 0;
            foreach (Line line in list)
            {
                if (line.Q.IndexOf(question) != -1)
                    ansvers += string.Format("{0}) {1}" + Environment.NewLine, (++num), line.A);
            }
            if (num == 0)
                return "Не найдено";
            return ansvers.Trim('\r', '\n');
        }
    }
}
