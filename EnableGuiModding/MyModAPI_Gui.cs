using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;

using VRage.FileSystem;
using VRageMath;
using VRageRender;

using System.Runtime.Serialization.Formatters.Binary;
using Sandbox.Game;

namespace Sandbox.ModAPI
{
    public static class CopyHelper
    {
        static public T DeepCopy<T>(T obj)
        {
            BinaryFormatter s = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                s.Serialize(ms, obj);
                ms.Position = 0;
                T t = (T)s.Deserialize(ms);

                return t;
            }
        }
    }

    public class MyScreenManagerWrapper
    {
        public List<MyGuiScreenBase> ActiveScreens
        {
            get
            {
                FieldInfo info = typeof(MyScreenManager).GetField("m_screens", BindingFlags.NonPublic | BindingFlags.Static);
                return (info.GetValue(null) as List<MyGuiScreenBase>);
            }
        }

        public MyGuiScreenBase GetTop()
        {
            FieldInfo info = typeof(MyScreenManager).GetField("m_screens", BindingFlags.NonPublic | BindingFlags.Static);
            return (info.GetValue(null) as List<MyGuiScreenBase>).Last();
        }

        public MyGuiScreenBase GetScreenByName(string name)
        {
            MyGuiScreenBase screen = null;
            FieldInfo info = typeof(MyScreenManager).GetField("m_screens", BindingFlags.NonPublic | BindingFlags.Static);
            screen = (info.GetValue(null) as List<MyGuiScreenBase>).Find(x => x.GetFriendlyName().Equals(name));

            return screen;
        }

        public void ResetScreensToDefault()
        {
            //CustomHudScreen.ResetHud();
        }

        public void AddScreen(MyGuiScreenBase screen)
        {
            MyScreenManager.AddScreen(screen);
        }

        public void RemoveScreen(MyGuiScreenBase screen)
        {
            MyScreenManager.RemoveScreen(screen);
        }

        public void RecreateControls()
        {
            MyScreenManager.RecreateControls();
        }

        public void ChangeHud(MyGuiScreenBase gui)
        {

        }
    }

    public class MyRenderProxyWrapper
    {
        public void PreloadTexture(string inDirectory, bool recursive)
        {
            MyRenderProxy.PreloadTextures(inDirectory, recursive);
        }

        public void UnloadTexture(string textureName)
        {
            MyRenderProxy.UnloadTexture(textureName);
        }
    }

    public static class MyGuiGateway
    {


        public static MyRenderProxyWrapper Renderer = new MyRenderProxyWrapper();
    }
}
