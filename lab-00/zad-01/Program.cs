string input = Console.ReadLine() ?? "";
string[] arr = input.Split(" ");
int mul = Int16.Parse(arr.Last());
foreach (string text in arr.SkipLast(1))
{
    for (int i = 0; i < mul; i++)
    {
        Console.Write(text);
    }
    Console.Write("\n");
}