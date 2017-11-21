using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] private Prototype proto;
    private Item p;

    public abstract void UseItem();

    public virtual void ShowItemInInventory(Transform t)
    {
        p = proto.Instantiate<Item>();
        p.transform.parent = t;
        p.transform.position = t.position;
        p.GetComponent<Animator>().SetTrigger("itemGrow");
    }
    public virtual void HideItemInInventory()
    {
        p.GetComponent<Animator>().SetTrigger("itemShrink");
        Invoke("SetInactive", 0.2f);
    }
    private void SetInactive()
    {
        p.GetComponent<Prototype>().ReturnToPool();
    }
}
