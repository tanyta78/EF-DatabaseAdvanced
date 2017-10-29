namespace Animals
{
    public class Kitten : Animal
    {
        public Kitten(string name, int age, string gender) : base(name, age, gender)
        {
        }

        public override string ProduceSound()
        {
            return "Meow";
        }
    }
}
