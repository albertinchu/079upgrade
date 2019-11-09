using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;
using MEC;
using Smod2.API;

namespace upgrade079
{
    partial class Commands1 : IEventHandlerCallCommand, IEventHandler079AddExp, IEventHandlerElevatorUse, IEventHandler079StartSpeaker, IEventHandlerWaitingForPlayers,
        IEventHandler079StopSpeaker, IEventHandlerSetRole
    {
        static Dictionary<string, bool> Pasivaa = new Dictionary<string, bool>();
        static Dictionary<string, bool> Speakers = new Dictionary<string, bool>();
        bool elevatoss = false;
     

        private IEnumerator<float> elevators()
        {
            elevatoss = true;
            yield return MEC.Timing.WaitForSeconds(20f);
            elevatoss = false;

        }
        private IEnumerator<float> Cooldown079(Player player)
        {

            yield return MEC.Timing.WaitForSeconds(120f);
            Pasivaa[player.SteamId] = true;

        }
        // cooldown de habilidad
        private IEnumerator<float> Cooldown0792(Player player)
        {

            yield return MEC.Timing.WaitForSeconds(60f);
            Pasivaa[player.SteamId] = true;

        }
        // cooldown de habilidad
        private IEnumerator<float> Cooldown0793(Player player)
        {

            yield return MEC.Timing.WaitForSeconds(30f);
            Pasivaa[player.SteamId] = true;

        }

        public void On079AddExp(Player079AddExpEvent ev)
        {
            float Xp;
            Xp = ev.ExpToAdd;
            
            if (ev.Player.Scp079Data.Level > 3)
            {
                
                ev.Player.Scp079Data.MaxAP = ev.Player.Scp079Data.MaxAP + (Xp / 10);

            }
        }

        public void OnCallCommand(PlayerCallCommandEvent ev)
        {
            if (ev.Command.StartsWith("up079info"))
            {
                ev.Player.SendConsoleMessage("079 Upgrade permite al 079 controlar con mayor facilidad toda la instalación, agrega nuevas habilidades al 079, 1º .nukeoff" +
                    "necesitas ser tier 4 y por 150 de energía, para la nuke pero no la desactiva (120s de cooldown)", "blue");
                ev.Player.SendConsoleMessage("2º .elevatorsoff te permite detener todos los ascensores de forma que solo los SCPS pueden usarlos" +
                      "necesitas ser tier 3 y cuesta 100 de energía (60s de cooldown)", "blue");
                ev.Player.SendConsoleMessage("3º .nukenow te permite detonar la nuke al instante matando a todos los seres vivos dentro de la instalación, " +
                     "necesitas ser tier 5 y cuesta 300 de energía", "red");
                ev.Player.SendConsoleMessage("4º .doorsclosed te permite cerrar todas las puertas de la instalación, " +
                     "necesitas ser tier 3 y cuesta 125 de energía, no tiene cooldown", "blue");
                ev.Player.SendConsoleMessage("5º .nanobots te permite atacar a un jugador aleatorio con una armada de nanobots que segun tu tier hacen mas o menos daño, " +
                   "Tier 1-2: coste 100 de energía 50 de daño, cooldown (60s), Tier 3-4: coste 50 de energía 50 de daño, cooldown (60s), Tier 5: coste 50 de energía 75 de daño y cooldown (30s)", "blue");
            }
            if (ev.Command.StartsWith("elevatorsoff"))
            {
                if (ev.Player.TeamRole.Role != Role.SCP_079) { ev.ReturnMessage = "Tu no eres SCP-079, pero buen inteneto ;)"; }
                if (ev.Player.TeamRole.Role == Role.SCP_079)
                {
                    if (ev.Player.Scp079Data.AP < 100) { ev.ReturnMessage = "Necesitas mas Energía (200)"; }
                    if ((Pasivaa[ev.Player.SteamId] == false) && (ev.Player.Scp079Data.AP >= 200)) { ev.ReturnMessage = "Habilidad en cooldown"; }
                    if ((ev.Player.Scp079Data.AP >= 100) && (Pasivaa[ev.Player.SteamId] == true))
                    {
                        ev.Player.Scp079Data.AP -= 100;
                        ev.Player.Scp079Data.Exp += 50;
                        if (ev.Player.Scp079Data.Level >= 4) { ev.Player.Scp079Data.MaxAP += 7; }
                        ev.Player.SendConsoleMessage("Protocolo 496E63656E64696F2064657465637461646F2C20616E756C616E646F20617363656E736F72657320 ejecutado", "blue");
                        ev.ReturnMessage = "Protocolo 496E63656E64696F2064657465637461646F2C20616E756C616E646F20617363656E736F72657320 ejecutado";
                        int p = (int)System.Environment.OSVersion.Platform;
                        if ((p == 4) || (p == 6) || (p == 128))
                        {
                            MEC.Timing.RunCoroutine(Cooldown0792(ev.Player), MEC.Segment.FixedUpdate);

                        }
                        else { MEC.Timing.RunCoroutine(Cooldown0792(ev.Player), 1); }

                        if ((p == 4) || (p == 6) || (p == 128))
                        {
                            MEC.Timing.RunCoroutine(elevators(), MEC.Segment.FixedUpdate);

                        }
                        else { MEC.Timing.RunCoroutine(elevators(), 1); }
                        Pasivaa[ev.Player.SteamId] = false;
                    }
                }

            }

            if (ev.Command.StartsWith("nukeoff"))
            {
                if (ev.Player.TeamRole.Role != Role.SCP_079) { ev.ReturnMessage = "Tu no eres SCP-079, pero buen inteneto ;)"; }
                if (ev.Player.TeamRole.Role == Role.SCP_079)
                {
                    if (ev.Player.Scp079Data.Level < 2) { ev.ReturnMessage = "Necesitas mas nivel"; }
                    if (ev.Player.Scp079Data.Level >= 2)
                    {
                        if (ev.Player.Scp079Data.AP < 200) { ev.ReturnMessage = "Necesitas mas Energía (200)"; }
                        if ((Pasivaa[ev.Player.SteamId] == false) && (ev.Player.Scp079Data.AP >= 200)) { ev.ReturnMessage = "Habilidad en cooldown"; }

                        if ((ev.Player.Scp079Data.AP >= 200) && (Pasivaa[ev.Player.SteamId] == true))
                        {
                            ev.Player.Scp079Data.AP -= 200;
                            ev.Player.SendConsoleMessage("Procedimiento 70726F746F636F6C6F206465206175746F646573747275636369F36E Cancelado.", "blue");
                            ev.ReturnMessage = "Procedimiento 70726F746F636F6C6F206465206175746F646573747275636369F36E Cancelado. ";
                            Pasivaa[ev.Player.SteamId] = false;
                            int p = (int)System.Environment.OSVersion.Platform;
                            if ((p == 4) || (p == 6) || (p == 128))
                            {
                                MEC.Timing.RunCoroutine(Cooldown079(ev.Player), MEC.Segment.FixedUpdate);

                            }
                            else { MEC.Timing.RunCoroutine(Cooldown079(ev.Player), 1); }
                            PluginManager.Manager.Server.Map.StopWarhead();
                            ev.Player.Scp079Data.Exp += 100;
                            if (ev.Player.Scp079Data.Level >= 4) { ev.Player.Scp079Data.MaxAP += 10; }
                        }
                        if (Pasivaa[ev.Player.SteamId] == false) { ev.ReturnMessage = "habilidad en cooldown"; }
                    }

                }
            }
            if (ev.Command.StartsWith("doorsclosed")) 
            {
                if (ev.Player.TeamRole.Role != Role.SCP_079) { ev.ReturnMessage = "Tu no eres SCP-079, pero buen inteneto ;)"; }
                if (ev.Player.TeamRole.Role == Role.SCP_079)
                {
                    if (ev.Player.Scp079Data.Level < 2) { ev.ReturnMessage = "Necesitas mas nivel"; }
                    if (ev.Player.Scp079Data.Level >= 2)
                    {
                        if (ev.Player.Scp079Data.AP < 125) { ev.ReturnMessage = "Necesitas mas Energía (125)"; }


                        if ((ev.Player.Scp079Data.AP >= 125))
                        {
                            ev.Player.Scp079Data.AP -= 125;
                            ev.Player.SendConsoleMessage("Puertas cerradas.", "blue");
                            ev.ReturnMessage = "Puertas cerradas. ";
                            ev.Player.Scp079Data.Exp += 25;
                            if(ev.Player.Scp079Data.Level >= 4) { ev.Player.Scp079Data.MaxAP += 12; }
                            foreach(Smod2.API.Door door in PluginManager.Manager.Server.Map.GetDoors())
                            {
                                door.Open = false;                      
                            }


                        }
                    }

                }

            }
            if (ev.Command.StartsWith("nanobots"))
            {
                if (ev.Player.TeamRole.Role != Role.SCP_079) { ev.ReturnMessage = "Tu no eres SCP-079, pero buen inteneto ;)"; }
                if (ev.Player.TeamRole.Role == Role.SCP_079)
                {


                    if (ev.Player.Scp079Data.Level < 3)
                    {
                        if (ev.Player.Scp079Data.Level < 1)
                        {
                            if (ev.Player.Scp079Data.AP < 100) { ev.ReturnMessage = "Necesitas mas Energía (100)"; }

                            if ((Pasivaa[ev.Player.SteamId] == false) && (ev.Player.Scp079Data.AP >= 100)) { ev.ReturnMessage = "Habilidad en cooldown"; }

                            if ((ev.Player.Scp079Data.AP >= 100) && (Pasivaa[ev.Player.SteamId] == true))
                            {
                                ev.Player.Scp079Data.AP -= 100;
                                ev.Player.SendConsoleMessage("Enviando nanobots al ataque.", "blue");
                                ev.ReturnMessage = "Enviando nanobots al ataque .";
                                Pasivaa[ev.Player.SteamId] = false;
                                int p = (int)System.Environment.OSVersion.Platform;
                                if ((p == 4) || (p == 6) || (p == 128))
                                {
                                    MEC.Timing.RunCoroutine(Cooldown0792(ev.Player), MEC.Segment.FixedUpdate);

                                }
                                else { MEC.Timing.RunCoroutine(Cooldown0792(ev.Player), 1); }
                                System.Random playrs = new System.Random();
                                int posic = playrs.Next(0, PluginManager.Manager.Server.GetPlayers().Count);
                                while ((PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team == Smod2.API.Team.SPECTATOR) || (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Role == Role.SCP_079) || (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team == Smod2.API.Team.NONE))
                                {
                                    if (posic > PluginManager.Manager.Server.NumPlayers)
                                    {
                                        posic = 0;
                                    }
                                    posic = posic + 1;
                                }
                                if (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team == Smod2.API.Team.SCP) { PluginManager.Manager.Server.GetPlayers()[posic].AddHealth(50); }

                                if (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team != Smod2.API.Team.SCP)
                                {
                                    if (PluginManager.Manager.Server.GetPlayers()[posic].GetHealth() <= 50)
                                    {
                                        PluginManager.Manager.Server.GetPlayers()[posic].Kill(DamageType.TESLA);
                                        ev.Player.Scp079Data.Exp += 30;
                                        if (ev.Player.Scp079Data.Level >= 4) { ev.Player.Scp079Data.MaxAP += 3; }
                                    }
                                    if (PluginManager.Manager.Server.GetPlayers()[posic].GetHealth() > 50)
                                    {
                                        PluginManager.Manager.Server.GetPlayers()[posic].AddHealth(-50);
                                    }


                                }
                                ev.Player.Scp079Data.Exp += 30;
                                if (ev.Player.Scp079Data.Level >= 4) { ev.Player.Scp079Data.MaxAP += 10; }
                            }
                            if (Pasivaa[ev.Player.SteamId] == false) { ev.ReturnMessage = "habilidad en cooldown"; }
                        }
                        if((ev.Player.Scp079Data.Level < 4)&&(ev.Player.Scp079Data.Level >= 2)) 
                        {

                            if (ev.Player.Scp079Data.AP < 50) { ev.ReturnMessage = "Necesitas mas Energía (50)"; }

                            if ((Pasivaa[ev.Player.SteamId] == false) && (ev.Player.Scp079Data.AP >= 50)) { ev.ReturnMessage = "Habilidad en cooldown"; }

                            if ((ev.Player.Scp079Data.AP >= 50) && (Pasivaa[ev.Player.SteamId] == true))
                            {
                                ev.Player.Scp079Data.AP -= 50;
                                ev.Player.SendConsoleMessage("Enviando nanobots al ataque.", "blue");
                                ev.ReturnMessage = "Enviando nanobots al ataque .";
                                Pasivaa[ev.Player.SteamId] = false;
                                int p = (int)System.Environment.OSVersion.Platform;
                                if ((p == 4) || (p == 6) || (p == 128))
                                {
                                    MEC.Timing.RunCoroutine(Cooldown0792(ev.Player), MEC.Segment.FixedUpdate);

                                }
                                else { MEC.Timing.RunCoroutine(Cooldown0792(ev.Player), 1); }
                                System.Random playrs = new System.Random();
                                int posic = playrs.Next(0, PluginManager.Manager.Server.GetPlayers().Count);
                                while ((PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team == Smod2.API.Team.SPECTATOR) || (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Role == Role.SCP_079) || (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team == Smod2.API.Team.NONE))
                                {
                                    if (posic > PluginManager.Manager.Server.NumPlayers)
                                    {
                                        posic = 0;
                                    }
                                    posic = posic + 1;
                                }
                                if (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team == Smod2.API.Team.SCP) { PluginManager.Manager.Server.GetPlayers()[posic].AddHealth(50); }

                                if (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team != Smod2.API.Team.SCP)
                                {
                                    if (PluginManager.Manager.Server.GetPlayers()[posic].GetHealth() <= 50)
                                    {
                                        PluginManager.Manager.Server.GetPlayers()[posic].Kill(DamageType.TESLA);
                                        ev.Player.Scp079Data.Exp += 30;
                                        if (ev.Player.Scp079Data.Level >= 4) { ev.Player.Scp079Data.MaxAP += 3; }
                                    }
                                    if (PluginManager.Manager.Server.GetPlayers()[posic].GetHealth() > 50)
                                    {
                                        PluginManager.Manager.Server.GetPlayers()[posic].AddHealth(-50);
                                    }


                                }
                                ev.Player.Scp079Data.Exp += 30;
                                if (ev.Player.Scp079Data.Level >= 4) { ev.Player.Scp079Data.MaxAP += 10; }
                            }
                            if (Pasivaa[ev.Player.SteamId] == false) { ev.ReturnMessage = "habilidad en cooldown"; }




                        }
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (ev.Player.Scp079Data.Level >= 4)
                    {
                        if (ev.Player.Scp079Data.AP < 50) { ev.ReturnMessage = "Necesitas mas Energía (50)"; }

                        if ((Pasivaa[ev.Player.SteamId] == false) && (ev.Player.Scp079Data.AP >= 50)) { ev.ReturnMessage = "Habilidad en cooldown"; }

                        if ((ev.Player.Scp079Data.AP >= 50) && (Pasivaa[ev.Player.SteamId] == true))
                        {
                            ev.Player.Scp079Data.AP -= 50;
                            ev.Player.SendConsoleMessage("Enviando nanobots mejorados al ataque.", "red");
                            ev.ReturnMessage = "Enviando nanobots mejorados al ataque .";
                            Pasivaa[ev.Player.SteamId] = false;
                     
                            
                             
                           
                                int p = (int)System.Environment.OSVersion.Platform;
                                if ((p == 4) || (p == 6) || (p == 128))
                                {
                                    MEC.Timing.RunCoroutine(Cooldown0793(ev.Player), MEC.Segment.FixedUpdate);

                                }
                                else { MEC.Timing.RunCoroutine(Cooldown0793(ev.Player), 1); }
                            

                            System.Random playrs = new System.Random();
                            int posic = playrs.Next(0, PluginManager.Manager.Server.GetPlayers().Count);
                            while ((PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team == Smod2.API.Team.SPECTATOR) || (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Role == Role.SCP_079) || (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team == Smod2.API.Team.NONE)) { if (posic > PluginManager.Manager.Server.NumPlayers) { posic = 0; } posic = posic + 1; }
                            if (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team == Smod2.API.Team.SCP) { PluginManager.Manager.Server.GetPlayers()[posic].AddHealth(50); }

                            if (PluginManager.Manager.Server.GetPlayers()[posic].TeamRole.Team != Smod2.API.Team.SCP)
                            {
                                if (PluginManager.Manager.Server.GetPlayers()[posic].GetHealth() <= 75)
                                {
                                    PluginManager.Manager.Server.GetPlayers()[posic].Kill(DamageType.TESLA);
                                    ev.Player.Scp079Data.Exp += 30;
                                    if (ev.Player.Scp079Data.Level >= 4) { ev.Player.Scp079Data.MaxAP += 3; }
                                }
                                if (PluginManager.Manager.Server.GetPlayers()[posic].GetHealth() > 75)
                                {
                                    PluginManager.Manager.Server.GetPlayers()[posic].AddHealth(-75);
                                }


                            }
                            ev.Player.Scp079Data.Exp += 30;
                            if (ev.Player.Scp079Data.Level >= 4) { ev.Player.Scp079Data.MaxAP += 15; }
                        }
                        if (Pasivaa[ev.Player.SteamId] == false) { ev.ReturnMessage = "habilidad en cooldown"; }
                    }

                }
            }
            if (ev.Command.StartsWith("nukenow"))
            {
                if (ev.Player.TeamRole.Role != Role.SCP_079) { ev.ReturnMessage = "Tu no eres SCP-079, pero buen inteneto ;)"; }
                if (ev.Player.TeamRole.Role == Role.SCP_079)
                {
                    if (ev.Player.Scp079Data.AP < 300) { ev.ReturnMessage = "Necesitas mas Energía (300)"; }
                    if (ev.Player.Scp079Data.AP >= 300)
                    {
                        ev.Player.Scp079Data.AP -= 400;
                        ev.Player.SendConsoleMessage("Lo importante es ganar... no?", "red");
                        ev.ReturnMessage = "..., Lo importante es ganar ....";
                        PluginManager.Manager.Server.Map.DetonateWarhead();
                    }
                }

            }

        }

        public void OnElevatorUse(PlayerElevatorUseEvent ev)
        {
            if (elevatoss)
            {
                if (ev.Player.TeamRole.Team != Smod2.API.Team.SCP) { ev.AllowUse = false; }


            }
        }

        public void On079StartSpeaker(Player079StartSpeakerEvent ev)
        {
            if(Speakers.ContainsKey(ev.Player.SteamId)) 
            {
                if (Speakers[ev.Player.SteamId] == false) 
                {
                    Speakers[ev.Player.SteamId] = true;

                    int p = (int)System.Environment.OSVersion.Platform;
                    if ((p == 4) || (p == 6) || (p == 128))
                    {
                        MEC.Timing.RunCoroutine(xpspeaker(ev.Player), MEC.Segment.FixedUpdate);

                    }
                    else { MEC.Timing.RunCoroutine(xpspeaker(ev.Player), 1); }
                }


            }
        }

        private IEnumerator<float> xpspeaker(Player player)
        {
            while (Speakers[player.SteamId] == true)
            {
                yield return MEC.Timing.WaitForSeconds(1f);
                player.Scp079Data.Exp += 1;
            }       
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            elevatoss = false;
            Speakers.Clear();
            Pasivaa.Clear();
            
        }

        public void On079StopSpeaker(Player079StopSpeakerEvent ev)
        {
            if (Speakers.ContainsKey(ev.Player.SteamId))
            {
                if (Speakers[ev.Player.SteamId] == true)
                {
                    Speakers[ev.Player.SteamId] = false;


                }


            }
        }

        public void OnSetRole(PlayerSetRoleEvent ev)
        {
           

            if (ev.Player.TeamRole.Role == Role.SCP_079) 
            {
                ev.Player.PersonalBroadcast(10, "079Upgrade: puedes usar los comandos: .nukeoff .nukenow .nanobots .doorsclosed y .elevatorsoff", false);
                if (!Pasivaa.ContainsKey(ev.Player.SteamId)) { Pasivaa.Add(ev.Player.SteamId, true); Speakers.Add(ev.Player.SteamId, false); }
            }
        }
    }
}
