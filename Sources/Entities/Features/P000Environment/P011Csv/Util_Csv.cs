using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Grayscale.Kifuwarakaku.Entities.Features
{


    public abstract class Util_Csv
    {

        public static List<List<string>> ReadCsv(string path)
        {
            List<List<string>> rows = new List<List<string>>();

            foreach (string line in File.ReadAllLines(path))
            {
                rows.Add(CsvLineParserImpl.UnescapeLineToFieldList(line, ','));
            }

            return rows;
        }

        public static List<List<string>> ReadCsv(string path, Encoding encoding)
        {
            List<List<string>> rows = new List<List<string>>();

            foreach (string line in File.ReadAllLines(path, encoding))
            {
                rows.Add(CsvLineParserImpl.UnescapeLineToFieldList(line, ','));
            }

            return rows;
        }

    }


}
