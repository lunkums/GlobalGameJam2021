using System.Collections;

public abstract class State
{
    public virtual IEnumerator Idle()
    {
        yield break;
    }

    public virtual IEnumerator Patrol()
    {
        yield break;
    }

    public virtual IEnumerator Chase()
    {
        yield break;
    }

    public virtual IEnumerator Attack()
    {
        yield break;
    }
}