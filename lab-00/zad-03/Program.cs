string fileName = System.Console.ReadLine() ?? "";
if (fileName == "")
{
    throw new ArgumentException("No filename");
}

int? maxValue = null;
int? maxValueLine = null;
int lineNumber = 1;

StreamReader sr = new StreamReader(fileName);
while (!sr.EndOfStream)
{
    int number = Int32.Parse(sr.ReadLine() ?? "");
    if (maxValue == null || number > maxValue)
    {
        maxValue = number;
        maxValueLine = lineNumber;
    }
    lineNumber++;
}
sr.Close();

System.Console.WriteLine($"{maxValue}, linijka: {maxValueLine}");