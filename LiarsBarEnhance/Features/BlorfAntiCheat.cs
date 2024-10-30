using System.Collections;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace LiarsBarEnhance.Features;

[HarmonyPatch]
public class BlorfAntiCheat
{
    private static List<int> RealCardTypes = new List<int>();
    private static int currentcard;

    [HarmonyPatch(typeof(BlorfGamePlay), "UserCode_RandomCards__Int32__Int32__Int32__Int32__Int32")]
    [HarmonyPrefix]
    public static bool RandomCardsPrefix(
        BlorfGamePlay __instance,
        int card1,
        int card2,
        int card3,
        int card4,
        int card5
    )
    {
        if (!__instance.isOwned)
            return true;
        __instance.CardTypes = new List<int>();
        RealCardTypes = new List<int>();
        var SetCardsCmd = AccessTools.Method(typeof(BlorfGamePlay), "SetCardsCmd");
        var joker = 0;
        for (int i = 0; i < 5; i++)
        {
            if (joker > 2)
            {
                __instance.CardTypes.Add(Random.Range(1, 3));
            }
            else
            {
                __instance.CardTypes.Add(Random.Range(1, 4));
                if (__instance.CardTypes[i] == 4)
                    joker++;
            }
        }
        RealCardTypes.Add(card1);
        RealCardTypes.Add(card2);
        RealCardTypes.Add(card3);
        RealCardTypes.Add(card4);
        RealCardTypes.Add(card5);
        SetCardsCmd.Invoke(__instance, null);
        return false;
    }

    [HarmonyPatch(typeof(BlorfGamePlay), "UserCode_SetCardsRpc")]
    [HarmonyPrefix]
    public static bool SetCardsRpcPrefix(BlorfGamePlay __instance)
    {
        var manager =
            AccessTools.Field(typeof(CharController), "manager").GetValue(__instance) as Manager;

        for (int i = 0; i < 5; i++)
        {
            currentcard = i;
            __instance.Cards[i].GetComponent<Card>().Devil = false;
            __instance.Cards[i].GetComponent<Card>().gameObject.layer = 0;
            __instance.Cards[i].GetComponent<Card>().cardtype = __instance.CardTypes[i];
            if (RealCardTypes[i] == -1)
            {
                RealCardTypes[i] = manager.BlorfGame.RoundCard;
                __instance.Cards[i].GetComponent<Card>().Devil = true;
            }
            __instance.Cards[i].GetComponent<Card>().Selected = false;
            __instance.Cards[i].GetComponent<Card>().gameObject.SetActive(true);
            __instance.Cards[i].GetComponent<Card>().SetCard();
        }
        return false;
    }

    [HarmonyPatch(typeof(Card), "SetCard")]
    [HarmonyPrefix]
    public static bool SetCardPrefix(Card __instance)
    {
        if (RealCardTypes[currentcard] == 1)
        {
            __instance.GetComponent<MeshFilter>().sharedMesh = Manager.Instance.BlorfGame.Card1;
        }
        else if (RealCardTypes[currentcard] == 2)
        {
            __instance.GetComponent<MeshFilter>().sharedMesh = Manager.Instance.BlorfGame.Card2;
        }
        else if (RealCardTypes[currentcard] == 3)
        {
            __instance.GetComponent<MeshFilter>().sharedMesh = Manager.Instance.BlorfGame.Card3;
        }
        else if (RealCardTypes[currentcard] == 4)
        {
            __instance.GetComponent<MeshFilter>().sharedMesh = Manager.Instance.BlorfGame.Card4;
        }
        if (RealCardTypes[currentcard] == -1)
        {
            __instance.GetComponent<MeshRenderer>().material = Manager.Instance.devil;
            __instance.Devil = true;
            if (Manager.Instance.BlorfGame.RoundCard == 1)
            {
                __instance.GetComponent<MeshFilter>().sharedMesh = Manager.Instance.BlorfGame.Card1;
                return false;
            }
            if (Manager.Instance.BlorfGame.RoundCard == 2)
            {
                __instance.GetComponent<MeshFilter>().sharedMesh = Manager.Instance.BlorfGame.Card2;
                return false;
            }
            if (Manager.Instance.BlorfGame.RoundCard == 3)
            {
                __instance.GetComponent<MeshFilter>().sharedMesh = Manager.Instance.BlorfGame.Card3;
            }
        }
        return false;
    }

    [HarmonyPatch(typeof(BlorfGamePlay), "ThrowCards")]
    [HarmonyPrefix]
    public static bool ThrowCardsPrefix(BlorfGamePlay __instance)
    {
        var waitforthreecardreset = AccessTools.Method(
            typeof(BlorfGamePlay),
            "waitforthreecardreset"
        );
        var ThrowCardsCmd = AccessTools.Method(typeof(BlorfGamePlay), "ThrowCardsCmd");
        var WaitforCheck = AccessTools.Method(typeof(BlorfGamePlay), "WaitforCheck");
        var WaitforCardThrow = AccessTools.Method(typeof(BlorfGamePlay), "WaitforCardThrow");

        var throwedField = AccessTools.Field(typeof(BlorfGamePlay), "throwed");
        var throwed = (bool)throwedField.GetValue(__instance);

        __instance.StartCoroutine((IEnumerator)waitforthreecardreset.Invoke(__instance, null));
        throwed = true;
        throwedField.SetValue(__instance, throwed);
        __instance.animator.SetBool("Throw", true);
        List<int> list = new List<int>();
        for (int i = 0; i < __instance.Cards.Count; i++)
        {
            if (
                __instance.Cards[i].gameObject.activeSelf
                && __instance.Cards[i].GetComponent<Card>().Selected
            )
            {
                if (__instance.Cards[i].GetComponent<Card>().Devil)
                {
                    list.Add(-1);
                }
                else
                {
                    list.Add(RealCardTypes[i]);
                }
            }
        }
        __instance.StartCoroutine((IEnumerator)WaitforCardThrow.Invoke(__instance, null));
        ThrowCardsCmd.Invoke(__instance, new object[] { list });
        __instance.StartCoroutine((IEnumerator)WaitforCheck.Invoke(__instance, null));
        return false;
    }
}
