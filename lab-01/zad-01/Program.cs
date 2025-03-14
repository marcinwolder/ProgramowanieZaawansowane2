class Program
{
    static void Main() {
        String biggest = "";
        StreamWriter sw = new StreamWriter("log.txt", append:true);
        String input;
        while((input = Console.ReadLine() ?? "") != "koniec!"){
            sw.WriteLine(input);
            if(String.Compare(biggest, input) < 0){
                biggest = input;
            }
        }
        sw.Close();
        Console.WriteLine("Ostatni napis: "+biggest);
    }
}