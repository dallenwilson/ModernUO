using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class BlackrockElemental : BaseCreature
    {
        [Constructible]
        public BlackrockElemental(int oreAmount = 2) : base(AIType.AI_Melee)
        {
            Body = 14;
            Hue = CraftResources.GetHue(CraftResource.Blackrock);
            BaseSoundID = 268;

            SetStr(236, 255);
            SetDex(136, 145);
            SetInt(75, 92);

            SetHits(140, 155);

            SetDamage(29);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Energy, 20);

            SetResistance(ResistanceType.Physical, 60, 80);
            SetResistance(ResistanceType.Fire, 60, 80);
            SetResistance(ResistanceType.Cold, 60, 80);
            SetResistance(ResistanceType.Poison, 60, 80);
            SetResistance(ResistanceType.Energy, 60, 80);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 60;

            PackItem(new BlackrockOre(oreAmount)
            {
                ItemID = 0x19B9
            });
        }

        public override string CorpseName => "an ore elemental corpse";
        public override string DefaultName => "a blackrock elemental";

        public override bool AutoDispel => true;
        public override bool BleedImmune => true;
        public override int TreasureMapLevel => 3;

        private static MonsterAbility[] _abilities = { MonsterAbilities.DeathExplosion };
        public override MonsterAbility[] GetMonsterAbilities() => _abilities;

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature bc && (bc.Controlled || bc.BardTarget == this))
            {
                damage /= 2; // 50% melee damage
            }
            else
            {
                damage /= 2; // 50% melee damage
            }
        }

        public override void AlterDamageScalarFrom(Mobile caster, ref double scalar)
        {
            scalar = 2.0;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Gems, 6);
        }
    }
}
