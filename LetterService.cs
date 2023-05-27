using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombineLetters
{
    public class LetterService: ILetterService
    {
        public void CombineTwoLetters(string inputFile, string inputFile2, string resultFile) 
        {
            //Merge the content of the two inpts letters and write to the combined letter.
            using (StreamWriter sw = new(resultFile))
            {
                using (StreamReader sr = new(inputFile))
                {
                    sw.Write(sr.ReadToEnd());
                }
                using (StreamReader sr = new(inputFile2))
                {
                    sw.Write(sr.ReadToEnd());
                }
            }
        }

        public void MoveLetters(string inputFile, string inputFile2, string archiveDirAdm, string archiveDirSch)
        {
            //Move the processed files from Input to Archive
            File.Move(inputFile, archiveDirAdm,true);
            File.Move(inputFile2, archiveDirSch,true);
        }

        public void GenerateReport(List<string> students, string path)
        {
            //Generate the report
            using (StreamWriter sw = new(path))
            {
                sw.WriteLine(@"
 {0} Report
 -----------------------------------
                
 Number of Combined Letters: {1}", DateTime.Today.ToString("MM/dd/yyyy", new CultureInfo("en-US")), students.Count);
                foreach(var id in students)
                {
                    sw.WriteLine("\t{0}",id);
                }
            }
        }
    }
}
