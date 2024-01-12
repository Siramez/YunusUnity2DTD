using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
    void NotifyDefeated(); // Add this method

    void SlowDown();

    bool IsAlive { get; } // Add this property
}


