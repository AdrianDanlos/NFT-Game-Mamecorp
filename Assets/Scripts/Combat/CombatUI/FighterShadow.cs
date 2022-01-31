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
        if (parentFighterName == "Fighter") setShadowPosition(player);
        else setShadowPosition(bot);
    }

    private void setShadowPosition(Transform fighterTransform)
    {
        // here we force the position of the current shadow to have the same X as the fighter
        transform.position = new Vector3(fighterTransform.position.x, transform.position.y, transform.position.z);
    }
}
