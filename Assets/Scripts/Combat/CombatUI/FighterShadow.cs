using UnityEngine;

public class FighterShadow : MonoBehaviour
{
    Transform player;
    Transform bot;
    string parentFighterName;

    void Start()
    {
        parentFighterName = transform.parent.parent.name;
        player = FindInactiveFighterGameObject.Find().transform;
        bot = GameObject.Find("Bot").transform;
    }

    void Update()
    {
        if (parentFighterName == "Fighter") setShadowPosition(player, -0.25f);
        else setShadowPosition(bot, 0.25f);
    }

    private void setShadowPosition(Transform fighterTransform, float shadowDisplacement)
    {
        // here we force the position of the current shadow to have the same X as the fighter (+ - a little displacement to make it look realistic)
        transform.position = new Vector3(fighterTransform.position.x + shadowDisplacement, transform.position.y, transform.position.z);
    }
}
