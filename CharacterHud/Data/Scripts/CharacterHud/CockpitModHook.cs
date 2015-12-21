
using Sandbox;
using Sandbox.ModAPI;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Definitions;
using Sandbox.Engine.Platform.VideoMode;
using Sandbox.Engine.Utils;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Cube;
using Sandbox.Game.GameSystems;
using Sandbox.Game.GUI.HudViewers;
using Sandbox.Game.Localization;
using Sandbox.Game.Screens.Helpers;
using Sandbox.Game.World;
using Sandbox.Game.Gui;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using VRage;
using VRage.Input;
using VRage.Utils;
using VRageMath;
using VRageRender;
using VRage.FileSystem;

using Color = VRageMath.Color;
using MyGuiConstants = Sandbox.Graphics.GUI.MyGuiConstants;
using Vector2 = VRageMath.Vector2;

namespace CharacterHudHook
{
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public class CharacterHudSession : MySessionComponentBase
    {
        public static bool init { get; private set; }

        public void Init()
        {
            init = true;
            /* hook in the hud */
            HudManager.Static.AddHud("character", new CharacterHud());
            HudManager.Static.Active = "character";

            MySandboxGame.Log.WriteLine("Character Hud Loaded");
        }

        private void WriteOutGUIs()
        {
            MySandboxGame.Log.WriteLine(MyScreenManager.GetGuiScreensForDebug().ToString());
        }

        public override void LoadData()
        {
            base.LoadData();
            if (!init)
                Init();
        }

        public override void BeforeStart()
        {
            base.BeforeStart();
        }

        protected override void UnloadData()
        {
            init = false;
        }

        public override void UpdateAfterSimulation()
        {
            base.UpdateAfterSimulation();
        }
    }
}
