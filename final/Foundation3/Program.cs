using System;
using System.ComponentModel;
using System.IO.Pipes;

// class Program
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Hello Foundation3 World!");
//     }
// }
public class Address
{
    private string _street;
    private string _city;

    private string _state;
    private string _country;
    public Address(string street, string city, string state, string country)
    {
        _street = street;
        _city = city;
        _state = state;
        _country = country;

    }
    public string GetAddress()
    {
        return $"{_street}, {_city}, {_state}, {_country}";
    }
}
public class Event
{
    private string _title;
    private string _description;
    private string _date;
    private string _time;
    private Address _address;

    public Event(string title, string description, string date, string time, Address address)
    {
        _title = title;
        _description = description;
        _date = date;
        _time = time;
        _address = address;
    }
    public virtual string GetStandardDetails()
    {
        return $"Title: {_title}\nDescription: {_description}\nDate: {_date}\nTime: {_time}\nAddress: {_address.GetAddress()}";
    }
    public virtual string GetFullDetails()
    {
        return GetFullDetails();
    }
    public virtual string ShortGetDescription()
    {
        return $"Event Type: {this.GetType().Name}, Title: {_title}, Date: {_date}";
    }

}
public class Lecture : Event
{
    private string _speaker;
    private int _capacity;
    public Lecture(string title, string description, string date, string time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        _speaker = speaker;
        _capacity = capacity;
    }
    public override string GetFullDetails()
    {
        return base.GetStandardDetails() + $"\nType: Lecture\nSpeaker: {_speaker}\nCapacity: {_capacity}";

    }
}
public class Reception : Event
{
    private string _rsvpEmail;
    public Reception(string title, string description, string date, string time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        _rsvpEmail = rsvpEmail;
    }
    public override string GetFullDetails()
    {
        return base.GetstandardDetails() + $"\nType: Reception\nRSVP Email: { _rsvpEmail} ";
        
    }
}
public class OutdoorGathering : Event
{
    private string _weather;

    public OutdoorGathering(string title, string description, string date, string time, Address address, string weather)
        : base(title, description, date, time, address)
    {
        _weather = weather;
    }

    public override string GetFullDetails()
    {
        return base.GetStandardDetails() + $"\nType: Outdoor Gathering\nWeather Forecast: {_weather}";
    }
}


class Program
{
    static void Main()
    {
        Address lectureAddress = new Address("123 Main St", "New York", "NY", "USA");
        Event lecture = new Lecture("Tech Talk", "Latest in AI", "August 5", "6:00 PM", lectureAddress, "Dr. Smith", 100);

        Address receptionAddress = new Address("456 Oak Ave", "Los Angeles", "CA", "USA");
        Event reception = new Reception("Networking Night", "Meet and greet with industry pros", "September 1", "7:00 PM", receptionAddress, "rsvp@events.com");

        Address outdoorAddress = new Address("789 Park Blvd", "Seattle", "WA", "USA");
        Event outdoor = new OutdoorGathering("Summer Picnic", "Enjoy food and games outdoors!", "July 20", "1:00 PM", outdoorAddress, "Sunny");

        Event[] events = { lecture, reception, outdoor };

        foreach (Event ev in events)
        {
            Console.WriteLine("----- STANDARD DETAILS -----");
            Console.WriteLine(ev.GetStandardDetails());
            Console.WriteLine("\n----- FULL DETAILS -----");
            Console.WriteLine(ev.GetFullDetails());
            Console.WriteLine("\n----- SHORT DESCRIPTION -----");
            Console.WriteLine(ev.GetShortDescription());
            Console.WriteLine("\n==============================\n");
        }
    }
}
