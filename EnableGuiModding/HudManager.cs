using EnableGuiModding;
using Sandbox;
using Sandbox.Game;
using Sandbox.Graphics.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.ModAPI
{
    public class HudManager
    {
        private Dictionary<string, MyGuiScreenBase> m_huds;
        private string m_active;

        private static HudManager m_instance = null;

        public static HudManager Static
        {
            get
            {
                if (m_instance == null)
                    m_instance = new HudManager();

                return m_instance;
            }
        }

        public HudManager()
        {
            m_huds = new Dictionary<string, MyGuiScreenBase>();
        }

        public void Start()
        {
            m_huds.Add("default", MyGuiSandbox.CreateScreen(EnableGuiMods.HudScreen));
        }

        public void Stop()
        {
            if(m_huds != null)
            {
                m_active = null;
                m_huds.Clear();
            }
        }

        public string Active
        {
            get
            {
                return m_active;
            }
            set
            {
                if(!String.IsNullOrEmpty(value))
                {
                    if (m_active != value)
                        m_active = value;
                    MySandboxGame.Log.WriteLine("Active HUD set to: " + value);
                }
            }
        }

        public MyGuiScreenBase GetHud(string name)
        {
            if(m_huds.ContainsKey(name))
            {
                return m_huds[name];
            }
            else
            {
                return null;
            }
        }

        public void AddHud(string name, MyGuiScreenBase hud)
        {
            StringBuilder log = new StringBuilder("HudManager - ");
            if(!String.IsNullOrEmpty(name))
            {
                if(hud != null)
                {
                    if (!m_huds.ContainsValue(hud))
                    {
                        if (m_huds.ContainsKey(name))
                        {
                            m_huds[name] = hud;
                            log.Append("HUD (" + name + ") replaced");
                        }
                        else
                        {
                            m_huds.Add(name, hud);
                            log.Append("HUD (" + name + ") added");
                        }
                    }
                    else
                    {
                        log.Append("Already have a hud: " + hud.GetFriendlyName());
                    }
                }
                else
                {
                    log.Append("Invalid hud: (null)");
                }
            }
            else
            {
                log.Append("Invalid name for hud: (null)");
            }
            MySandboxGame.Log.WriteLine(log.ToString());
        }

        public string ListHuds()
        {
            StringBuilder list = new StringBuilder("HudManager: ");

            if(m_huds != null)
            {
                foreach(var entry in m_huds)
                {
                    list.Append(entry.Key);
                    list.Append(" - ");
                    list.Append(entry.Value.GetFriendlyName());
                    list.Append(Environment.NewLine);
                }
            }

            return list.ToString();
        }

    }
}
