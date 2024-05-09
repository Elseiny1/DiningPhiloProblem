//Dining Philosophers
///<summary>
///5 philosophers with 5 chopsticks
///sit arround circular table
///each of them wants to eat with a chopstick in each hand(right, left)at random time
///This Situatio is a deadlock proplem and we will to avid deadlock in this solution :)
/// </summary>

//Cnopsticks Actions
class Chopstick
{
    bool[] chopstickes=new bool[5];

    //Get Chopsticks
    public void Get(int left,int right)
    {
        lock (this)
        {
            while(chopstickes[left]||chopstickes[right])
                Monitor.Wait(this);

            chopstickes[left]=true;
            chopstickes[right] = true;
        }
    }
    //Put Chopsticks
    public void Put(int left,int right)
    {
        lock(this)
        {
            chopstickes[left]=false;
            chopstickes[right]=false;
            Monitor.PulseAll(this);
        }
    }
}
//Philosophers Actions
class Philosopher
{
    int _philoNumber;
    int _thinkDelay;
    int _eatDelay;
    int _left,_right;
    Chopstick _chopstick;

    //initialling Attriputes
    public Philosopher(int philoNumber,
        int thinkDelay,
        int eatDelay,
        Chopstick chopstick)
    {
        _philoNumber = philoNumber;
        _thinkDelay = thinkDelay;
        _eatDelay = eatDelay;
        _left = philoNumber == 0 ? 4 : (philoNumber - 1) % 5;
        _right = (philoNumber) % 5;
        _chopstick = chopstick;
        new Thread(new ThreadStart(Run)).Start();//running methode passed to be excuted with each new object
    }
    public void Run()
    {
        try
        {
            for(; ; )
            {
                Thread.Sleep(_thinkDelay);
                _chopstick.Get(_left, _right);
                Console.WriteLine("Philosopher "+_philoNumber+" is eating...");
                Console.ReadLine();
                Thread.Sleep(_thinkDelay);
                _chopstick.Put(_left,_right);
            }
        }
        catch
        {
            return;
        }
    }
}

//Executing code
class Philo
{
    public static void Main(string[] args)
    {
        Chopstick chopstick = new();
        new Philosopher(0, 10, 5, chopstick);
        new Philosopher(1, 20, 40, chopstick);
        new Philosopher(2, 30, 30, chopstick);
        new Philosopher(3, 40, 20, chopstick);
        new Philosopher(4, 50, 10, chopstick);

    }

}


