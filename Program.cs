// See https://aka.ms/new-console-template for more information
using GRHWConsole;

if (args.Length == 0)
{
    // Normally this and other messages would be stored elsewhere
    // to make them easy to modify without having to recompile
    Console.WriteLine("You must provide a file path parameter");
}
else
{
    if (File.Exists(args[0]))
    {
        Console.WriteLine($"Reading data from {args[0]}");

        try
        {
            string[] lines = await File.ReadAllLinesAsync(args[0]);

            var data =
                (from line in
                     from line in lines
                     select line.Split(',')
                              .ToArray<string>()
                 select new SomeData()
                 {
                     LastName = line[(int)ArrayElement.LastName],
                     FirstName = line[(int)ArrayElement.FirstName],
                     Email = line[(int)ArrayElement.Email],
                     FavoriteColor = line[(int)ArrayElement.FavoriteColor],
                     DateOfBirth = DateTime.Parse(line[(int)ArrayElement.DoB])
                 })
                 .ToList<SomeData>();
            var check = data;
            if (args.Length > 1)
            {
                Console.WriteLine($"Sort by {args[1]}");
                string sortBy = args[1];
                switch(sortBy.ToLower())
                {
                    case "color":
                        data = data.OrderBy(d => d.FavoriteColor)
                            .ThenBy(d => d.LastName)
                            .ToList<SomeData>();
                        break;
                    case "birthdate":
                        data = data.OrderBy(d => d.DateOfBirth)
                        .ToList<SomeData>();
                        break;
                    case "name":
                        data = data.OrderByDescending(d => d.LastName)
                        .ToList<SomeData>();
                        break;
                    default:
                        Console.WriteLine("Invalid sort parameter provided");
                        break;
             
                }

            }
            foreach (var line in data)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("Done");
        }
        // for sake of brevity, I am only catching generic exception
        catch (Exception)
        {

            throw;
        }


    }
    else
    {
        Console.WriteLine("The file specified does not exist");
    }

}



