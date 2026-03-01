using ModernUO.Serialization;

namespace Server.Items;

[SerializationGenerator(2, false)]
public abstract partial class BaseGranite : Item
{
    public BaseGranite(CraftResource resource) : base(0x1779)
    {
        Hue = CraftResources.GetHue(resource);
        Stackable = Core.ML;

        _resource = resource;
    }

    public override double DefaultWeight => Core.ML ? 1.0 : 10.0;

    [SerializableProperty(0)]
    [CommandProperty(AccessLevel.GameMaster)]
    public CraftResource Resource
    {
        get => _resource;
        set
        {
            if (_resource != value)
            {
                _resource = value;
                Hue = CraftResources.GetHue(value);

                InvalidateProperties();
                this.MarkDirty();
            }
        }
    }

    public override int LabelNumber => 1044607; // high quality granite

    private void Deserialize(IGenericReader reader, int version)
    {
        _resource = (CraftResource)reader.ReadInt();
    }

    public override void GetProperties(IPropertyList list)
    {
        base.GetProperties(list);

        if (!CraftResources.IsStandard(_resource))
        {
            var num = CraftResources.GetLocalizationNumber(_resource);

            if (num > 0)
            {
                list.Add(num);
            }
            else
            {
                list.Add(CraftResources.GetName(_resource));
            }
        }
    }
}

[SerializationGenerator(0, false)]
public partial class Granite : BaseGranite
{
    [Constructible]
    public Granite() : base(CraftResource.Iron)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class RustyGranite : BaseGranite
{
    [Constructible]
    public RustyGranite() : base(CraftResource.Rusty)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class OldCopperGranite : BaseGranite
{
    [Constructible]
    public OldCopperGranite() : base(CraftResource.OldCopper)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class DullCopperGranite : BaseGranite
{
    [Constructible]
    public DullCopperGranite() : base(CraftResource.DullCopper)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class ShadowIronGranite : BaseGranite
{
    [Constructible]
    public ShadowIronGranite() : base(CraftResource.ShadowIron)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class CopperGranite : BaseGranite
{
    [Constructible]
    public CopperGranite() : base(CraftResource.Copper)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class BronzeGranite : BaseGranite
{
    [Constructible]
    public BronzeGranite() : base(CraftResource.Bronze)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class GoldGranite : BaseGranite
{
    [Constructible]
    public GoldGranite() : base(CraftResource.Gold)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class RoseGranite : BaseGranite
{
    [Constructible]
    public RoseGranite() : base(CraftResource.Rose)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class AgapiteGranite : BaseGranite
{
    [Constructible]
    public AgapiteGranite() : base(CraftResource.Agapite)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class BloodrockGranite : BaseGranite
{
    [Constructible]
    public BloodrockGranite() : base(CraftResource.Bloodrock)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class SilverGranite : BaseGranite
{
    [Constructible]
    public SilverGranite() : base(CraftResource.Silver)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class VeriteGranite : BaseGranite
{
    [Constructible]
    public VeriteGranite() : base(CraftResource.Verite)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class ValoriteGranite : BaseGranite
{
    [Constructible]
    public ValoriteGranite() : base(CraftResource.Valorite)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class MytherilGranite : BaseGranite
{
    [Constructible]
    public MytherilGranite() : base(CraftResource.Mytheril)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class BlackrockGranite : BaseGranite
{
    [Constructible]
    public BlackrockGranite() : base(CraftResource.Blackrock)
    {
    }
}
