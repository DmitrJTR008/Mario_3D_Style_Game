using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : BaseBonus
{
    public override void Consume(Player player)
    {
        player.AddHealth();
    }
    
}
