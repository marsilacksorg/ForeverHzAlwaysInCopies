// using BepInEx;
using GorillaNetworking;
using HarmonyLib;
using System;
using System.Threading;
using UnityEngine;
using MelonLoader;
using easyInputs;
using ForeverHz;
// using Valve.VR;

[assembly: MelonInfo(typeof(ForeverHz.Plugin), PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
[assembly: MelonGame(null, null)]
namespace ForeverHz
{
    public class Plugin : MelonMod
    {
        public static Plugin instance;
        public int HZ = 45;
        public bool gripOnly = true;
        public override void OnEarlyInitializeMelon() =>
            instance = this;
        
        // Yes I did remove the OnGUI for standalone modding reasons but I might add a ingame gui in the future to change the sliders but for now its auto set to 45HZs

        /*void OnGUI()
        {
            HZ = (int)GUI.HorizontalSlider(new Rect(4f, 4f, 400f, 40f), HZ, 0f, 150f);
            GUI.Label(new Rect(414f, 1f, 144f, 60f), "HZ: " + HZ.ToString());
            GUI.Label(new Rect(4f, 40f, 900f, 40f), "HZ slider by @goldentrophy");
            gripOnly = GUI.Toggle(new Rect(300f, 40f, 1080f, 40f), gripOnly, "Right Grip");
        }*/

        private static float lastTime;
        public override void OnUpdate()
        {
            if (gripOnly && !EasyInputs.GetGripButtonDown(EasyHand.RightHand))
                return;

            float targetDelta = 1f / HZ;
            float elapsed = Time.realtimeSinceStartup - lastTime;

            if (elapsed < targetDelta)
            {
                int sleepMs = Mathf.FloorToInt((targetDelta - elapsed) * 1000);
                if (sleepMs > 0)
                    Thread.Sleep(sleepMs);
            }

            lastTime = Time.realtimeSinceStartup;
        }
    }
}
