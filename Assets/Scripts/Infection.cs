using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Infection : NetworkBehaviour
{
    private bool isInfected = false;
    
    public SkinnedMeshRenderer protoColorChange;
    public Color infectedColor = Color.red;
    private Color originalColor;

    void Start()
    {
        originalColor = protoColorChange.material.color;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!isLocalPlayer)
            return;

        var target = col.gameObject.GetComponent<Infection>();
        if (target != null)
        {
            if (isInfected && !target.isInfected)
            {
                GameManager.instance.CmdPlayerInfected(UserId.GetId());
                target.RpcSetInfect(true);
            }
        }
    }

    [ClientRpc]
    public void RpcSetInfect(bool infected)
    {
        isInfected = infected;
        protoColorChange.material.color = infected ? infectedColor : originalColor;
    }
}
