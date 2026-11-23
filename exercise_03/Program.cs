\using System;
using System.Collections.Generic;
using System.Linq;
using AddisEats.Catalog;

// ==========================================
// Exercise 1: DeliveryFee with switch expression
// ==========================================
decimal DeliveryFee(string area, bool isVip = false)
{
    if (isVip)
        return 0m;

    return area.Trim().ToLowerInvariant() switch
    {
        "bole" => 120m,
        "kazanchis" => 100m,
        "piassa" => 110m,
        "mexico" => 90m,
        _ => 150m
    };
}

Console.WriteLine("=== Exercise 1: DeliveryFee ===");
Console.WriteLine($"Positional (\"Bole\", false): {DeliveryFee("Bole", false)} ETB");
Console.WriteLine($"Named arg (area: \"Kazanchis\", isVip: true): {DeliveryFee(area: "Kazanchis", isVip: true)} ETB");
Console.WriteLine($"Default isVip (\"Piassa\"): {DeliveryFee("Piassa")} ETB");

// ==========================================
// Exercise 2: Overloading Describe
// ==========================================
void Describe(int value) => Console.WriteLine($"Overload chosen: int -> {value}");
void Describe(string value) => Console.WriteLine($"Overload chosen: string -> {value}");
void Describe(decimal value) => Console.WriteLine($"Overload chosen: decimal -> {value} ETB");

// Fourth overload differing ONLY by return type (uncommenting causes a compile error):
// string Describe(int value) => value.ToString();
// Compiler error in C#:
// error CS0111: Type '...' already defines a member called 'Describe' with the same parameter types

Console.WriteLine("\n=== Exercise 2: Describe Overloads ===");
Describe(42);
Describe("Doro Wat");
Describe(350.50m);

// ==========================================
// Exercise 3: Restaurant (init Id, settable Name, computed DisplayName)
// ==========================================
Console.WriteLine("\n=== Exercise 3: Restaurant ===");
var restaurant = new Restaurant("Bole")
{
    Id = 101,
    Name = "Habesha Kitchen"
};
Console.WriteLine($"Before rename: Id={restaurant.Id}, Name={restaurant.Name}, DisplayName={restaurant.DisplayName}");

restaurant.Name = "Addis Delight";
Console.WriteLine($"After rename:  Id={restaurant.Id}, Name={restaurant.Name}, DisplayName={restaurant.DisplayName}");
// Note: restaurant.Id = 202; -> init-only, so reassigning outside object initializer is a compile error (CS8852).

// ==========================================
// Exercise 4: record vs class equality
// ==========================================
Console.WriteLine("\n=== Exercise 4: Customer Record vs Class ===");
var rec1 = new CustomerRecord("Selamawit", "+251911000001");
var rec2 = new CustomerRecord("Selamawit", "+251911000001");
Console.WriteLine($"Record equality (rec1 == rec2): {rec1 == rec2}");

var cls1 = new CustomerClass { Name = "Selamawit", PhoneNumber = "+251911000001" };
var cls2 = new CustomerClass { Name = "Selamawit", PhoneNumber = "+251911000001" };
Console.WriteLine($"Class equality  (cls1 == cls2): {cls1 == cls2}");
Console.WriteLine("One-line difference: Records use value-based equality on their properties, whereas classes default to reference equality.");


Console.WriteLine("\n=== Exercise 5: LINQ Chains ===");
var orders = new List<Order>
{
    new(1, "Bole", "Abebe", 650m, DateTime.UtcNow.AddDays(-1)),
    new(2, "Bole", "Makeda", 420m, DateTime.UtcNow.AddDays(-2)),
    new(3, "Kazanchis", "Dawit", 800m, DateTime.UtcNow.AddDays(-3)),
    new(4, "Kazanchis", "Hanna", 300m, DateTime.UtcNow.AddDays(-4)),
    new(5, "Bole", "Yonas", 1200m, DateTime.UtcNow.AddDays(-5)),
    new(6, "Piassa", "Tigist", 550m, DateTime.UtcNow.AddDays(-6)),
    new(7, "Bole", "Bethel", 900m, DateTime.UtcNow.AddDays(-7)),
    new(8, "Kazanchis", "Samuel", 450m, DateTime.UtcNow.AddDays(-8)),
    new(9, "Bole", "Rahel", 750m, DateTime.UtcNow.AddDays(-9)),
    new(10, "Piassa", "Kaleb", 600m, DateTime.UtcNow.AddDays(-10)),
    new(11, "Bole", "Eyerusalem", 510m, DateTime.UtcNow.AddDays(-11)),
    new(12, "Kazanchis", "Nahom", 950m, DateTime.UtcNow.AddDays(-12)),
    new(13, "Bole", "Liya", 1100m, DateTime.UtcNow.AddDays(-13)),
    new(14, "Piassa", "Bereket", 400m, DateTime.UtcNow.AddDays(-14)),
    new(15, "Bole", "Mulu", 2000m, DateTime.UtcNow.AddDays(-15)),
    new(16, "Kazanchis", "Saron", 700m, DateTime.UtcNow.AddDays(-16)),
    new(17, "Bole", "Abel", 530m, DateTime.UtcNow.AddDays(-17)),
    new(18, "Piassa", "Frehiwot", 820m, DateTime.UtcNow.AddDays(-18)),
    new(19, "Bole", "Henok", 640m, DateTime.UtcNow.AddDays(-19)),
    new(20, "Kazanchis", "Meron", 590m, DateTime.UtcNow.AddDays(-20)),
    new(21, "Bole", "Zemenu", 780m, DateTime.UtcNow.AddDays(-21)),
    new(22, "Piassa", "Kidist", 910m, DateTime.UtcNow.AddDays(-22)),
    new(23, "Bole", "Biniyam", 1050m, DateTime.UtcNow.AddDays(-23)),
    new(24, "Kazanchis", "Tsion", 670m, DateTime.UtcNow.AddDays(-24)),
    new(25, "Bole", "Eyob", 490m, DateTime.UtcNow.AddDays(-25))
};

// 1) Orders in Bole over 500 ETB
var boleOver500 = orders
    .Where(o => o.Area.Equals("Bole", StringComparison.OrdinalIgnoreCase) && o.AmountEtb > 500m)
    .ToList();
Console.WriteLine($"Bole orders > 500 ETB count: {boleOver500.Count}");

// 2) Every customer name sorted alphabetically
var sortedCustomers = orders
    .Select(o => o.CustomerName)
    .Distinct()
    .OrderBy(name => name)
    .ToList();
Console.WriteLine("Sorted customers: " + string.Join(", ", sortedCustomers));



var kazanchisRevenue = orders
    .Where(o => o.Area.Equals("Kazanchis", StringComparison.OrdinalIgnoreCase))
    .Sum(o => o.AmountEtb);
Console.WriteLine($"Kazanchis total revenue: {kazanchisRevenue} ETB");

// 4) Page 3 at 10 per page, newest first (skips first 20 items, takes next 10)
var page3NewestFirst = orders
    .OrderByDescending(o => o.CreatedAt)
    .Skip((3 - 1) * 10)
    .Take(10)
    .ToList();
Console.WriteLine("Page 3 (newest first) order IDs: " + string.Join(", ", page3NewestFirst.Select(o => o.Id)));


Console.WriteLine("\n=== Exercise 6: IDiscountPolicy ===");
decimal subtotal = 500m;
Console.WriteLine($"10% off 500 ETB:      {DiscountCalculator.ApplyPolicy(subtotal, new TenPercentDiscountPolicy())} ETB");
Console.WriteLine($"50 ETB off 500 ETB:   {DiscountCalculator.ApplyPolicy(subtotal, new FiftyBirrDiscountPolicy())} ETB");
Console.WriteLine($"100 ETB off >400 ETB: {DiscountCalculator.ApplyPolicy(subtotal, new TieredBirrDiscountPolicy(400m, 100m))} ETB");


Console.WriteLine("\n=== Exercise 7: try / catch / finally ===");
PaymentValidator.ValidatePositiveAmountWithAudit(250m);
PaymentValidator.ValidatePositiveAmountWithAudit(0m);
PaymentValidator.ValidatePositiveAmountWithAudit(-50m);