namespace Engine
{
    public class HealingPotion : Item
    {
        public int AmountToHeal { get; set; }

        public HealingPotion(int id, string name, string namePlural, int amountToHeal, int price, bool canOnlyHaveOne, bool vendorWants) : base(id, name, namePlural, price, canOnlyHaveOne, vendorWants)
        {
            AmountToHeal = amountToHeal;
        }
    }
}
