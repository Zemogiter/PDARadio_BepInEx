using System.Reflection;
using HarmonyLib;
using Story;
using UnityEngine;
using BepInEx;

namespace PDARadioBepInEx
{

    [BepInPlugin("com.GruffCassquatch.PDARadioBepInEx", "PDARadioBepInEx", "1.0.0.0")]

    public class PDARadioBepInEx : BaseUnityPlugin
    {
        private void Start()
        {
            var harmony = new Harmony("com.GruffCassquatch.PDARadioBepInEx");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
    [HarmonyPatch(typeof(StoryGoalManager))]
    [HarmonyPatch("InvokePendingMessageEvent")]
    internal class StoryGoalManager_InvokePendingMessageEvent_Patch
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            if (GameObject.Find("EscapePod").transform.Find("ModulesRoot/RadioRoot/Radio(Clone)").gameObject.GetComponent<LiveMixin>().IsFullHealth())
            {
                StoryGoalManager.main.Invoke("ExecutePendingRadioMessage", 0f);
            }
        }
    }

}
