namespace Engine
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }
        public int Price { get; set; }
        public bool CanOnlyHaveOne { get; set; }
        public bool VendorWants { get; set; }

        public Item(int id, string name, string namePlural, int price, bool canOnlyHaveOne, bool vendorWants)
        {
            ID = id;
            Name = name;
            NamePlural = namePlural;
            Price = price;
            CanOnlyHaveOne = canOnlyHaveOne;
            VendorWants = vendorWants;
        }
    }
}
