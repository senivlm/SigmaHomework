using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaHomework.Products
{
    public enum MeatCategory
    {
        TopGrade,
        FirstGrade,
        SecondGrade
    }
    public enum MeatType
    {
        Mutton,
        Veal,
        Pork,
        Chicken
    }

    public class Meat : Product
    {
        public MeatCategory Category { get; }
        public MeatType Type { get; }

        public override void ChangePrice(decimal percent)
        {
            var categoryPercent = Category switch
            {
                MeatCategory.TopGrade => 0,
                MeatCategory.FirstGrade => 3,
                MeatCategory.SecondGrade => 5,
                _ => throw new NotImplementedException()
            };
            base.ChangePrice(percent + categoryPercent);
        }
        public override bool Equals(object? otherProduct)
        {
            return base.Equals(otherProduct) &&
                Category.Equals(((Meat)otherProduct).Category) &&
                Type.Equals(((Meat)otherProduct).Type);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() ^
                Category.GetHashCode() ^
                Type.GetHashCode();
        }
        public override string ToString()
        {
            return base.ToString() +
                $"Product category: {Category}\n" +
                $"Product type: {Type}\n";
        }

        public Meat(MeatCategory category, MeatType type, string name, decimal price, decimal weight) : base(name, price, weight)
        {
            Category = category;
            Type = type;
        }
    }
}
