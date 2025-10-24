namespace ConsoleApp1
{
    public interface IPizzaBuilder
    {
        void BuildDough();
        void BuildIngredients();
        Pizza GetResult();

    }

    public class Pizza
    {
        private int size;
        public int Size { get => size; set => size = value; }
        private string doughType;
        public string DoughType { get => doughType; set => doughType = value; }
        private List<string> ingredients = new();
        public void Add(string ingredient) => ingredients.Add(ingredient);
        public void Show()
        {
            if(ingredients.Count == 0)
            {
                Console.WriteLine("Your pizza is empty!");
                return;
            }

            Console.Write($"Your pizza: \nSize: {size}cm\nDough type: {doughType}\nIngredients: ");
            for(int i = 0; i < ingredients.Count; i++)
            {
                if(i == ingredients.Count-1)
                    Console.Write($"{ingredients[i]}");
                else
                    Console.Write($"{ingredients[i]}, ");
            }
            Console.Write("\n");
        }
    }
    public class MargharitaPizzaBuilder : IPizzaBuilder
    {
        private Pizza pizza = new();

        public void BuildDough()
        {
            pizza.Size = 32;
            pizza.DoughType = "Thin crust";
        }
        public void BuildIngredients()
        {
            pizza.Add("Crushed San Marzano tomato sauce");
            pizza.Add("Fresh mozzarella");
            pizza.Add("Basil");
            pizza.Add("Drizzle of olive oil");
        }
        public Pizza GetResult()
        {
            return pizza;
        }

    }

    public class HawaiianPizzaBuilder : IPizzaBuilder
    {
        private Pizza pizza = new();

        public void BuildDough()
        {
            pizza.Size = 32;
            pizza.DoughType = "Thin crust";
        }
        public void BuildIngredients()
        {
            pizza.Add("Crushed San Marzano tomato sauce");
            pizza.Add("Smoky honey ham");
            pizza.Add("Pineapple");
            pizza.Add("Mozarella");
            pizza.Add("Bacon");
        }
        public Pizza GetResult()
        {
            return pizza;
        }

    }

    public class DiavolaPizzaBuilder : IPizzaBuilder
    {
        private Pizza pizza = new();

        public void BuildDough()
        {
            pizza.Size = 32;
            pizza.DoughType = "Thin crust";
        }
        public void BuildIngredients()
        {
            pizza.Add("Crushed San Marzano tomato sauce");
            pizza.Add("Fior di latte cheese");
            pizza.Add("Salami picante");
            pizza.Add("Nduja");
            pizza.Add("Jalapeno peppers");
        }
        public Pizza GetResult()
        {
            return pizza;
        }

    }

    public class YourPizzaBuilder: IPizzaBuilder
    {
        private string[] IngredientsVege = ["Spinach", "Asparagus", "Zuccini", "piri-piri pepper", "onion", "pepperoni pepper", "garlic", "olives", "arugula", "corn", "tomato", "cucumber", "mushrooms" ];
        private string[] IngredientsCheese = ["Blue cheese", "Cheddar cheese", "Cottage cheese", "Feta cheese", "Goat cheese", "Mascarpone", "Melted cheese", "Smoked cheese", "Mozarella"];
        private string[] IngredientsMeat = ["Pepperoni", "Ham", "Sausage", "Bacon", "Beef", "Chicken", "Salami", "Chorizo", "Chicken", "Tuna", "Parma Ham"];

        private int currentIngredients = 0;
        private bool pizzaStopped = false;

        private Pizza pizza = new();
        public void BuildDough()
        {
            bool buildedDough = false;

            while (!buildedDough)
            {
                Console.Write("Your dough size:\n0. Stop making pizza!\n1. 24cm\n2. 32cm\n3. 45cm\n > ");
                try
                {
                    int ans = Convert.ToInt32(Console.ReadLine());
                    switch (ans)
                    {
                        case 0:
                            buildedDough = true;
                            pizzaStopped = true;
                            return;
                        case 1:
                            pizza.Size = 24;
                            buildedDough = true;
                            break;
                        case 2:
                            pizza.Size = 32;
                            buildedDough = true;
                            break;
                        case 3:
                            pizza.Size = 45;
                            buildedDough = true;
                            break;
                        default:
                            throw new Exception("You picked wrong sizing!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            buildedDough = false;

            while (!buildedDough)
            {
                Console.Write("Your dough type:\n0. Stop making pizza!\n1. Thin crust\n2. Thic crust\n3. Cheese in edges\n > ");
                try
                {
                    int ans = Convert.ToInt32(Console.ReadLine());
                    switch (ans)
                    {
                        case 0:
                            buildedDough = true;
                            pizzaStopped = true;
                            return;
                        case 1:
                            pizza.DoughType = "Thin crust";
                            buildedDough = true;
                            break;
                        case 2:
                            pizza.DoughType = "Thic crust";
                            buildedDough = true;
                            break;
                        case 3:
                            pizza.DoughType = "Cheese in the edges";
                            buildedDough = true;
                            break;
                        default:
                            throw new Exception("You picked wrong sizing!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

        }

        private void BuildIngredient(string[] ingredients)
        {
            Console.WriteLine("0. Back");
            for(int i = 0; i < ingredients.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {ingredients[i]}");
            }
            Console.Write(" > ");

            int ans = Convert.ToInt32(Console.ReadLine())-1;
            if (ans < 0 || ans >= ingredients.Length)
            {
                currentIngredients -= 1;
                if (ans != -1)
                {
                    throw new Exception("Invalid ingredient!");
                }
                return;
            }
            
                

            pizza.Add(ingredients[ans]);
        }

        public void BuildIngredients()
        {
            if (pizzaStopped)
                return;


            Console.WriteLine("Now pick up ingredients from the list (max 6, to END press ENTER)");
            try
            {
                
                while (currentIngredients < 6)
                {
                    Console.Write("Select your ingredients!\nView list of (to END press ENTER):\n1. Vegetarian ingredients\n2. Cheese ingredients\n3. Meat ingredients\n > ");
                    string ans = Console.ReadLine();
                    if(ans.Trim() == "")
                    {
                        return;
                    }
                    int opt = Convert.ToInt32(ans);
                    switch (opt)
                    {
                        case 1:
                            Console.WriteLine("Pick Vegetarian ingredient!");
                            BuildIngredient(IngredientsVege);
                            break;
                        case 2:
                            Console.WriteLine("Pick Cheese ingredient!");
                            BuildIngredient(IngredientsCheese);
                            break;
                        case 3:
                            Console.WriteLine("Pick Meat ingredient!");
                            BuildIngredient(IngredientsMeat);
                            break;
                        default:
                            throw new Exception("You picked invalid ingredients list!");
                    }

                    currentIngredients += 1;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        public Pizza GetResult()
        {
            return this.pizza;
        }
    }

    public class Director
    {
        private IPizzaBuilder pizzaBuilder;
        public Director(IPizzaBuilder pizzaBuilder)
        {
            this.pizzaBuilder = pizzaBuilder;
        }

        public void Construct()
        {
            pizzaBuilder.BuildDough();
            pizzaBuilder.BuildIngredients();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            IPizzaBuilder pizzaMargharita = new MargharitaPizzaBuilder();
            Director director = new(pizzaMargharita);
            director.Construct();

            Pizza p1 = pizzaMargharita.GetResult();
            p1.Show();

            IPizzaBuilder pizzaHawaii = new HawaiianPizzaBuilder();
            director = new(pizzaHawaii);
            director.Construct();

            Pizza p2 = pizzaHawaii.GetResult();
            p2.Show();

            IPizzaBuilder pizzaDiavola = new DiavolaPizzaBuilder();
            director = new(pizzaDiavola);
            director.Construct();

            Pizza p3 = pizzaDiavola.GetResult();
            p3.Show();

            IPizzaBuilder MyPizza = new YourPizzaBuilder();
            director = new(MyPizza);
            director.Construct();

            Pizza p4 = MyPizza.GetResult();
            p4.Show();
        }
    }
}
