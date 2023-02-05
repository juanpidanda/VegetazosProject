using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable 
{
    public bool InteractWithDash(Vector2 direction, float distance, float time, float speed)
    {
        return true;
    }

}
