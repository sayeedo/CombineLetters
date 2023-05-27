using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombineLetters
{
    public interface ILetterService
    {
        /// <summary>
        /// Combine two letter files into one file
        /// </summary>
        /// <param name="inputFile">File path for the first letter</param>
        /// <param name="inputFile2">File path for the second letter</param>
        /// <param name="resultFile">File path for the result letter</param>
        void CombineTwoLetters(string inputFile, string inputFile2, string resultFile);

        /// <summary>
        /// Move the letters to Archive
        /// </summary>
        /// <param name="inputFile">File path for the first letter</param>
        /// <param name="inputFile2">File path for the second letter</param>
        /// <param name="archiveDirAdm">File path for the archive of first letter</param>
        /// <param name="archiveDirSch">File path for the archive of second letter</param>
        void MoveLetters(string inputFile, string inputFile2, string archiveDirAdm, string archiveDirSch);

        /// <summary>
        /// Generate report for the current date
        /// </summary>
        /// <param name="students">List of students whose letters have been combined</param>
        /// <param name="path">File path for the report</param>
        void GenerateReport(List<string> students, string path);
    }
}
