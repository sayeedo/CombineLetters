using System.Configuration;

namespace CombineLetters
{
    public class Program
    {
        static void Main()
        {         
            LetterService letterService = new();

            //Todays date in yyyyMMdd format
            var currentDate = DateTime.Today.ToString("yyyyMMdd");

            //Fixed directories
            var inputDir = ConfigurationManager.AppSettings["inputPath"];
            var outputDir = ConfigurationManager.AppSettings["outputPath"];
            var archiveDir = ConfigurationManager.AppSettings["archivePath"];
            
            try
            {
                //Check if the Input, Ouput and Archive Direcotries exist in the system
                if (Directory.Exists(inputDir) && Directory.Exists(outputDir) && Directory.Exists(archiveDir))
                {
                    DirectoryInfo admissions = new(Path.Combine(inputDir, "Admission"));
                    DirectoryInfo scholarships = new(Path.Combine(inputDir, "Scholarship"));

                    //Check if the Input directory contains Admission and Scholarship subdirectories
                    if (admissions.Exists && scholarships.Exists)
                    {
                        //Look for/Get the folder with today's date from Admission and Scholarship directories
                        DirectoryInfo[] admissionsToday = admissions.GetDirectories(currentDate);
                        DirectoryInfo[] scholarshipsToday = scholarships.GetDirectories(currentDate);

                        //Create folder with today's date in Output and Archive. This will allow us to validate if the application was run or not and if there were no file for processing.
                        Directory.CreateDirectory(Path.Combine(outputDir, currentDate));
                        Directory.CreateDirectory(Path.Combine(archiveDir, currentDate));

                        //This will hold list of student Id's whose letters were combined
                        var combinedStudents = new List<string>();

                        //Check if the folder with today's date was found in Admission and Scholarship directories
                        if (admissionsToday.Length > 0 && scholarshipsToday.Length > 0)
                        {
                            FileInfo[] admissionsFiles = admissionsToday.First().GetFiles();

                            //Check if there are student files in the dated folder in Admission directory
                            //If yes, loop throw the files, extract student Id nd check if any of the students have a scholarship letter
                            if (admissionsFiles.Length > 0)
                            {
                                foreach (var file in admissionsFiles)
                                {
                                    var studentId = file.Name.Split('-')[1].Split('.')[0];
                                    var scholarshipFiles = scholarshipsToday.First().GetFiles("*"+studentId+"*").FirstOrDefault();

                                    //If the student also has a scholarship letter, combine the letters and process
                                    if ( scholarshipFiles != null )
                                    {
                                        //Combining the letters
                                        var fileName = "Combined-" + studentId + ".txt";
                                        var fullOutputPath = Path.Combine(outputDir, currentDate, fileName);
                                        letterService.CombineTwoLetters(file.FullName, scholarshipFiles.FullName, fullOutputPath );

                                        //Archival of the processed files
                                        var fullArchivePathAdm = Path.Combine(archiveDir, currentDate, file.Name);
                                        var fullArchivePathSch = Path.Combine(archiveDir, currentDate, scholarshipFiles.Name);
                                        letterService.MoveLetters(file.FullName, scholarshipFiles.FullName, fullArchivePathAdm, fullArchivePathSch);

                                        //Maintain list fo student Id's for report generation
                                        combinedStudents.Add(studentId);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no files to process in Admission directory\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There's no folder for today's date in either Admission or Scholarship directory\n");
                        }

                        //Generating report
                        var currentTimeStamp = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss");
                        var reportName = "Report-" + currentTimeStamp + ".txt";
                        var fullReportpath = Path.Combine(outputDir, currentDate, reportName);
                        letterService.GenerateReport(combinedStudents, fullReportpath);

                        Console.WriteLine("Operation Complete!");
                    }
                    else
                    {
                        throw new DirectoryNotFoundException("Admission/Scholarship Directory doesn't exist.");
                    }
                }
                else
                {
                    throw new DirectoryNotFoundException("Input/Output/Archive Directory doesn't exist.");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("An error occured!. Please check -> {0}",error.Message);
            }
        }
    }
}