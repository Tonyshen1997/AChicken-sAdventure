using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightStarter : MonoBehaviour
{
    public void startBossFight()
    {
        Boss_AI boss = FindObjectOfType<Boss_AI>();
        boss.aiState = Boss_AI.AIState.Phase1;
    }
}
