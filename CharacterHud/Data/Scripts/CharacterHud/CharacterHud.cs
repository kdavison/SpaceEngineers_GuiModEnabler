using Sandbox.Common;
using Sandbox.Engine.Platform.VideoMode;
using Sandbox.Game.Gui;
using Sandbox.Game.Localization;
using Sandbox.Game.Screens.Helpers;
using Sandbox.Graphics.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage.Utils;
using VRageMath;

namespace CharacterHudHook
{
    class CharacterHud : MyGuiScreenHudBase
    {
        private MyGuiControlToolbar m_toolbarControl;

        public CharacterHud() : base()
        {
            RecreateControls(true);
        }

        public override void RecreateControls(bool constructor)
        {
            base.RecreateControls(constructor);

            m_toolbarControl = new MyGuiControlToolbar();
            m_toolbarControl.Position = new Vector2(0.5f, 0.99f);
            m_toolbarControl.OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_BOTTOM;
            m_toolbarControl.IsActiveControl = false;
            Elements.Add(m_toolbarControl);
        }

        public override string GetFriendlyName()
        {
            return "CharacterHudExample";
        }

        public override bool Draw()
        {
            if (!base.Draw())
                return false;

            m_toolbarControl.Visible = !MyHud.MinimalHud;

            if (MyHud.MinimalHud)
            {

            }

            return true;
        }
    }
}
