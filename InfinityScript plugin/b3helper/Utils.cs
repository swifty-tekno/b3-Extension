using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfinityScript;
using System.Net;

namespace snipe
{
    public partial class SNIPE
    {
        public void CMD_HideBombIcon(Entity player)
        {
            player.SetClientDvar("waypointIconHeight", "1");
            player.SetClientDvar("waypointIconWidth", "1");
        }

        public void CMD_GiveMaxAmmo(Entity player)
        {
            player.Call("giveMaxAmmo", player.CurrentWeapon);
        }

        public void CMD_applyfilmtweak(Entity sender, string ft)
        {
            switch (ft)
            {
                case "0":
                    sender.SetClientDvar("r_filmusetweaks", "0");
                    sender.SetClientDvar("r_filmtweakenable", "0");
                    sender.SetClientDvar("r_colorMap", "1");
                    sender.SetClientDvar("r_specularMap", "1");
                    sender.SetClientDvar("r_normalMap", "1");
                    return;
                case "1":
                    sender.SetClientDvar("r_filmtweakdarktint", "0.65 0.7 0.8");
                    sender.SetClientDvar("r_filmtweakcontrast", "1.3");
                    sender.SetClientDvar("r_filmtweakbrightness", "0.15");
                    sender.SetClientDvar("r_filmtweakdesaturation", "0");
                    sender.SetClientDvar("r_filmusetweaks", "1");
                    sender.SetClientDvar("r_filmtweaklighttint", "1.8 1.8 1.8");
                    sender.SetClientDvar("r_filmtweakenable", "1");
                    return;
                case "2":
                    sender.SetClientDvar("r_filmtweakdarktint", "1.15 1.1 1.3");
                    sender.SetClientDvar("r_filmtweakcontrast", "1.6");
                    sender.SetClientDvar("r_filmtweakbrightness", "0.2");
                    sender.SetClientDvar("r_filmtweakdesaturation", "0");
                    sender.SetClientDvar("r_filmusetweaks", "1");
                    sender.SetClientDvar("r_filmtweaklighttint", "1.35 1.3 1.25");
                    sender.SetClientDvar("r_filmtweakenable", "1");
                    return;
                case "3":
                    sender.SetClientDvar("r_filmtweakdarktint", "0.8 0.8 1.1");
                    sender.SetClientDvar("r_filmtweakcontrast", "1.3");
                    sender.SetClientDvar("r_filmtweakbrightness", "0.48");
                    sender.SetClientDvar("r_filmtweakdesaturation", "0");
                    sender.SetClientDvar("r_filmusetweaks", "1");
                    sender.SetClientDvar("r_filmtweaklighttint", "1 1 1.4");
                    sender.SetClientDvar("r_filmtweakenable", "1");
                    return;
                case "4":
                    sender.SetClientDvar("r_filmtweakdarktint", "1.8 1.8 2");
                    sender.SetClientDvar("r_filmtweakcontrast", "1.25");
                    sender.SetClientDvar("r_filmtweakbrightness", "0.02");
                    sender.SetClientDvar("r_filmtweakdesaturation", "0");
                    sender.SetClientDvar("r_filmusetweaks", "1");
                    sender.SetClientDvar("r_filmtweaklighttint", "0.8 0.8 1");
                    sender.SetClientDvar("r_filmtweakenable", "1");
                    return;
                case "5":
                    sender.SetClientDvar("r_filmtweakdarktint", "1 1 2");
                    sender.SetClientDvar("r_filmtweakcontrast", "1.5");
                    sender.SetClientDvar("r_filmtweakbrightness", "0.07");
                    sender.SetClientDvar("r_filmtweakdesaturation", "0");
                    sender.SetClientDvar("r_filmusetweaks", "1");
                    sender.SetClientDvar("r_filmtweaklighttint", "1 1.2 1");
                    sender.SetClientDvar("r_filmtweakenable", "1");
                    return;
                case "6":
                    sender.SetClientDvar("r_filmtweakdarktint", "1.5 1.5 2");
                    sender.SetClientDvar("r_filmtweakcontrast", "1");
                    sender.SetClientDvar("r_filmtweakbrightness", "0.0.4");
                    sender.SetClientDvar("r_filmtweakdesaturation", "0");
                    sender.SetClientDvar("r_filmusetweaks", "1");
                    sender.SetClientDvar("r_filmtweaklighttint", "1.5 1.5 1");
                    sender.SetClientDvar("r_filmtweakenable", "1");
                    return;
                case "7":
                    sender.SetClientDvar("r_specularMap", "2");
                    sender.SetClientDvar("r_normalMap", "0");
                    return;
                case "8":
                    sender.SetClientDvar("cg_drawFPS", "1");
                    sender.SetClientDvar("cg_fovScale", "1.5");
                    return;
                case "9":
                    sender.SetClientDvar("r_debugShader", "1");
                    return;
                case "10":
                    sender.SetClientDvar("r_colorMap", "3");
                    return;
                case "11":
                    sender.SetClientDvar("com_maxfps", "0");
                    sender.SetClientDvar("con_maxfps", "0");
                    return;
                case "default":
                    sender.SetClientDvar("r_filmtweakdarktint", "0.7 0.85 1");
                    sender.SetClientDvar("r_filmtweakcontrast", "1.4");
                    sender.SetClientDvar("r_filmtweakdesaturation", "0.2");
                    sender.SetClientDvar("r_filmusetweaks", "0");
                    sender.SetClientDvar("r_filmtweaklighttint", "1.1 1.05 0.85");
                    sender.SetClientDvar("cg_fov", "66");
                    sender.SetClientDvar("cg_scoreboardpingtext", "1");
                    sender.SetClientDvar("waypointIconHeight", "13");
                    sender.SetClientDvar("waypointIconWidth", "13");
                    sender.SetClientDvar("cl_maxpackets", "100");
                    sender.SetClientDvar("r_fog", "0");
                    sender.SetClientDvar("fx_drawclouds", "0");
                    sender.SetClientDvar("r_distortion", "0");
                    sender.SetClientDvar("r_dlightlimit", "0");
                    sender.SetClientDvar("cg_brass", "0");
                    sender.SetClientDvar("snaps", "30");
                    sender.SetClientDvar("com_maxfps", "100");
                    sender.SetClientDvar("clientsideeffects", "0");
                    sender.SetClientDvar("r_filmTweakBrightness", "0.2");
                    return;
            }
        }

        public void CMD_knife(bool state)
        {
            if (state)
                EnableKnife();
            else
                DisableKnife();
        }

        public bool IsGameModeTeamBased()
        {
            string gameType = Call<string>("getDvar", "g_gametype");
            if (gameType == "ffa" || gameType == "gg" || gameType.Contains("inf") || gameType.Contains("gun"))
                return false;
            return true;
        }

        private void Hud_alive_players(Entity player)
        {
            HudElem hudAlliesLabel = HudElem.CreateFontString(player, "hudsmall", 0.6f);
            hudAlliesLabel.SetPoint("DOWNRIGHT", "DOWNRIGHT", -19, 96);
            hudAlliesLabel.SetText("^5Allies ^7: ");
            hudAlliesLabel.Color = new Vector3(1.0f, 0.33984375f, 0.19921875f);
            hudAlliesLabel.GlowColor = new Vector3(1f, 0.863f, 0.0f);
            hudAlliesLabel.GlowAlpha = 1f;
            hudAlliesLabel.Alpha = 1f;
            hudAlliesLabel.HideWhenInMenu = true;

            HudElem hudEnemiesLabel = HudElem.CreateFontString(player, "hudsmall", 0.6f);
            hudEnemiesLabel.SetPoint("DOWNRIGHT", "DOWNRIGHT", -19, 114);
            hudEnemiesLabel.SetText("^1Enemy ^7: ");
            hudEnemiesLabel.Color = new Vector3(0.921875f, 0.4375f, 0.38671875f);
            hudEnemiesLabel.GlowColor = new Vector3(1f, 0.255f, 0.212f);
            hudEnemiesLabel.GlowAlpha = 1f;
            hudEnemiesLabel.Alpha = 1f;
            hudEnemiesLabel.HideWhenInMenu = true;

            HudElem hudAliveCountEnemies = HudElem.CreateFontString(player, "hudsmall", 0.6f);
            hudAliveCountEnemies.SetPoint("DOWNRIGHT", "DOWNRIGHT", -9, 115);
            hudAliveCountEnemies.GlowColor = new Vector3(1f, 0.1f, 0.5f);
            hudAliveCountEnemies.GlowAlpha = 1f;
            hudAliveCountEnemies.Alpha = 1f;
            hudAliveCountEnemies.HideWhenInMenu = true;

            HudElem hudAliveCountAllies = HudElem.CreateFontString(player, "hudsmall", 0.6f);
            hudAliveCountAllies.SetPoint("DOWNRIGHT", "DOWNRIGHT", -9, 95);
            hudAliveCountAllies.GlowColor = new Vector3(1f, 0.1f, 0.5f);
            hudAliveCountAllies.GlowAlpha = 1f;
            hudAliveCountAllies.Alpha = 1f;
            hudAliveCountAllies.HideWhenInMenu = true;

            this.OnInterval(500, (Func<bool>)(() =>
            {
                if (SessionTeam(player) == "spectator")
                {
                    hudAliveCountAllies.Alpha = 0f;
                    hudAliveCountEnemies.Alpha = 0f;
                    hudAlliesLabel.Alpha = 0f;
                    hudEnemiesLabel.Alpha = 0f;
                    return false;
                }

                int aliveAxisCount = Call<int>("getteamplayersalive", "axis");
                int aliveAlliesCount = Call<int>("getteamplayersalive", "allies");

                string playerTeam = SessionTeam(player);
                string axisValue = aliveAxisCount.ToString();
                string alliesValue = aliveAlliesCount.ToString();


                if (aliveAxisCount == 1)
                {
                    axisValue = "^1Last Alive : ^7" + GetPlayer("axis", true).Name;
                }
                if (aliveAlliesCount == 1)
                {
                    alliesValue = "^5Last Alive : ^7" + GetPlayer("allies", true).Name;
                }

                if (SessionTeam(player) == "axis")
                {
                    if (aliveAlliesCount == 1)
                    {
                        hudAliveCountEnemies.Font = "hudsmall";
                        hudEnemiesLabel.Alpha = 0f;
                    }
                    else
                    {
                        hudAliveCountEnemies.Font = "hudbig";
                        hudEnemiesLabel.Alpha = 1f;
                    }

                    if (aliveAxisCount == 1)
                    {
                        hudAliveCountAllies.Font = "hudsmall";
                        hudAlliesLabel.Alpha = 0f;
                    }
                    else
                    {
                        hudAliveCountAllies.Font = "hudbig";
                        hudAlliesLabel.Alpha = 1f;
                    }

                    hudAliveCountAllies.SetText(axisValue);
                    hudAliveCountEnemies.SetText(alliesValue);
                }
                else if (SessionTeam(player) == "allies")
                {
                    if (aliveAlliesCount == 1) { hudAlliesLabel.Alpha = 0f; }
                    else { hudAlliesLabel.Alpha = 1f; }

                    if (aliveAxisCount == 1) { hudEnemiesLabel.Alpha = 0f; }
                    else { hudEnemiesLabel.Alpha = 1f; }

                    hudAliveCountAllies.SetText(alliesValue);
                    hudAliveCountEnemies.SetText(axisValue);
                }
                return true;
            }));
        }

        public static string SessionTeam(Entity player)
        {
            return player.GetField<string>("sessionteam");
        }
        public Entity GetPlayer(string team, bool IsAlive)
        {
            List<Entity> chosenPlayers = new List<Entity>();

            team = team.ToLowerInvariant();
            if (team != "allies" && team != "axis")
            {
                Log.Error($"Invalid team: {team}. Using Axis.");
                team = "axis";
            }

            foreach (Entity player in Players)
            {
                if (SessionTeam(player) != team) { continue; }
                if (player.IsAlive != IsAlive) { continue; }

                chosenPlayers.Add(player);
            }

            if (chosenPlayers.Count == 0) return null;
            return chosenPlayers[rng.Next(chosenPlayers.Count)];
        }
        public static Random rng = new Random();

        //spawn shit idk
        public void MAIN_ResetSpawnAction()
        {
            foreach (Entity player in Players)
                player.SetField("spawnevent", 0);
        }

    }

    public static partial class Extensions
    {
        public static void IPrintLnBold(this Entity player, string message)
        {
            player.Call("iprintlnbold", message);
        }

        public static void IPrintLn(this Entity player, string message)
        {
            player.Call("iprintln", message);
        }

        public static int GetEntityNumber(this Entity player)
        {
            return player.Call<int>("getentitynumber");
        }

        public static void Suicide(this Entity player)
        {
            player.Call("suicide");
        }

        public static string GetTeam(this Entity player)
        {
            return player.GetField<string>("sessionteam");
        }

        public static bool IsSpectating(this Entity player)
        {
            return player.GetTeam() == "spectator";
        }

        //fix later want clantag cmd
        /*
        public static string GetClantag(this Entity player)
        {
            if (player == null || !player.IsPlayer)
                return null;
            int address = LAdmin.Data.ClantagPlayerDataSize * player.GetEntityNumber() + LAdmin.Data.ClantagOffset;
            return LAdmin.Mem.ReadString(address, 8);
        }

        public static void SetClantag(this Entity player, string clantag)
        {
            if (player == null || !player.IsPlayer || clantag.Length > 7)
                return;
            int address = LAdmin.Data.ClantagPlayerDataSize * player.GetEntityNumber() + LAdmin.Data.ClantagOffset;
            unsafe
            {
                for (int i = 0; i < clantag.Length; i++)
                {
                    *(byte*)(address + i) = (byte)clantag[i];
                }
                *(byte*)(address + clantag.Length) = 0;
            }
        }
        */
    }
}