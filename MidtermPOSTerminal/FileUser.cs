using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPOSTerminal
{
    class FileUser
    {
        //methods involving text file reading/making list using said file

        public static List<string[]> GetFile()
        {//gets a file from the specified directory
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "goodsList.csv");
            var fileContents = ReadResults(fileName);
            return fileContents;
        }

        public static List<string[]> ReadResults(string fileName)
        {//
            var results = new List<string[]> { };
            using (var reader = new StreamReader(fileName))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    results.Add(values);
                }
            }
            return results;
        }

        public static List<Goods> MakeGoods()
        {
            List<Goods> goodsList = new List<Goods> { };
            List<string[]> stringList = GetFile();
            stringList.Remove(stringList[0]);
            foreach (var file in stringList)
            {
                Goods good = new Goods(file[0], double.Parse(file[1]), file[2], file[3]);
                goodsList.Add(good);
            }
            return goodsList;
        }
    }
}
