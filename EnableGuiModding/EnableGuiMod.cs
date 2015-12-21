using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage.Plugins;
using VRage.Compiler;
using VRage.FileSystem;
using Sandbox;
using Sandbox.ModAPI;
using Sandbox.Game;
using Sandbox.Graphics.GUI;
using Sandbox.Game.World;

namespace EnableGuiModding
{
    public class EnableGuiMods : IPlugin
    {
        private static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public void Dispose()
        {

        }

        string getPath(string assembly)
        {
            return Path.Combine(MyFileSystem.ExePath, assembly);
        }

        public static Type HudScreen = null;

        public void Init(object gameInstance)
        {
            /* Add the new Gui Mod API to the path */
            IlCompiler.Options.ReferencedAssemblies.Add(getPath("EnableGuiModding.dll"));

            /* Allow GUI Stuff */
            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Graphics.MyGuiManager));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Graphics.GUI.MyScreenManager));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Graphics.GUI.MyGuiControlBlockInfo));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Game.Gui.MyGuiScreenHudBase));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Game.GUI.HudViewers.MyHudControlChat));

            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Game.Localization.MySpaceTexts));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(MyCommonTexts));

            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Game.Screens.MyGuiScreenBriefing));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Game.Screens.Helpers.MyGuiControlToolbar));

            IlChecker.AllowNamespaceOfTypeCommon(typeof(VRage.Input.MyInput));

            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Engine.Utils.MyFakes));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Game.World.MySession));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Engine.Platform.VideoMode.MyAspectRatio));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(MySandboxGame));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Game.GameSystems.MyGravityProviderSystem));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(VRageRender.MyRenderProxy));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(Sandbox.Game.Entities.Cube.MyEntityOreDeposit));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(VRage.FileSystem.MyFileSystem));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(System.IO.StreamReader));

            IlChecker.AllowNamespaceOfTypeModAPI(typeof(MyGuiGateway));
            IlChecker.AllowNamespaceOfTypeCommon(typeof(MyGuiGateway));

            /* Hook in some HUD Stuff */
            HudScreen = DeepClone(MyPerGameSettings.GUI.HUDScreen);
            MyPerGameSettings.GUI.HUDScreen = typeof(HudInjector); //Set the "default" HUD to the custom class

            MySandboxGame.Log.WriteLine("Gui Modding Enabled");
        }

        int log_count = 0;
        public void Update()
        {
            if ((log_count + 1) % 1000 == 0)
            {
                MySandboxGame.Log.WriteLine(MyScreenManager.GetGuiScreensForDebug().ToString());
                log_count = 0;
            }
        }
    }
}
