using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpora
{
    class Config
    {
        public static Font Font { get; set; } = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
        public static int OffsetX { get; set; } = 50;
        public static int OffsetY { get; set; } = 50;
        public static string Splitter { get; set; } = "@@@";
        public static float Interval { get; set; } = 1;
        public static double Opacity { get; set; } = 1;

        public static void GetConfig()
        {
            try
            {
                StreamReader sr = new StreamReader("config.ini");
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.StartsWith("#"))
                        continue;
                    try
                    {
                        switch (line.Split('=')[0].Trim(' '))
                        {
                            case "OffsetX":
                                OffsetX = int.Parse(line.Split('=')[1].Trim(' '));
                                break;
                            case "OffsetY":
                                OffsetY = int.Parse(line.Split('=')[1].Trim(' '));
                                break;
                            case "Splitter":
                                Splitter = line.Split('=')[1].Trim(' ');
                                break;
                            case "Interval":
                                Interval = float.Parse(line.Split('=')[1].Trim(' ').Replace('.', ','));
                                break;
                            case "Opacity":
                                Opacity = double.Parse(line.Split('=')[1].Trim(' ').Replace('.', ','));
                                break;
                            case "Font":
                                Font = new Font(line.Split('=')[1].Trim(' '), Font.Size);
                                break;
                            case "FontSize":
                                Font = new Font(Font.Name, float.Parse(line.Split('=')[1].Trim(' ').Replace('.', ',')));
                                break;
                            default:
                                continue;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                sr.Close();
            }
            catch (IOException)
            { }
        }
    }
}
