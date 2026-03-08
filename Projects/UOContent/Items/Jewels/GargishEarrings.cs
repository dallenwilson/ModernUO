using ModernUO.Serialization;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class GargishEarrings : BaseArmor
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
        public GargishEarrings()
            : base(0x4213)
        {
            Layer = Layer.Earrings;
        }
    }
}
