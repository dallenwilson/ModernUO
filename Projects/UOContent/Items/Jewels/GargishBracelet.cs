using ModernUO.Serialization;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class GargishBracelet : BaseBracelet
    {
        public override int RequiredRaces => Race.AllowGargoylesOnly;

        [Constructible]
        public GargishBracelet()
            : base(0x4211)
        {
            //Weight = 0.1;
        }
    }
}
