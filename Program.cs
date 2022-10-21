

abstract class Worker
{
    public string Name;
    public string Position;
    public string WorkDay;
    public Worker(string name)
    {
        Name = name;
    }
    public string Call()
    {
        return "Созвон\n";
    }
    public string WriteCode()
    {
        return "Кодим\n";
    }
    public string Relax()
    {
        return "Заслужений вiдпочинок\n";
    }
    public abstract string FillWorkDay();
}

class Developer : Worker
{
    public Developer(string name) : base(name)
    {
        Position = "Developer";
    }
    public override string FillWorkDay()
    {
        return Call() + WriteCode() + Relax();

    }
}

class Manager : Worker
{
    private static Random quantity = new Random();
    public Manager(string name) : base(name)
    {
        Position = "Manager";
    }

    int j = quantity.Next(1, 10);
    int k = quantity.Next(1, 5);
    public override string FillWorkDay()
    {
        string call1 = "";
        for (int i = 0; i < j; i++)
        {
            call1 += Call();
        }
        string call2 = "";
        for (int i = 0; i < k; i++)
        {
            call2 += Call();
        }
        return call1 + Relax() + call2;
    }
}

class Team
{
    private string name;
    private List<Worker> workers = new List<Worker>();

    public Team(string _name)
    {
        name = _name;
    }

    public void AddWorker(Worker worker)
    {
        workers.Add(worker);
    }
    public void PrintTeamInfo()
    {
        Console.WriteLine("Назва команди: " + name);
        foreach (var item in workers)
        {
            Console.WriteLine(item.Name);
        }
    }

    public void PrintDetailsInfo(int i)
    {
        Console.WriteLine(workers[i].Name + " " + workers[i].Position);

    }

    public void Display(List<string> positionList)
    {
        Console.WriteLine("Назва команди: " + name);
        for (int i = 0; i < positionList.Count; i++)
        {
            if (positionList[i] == "Developer")
            {
                Developer dev = new Developer(name);
                PrintDetailsInfo(i);
                Console.WriteLine(dev.FillWorkDay());
            }
            else if (positionList[i] == "Manager")
            {
                Manager man = new Manager(name);
                PrintDetailsInfo(i);
                Console.WriteLine(man.FillWorkDay());
            }
        }
    }
}


class ListOfPosition
{
    List<string> positionList = new List<string>();
    
    public void addPosition(string position)
    {
        positionList.Add(position);
    }

    public List<string> DispayPosition()
    {
        return positionList;
    }
}
internal class Program
{
    private static void Main(string[] args)
    {
        int TeamCheck(List<Team> ListOfTeams, Dictionary<int, string> SearchTeamName)
        {

            int Index = -1;
            string CheckTeamName = "";
            Console.WriteLine("Введiть назву команди ");
            CheckTeamName = Console.ReadLine();
            bool succes = false;
            while (!succes)
            {
                for (int i = 0; i < ListOfTeams.Count; i++)
                {
                    if (CheckTeamName == SearchTeamName[i])
                    {
                        Index = i;
                        succes = true;
                        break;
                    }
                }
                if (succes == false)
                {
                    Console.WriteLine("Команда не знайдена. Спробуйте ще раз!");
                    CheckTeamName = Console.ReadLine();
                }
            }
            return Index;
        }
        List<string> Teams = new List<string>();
        List<Team> ListOfTeams = new List<Team>();
        List<ListOfPosition> WorkerPositionList = new List<ListOfPosition>();
        Dictionary<int, string> SearchTeamName = new Dictionary<int, string>();
        string nameOfTeam = "";
        int IndexOfTeam = -1;
        string WorkerPosition = "";
        while (true)
        {
            int action = 0;
            Console.WriteLine("Виберiть дiю 1,2,3,4\n1. Додати робiтника\n2. Додати команду\n3. Надрукувати iнформацiю про членiв команди\n4. Надрукувати деталi\n5. Вийти з програми");
            try
            {
                action = Int32.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
            switch (action)
            {
                
                case 1:
                    if (IndexOfTeam < 0)
                    {
                        Console.WriteLine("Спочатку додайте команду\n");
                        break;
                    }
                    int index1 = TeamCheck(ListOfTeams, SearchTeamName);
                    
                    string WorkerName;
                    Console.WriteLine("Введiть iм`я: ");
                    WorkerName = Console.ReadLine();
                    Console.WriteLine("Введiть посаду: ");
                    WorkerPosition = Console.ReadLine();
                    while (WorkerPosition != "Developer" && WorkerPosition != "Manager")
                    {
                        Console.WriteLine("Посади з такою назвою нема! Спробуйте знову");
                        WorkerPosition = Console.ReadLine();
                    }
                    WorkerPositionList[index1].addPosition(WorkerPosition);
                    if (WorkerPosition == "Developer")
                    {
                        Worker worker = new Developer(WorkerName);
                        ListOfTeams[index1].AddWorker(worker);
                    }
                    else if (WorkerPosition == "Manager")
                    {
                        Worker worker = new Manager(WorkerName);
                        ListOfTeams[index1].AddWorker(worker);
                    }
                    Console.WriteLine();
                    break;

                case 2:
                    bool check = false;
                    Console.WriteLine("Введiть назву команди: ");
                    nameOfTeam = Console.ReadLine();
                        for (int i = 0; i < ListOfTeams.Count; i++)
                        {
                            if (nameOfTeam == SearchTeamName[i])
                            {
                                Console.WriteLine("Ця назва уже зайнята. Спробуйте ще раз\n");
                            check = true;
                            break;
                            }
                        }
                    if (check == true) break;
                    Teams.Add(nameOfTeam);
                    IndexOfTeam++;
                    SearchTeamName.Add(IndexOfTeam, nameOfTeam);
                    WorkerPositionList.Add(new ListOfPosition());
                    ListOfTeams.Add(new Team(nameOfTeam));

                    break;

                case 3:
                    if (IndexOfTeam < 0)
                    {
                        Console.WriteLine("Спочатку додайте команду\n");
                        break;
                    }
                    int index2 = TeamCheck(ListOfTeams, SearchTeamName);
                    ListOfTeams[index2].PrintTeamInfo();
                    Console.WriteLine();
                    break;

                case 4:
                    if (IndexOfTeam < 0)
                    {
                        Console.WriteLine("Спочатку додайте команду\n");
                        break;
                    }
                    for (int i = 0; i < ListOfTeams.Count; i++)
                    {
                        ListOfTeams[i].Display(WorkerPositionList[i].DispayPosition());
                        Console.WriteLine();
                    }
                    break;

                case 5: return;
            }
        }
    }
}