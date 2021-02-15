using System;
using System.Collections.Generic;
using InfinityScript;

namespace snipe
{
    public partial class SNIPE
    {

        public void SNIPE_OnServerStart()
        {
            Log.Info("Loading iSnipe Configuration.");
            SetupKnife();
            PlayerActuallySpawned += SNIPE_OnPlayerSpawn;
            OnPlayerDamageEvent += SNIPE_PeriodicChecks;
            PlayerConnected += SNIPE_OnPlayerConnect;

            if (sv_antiknife)
            {
                DisableKnife();
                Log.Info("Knife has been disabled by default.");
            }

            Log.Info("iSnipe Configuration loaded.");
        }

        public void SNIPE_OnPlayerSpawn(Entity player)
        {
            CMD_HideBombIcon(player);
            CMD_GiveMaxAmmo(player);
            if (sv_replace_secondary)
                if (player.HasWeapon("stinger_mp"))
                {
                    player.TakeWeapon("stinger_mp");
                    player.GiveWeapon("iw5_44magnum_mp");
                    player.Call("SetWeaponAmmoStock", "iw5_44magnum_mp", "0");
                    player.Call("SetWeaponAmmoClip", "iw5_44magnum_mp", "0");

                    OnInterval(1000, () =>
                    {
                        player.Call("SetWeaponAmmoStock", "iw5_44magnum_mp", "0");
                        player.Call("SetWeaponAmmoClip", "iw5_44magnum_mp", "0");
                        return true;
                    });
                }
            if (sv_anti_plant)
                player.OnNotify("weapon_change", delegate (Entity Player, Parameter weap)
                {
                    if (weap.ToString() == "briefcase_bomb_mp")
                    {
                        Player.TakeWeapon("briefcase_bomb_mp");
                        Player.IPrintLnBold(Call<string>("getDvar", "msg_antiplant"));
                    }
                });
            string weapon = player.CurrentWeapon;
            if (sv_anti_hardscope && SNIPE_IsHardscopeWeapon(weapon))
            {
                player.SetField("adscycles", 0);
                player.SetField("letmehardscope", 0);
                player.OnInterval(100, ent =>
                {
                    if (!ent.IsAlive)
                        return false;
                    if (ent.GetField<int>("letmehardscope") == 1)
                        return true;

                    float ads = ent.Call<float>("playerads");
                    int adscycles = player.GetField<int>("adscycles");
                    if (ads == 1f)
                        adscycles++;
                    else
                        adscycles = 0;

                    if (Double.TryParse(Call<string>("getDvar", "sv_hstimer"), out double timer))
                    {
                        if (adscycles >= timer * 10)
                        {
                            ent.Call("allowads", false);
                            ent.IPrintLnBold(Call<string>("getDvar", "msg_antihardscope"));
                        }

                        if (ent.Call<int>("adsbuttonpressed") == 0 && ads == 0)
                        {
                            ent.Call("allowads", true);
                        }

                        player.SetField("adscycles", adscycles);
                        return true;
                    }
                    else
                    {
                        Log.Error("antiHardScope timer must be a double value. Domain [0 1]");
                        return false;
                    }
                });
            }
        }

        public void SNIPE_PeriodicChecks(Entity player, Entity inflictor, Entity attacker, int damage, int dFlags, string mod, string weapon, Vector3 point, Vector3 dir, string hitLoc)
        {
            if (sv_anti_falldamage && mod == "MOD_FALLING")
            {
                player.Health += damage;
                return;
            }
        }

        public static List<string> Hardscopeweapon = new List<string>()
        {
            "l96a1",
            "msr",
            "barrett",
            "rsass",
            "dragunov",
            "as50",
        };

        public bool SNIPE_IsHardscopeWeapon(string weapon)
        {
            foreach (string weap in Hardscopeweapon)
            {
                if (weapon.Contains(weap))
                    return true;
            }
            return false;
        }

        public void SNIPE_OnPlayerConnect(Entity player)
        {
            player.OnNotify("giveLoadout", (ent) =>
            {
                CMD_GiveMaxAmmo(ent);
            });

        }

    }
}