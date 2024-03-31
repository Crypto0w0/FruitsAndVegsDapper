using Dapper;
using DapperTest1;
using System.Data.SqlClient;

class Program
{
    static string connectionString = "Data Source=localhost;Database=FruitsAndVegetables;Integrated Security=false;User ID=root;Password=Alex228420;";

    static void Main()
    {
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection succeded");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Connection error: {e.Message}");
            }
        }
        
        int ans;
        Console.WriteLine("1 - Get all information");
        Console.WriteLine("2 - Get all names");
        Console.WriteLine("3 - Get all colours");
        Console.WriteLine("4 - Get Max calories");
        Console.WriteLine("5 - Get Min calories");
        Console.WriteLine("6 - Get average calories");
        Console.WriteLine("7 - Get amount of vegetables");
        Console.WriteLine("8 - Get amount of fruits");
        Console.WriteLine("9 - Get amount of fruits and vegetables of the defined colour");
        Console.WriteLine("10 - Get amount of fruits and vegetables of every colour");
        Console.WriteLine("11 - Get fruits and vegetables with lower calories than defined");
        Console.WriteLine("12 - Get fruits and vegetables with higher calories than defined");
        Console.WriteLine("13 - Get fruits and vegetables with calories in the defined diapasone");
        Console.WriteLine("14 - Get fruits and vegetables with red or yellow colour");
        Console.WriteLine("15 - Find a fruit or vegetable by id");
        Console.WriteLine("16 - Redact a fruit or vegetable");
        Console.WriteLine("17 - Delete a fruit or vegetable");
        
        ans = Convert.ToInt32(Console.ReadLine());
        if (ans == 1)
        {
            GetAllInfo();
        }
        else if (ans == 2)
        {
            GetAllNames();
        }
        else if (ans == 3)
        {
            GetAllColours();
        }
        else if (ans == 4)
        {
            GetMaxCal();
        }
        else if (ans == 5)
        {
            GetMinCal();
        }
        else if (ans == 6)
        {
            GetAvgCal();
        }
        else if (ans == 7)
        {
            VegetablesNum();
        }
        else if (ans == 8)
        {
            FruitsNum();
        }
        else if (ans == 9)
        {
            Console.WriteLine("Colour: ");
            string c = Console.ReadLine();
            FrAndVegNumWithCol(c);
        }
        else if (ans == 10)
        {
            FrAndVegEveryCol();
        }
        else if (ans == 11)
        {
            Console.WriteLine("Calories: ");
            int cal = Convert.ToInt32(Console.ReadLine());
            FrAndVegWithCaloriesLower(cal);
        }
        else if (ans == 12)
        {
            Console.WriteLine("Calories: ");
            int cal = Convert.ToInt32(Console.ReadLine());
            FrAndVegWithCaloriesHigher(cal);
        }
        else if (ans == 13)
        {
            Console.WriteLine("Min calories: ");
            int min = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Max calories: ");
            int max = Convert.ToInt32(Console.ReadLine());
            FrAndVegWithCaloriesInDiapasone(min, max);
        }
        else if (ans == 14)
        {
            FrAndVegWithRedOrYellColour();
        }
        else if (ans == 15)
        {
            Console.WriteLine("ID: ");
            int ID = Convert.ToInt32(Console.ReadLine());
            FindByID(ID);
        }
        else if (ans == 16)
        {
            Console.WriteLine("ID: ");
            int ID = Convert.ToInt32(Console.ReadLine());
            RedactByID(ID);
        }
        else if (ans == 17)
        {
            Console.WriteLine("ID: ");
            int ID = Convert.ToInt32(Console.ReadLine());
            DeleteByID(ID);
        }
        else Console.WriteLine("Unknown command");
    }

    static void GetAllInfo() //!Changed!
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM fruits_and_vegetables";
            IEnumerable<Product> products = connection.Query<Product>(query);
            foreach (var product in products)
            {
                Console.WriteLine(
                    $"id: {product.id}, name: {product.name}, type: {product.type}, colour: {product.colour}, calories: {product.calories}");
            }

        }
    }
    static void FindByID(int ID) //!Added!
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM fruits_and_vegetables";
            IEnumerable<Product> products = connection.Query<Product>(query);
            foreach (var product in products)
            {
                if (product.id == ID)
                {
                    Console.WriteLine("Name: " + product.name + " Type: " + product.type + " Colour: " + product.colour + " Calories: " + product.calories);
                }
            }
        }
    }
    static void RedactByID(int ID) //!Added!
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM fruits_and_vegetables";
            IEnumerable<Product> products = connection.Query<Product>(query);
            foreach (var product in products)
            {
                if (product.id == ID)
                {
                    product.name = Console.ReadLine();
                    product.colour = Console.ReadLine();
                    product.type = Console.ReadLine();
                    product.calories = Convert.ToInt32(Console.ReadLine());
                }
            }
        }
    }
    static void DeleteByID(int ID) //!Added!
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM fruits_and_vegetables";
            IEnumerable<Product> products = connection.Query<Product>(query);
            foreach (var product in products)
            {
                if (product.id == ID)
                {
                    products = products.Where(p => p.id != product.id);
                }
            }
        }
    }
    static void GetAllNames()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT name FROM fruits_and_vegetables";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader["name"]);
                }
                reader.Close();
            }
        }
    }
    static void GetAllColours()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT colour FROM fruits_and_vegetables";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader["colour"]);
                }
                reader.Close();
            }
        }
            
    }
    static void GetMaxCal()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT MAX(calories) FROM fruits_and_vegetables";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                Console.WriteLine(result);
            }
        }
    }
    static void GetMinCal()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT MIN(calories) FROM fruits_and_vegetables";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                Console.WriteLine(result);
            }
        }
    }
    static void GetAvgCal() //!Changed!
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT AVG(calories) FROM fruits_and_vegetables";
            IEnumerable<Product> products = connection.Query<Product>(query);
            double avg = 0;
            foreach (var product in products)
            {
                avg += product.calories;
            }
            Console.WriteLine(avg/products.Count());
        }
    }
    static void VegetablesNum()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM fruits_and_vegetables WHERE type = 'Vegetable'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                Console.WriteLine(result);
            }
        }
    }
    static void FruitsNum()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM fruits_and_vegetables WHERE type = 'Fruit'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                Console.WriteLine(result);
            }
        }
    }
    static void FrAndVegNumWithCol(string userColour)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT name FROM fruits_and_vegetables WHERE colour = @Colour";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Colour", userColour);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["name"]);
                }
            }
        }
    }
    static void FrAndVegEveryCol()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT colour, COUNT(*) AS count FROM fruits_and_vegetables GROUP BY colour";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Colour: {reader["colour"]}, amount: {reader["count"]}");
                }
            }
        }
    }
    static void FrAndVegWithCaloriesLower(int cal)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT name FROM fruits_and_vegetables WHERE calories < @Calories";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Calories", cal);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["name"]);
                    }
                }
            }
        }
    }
    static void FrAndVegWithCaloriesHigher(int cal)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT name FROM fruits_and_vegetables WHERE calories > @Calories";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Calories", cal);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["name"]);
                    }
                }
            }
        }
    }
    static void FrAndVegWithCaloriesInDiapasone(int min, int max)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT name FROM fruits_and_vegetables WHERE calories BETWEEN @MinCal AND @MaxCal";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MinCal", min);
                command.Parameters.AddWithValue("@MaxCal", max);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["name"]);
                    }
                }
            }
        }
    }
    static void FrAndVegWithRedOrYellColour()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT Name FROM fruits_and_vegetables WHERE colour = 'Red' OR colour = 'Yellow'";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader["name"]);
                }
            }
        }
    }
}