using Smod2;
using Smod2.Attributes;

namespace upgrade079
{
    [PluginDetails(
        author = "Albertinchu ",
        name = "Passivesandskills2",
        description = "Que pasará si dejamos que los jugadores tengan poderes...",
        id = "albertinchu.079upgrade",
        version = "3.5.0",
        SmodMajor = 3,
        SmodMinor = 4,
        SmodRevision = 0
        )]
    public class _079upgrade : Plugin
    {

        public override void OnDisable()
        {
            this.Info("079upgrade - Desactivado");
        }

        public override void OnEnable()
        {
            this.Info("079upgrade - Activado para mas información usa .079upgrade.");
        }

        public override void Register()
        {


            this.AddEventHandlers(new Commands1(this));
 


        }
        public void RefreshConfig()
        {


        }
    }

}