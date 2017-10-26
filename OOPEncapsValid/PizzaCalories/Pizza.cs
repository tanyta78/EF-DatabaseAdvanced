namespace PizzaCalories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Pizza
   {
       private string name;
       private List<Topping> toppings;
       private Dough dough;
       private int numberOfToppings;

       public Pizza(string name)
       {
           this.Name = name;
           this.toppings = new List<Topping>();
          
       }

        public string Name
       {
           get { return this.name; }
           set
           {
               if (value.Length < 1 || value.Length > 15)
               {
                   throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
               }
               this.name = value;
           }
       }

       public Dough Dough
       {
           set { this.dough = value; }
       }

       public int NumberOfToppings
       {
           get { return this.Toppings.Count; }
           set {
               if (value < 0 || value > 10)
               {
                   throw new ArgumentException("Number of toppings should be in range [0..10].");
               }

               this.numberOfToppings = value;
           }
       }

       public List<Topping> Toppings { get => toppings; set => toppings = value; }


       public void AddTopping(Topping top)
       {
            
           this.toppings.Add(top);
       }

       public double GetCalories()
       {
           return this.dough.GetCalories() + this.toppings.Sum(t => t.GetCalories());
       }
    }
}
