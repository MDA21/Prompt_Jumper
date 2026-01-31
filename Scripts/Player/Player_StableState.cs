using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StableState : EntityState
{   
    
    public Player_StableState(Player player, StateMachine stateMachine, string stateName):base(player, stateMachine, stateName)
    {
        
    }
    

    public override void Enter()
    {
        base.Enter();
        player.sr.sprite = player.stableSprite;
    }

    public override void Skill()
    {
        base.Skill();
        if (player.stableBubblePrefabs != null && player.stableBubblePrefabs.Count > 0)
        {
            // Get a random prefab from the list
            int index = Random.Range(0, player.stableBubblePrefabs.Count);
            GameObject prefab = player.stableBubblePrefabs[index];

            // Spawn at player's feet (position)
            // Assuming the pivot is center, feet might be position - half height? 
            // Or just use position and let the bubble prefab handle its own pivot.
            // User said "at feet", I'll use transform.position for now, assuming the bubble is ground-aligned or player pivot is bottom.
            // If player pivot is center, I might need to adjust.
            // Looking at Player.cs OnDrawGizmos, it draws a line down by groundCheckDistance.
            // So feet is likely transform.position + Vector3.down * groundCheckDistance (approx).
            // But usually "at feet" means the position where the player stands.
            // I'll spawn at transform.position and let the user adjust offset if needed, 
            // or better, I'll add a small offset downwards if it looks like the player pivot is center.
            // Spawn at player's feet (position - 0.5y)
            Vector3 spawnPos = player.transform.position;
            spawnPos.y -= 0.5f;
            
            GameObject bubble = Object.Instantiate(prefab, spawnPos, Quaternion.identity);

            // Ensure it covers other layers
            SpriteRenderer sr = bubble.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = 20; // High sorting order as per design doc
            }
            if (player.Health != null)
            {
                player.Health.TakeDamage(player.playerStableCost);
            }
        }
    }
    
}
