using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class MytherilElemental : BaseCreature
    {
        [Constructible]
        public MytherilElemental(int oreAmount = 2) : base(AIType.AI_Melee)
        {
            Body = 14;
            Hue = CraftResources.GetHue(CraftResource.Mytheril);
            BaseSoundID = 268;

            SetStr(236, 255);
            SetDex(136, 145);
            SetInt(75, 92);

            SetHits(140, 155);

            SetDamage(29);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 60, 80);
            SetResistance(ResistanceType.Fire, 20, 45);
            SetResistance(ResistanceType.Cold, 50, 65);
            SetResistance(ResistanceType.Poison, 20, 45);
            SetResistance(ResistanceType.Energy, 50, 65);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 60;

            PackItem(new MytherilOre(oreAmount)
            {
                ItemID = 0x19B9
            });
        }

        public override string CorpseName => "an ore elemental corpse";
        public override string DefaultName => "a mytheril elemental";

        public override bool AutoDispel => true;
        public override bool BleedImmune => true;
        public override int TreasureMapLevel => 2;

        private static MonsterAbility[] _abilities = { MonsterAbilities.DeathExplosion };
        public override MonsterAbility[] GetMonsterAbilities() => _abilities;

        public override void CheckReflect(Mobile caster, ref bool reflect)
        {
            reflect = true; // Every spell is reflected back to the caster
        }

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature bc && (bc.Controlled || bc.BardTarget == this))
            {
                damage /= 2; // Immune to pets and provoked creatures
            }
            else
            {
                damage /= 2; // 50% melee damage
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems, 5);
        }
    }
}
