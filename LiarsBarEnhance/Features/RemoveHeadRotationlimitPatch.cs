using HarmonyLib;

namespace LiarsBarEnhance.Features;

[HarmonyPatch]
public class RemoveHeadRotationlimitPatch
{
    [HarmonyPatch(typeof(CharController), "ClampAngle")]
    [HarmonyPrefix]
    public static void ClampAnglePrefix(float lfAngle, ref float __result, ref bool __runOriginal)
    {
        __runOriginal = false;
        
        var newAngle = lfAngle;
        
        if (newAngle < -360.0)
            newAngle += 360f;
        
        if (newAngle > 360.0)
            newAngle -= 360f;
        
        __result = newAngle;
    }
}