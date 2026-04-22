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
            else if (choice.Equals("b", StringComparison.OrdinalIgnoreCase))
            {
                View();
            }
            else if (choice.Equals("c", StringComparison.OrdinalIgnoreCase))
            {
                Delete();
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

    static void View()
    {
        if (!File.Exists("passwords.json"))
        {
            File.WriteAllText("passwords.json", "[]");
        }

        var json = File.ReadAllText("passwords.json");
        
        
        Console.WriteLine(json);
    }

    static void Delete()
    {
        if (!File.Exists("passwords.json"))
        {
            File.WriteAllText("passwords.json", "[]");
        }

        var json = File.ReadAllText("passwords.json");
        var passwords = string.IsNullOrWhiteSpace(json) ? new List<Passwords>() : JsonSerializer.Deserialize<List<Passwords>>(json) ?? new List<Passwords>();

        Console.Write("Enter email to delete: ");
        var deleteEmail = Console.ReadLine();

        passwords = passwords.Where(email => email.email != deleteEmail).ToList();

        File.WriteAllText("passwords.json", JsonSerializer.Serialize(passwords));
    }
}

record Passwords(string? email, string? password);


