//using _131241;
using System;
using System.Collections.Generic;
using System.Linq;



public class MetalEvent
{
    private DateTime date;
    private double coefficient;

    public MetalEvent(string eventName, string band, DateTime date, double price, double coefficient)
    {
        EventName = eventName;
        Band = band;
        this.date = date;
        Price = price;
        this.coefficient = coefficient;
    }

				public string EventName { get; set; }
				public string Band { get; set; }
				public string Date { get; set; }
				public double Price { get; set; }
				public object HeavyCoefficient { get; set; }

}
internal class Program
{
	public static void Main(string[] args)
	{
		Dictionary<string, double> heavyCoefficient = new Dictionary<string, double
		>();
		Dictionary<string, MetalEvent> events = new Dictionary<string, MetalEvent>
			();
		

		string command = "";
		do
		{
			string[] commandData = command.Split(' ').ToArray();
			string commandDataFull = "";
			switch (commandData[0])
			{
				case "AddMetal":
					commandDataFull = string.Join(" ", commandData.Skip(1).ToArray());

					string[] bandData = commandDataFull.Split('|');
					

					if (!heavyCoefficient.ContainsKey(bandData[0]))
					{
						heavyCoefficient.Add(bandData[0], double.Parse(bandData[1])
						);
					}


					break;



				case "AddEvent":
					commandDataFull = string.Join(" ", commandData.Skip(1).ToArray());

					string[] eventData = commandDataFull.Split(' ');
					string eventName = eventData[1];
					string band = eventData[0];
					string dateString = eventData[2];
					string[] dateComponents = dateString.Split('/');
					int day = int.Parse(dateComponents[0]); int month = int.Parse(dateComponents[1]); int year = int.Parse(dateComponents[2]);
					DateTime date = new DateTime(year, month, day); double price = double.Parse(eventData[3]); double coefficient = double.MinValue; coefficient = heavyCoefficient[band]; events.Add(band, new MetalEvent(
					eventName, band, date, price, coefficient
				));


					break;
				case "PrintMetalByName":
					var heavyByName = heavyCoefficient.OrderBy(item => item.Value)
						.ToDictionary(item => item.Key, item => item.Value);
					foreach (var item in heavyByName)
					{
						Console.WriteLine($"{item.Key}|{item.Value}");
					}
					break;
				case "PrintMetalByHeaviness":

					var heavyByHeaviness = heavyCoefficient.OrderBy(item => item.Key)
						.ToDictionary(item => item.Key, item => item.Value);
					foreach (var item in heavyByHeaviness)
					{
						Console.WriteLine($"{item.Key}|{item.Value}");
					}
					break;
				case "PrintMetalEvents":

					var heavyEvents = events
						.OrderBy(item => item.Value)
						.ThenByDescending(item => item.Value.HeavyCoefficient)
						.ThenBy(item => item.Value.Band)
						.ThenBy(item => item.Value.Price);
					foreach (var item in heavyEvents)
					{
                        Console.WriteLine($"{item.Value.EventName}|{item.Value.Band}|{item.Value.Date:dd/MM/yy}|{ item.Value.Price}");
						
					}
					break;



				case "LetsRock":
					var groupedDictionary = events
						.GroupBy(item => item.Value.Date)
						.Select(group => group
							.OrderByDescending(item => item.Value.HeavyCoefficient)
							.ThenBy(item => item.Value.Price)
							.ThenBy(item => item.Value.Band)
							.First()
						)
						.OrderBy(item => item.Key)
						.ToDictionary(item => item.Key, item => item.Value);

					foreach (var item in groupedDictionary)
					{

						Console.WriteLine($"{item.Value.EventName}|{item.Value.Band}|{item.Value.Date:dd/MM/yy}|{item.Value.Price}");
					}
					break;
				
			}
			//skip invalid commands break;

			command = Console.ReadLine();
		} while (command != "END");

	}
}
