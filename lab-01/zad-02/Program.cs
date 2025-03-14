string fileName = Console.ReadLine() ?? "";
string wordFind = Console.ReadLine() ?? "";

int lineCnt = 0;
StreamReader sr = new StreamReader(fileName);
while (!sr.EndOfStream)
{
    lineCnt++;
    string line = sr.ReadLine() ?? "";
    int i = line.IndexOf(wordFind);
    while(i != -1){
        System.Console.WriteLine("linijka: "+lineCnt+", pozycja: "+(i+1));
        i = line.IndexOf(wordFind, i+1);
    }
}
sr.Close();
