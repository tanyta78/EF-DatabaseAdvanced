namespace PizzaCalories
{
    using System;

    public  class Topping
   {
       private string type;
       private double weight;

        public Topping(string type, double weight)
        {
            this.Type = type;
            this.Weight = weight;
        }

        public string Type
       {
           get { return this.type; }
          private  set
            {

                if (value.ToLower() != "meat" && value.ToLower() != "veggies" && value.ToLower() != "cheese" && value.ToLower() != "sauce")
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                this.type = value;
            }
       }

       public double Weight
       {
           get { return this.weight; }
          private set
           {
               if (value > 50 || value < 1)
               {
                   throw new ArgumentException($"{this.Type} weight should be in the range [1..50].");
               }
                this.weight = value;
           }
       }

       public double GetCalories()
       {
           var cal = 2 * this.Weight * this.GetTypeModifier();
           return cal;
       }

       private double GetTypeModifier()
       {
           var modifier = 1.0;
           switch (this.type.ToLower())
           {
               case "meat":
                   modifier = 1.2;
                   break;
               case "veggies":
                   modifier = 0.8;
                   break;
               case "cheese":
                   modifier = 1.1;
                   break;
               case "sauce":
                   modifier = 0.9;
                   break;
            }
           return modifier;
        }
   }
}
