using System;
using Server.Items;

namespace Server.Engines.BulkOrders
{
    public enum BulkMaterialType
    {
        None,
        Rusty,
        OldCopper,
        DullCopper,
        ShadowIron,
        Copper,
        Bronze,
        Gold,
        Rose,
        Agapite,
        Bloodrock,
        Silver,
        Verite,
        Valorite,
        Mytheril,
        Blackrock,
        Spined,
        Horned,
        Barbed
    }

    public enum BulkGenericType
    {
        Iron,
        Cloth,
        Leather
    }

    public static class BGTClassifier
    {
        public static BulkGenericType Classify(BODType deedType, Type itemType)
        {
            if (deedType != BODType.Tailor)
            {
                return BulkGenericType.Iron;
            }

            return itemType?.IsSubclassOf(typeof(BaseArmor)) != false || itemType.IsSubclassOf(typeof(BaseShoes))
                ? BulkGenericType.Leather
                : BulkGenericType.Cloth;
        }
    }
}
