using MyFirstDotnetProgram.Models;

// namespace X
// using X
// u dont have to import just use the namespace


MenuItem item1 = new MenuItem(1, "Doro Wet", "Wet", 1000);

Console.WriteLine(item1.Name);

item1.Name = "Kik wet";
Console.WriteLine(item1.Name);


Console.WriteLine(item1.DerivedVAT);


List<MenuItem> menuItems = new List<MenuItem>{
     new MenuItem(1, "Burger", "Food", 10.5m),
    new MenuItem(2, "Pizza", "Food", 15.0m),
    new MenuItem(3, "Coke", "Drink", 2.5m),
    new MenuItem(4, "Water", "Drink", 1.0m),
    new MenuItem(5, "Salad", "Food", 8.0m),
    new MenuItem(6, "Fries", "Food", 3.5m),
    new MenuItem(7, "Ice Cream", "Dessert", 4.0m)

};



// in list the items r separated by ,
// List<x> nu = new List<x>{ item1 , item2}

foreach (MenuItem item in menuItems){
    Console.WriteLine(item.Name);
}

// LINQ queries
// in the list filtering methods

var foods = menuItems.Where(i => i.Category == "Food");


foreach (MenuItem item in foods)
{
    Console.WriteLine(item.Name);
}

// select all column vals
var prices = menuItems.Select(x => x.Price).ToList();


foreach (price in prices)
{
    Console.WriteLine(price);
}


