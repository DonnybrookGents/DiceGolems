using UnityEngine;

public class ConfusionOverride : ActionFilterOverride{
    public override System.Object Execute(System.Object obj, ActionFilter filter){
        int damage = (int)obj;
        if(Random.Range(0,2) == 0){
            return damage * 2;
        }
        return 0;
    }

}
