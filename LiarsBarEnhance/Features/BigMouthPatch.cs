using HarmonyLib;
using UnityEngine;

namespace LiarsBarEnhance.Features;

[HarmonyPatch]
public class BigMouthPatch
{
    [HarmonyPatch(typeof(FaceAnimator), "Update")]
    [HarmonyPostfix]
    public static void UpdatePostfix(FaceAnimator __instance)
    {
        if (!__instance.isOwned)
            return;

        Vector3 pos = __instance.Mouth.transform.localPosition;
        if (Input.GetKey(KeyCode.O))
            __instance.Mouth.transform.localEulerAngles = new Vector3(pos.x, pos.y, 300f);
    }
}