class Program
{
    static void Main(string[] args){
        string fileName = args[0];
        int n = Int32.Parse(args[1]);
        int down = Int32.Parse(args[2]);
        int up = Int32.Parse(args[3]);
        int seed = Int32.Parse(args[4]);
        bool real = Boolean.Parse(args[5]);

        StreamWriter sw = new StreamWriter(fileName);

        Random random = new Random(seed);
        for(int i = 0; i < n; i++){
            if(real){
                sw.WriteLine(random.NextDouble()*(up-down)+down);
            } else {
                sw.WriteLine(random.Next(down, up));
            }
        }

        sw.Close();
    }
}