using BepInEx;
using HarmonyLib;

namespace ExampleMod
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        private const string ModName = "InvincibilityMod";
        private const string ModAuthor  = "reddust9";
        private const string ModGuid = "com.reddust9.invincibility";
        private const string ModVersion = "1.0.0";
        internal void Awake()
        {
            // Creating new harmony instance
            var harmony = new Harmony(ModGuid);
            var mDamage = typeof(SpiderHealthSystem).GetMethod("Damage");
            var mDisintegrate = typeof(SpiderHealthSystem).GetMethod("Disintegrate");
            var hmPrefix = new HarmonyMethod(typeof(Patch1).GetMethod("Prefix"));
            if (mDamage == null || mDisintegrate == null)
            {
                throw new Exception("Unable to get methods to patch");
            }

            harmony.Patch(mDamage, prefix: hmPrefix);
            harmony.Patch(mDisintegrate, prefix: hmPrefix);
            
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
        }

        private void Update()
        {
            SurvivalMode.instance.Lives = 99;
        }
        public class Patch1
        {
            public static bool Prefix()
            {
                return false;
            }
        }
    }
}
