using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CrazyState : EntityState
{   
    
    public Player_CrazyState(Player player, StateMachine stateMachine, string stateName):base(player, stateMachine, stateName)
    {
        
    }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        player.sr.sprite = player.crazySprite;
        SetCooldown(0.5f);
    }

    public override void Skill()
    {
        base.Skill();
        if (!CanUseSkill()) return;
        if (player.crazyBubblePrefabs != null && player.crazyBubblePrefabs.Count > 0)
        {
            int index = Random.Range(0, player.crazyBubblePrefabs.Count);
            GameObject prefab = player.crazyBubblePrefabs[index];

            Vector3 spawnPos = player.transform.position;
            spawnPos.y -= (0.49f + 0.7f);

            GameObject bubble = Object.Instantiate(prefab, spawnPos, Quaternion.identity);

            SpriteRenderer sr = bubble.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = 20;
            }
            
            player.StartCrazyBubbleRoutine(bubble);
            StartCooldown();
            if (player.Health != null)
            {
                player.Health.Add(player.playerCrazyReward);
            }
        }
    }
}
