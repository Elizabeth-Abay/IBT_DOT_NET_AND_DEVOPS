namespace MyFirstDotnetProgram.Models
{
   // in the namespace u shldnt have variables directly

    public class MenuItem
    {
        // the default prop of classes is internal

        // get , init , set -- value
        // get - allows a prop to be get
        // set - allows a prop to be set
        // init - allows a prop to be set only once
        const decimal VATValue = 0.15m; 
        public int Id { get; init; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }



         public decimal DerivedVAT => Price * VATValue;

        public MenuItem(int Id, string Name, string Category, decimal Price)
        {
            this.Id = Id;
            this.Name = Name;
            this.Category = Category;
            this.Price = Price;
        }
    }
}