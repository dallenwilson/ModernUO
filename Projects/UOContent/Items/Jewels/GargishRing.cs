using ModernUO.Serialization;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class GargishRing : BaseRing
    {
        public override int RequiredRaces => Race.AllowGargoylesOnly;

        [Constructible]
        public GargishRing()
            : base(0x4212)
        {
            //Weight = 0.1;
        }
    }
}
