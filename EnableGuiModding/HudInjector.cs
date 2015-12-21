using Sandbox.Game;
using Sandbox.Game.Gui;
using Sandbox.Graphics.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sandbox.ModAPI;
using Sandbox;
using EnableGuiModding;

namespace Sandbox.ModAPI
{
    public class HudInjector : MyGuiScreenBase
    {
        private string m_activeHud = null;

        public HudInjector()
        {
            CanBeHidden = true;
            CanHideOthers = false;
            CanHaveFocus = false;
            m_drawEvenWithoutFocus = true;
            m_closeOnEsc = false;
        }

        public override void LoadData()
        {
            MySandboxGame.Log.WriteLine("HudInjector.LoadData - START");

            base.LoadData();

            HudManager.Static.Start();

            MySandboxGame.Log.WriteLine("HudInjector.LoadData - STOP");
        }

        public override void UnloadData()
        {
            MySandboxGame.Log.WriteLine("HudInjector.UnloadData - START");

            base.UnloadData();

            if (m_activeHud != null)
                m_activeHud = null;
            HudManager.Static.Stop();

            MySandboxGame.Log.WriteLine("HudInjector.UnloadData STOP");
        }

        public override string GetFriendlyName()
        {
            return "HudInjector";
        }

        private void CloseAllAbove(MyGuiScreenBase target)
        {
            //don't close MyGuiScreenGamePlay - thats the game itself basically.
            //don't close HudInjector - need this to actually load huds
            //don't close the current hud, remove it gracefully later

            List<MyGuiScreenBase> m_activeScreens;

            FieldInfo info = typeof(MyScreenManager).GetField("m_screens", BindingFlags.NonPublic | BindingFlags.Static);
            m_activeScreens = (info.GetValue(null) as List<MyGuiScreenBase>);

            for(int index=m_activeScreens.Count-1; index > 0; --index)
            {
                if (m_activeScreens[index] == target)
                    return;
                MyScreenManager.RemoveScreen(m_activeScreens[index]);
            }
        }

        public override bool Draw()
        {
            if (!base.Draw())
                return false;

            if(IsLoaded)
            {
                if(m_activeHud == null)
                {
                    if(HudManager.Static.Active == null)
                    {
                        HudManager.Static.Active = "default";
                    }
                    m_activeHud = HudManager.Static.Active;

                    MySandboxGame.Log.WriteLine("1 ACTIVE: " + m_activeHud + " NULL?: " + (HudManager.Static.GetHud(m_activeHud) == null));

                    MyGuiSandbox.AddScreen(HudManager.Static.GetHud(m_activeHud));
                }
                else
                {
                    if(m_activeHud != HudManager.Static.Active)
                    {
                        CloseAllAbove(HudManager.Static.GetHud(m_activeHud));
                        MyGuiSandbox.RemoveScreen(HudManager.Static.GetHud(m_activeHud));

                        MySandboxGame.Log.WriteLine("2.1 OLD: " + m_activeHud + " NEW: " + HudManager.Static.Active);

                        m_activeHud = HudManager.Static.Active;

                        MySandboxGame.Log.WriteLine("2.2 ACTIVE: " + m_activeHud + " NULL?: " + (HudManager.Static.GetHud(m_activeHud) == null));

                        MyGuiSandbox.AddScreen(HudManager.Static.GetHud(m_activeHud));
                    }
                }
            }

            return true;
        }

        public override bool CloseScreen()
        {
            MySandboxGame.Log.WriteLine("HudInjector.CloseScreen - START");

            MySandboxGame.Log.WriteLine("HudInjector.CloseScreen - STOP");

            return true;
        }
    }
}
