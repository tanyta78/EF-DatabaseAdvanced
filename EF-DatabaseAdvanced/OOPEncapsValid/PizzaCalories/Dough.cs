namespace PizzaCalories
{
    using System;

    public class Dough
   {
       private string flourType;
       private string bakingTechnique;
       private double weight;

       public Dough(string flourType, string bakingTechnique, double weight)
       {
           this.FlourType = flourType;
           this.BakingTechnique = bakingTechnique;
           this.Weight = weight;
       }

       public string BakingTechnique
       {
           get { return this.bakingTechnique; }
           private set
           {
               if (value.ToLower() != "crispy" && value.ToLower() != "chewy" && value.ToLower() != "homemade")
               {
                   throw new ArgumentException("Invalid type of dough.");
               }

                this.bakingTechnique = value;
           }
       }

       public string FlourType
       {
           get { return this.flourType; }
           private set
           {
               if (value.ToLower()!="white" && value.ToLower()!= "wholegrain")
               {
                   throw new ArgumentException("Invalid type of dough.");
               }
                this.flourType = value;
           }
       }

       public double Weight
       {
           get { return this.weight; }
           private set
           {
               if (value>200 || value<1)
               {
                   throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                this.weight = value;
           }
       }

       public double GetCalories()
       {
           var cal = 2*this.Weight* this.GetFlourModifier()* this.GetTechniqueModifier();
           return cal;
       }

       private double GetTechniqueModifier()
       {
           var modifier = 1.0;
           switch (this.bakingTechnique.ToLower())
           {
               case "crispy":
                   modifier = 0.9;
                   break;
               case "chewy":
                   modifier = 1.1;
                   break;
               case "homemade":
                   modifier = 1.0;
                   break;

            }
           return modifier;
        }

       private double GetFlourModifier()
       {
           var modifier = 1.0;
           switch (this.flourType.ToLower())
           {
                case "white":
                    modifier = 1.5;
                    break;
                case "wholegrain":
                    modifier = 1.0;
                    break;
            }
           return modifier;
       }
   }
}
