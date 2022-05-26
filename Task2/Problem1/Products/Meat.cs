using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Products.ProductPriceChanges;

namespace Task2.Products
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
        #region Properties
        public MeatCategory Category { get; private set; }
        public MeatType Type { get; private set; }
        #endregion

        #region Constructors
        public Meat() : this("", 0.0m, 0.1m, default, default)
        {

        }
        public Meat(string name, decimal price, decimal weight, MeatCategory category, MeatType type) : base(name, price, weight)
        {
            Category = category;
            Type = type;
        }
        #endregion

        #region Methods
        public void Deconstruct(out string name, out decimal price, out decimal weight, out MeatCategory category, out MeatType type)
        {
            Deconstruct(out name, out price, out weight);
            category = Category;
            type = Type;
        }
        public override void ChangePrice(decimal percent)
        {
            var percentStandards = PriceReader.GetMeatPriceChanges();
            var categoryPercent = percentStandards.ContainsKey(Category) ? percentStandards[Category] : 0;
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
        public override void Parse(string stringToParse)
        {
            if (stringToParse == null || stringToParse.Split(' ').Length != 5)
            {
                throw new ArgumentException("Wrong string to parse!");
            }
            var splitedString = stringToParse.Split(' ');
            base.Parse(string.Join(' ', splitedString[0..3]));
            Category = (MeatCategory)Enum.Parse(typeof(MeatCategory), splitedString[3]);
            Type = (MeatType)Enum.Parse(typeof(MeatType), splitedString[4]);
        }
        #endregion
    }
}
