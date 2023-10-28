using UnityEngine.Events;

public interface IScareable
{
    public static UnityAction<IScareable> OnScared { get; set; }
    public static UnityAction<IScareable> OnUnscared { get; set; }
    
    public float MaxTimeScared { get; set; }
    public bool IsScared { get; }

    public void BeScared();
}
