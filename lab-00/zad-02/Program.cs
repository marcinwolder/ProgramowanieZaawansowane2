int sum = 0;
int count = 0;
int input = Int32.Parse(System.Console.ReadLine() ?? "0");
while (input != 0)
{
    sum += input;
    count += 1;
    input = Int32.Parse(System.Console.ReadLine() ?? "0");
}

System.Console.WriteLine(Convert.ToDouble(sum) / count);

StreamWriter sw = new StreamWriter("wynik.txt", append: true);
sw.WriteLine(Convert.ToDouble(sum) / count);
sw.Close();
