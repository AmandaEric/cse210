using System;
using System.Collections.Generic;

public class Activity
{
    private string _date;
    private double _duration;

    public Activity(string date, double duration)
    {
        _date = date;
        _duration = duration;
    }

    public string GetDate() => _date;
    public double GetDuration() => _duration;

    public virtual double GetDistance() => 0;
    public virtual double GetSpeed() => 0;
    public virtual double GetPace() => 0;

    public virtual string GetSummary()
    {
        return $"{_date} {this.GetType().Name} ({_duration} min): Distance: {GetDistance():0.0} km, Speed: {GetSpeed():0.0} kph, Pace: {GetPace():0.00} min per km";
    }
}

public class Running : Activity
{
    private double _distance;

    public Running(string date, double duration, double distance)
        : base(date, duration)
    {
        _distance = distance;
    }

    public override double GetDistance() => _distance;
    public override double GetSpeed() => (_distance / GetDuration()) * 60;
    public override double GetPace() => GetDuration() / _distance;
}

public class Cycling : Activity
{
    private double _speed;

    public Cycling(string date, double duration, double speed)
        : base(date, duration)
    {
        _speed = speed;
    }

    public override double GetSpeed() => _speed;
    public override double GetDistance() => _speed * (GetDuration() / 60);
    public override double GetPace() => 60 / _speed;
}

public class Swimming : Activity
{
    private int _laps;

    public Swimming(string date, double duration, int laps)
        : base(date, duration)
    {
        _laps = laps;
    }

    public override double GetDistance() => _laps * 50 / 1000.0;
    public override double GetSpeed() => GetDistance() / GetDuration() * 60;
    public override double GetPace() => GetDuration() / GetDistance();
}

class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running("03 Nov 2022", 30, 4.8),        
            new Cycling("10 Nov 2022", 45, 20),         
            new Swimming("30 Nov 2022", 40, 30)        
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
