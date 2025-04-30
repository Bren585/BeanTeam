using UnityEngine;

public class Disc : Emitter
{
    public virtual void Skill()
    {
        Debug.Log("Null Skill");
    }

    public override void Shoot(Vector3 position, Vector3 direction)
    {
        base.Shoot(position, direction);
    }
<<<<<<< Updated upstream
   
}
=======

    public virtual void PassiveEnter()
    {

    }

    public virtual void PassiveExit()
    {

    }
}
>>>>>>> Stashed changes
