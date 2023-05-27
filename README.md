Task: Develop a console application to process admission and scholarship letters of students.
Author: Sayeed Ouddin
Date: 05/26/2023

How to Test/Validate the application:

  Prerequisites:
    1. Update the root path of Input, Output and Archive directories in App.config
    2. The subdirectories of Input should be named 'Admission' and 'Scholarship' respectively.
    3. If present, the folder names in Admission/Scholarship directory should be in yyyyMMdd format.
    4. Install 'System.Configuration.ConfigurationManager' package from NuGet. This allows us to read root paths from App.config file.
  
  Assumptions:
    1. The application expects the directories to be present in the local system. 
    2. Only the processed files are moved from the dated folder in Input to Archive directory.
    3. The unmatched files remain in their parent directory.
    4. The application does not validate if the current day is a weekday. This is expected to be handled during scheduling.
  
  Features:
    1. Validate if directories exist
    2. Check if a student has letters from both the Admissions Office and the Financial Aid Office.
    3. If yes, combine the letters and move it to the Output directory.
    4. Next, Move the processed files from Input directory to Archive directory.
    5. Generate a report for each run of the application which lists the number of combied letters and students ids associated with it.
  
  Testing:
    1. Ensure prerequisites are met.
    2. Run the application.
    3. Navigate to Output and validate the report
  
  Comments:
    1. The application creates folders with current date in Output and Archive directories to move the respective files. This will help to track if
       the application was run on a certain/previous day.
    2. The report is generated irrespective of the files for a current day.
    3. The report's name is suffixed with current date and local time so as to ensure there are seperate reports for each run of the application on the same day.
    4. The application will handle the below cases,
        i. If the application was run before/after the scheduled time 
          - It will process the files and generate report on the first run. If the application runs again and there are no files to be processed, it will still 
            generate a report indicating number of combined letters as '0'.
        ii. If the application wasn't run the day before but runs today
          - The application will process files on the current date of the run. 
          - There is currently no functionality to check previous day's run/results. This can be added as an extra functionality.
  
  Problems faced:
	1. The sample report has date in MM/dd/yyyy format. It took some time to understand why DateTime.Today.ToString("MM/dd/yyyy") will not return the desired format. 
	   Had to go through the documentaion to find out that I had to paas the object of CultureInfo("en-US") as provider to ToString() for it.
	2. File.Move() expects full path of destination file (incl. name). Checking this earlier would have saved me some time.
  
