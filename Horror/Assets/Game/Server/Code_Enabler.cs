using UnityEngine;
using System.Collections;

public class Code_Enabler : MonoBehaviour {

	public GameObject game;
    public GameObject mainMenu;

    /// <summary>
    /// Enable one GO and remove the other
    /// </summary>
    public void Awake()
    {
        if (PhotonNetwork.room != null)
        {
            Destroy(this.mainMenu);
            this.game.active = true;
        }
        else
        {
            Destroy(this.game);
            this.mainMenu.active = true;
        }
        
        // now this script is not needed anymore. destroy it and it's gameobject
        Destroy(this.gameObject);
    }
}
