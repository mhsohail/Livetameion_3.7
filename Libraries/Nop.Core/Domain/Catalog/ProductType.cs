namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a product type
    /// </summary>
    public enum ProductType
    {
        /// <summary>
        /// Simple
        /// </summary>
        SimpleProduct = 5,
        /// <summary>
        /// Grouped (product with variants)
        /// </summary>
        GroupedProduct = 10,
        Car = 15,
        Hotel = 20,
        Restaurant = 25,
        GroupDeal = 30,
        Food = 35
    }
}
