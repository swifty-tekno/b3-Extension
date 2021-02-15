using InfinityScript;
using System;
using System.Collections.Generic;
using System.Linq;

namespace snipe
{
    public partial class SNIPE : BaseScript
    {
        event Action<Entity> PlayerActuallySpawned = ent => { };
        event Action<Entity, Entity, Entity, int, int, string, string, Vector3, Vector3, string> OnPlayerDamageEvent = (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => { };
        event Action<Entity, Entity, Entity, int, string, string, Vector3, string> OnPlayerKilledEvent = (t1, t2, t3, t4, t5, t6, t7, t8) => { };
        event Action OnGameEnded = () => { };

        private ChatAlias chat = new ChatAlias();

        //Mode
        volatile string MapRotation = "";

        //Unlimited ammo
        public static bool activeunlimitedammo = false;

        private bool sv_hideCommands;

        public bool sv_snipemode;
        public bool sv_anti_hardscope;
        public bool sv_anti_hardscope_timer;
        public bool msg_anti_hardscope;

        public bool sv_anti_plant;
        public bool msg_anti_plant;

        public bool sv_anti_falldamage;
        public bool sv_antiknife;
        public bool sv_replace_secondary;
        public bool sv_hud_alive_couner;

        public List<Entity> onlinePlayers = new List<Entity>();

        public SNIPE()
        {
            Log.Info("b3Extension plugin by Musta#6382 and Pickle Rick#5230. <= Used as base for snipe script");

            //snipe dvars

            Call("setDvarifUninitialized", "sv_snipemode", "1"); //Done

            Call("setDvarifUninitialized", "sv_antihardscope", "1"); //Done
            Call("setDvarifUninitialized", "sv_hstimer", "0.43"); //Done set value like 0.43
            Call("setDvarifUninitialized", "msg_antihardscope", "^1Hardscoping is not Allowed!!!");

            Call("setDvarifUninitialized", "sv_antiplant", "1"); //Done
            Call("setDvarifUninitialized", "msg_antiplant", "^1Planting bomb is not Allowed!!!"); //Done

            Call("setDvarifUninitialized", "sv_antifalldamage", "1"); //Done
            Call("setDvarifUninitialized", "sv_antiknife", "1"); //Done
            Call("setDvarifUninitialized", "sv_replacescondary", "1"); //Done
            Call("setDvarifUninitialized", "sv_hud_alive_counter", "1"); //Done

            //Call("setDvarifUninitialized", "sv_hideCommands", "1"); //Done

            //end of snipe dvars

            //Making and Settings dvars if they are unused and have value.
            Call("setDvarifUninitialized", "sv_hideCommands", "1"); //Done
            Call("setDvarifUninitialized", "sv_gmotd", "^:Welcome to the server."); //Done

            Call("setDvarifUninitialized", "sv_objText", "^1This is menu text."); //Done
            Call("setDvarifUninitialized", "sv_clientDvars", "1"); //Done
            Call("setDvarifUninitialized", "sv_rate", "210000");
            Call("setDvarifUninitialized", "sv_serverDvars", "1"); //Done


            Call("setDvarifUninitialized", "sv_b3Execute", "null"); //Done

            //Loading Server Dvars.
            ServerDvars();


            //Assigning things.
            PlayerConnected += OnPlayerConnect;
            PlayerDisconnected += OnPlayerDisconnect;

            if (sv_snipemode)
            {
                SNIPE_OnServerStart();
            }

            OnInterval(50, () =>
            {
                if (Call<string>("getDvar", "sv_b3Execute") != "null")
                {
                    string content = Call<string>("getDvar", "sv_b3Execute");
                    ProcessCommand(content);
                    Call("setDvar", "sv_b3Execute", "null");
                }
                return true;
            });

            sv_hideCommands = Call<bool>("getDvar", "sv_hideCommands", "1");

            sv_snipemode = Call<bool>("getDvar", "sv_snipemode", "1");

            sv_anti_hardscope = Call<bool>("getDvar", "sv_antihardscope", "1");
            sv_anti_plant = Call<bool>("getDvar", "sv_antiplant", "1");
            sv_anti_falldamage = Call<bool>("getDvar", "sv_antifalldamage", "1");
            sv_antiknife = Call<bool>("getDvar", "sv_antiknife", "1");
            sv_replace_secondary = Call<bool>("getDvar", "sv_replacescondary", "1");
            sv_hud_alive_couner = Call<bool>("getDvar", "sv_hud_alive_counter", "1");


            OnNotify("game_ended", level =>
            {
                OnGameEnded();
            });

            // CUSTOM EVENTS

            MAIN_ResetSpawnAction();
        }


        public void ServerDvars()
        {
            if (Call<int>("getDvarInt", "sv_serverDvars") != 0)
            {
                Function.Call("setdevDvar", "sv_network_fps", 200);
                Function.Call("setDvar", "sv_hugeSnapshotSize", 10000);
                Function.Call("setDvar", "sv_hugeSnapshotDelay", 100);
                Function.Call("setDvar", "sv_pingDegradation", 0);
                Function.Call("setDvar", "sv_pingDegradationLimit", 9999);
                Function.Call("setDvar", "sv_acceptableRateThrottle", 9999);
                Function.Call("setDvar", "sv_newRateThrottling", 2);
                Function.Call("setDvar", "sv_minPingClamp", 50);
                Function.Call("setDvar", "sv_cumulThinkTime", 1000);
                Function.Call("setDvar", "sys_lockThreads", "all");
                Function.Call("setDvar", "com_maxFrameTime", 1000);
                Function.Call("setDvar", "com_maxFps", 0);
                Function.Call("setDvar", "sv_voiceQuality", 9);
                Function.Call("setDvar", "maxVoicePacketsPerSec", 1000);
                Function.Call("setDvar", "maxVoicePacketsPerSecForServer", 200);
                Function.Call("setDvar", "cg_everyoneHearsEveryone", 1);
                Function.Call("makedvarserverinfo", "motd", Call<string>("getDvar", "sv_gmotd"));
                Function.Call("makedvarserverinfo", "didyouknow", Call<string>("getDvar", "sv_gmotd"));
            }
        }



        public void OnPlayerConnect(Entity player)
        {
            if (player.IsPlayer)
                onlinePlayers.Add(player);

            if (sv_hud_alive_couner)
            { Hud_alive_players(player); }

            //Client Performance dvar
            if (Call<int>("getDvarInt", "sv_clientDvars") != 0)
            {
                player.SetClientDvar("cg_objectiveText", Call<String>("getDvar", "sv_objText"));
                player.SetClientDvar("sys_lockThreads", "all");
                player.SetClientDvar("com_maxFrameTime", "1000");
                player.SetClientDvar("rate ", Call<string>("getDvar", "sv_rate"));
                player.SpawnedPlayer += () =>
                {
                    player.SetClientDvar("cg_objectiveText", Call<String>("getDvar", "sv_objText"));
                };
            }

            player.SpawnedPlayer += () =>
            {
                if (player.HasField("frozen"))
                {
                    if (player.GetField<int>("frozen") == 1)
                    {
                        player.Call("freezecontrols", true);
                    }
                }
            };
            player.OnNotify("giveloadout", delegate (Entity entity)
            {
                if (entity.HasField("frozen"))
                {
                    if (entity.GetField<int>("frozen") == 1)
                    {
                        entity.Call("freezecontrols", true);
                    }
                }
            });
        }

        public override void OnPlayerDisconnect(Entity player)
        {
            onlinePlayers.Remove(player);
        }


        public override EventEat OnSay3(Entity player, ChatType type, string name, ref string message)
        {

            message = message.ToLower();
            if (message.StartsWith("!") || message.StartsWith("@"))
            {
                if (sv_hideCommands)
                    return EventEat.EatGame;
            }

            if (player.GetField<int>("muted") == 1)
            {
                return EventEat.EatGame;
            }

            string alias = chat.CheckAlias(player);

            if (!string.IsNullOrWhiteSpace(alias))
            {
                string text = alias + "^7: " + message;

                if (type == ChatType.Team)
                {
                    text = alias + "^7:^5 " + message;
                }

                if (SessionTeam(player) == "spectator")
                {
                    text = "^7(Spectator) " + text;
                }

                else if (!player.IsAlive)
                {
                    text = (!IsGameModeTeamBased()) ? ("^7(^1Dead^7) " + text) : ("^7(^1Dead^7) " + text);
                }

                if (!IsGameModeTeamBased())
                {
                    Utilities.RawSayAll(text);
                }

                else if (type == ChatType.Team)
                {
                    text = "^7[^2Team^7] " + text;
                    foreach (Entity item in onlinePlayers.Where((Entity x) => x.GetField<string>("sessionteam") == player.GetTeam()))
                        Utilities.RawSayTo(item, text);
                }

                else
                    Utilities.RawSayAll(text);

                return EventEat.EatGame;
            }

            return EventEat.EatNone;
        }

        public override void OnStartGameType()
        {
            MAIN_ResetSpawnAction();

            base.OnStartGameType();
        }

        public override void OnExitLevel()
        {
            MAIN_ResetSpawnAction();
            base.OnExitLevel();
        }

        public void AC130All()
        {
            foreach (Entity player in Players)
            {
                player.TakeAllWeapons();
                player.GiveWeapon("ac130_105mm_mp");
                player.GiveWeapon("ac130_40mm_mp");
                player.GiveWeapon("ac130_25mm_mp");
                player.SwitchToWeaponImmediate("ac130_25mm_mp");
            }
        }

        public static int GetEntityNumber(Entity player)
        {
            return player.Call<int>("getentitynumber");
        }

        public void Mode(string dsrname, string map = "")
        {
            if (string.IsNullOrWhiteSpace(map))
                map = Call<string>("getDvar", "mapname");

            if (!string.IsNullOrWhiteSpace(MapRotation))
            {
                Log.Error("ERROR: Modechange already in progress.");
                return;
            }

            map = map.Replace("default:", "");
            using (System.IO.StreamWriter DSPLStream = new System.IO.StreamWriter("players2\\EX.dspl"))
            {
                DSPLStream.WriteLine(map + "," + dsrname + ",1000");
            }
            MapRotation = Call<string>("getDvar", "sv_maprotation");
            OnExitLevel();
            Utilities.ExecuteCommand("sv_maprotation EX");
            Utilities.ExecuteCommand("map_rotate");
            Utilities.ExecuteCommand("sv_maprotation " + MapRotation);
            MapRotation = "";
        }

        public Entity GetPlayer(string entref)
        {
            foreach (Entity player in Players)
            {
                if (player.EntRef.ToString() == entref)
                {
                    return player;
                }
            }
            return null;
        }

        public void ChangeTeam(Entity player, string team)
        {
            player.SetField("sessionteam", team);
            player.Notify("menuresponse", "team_marinesopfor", team);
        }
    }
}
