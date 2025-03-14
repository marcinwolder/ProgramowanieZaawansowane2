StreamReader sr = new StreamReader("../zad-03/log.txt");

int lineCnt = 0;
int charCnt = 0;
double? min = null;
double? max = null;
double sum = 0;

while(!sr.EndOfStream){
    string line = sr.ReadLine() ?? "";
    lineCnt++;
    charCnt += line.Length;

    double num = Double.Parse(line);
    sum += num;
    if(min == null || num < min){
        min = num;
    }
    if(max == null || max < num){
        max = num;
    }
}

System.Console.WriteLine("Liczba lini w pliku: "+lineCnt);
System.Console.WriteLine("Liczba znaków w pliku: "+charCnt);
System.Console.WriteLine("Największa liczba: "+max);
System.Console.WriteLine("Najmniejsza liczba: "+min);
System.Console.WriteLine("Średnia liczb: "+sum/lineCnt);