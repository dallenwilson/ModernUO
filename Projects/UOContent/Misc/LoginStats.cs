using Server.Mobiles;
using Server.Network;
using ModernUO.CodeGeneratedEvents;

namespace Server.Misc
{
    public static class LoginStats
    {
        [OnEvent(nameof(PlayerMobile.PlayerLoginEvent))]
        public static void OnLogin(PlayerMobile pm)
        {
            var userCount = NetState.Instances.Count;
            var itemCount = World.Items.Count;
            var mobileCount = World.Mobiles.Count;

            pm.SendMessage(
                $"Welcome, {pm.Name}! There {(userCount == 1 ? "is" : "are")} currently {userCount} user{(userCount == 1 ? "" : "s")} " +
                $"online, with {itemCount} item{(itemCount == 1 ? "" : "s")} and {mobileCount} mobile{(mobileCount == 1 ? "" : "s")} in the world."
            );

            World.Broadcast( 0x35, true,$"{pm.Name} has Logged In. There {(userCount == 1 ? "is" : "are")} now {userCount} user{(userCount == 1 ? "" : "s")} online.");
        }

        [OnEvent(nameof(PlayerMobile.PlayerLogoutEvent))]
        public static void OnLogout(PlayerMobile pm)
        {
            var userCount = NetState.Instances.Count;
            var itemCount = World.Items.Count;
            var mobileCount = World.Mobiles.Count;

            World.Broadcast( 0x35, true,$"{pm.Name} has Logged Out. There {(userCount == 1 ? "is" : "are")} now {userCount} user{(userCount == 1 ? "" : "s")} online.");
        }

    }
}
