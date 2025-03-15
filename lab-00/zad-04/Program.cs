List<string> tony = ["C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "B", "H"];

string input = System.Console.ReadLine() ?? "";
if (input == "")
{
    throw new Exception("No input");
}

int[] skoki = [2, 2, 1, 2, 2, 2, 1];

int index = tony.IndexOf(input);
System.Console.WriteLine(tony[index]);
foreach (var i in skoki)
{
    index = (index + i) % 12;
    System.Console.WriteLine(tony[index]);
}