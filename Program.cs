namespace OverseerOfSecrets;

using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome, what would you like to do?");
            Console.WriteLine("A. Add Password \n B. View Passwords \n C. Delete Passwords \n D. Quit");
            Console.WriteLine("Select one: ");
            string? choice = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(choice))
            {
                continue;
            }
            else if (choice.Equals("d", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
            else if (choice.Equals("a", StringComparison.OrdinalIgnoreCase))
            {
                Add();
            }
        }
    }

    static void Add()
    {
        Console.WriteLine("Username/Email: ");
        string? email = Console.ReadLine();
        Console.WriteLine("Password: ");
        string? password = Console.ReadLine();

        var newPassword = new Passwords(email, password);

        if (!File.Exists("passwords.json"))
        {
            File.WriteAllText("passwords.json", "[]");
        }

        var json = File.ReadAllText("passwords.json");
        
        var passwords = string.IsNullOrWhiteSpace(json) ? new List<Passwords>() : JsonSerializer.Deserialize<List<Passwords>>(json) ?? new List<Passwords>();

        passwords.Add(newPassword);

        File.WriteAllText("passwords.json", JsonSerializer.Serialize(passwords));
    }
}

record Passwords(string? email, string? password);

// users can view delete or add new one

// accepts input
// places email and password in record class
// store in json

// users can view it
// prints contents of json to terminal

// users can delete it
// delete contents of json

// Read existing list
// var json = File.ReadAllText("data.json");
// var people = JsonSerializer.Deserialize<List<Person>>(json) ?? new List<Person>();

// // Add new item
// people.Add(newPerson);

// // Write back
// File.WriteAllText("data.json", JsonSerializer.Serialize(people));

