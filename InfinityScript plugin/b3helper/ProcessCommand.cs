using InfinityScript;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace snipe
{
    public partial class SNIPE
    {
        public void ProcessCommand(string message)
        {
            try
            {
                string[] msg = message.Split(' ');
                msg[0] = msg[0].ToLowerInvariant();
                if (msg[0].StartsWith("!afk"))
                {
                    Entity player = GetPlayer(msg[1]);
                    ChangeTeam(player, "spectator");
                }
                if (msg[0].StartsWith("!setafk"))
                {
                    Entity target = GetPlayer(msg[1]);
                    ChangeTeam(target, "spectator");
                }
                if (msg[0].StartsWith("!kill"))
                {
                    Entity target = GetPlayer(msg[1]);
                    target.Call("suicide");
                }
                if (msg[0].StartsWith("!suicide"))
                {
                    Entity player = GetPlayer(msg[1]);
                    player.Call("suicide");
                }
                if (msg[0].StartsWith("!godmode"))
                {
                    Entity player = GetPlayer(msg[1]);
                    if (!player.HasField("godmodeon"))
                    {
                        player.SetField("godmodeon", "0");
                    }
                    if (player.GetField<int>("godmodeon") == 1)
                    {
                        player.Health = 30;
                        player.SetField("godmodeon", "0");
                        Utilities.RawSayAll($"^1{player.Name} GodMode has been deactivated.");
                    }
                    else if (player.GetField<int>("godmodeon") == 0)
                    {
                        player.Health = -1;
                        player.SetField("godmodeon", "1");
                        Utilities.RawSayAll($"^1{player.Name} GodMode has been activated.");
                    }
                }
                if (msg[0].StartsWith("!teleport"))
                {
                    Entity teleporter = GetPlayer(msg[1]);
                    Entity reciever = GetPlayer(msg[2]);

                    teleporter.Call("setOrigin", reciever.Origin);
                }
                if (msg[0].StartsWith("!mode"))
                {
                    if (!System.IO.File.Exists($@"admin\{msg[1]}.dsr") && !System.IO.File.Exists($@"players2\{msg[1]}.dsr"))
                    {
                        Utilities.RawSayAll("^1DSR not found.");
                        return;
                    }
                    Mode(msg[1]);
                }
                if (msg[0].StartsWith("!gametype"))
                {
                    if (!System.IO.File.Exists($@"admin\{msg[1]}.dsr") && !System.IO.File.Exists($@"players2\{msg[1]}.dsr"))
                    {
                        Utilities.RawSayAll("^1DSR not found.");
                        return;
                    }
                    string newMap = msg[2];
                    Mode(msg[1], newMap);

                }
                if (msg[0].StartsWith("!ac130"))
                {
                    if (msg[1].StartsWith("*all*"))
                    {
                        AC130All();
                    }
                    else
                    {
                        Entity player = GetPlayer(msg[1]);
                        AfterDelay(500, () =>
                        {
                            player.TakeAllWeapons();
                            player.GiveWeapon("ac130_105mm_mp");
                            player.GiveWeapon("ac130_40mm_mp");
                            player.GiveWeapon("ac130_25mm_mp");
                            player.SwitchToWeaponImmediate("ac130_25mm_mp");
                        });
                    }

                }
                if (msg[0].StartsWith("!mute"))
                {
                    Entity player = GetPlayer(msg[1]);
                    if (!player.HasField("muted"))
                    {
                        player.SetField("muted", 0);
                    }
                    if (player.GetField<int>("muted") == 1)
                    {
                        player.SetField("muted", 0);
                        Utilities.RawSayAll($"^1{player.Name} has been unmute.");
                    }
                    else if (player.GetField<int>("muted") == 0)
                    {
                        player.SetField("muted", 1);
                        Utilities.RawSayAll($"^1{player.Name} has been muted.");
                    }
                }
                if (msg[0].StartsWith("!freeze"))
                {
                    Entity player = GetPlayer(msg[1]);
                    if (!player.HasField("frozen"))
                    {
                        player.SetField("frozen", 0);
                    }
                    if (player.GetField<int>("frozen") == 1)
                    {
                        player.Call("freezecontrols", false);
                        player.SetField("frozen", 0);
                        Utilities.RawSayAll($"^1{player.Name} has been unfrozen.");
                    }
                    else if (player.GetField<int>("frozen") == 0)
                    {
                        player.Call("freezecontrols", true);
                        player.SetField("frozen", 1);
                        Utilities.RawSayAll($"^1{player.Name} has been frozen.");
                    }
                }
                if (msg[0].StartsWith("!changeteam"))
                {
                    Entity player = GetPlayer(msg[1]);
                    string playerteam = player.GetField<string>("sessionteam");

                    switch (playerteam)
                    {
                        case "axis":
                            ChangeTeam(player, "allies");
                            break;
                        case "allies":
                            ChangeTeam(player, "axis");
                            break;
                        case "spectator":
                            Utilities.RawSayAll($"^1{player.Name} team can't be changed because player is spectator.");
                            break;
                    }

                }
            }
            catch (Exception e)
            {
                Log.Error("Error in Command Processing. Error:" + e.Message + e.StackTrace);
            }
        }
    }
}