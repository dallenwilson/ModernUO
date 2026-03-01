using System;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Engines.Harvest
{
    public class Mining : HarvestSystem
    {
        private static Mining _system;

        private static readonly int[] _offsets =
        {
            -1, -1,
            -1, 0,
            -1, 1,
            0, -1,
            0, 1,
            1, -1,
            1, 0,
            1, 1
        };

        private static readonly int[] _mountainCaveTiles =
        {
            220, 231,
            236, 247,
            252, 236,
            268, 279,
            286, 297,
            321, 324,
            467, 474,
            476, 487,
            492, 495,
            543, 579,
            581, 621,
            // 1010, 1010, // This is missing from the files in newer clients
            1741, 1757,
            1771, 1790,
            1801, 1824,
            1831, 1854,
            1861, 1884,
            1981, 2004,
            2028, 2033,
            2100, 2105,

            0x3F39, 0x3F74,
            0x3F82, 0x3F8F,
            0x3F91, 0x3FCF
        };

        private static readonly int[] _mountainCaveStaticTiles =
        {
            0x053B, 0x054F,
        };

        private static readonly int[] _sandTiles =
        {
            22, 62,
            68, 75,

            286, 301,
            402, 402,
            424, 427,
            441, 465,
            642, 645,
            650, 657,
            821, 828,
            833, 836,
            845, 852,
            857, 860,
            951, 958,
            967, 970,

            1447, 1458,
            1611, 1618,
            1623, 1626,
            1635, 1642,
            1647, 1650
        };

        private Mining()
        {
            OreAndStone = new HarvestDefinition
            {
                BankWidth = 8,
                BankHeight = 8,
                MinTotal = 10,
                MaxTotal = 34,
                MinRespawn = TimeSpan.FromMinutes(10.0),
                MaxRespawn = TimeSpan.FromMinutes(20.0),
                Skill = SkillName.Mining,
                LandTiles = _mountainCaveTiles,
                StaticTiles = _mountainCaveStaticTiles,
                RangedTiles = true,
                MaxRange = 2,
                ConsumedPerHarvest = 1,
                ConsumedPerFeluccaHarvest = 2,
                EffectActions = new[] { 11 },
                EffectSounds = new[] { 0x125, 0x126 },
                EffectCounts = new[] { 1 },
                EffectDelay = TimeSpan.FromSeconds(1.6),
                EffectSoundDelay = TimeSpan.FromSeconds(0.9),
                NoResourcesMessage = 503040,     // There is no metal here to mine.
                DoubleHarvestMessage = 503042,   // Someone has gotten to the metal before you.
                TimedOutOfRangeMessage = 503041, // You have moved too far away to continue mining.
                OutOfRangeMessage = 500446,      // That is too far away.
                FailMessage = 503043,            // You loosen some rocks but fail to find any useable ore.
                PackFullMessage = 1010481,       // Your backpack is full, so the ore you mined is lost.
                ToolBrokeMessage = 1044038       // You have worn out your tool!
            };

            HarvestResource[] res =
            {
/* 00 */        new(00.0, 00.0, 100.0, 1007072, typeof(IronOre), typeof(Granite)),
/* 01 */        new(50.0, 10.0, 90.0, 503044, typeof(RustyOre),typeof(RustyGranite),typeof(RustyElemental)),
/* 02 */        new(60.0, 20.0, 100.0, 503044, typeof(OldCopperOre), typeof(OldCopperGranite), typeof(OldCopperElemental)),
/* 03 */        new(65.0, 25.0, 105.0, 1007073, typeof(DullCopperOre), typeof(DullCopperGranite), typeof(DullCopperElemental)),
/* 04 */        new(70.0, 30.0, 110.0, 1007074, typeof(ShadowIronOre), typeof(ShadowIronGranite), typeof(ShadowIronElemental)),
/* 05 */        new(75.0, 35.0, 115.0, 1007075, typeof(CopperOre), typeof(CopperGranite), typeof(CopperElemental)),
/* 06 */        new(80.0, 40.0, 120.0, 1007076, typeof(BronzeOre), typeof(BronzeGranite), typeof(BronzeElemental)),
/* 07 */        new(85.0, 45.0, 125.0, 1007077, typeof(GoldOre), typeof(GoldGranite), typeof(GoldenElemental)),
/* 08 */        new(87.0, 47.0, 127.0, 503044, typeof(RoseOre), typeof(RoseGranite), typeof(RoseElemental)),
/* 09 */        new(90.0, 50.0, 130.0, 1007078, typeof(AgapiteOre), typeof(AgapiteGranite), typeof(AgapiteElemental)),
/* 10 */        new(93.0, 53.0, 133.0, 503044, typeof(BloodrockOre), typeof(BloodrockGranite), typeof(BloodrockElemental)),
/* 11 */        new(95.0, 55.0, 135.0, 503044, typeof(SilverOre), typeof(SilverGranite), typeof(SilverElemental)),
/* 12 */        new(95.0, 55.0, 135.0, 1007079, typeof(VeriteOre), typeof(VeriteGranite), typeof(VeriteElemental)),
/* 13 */        new(99.0, 59.0, 139.0, 1007080, typeof(ValoriteOre), typeof(ValoriteGranite), typeof(ValoriteElemental)),
/* 14 */        new(99.5, 59.5, 135.9, 503044, typeof(MytherilOre), typeof(MytherilGranite), typeof(MytherilElemental)),
/* 15 */        new(100.0, 60.0, 140.0, 503044, typeof(BlackrockOre), typeof(BlackrockGranite), typeof(BlackrockElemental))
            };

            HarvestVein[] veins =
            {
/* 490 */       new(450, 0.0, res[0], null),    // Iron
/* 000 */       new(050, 0.5, res[1], res[0]),  // Rusty
/* 000 */       new(050, 0.5, res[2], res[0]),  // Old Copper
/* 112 */       new(070, 0.5, res[3], res[0]),  // Dull Copper
/* 098 */       new(058, 0.5, res[4], res[0]),  // Shadow Iron
/* 084 */       new(065, 0.5, res[5], res[0]),  // Copper
/* 070 */       new(060, 0.5, res[6], res[0]),  // Bronze
/* 056 */       new(046, 0.5, res[7], res[0]),  // Gold
/* 000 */       new(030, 0.5, res[8], res[0]),  // Rose
/* 042 */       new(032, 0.5, res[9], res[0]),  // Agapite
/* 000 */       new(020, 0.5, res[10], res[0]), // Bloodrock
/* 000 */       new(025, 0.5, res[11], res[0]), // Silver
/* 028 */       new(020, 0.5, res[12], res[0]), // Verite
/* 014 */       new(010, 0.5, res[13], res[0]), // Valorite
/* 000 */       new(009, 0.5, res[14], res[0]), // Mytheril
/* 000 */       new(005, 0.5, res[15], res[0])  // Blackrock
            };

            OreAndStone.Resources = res;
            OreAndStone.Veins = veins;

            if (Core.ML)
            {
                OreAndStone.BonusResources = new[]
                {
                    new BonusHarvestResource(0, 99.4, null, null), // Nothing
                    new BonusHarvestResource(100, .1, 1072562, typeof(BlueDiamond)),
                    new BonusHarvestResource(100, .1, 1072567, typeof(DarkSapphire)),
                    new BonusHarvestResource(100, .1, 1072570, typeof(EcruCitrine)),
                    new BonusHarvestResource(100, .1, 1072564, typeof(FireRuby)),
                    new BonusHarvestResource(100, .1, 1072566, typeof(PerfectEmerald)),
                    new BonusHarvestResource(100, .1, 1072568, typeof(Turquoise))
                };
            }

            OreAndStone.RaceBonus = Core.ML;
            OreAndStone.RandomizeVeins = Core.ML;

            Sand = new HarvestDefinition
            {
                BankWidth = 8,
                BankHeight = 8,
                MinTotal = 6,
                MaxTotal = 12,
                MinRespawn = TimeSpan.FromMinutes(10.0),
                MaxRespawn = TimeSpan.FromMinutes(20.0),
                Skill = SkillName.Mining,
                LandTiles = _sandTiles,
                StaticTiles = Array.Empty<int>(),
                RangedTiles = true,
                MaxRange = 2,
                ConsumedPerHarvest = 1,
                ConsumedPerFeluccaHarvest = 1,
                EffectActions = new[] { 11 },
                EffectSounds = new[] { 0x125, 0x126 },
                EffectCounts = new[] { 6 },
                EffectDelay = TimeSpan.FromSeconds(1.6),
                EffectSoundDelay = TimeSpan.FromSeconds(0.9),
                NoResourcesMessage = 1044629, // There is no sand here to mine.
                DoubleHarvestMessage = 1044629, // There is no sand here to mine.
                TimedOutOfRangeMessage = 503041, // You have moved too far away to continue mining.
                OutOfRangeMessage = 500446, // That is too far away.
                FailMessage = 1044630, // You dig for a while but fail to find any of sufficient quality for glassblowing.
                PackFullMessage = 1044632, // Your backpack can't hold the sand, and it is lost!
                ToolBrokeMessage = 1044038 // You have worn out your tool!
            };

            res = new[]
            {
                new HarvestResource(100.0, 70.0, 400.0, 1044631, typeof(Sand))
            };

            veins = new[]
            {
                new HarvestVein(1000, 0.0, res[0], null)
            };

            Sand.Resources = res;
            Sand.Veins = veins;

            Definitions = new[] { OreAndStone, Sand };
        }

        public static Mining System => _system ??= new Mining();

        public HarvestDefinition OreAndStone { get; }

        public HarvestDefinition Sand { get; }

        public override Type GetResourceType(
            Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc,
            HarvestResource resource
        )
        {
            if (def != OreAndStone)
            {
                return base.GetResourceType(from, tool, def, map, loc, resource);
            }

            if (from.Skills.Mining.Base >= 100.0 && from is PlayerMobile pm && pm.StoneMining && pm.ToggleMiningStone
                && Utility.RandomDouble() < 0.1)
            {
                return resource.Types[1];
            }

            return resource.Types[0];
        }

        public override bool CheckHarvest(Mobile from, Item tool)
        {
            if (!base.CheckHarvest(from, tool))
            {
                return false;
            }

            if (from.Mounted)
            {
                from.SendLocalizedMessage(501864); // You can't mine while riding.
                return false;
            }

            if (from.IsBodyMod && !from.Body.IsHuman)
            {
                from.SendLocalizedMessage(501865); // You can't mine while polymorphed.
                return false;
            }

            return true;
        }

        public override void SendSuccessTo(Mobile from, Item item, HarvestResource resource)
        {
            if (item is BaseGranite)
            {
                from.SendLocalizedMessage(1044606); // You carefully extract some workable stone from the ore vein!
            }
            else
            {
                base.SendSuccessTo(from, item, resource);
            }
        }

        public override bool CheckHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            if (!base.CheckHarvest(from, tool, def, toHarvest))
            {
                return false;
            }

            if (def == Sand && !(from is PlayerMobile mobile && mobile.Skills.Mining.Base >= 100.0 &&
                                 mobile.SandMining))
            {
                OnBadHarvestTarget(from, tool, toHarvest);
                return false;
            }

            if (from.Mounted)
            {
                from.SendLocalizedMessage(501864); // You can't mine while riding.
                return false;
            }

            if (from.IsBodyMod && !from.Body.IsHuman)
            {
                from.SendLocalizedMessage(501865); // You can't mine while polymorphed.
                return false;
            }

            return true;
        }

        public override HarvestVein MutateVein(
            Mobile from, Item tool, HarvestDefinition def, HarvestBank bank,
            object toHarvest, HarvestVein vein
        )
        {
            if (tool is GargoylesPickaxe && def == OreAndStone)
            {
                var veinIndex = Array.IndexOf(def.Veins, vein);

                if (veinIndex >= 0 && veinIndex < def.Veins.Length - 1)
                {
                    return def.Veins[veinIndex + 1];
                }
            }

            return base.MutateVein(from, tool, def, bank, toHarvest, vein);
        }

        public override void OnHarvestFinished(
            Mobile from, Item tool, HarvestDefinition def, HarvestVein vein,
            HarvestBank bank, HarvestResource resource, object harvested
        )
        {
            if (tool is not GargoylesPickaxe || def != OreAndStone || !(Utility.RandomDouble() < 0.1))
            {
                return;
            }

            var res = vein.PrimaryResource;

            if (res == resource && res.Types.Length >= 3)
            {
                var map = from.Map;

                if (map == null)
                {
                    return;
                }

                try
                {
                    var spawned = res.Types[2].CreateEntityInstance<BaseCreature>(25);
                    if (spawned != null)
                    {
                        var offset = Utility.Random(8) * 2;

                        for (var i = 0; i < _offsets.Length; i += 2)
                        {
                            var x = from.X + _offsets[(offset + i) % _offsets.Length];
                            var y = from.Y + _offsets[(offset + i + 1) % _offsets.Length];

                            if (map.CanSpawnMobile(x, y, from.Z))
                            {
                                spawned.OnBeforeSpawn(new Point3D(x, y, from.Z), map);
                                spawned.MoveToWorld(new Point3D(x, y, from.Z), map);
                                spawned.Combatant = from;
                                return;
                            }

                            var z = map.GetAverageZ(x, y);

                            if ((z - from.Z).Abs() < 10 && map.CanSpawnMobile(x, y, z))
                            {
                                spawned.OnBeforeSpawn(new Point3D(x, y, z), map);
                                spawned.MoveToWorld(new Point3D(x, y, z), map);
                                spawned.Combatant = from;
                                return;
                            }
                        }

                        spawned.OnBeforeSpawn(from.Location, from.Map);
                        spawned.MoveToWorld(from.Location, from.Map);
                        spawned.Combatant = from;
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

        public override object GetLock(Mobile from, Item tool, HarvestDefinition def, object toHarvest) => this;

        public override bool BeginHarvesting(Mobile from, Item tool)
        {
            if (!base.BeginHarvesting(from, tool))
            {
                return false;
            }

            from.SendLocalizedMessage(503033); // Where do you wish to dig?
            return true;
        }

        public override void OnHarvestStarted(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            base.OnHarvestStarted(from, tool, def, toHarvest);

            if (Core.ML)
            {
                from.RevealingAction();
            }
        }

        public override void OnBadHarvestTarget(Mobile from, Item tool, object toHarvest)
        {
            if (toHarvest is LandTarget)
            {
                from.SendLocalizedMessage(501862); // You can't mine there.
            }
            else
            {
                from.SendLocalizedMessage(501863); // You can't mine that.
            }
        }
    }
}
