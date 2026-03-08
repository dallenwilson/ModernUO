using ModernUO.Serialization;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class GargishNecklace : BaseArmor
    {
        public override int RequiredRaces => Race.AllowGargoylesOnly;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;
        public override CraftResource DefaultResource => CraftResource.Iron;
        public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;

        public override int BasePhysicalResistance { get { return 1; } }
        public override int BaseFireResistance { get { return 2; } }
        public override int BaseColdResistance { get { return 2; } }
        public override int BasePoisonResistance { get { return 2; } }
        public override int BaseEnergyResistance { get { return 3; } }

        public override int InitMinHits { get { return 30; } }
        public override int InitMaxHits { get { return 40; } }

        [Constructible]
        public GargishNecklace()
            : base(0x4210)
        {
            Layer = Layer.Neck;
        }


        public GargishNecklace(int itemID)
            : base(itemID)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class GargishAmulet : GargishNecklace
    {
        [Constructible]
        public GargishAmulet()
            : base(0x4D0B)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class GargishStoneAmulet : GargishNecklace
    {
        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Stone; } }
        public override int AosStrReq { get { return 40; } }
        public override int OldStrReq { get { return 20; } }

        [Constructible]
        public GargishStoneAmulet()
            : base(0x4D0A)
        {
            this.Hue = 2500;
        }
    }
}
