using System;
using DiscordRPC;

namespace Neutronic_RPC //fun rpc shit yeah change these to what u want
{
    public class RPC
    {
        public static DiscordRpcClient client;
        public static Timestamps rpctimestamp { get; set; }
        private static RichPresence presence;
        public static void InitializeRPC()
        {
            client = new DiscordRpcClient("1114575079041404928");
            client.Initialize();
            Button[] buttons = { new Button() { Label = "Discord", Url = "https://discord.gg/Z4yUEmDN6x" }, new Button() { Label = "Website", Url = "https://neutronic.xyz" } };

            presence = new RichPresence()
            {
                Details = "Neutronic On Top",
                State = "Get Rich, Get Neutronic",
                Timestamps = rpctimestamp,
                Buttons = buttons,

                Assets = new Assets()
                {
                    LargeImageKey = "d5",
                    LargeImageText = "Neutronic",
                }
            };
            SetState("Login Page");
        }
        public static void SetState(string state, bool watching = false)
        {
            if (watching)
                state = "Looking at " + state;

            presence.State = state;
            client.SetPresence(presence);
        }
    }
}
