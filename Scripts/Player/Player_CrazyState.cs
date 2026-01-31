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
    }

    public override void Skill()
    {
        base.Skill();
        if (player.crazyBubblePrefabs != null && player.crazyBubblePrefabs.Count > 0)
        {
            // Get a random prefab
            int index = Random.Range(0, player.crazyBubblePrefabs.Count);
            GameObject prefab = player.crazyBubblePrefabs[index];

            // Spawn at player's feet (position - 0.5y)
            Vector3 spawnPos = player.transform.position;
            spawnPos.y -= 0.5f;

            GameObject bubble = Object.Instantiate(prefab, spawnPos, Quaternion.identity);

            // Ensure it covers other layers
            SpriteRenderer sr = bubble.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = 20;
            }
            
            player.StartCrazyBubbleRoutine(bubble);
        }
    }
}
