using System.Runtime.Serialization;
using Task14.Enums;
using Task14.GoodsModels.GoodsInterfaces;

namespace Task14.GoodsModels.GoodsAbstractions
{
    [KnownType(typeof(Table))]
    [KnownType(typeof(Chair))]
    [DataContract]
    public abstract class FurnitureProductBase : IndustrialProductBase, IFurnitureProduct
    {
        #region Fields
        private int _length;
        private int _width;
        private int _height;
        #endregion

        #region Properties
        [DataMember(Name = "Length")]
        public int Length
        {
            get => _length;
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Length can not be negative or zero!");
                }
                _length = value;
            }
        }
        [DataMember(Name = "Width")]
        public int Width
        {
            get => _width;
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Width can not be negative or zero!");
                }
                _width = value;
            }
        }
        [DataMember(Name = "Height")]
        public int Height
        {
            get => _height;
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Height can not be negative or zero!");
                }
                _height = value;
            }
        }
        [DataMember(Name = "Material")]
        public Material Material { get; protected set; }
        #endregion

        #region Constructors
        public FurnitureProductBase(string name, decimal price, int length, int width, int height, Material material) : base(name, price)
        {
            Length = length;
            Width = width;
            Height = height;
            Material = material;
        }
        #endregion

        #region Methods
        public override bool Equals(object? otherProduct)
        {
            if (otherProduct != null && otherProduct is FurnitureProductBase product)
            {
                return base.Equals(product) &&
                        Length.Equals(product.Length) &&
                        Width.Equals(product.Width) &&
                        Height.Equals(product.Height) &&
                        Material.Equals(product.Material);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() ^
                Length.GetHashCode() ^
                Width.GetHashCode() ^
                Height.GetHashCode() ^
                Material.GetHashCode();
        }
        public override string ToString()
        {
            return base.ToString() +
                $"Length: {Length}\n" +
                $"Width: {Width}\n" +
                $"Height: {Height}\n" +
                $"Material: {Material}\n";
        }
        #endregion
    }
}
