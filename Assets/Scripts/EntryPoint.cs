using System.IO;
using UnityEngine;
using System.Collections;


public class EntryPoint : MonoBehaviour
{
    public static GameObject fighterGameObject;
    IEnumerator Start()
    {
        HideFighter();

        //TODO: Set the time of loading screen
        yield return new WaitForSeconds(0f);

        bool saveFilesFound = File.Exists(JsonDataManager.getFilePath(JsonDataManager.UserFileName)) &&
            File.Exists(JsonDataManager.getFilePath(JsonDataManager.FighterFileName));

        if (saveFilesFound)
        {
            JsonDataManager.ReadUserFile();
            JsonDataManager.ReadFighterFile();
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.MainMenu.ToString());
        }

        else UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.ChooseFirstFighter.ToString());

        // tests
        // SkillCollection.GetAllRaritySkillCount();
    }

    private void HideFighter()
    {
        fighterGameObject = GameObject.Find("Fighter");
        fighterGameObject.SetActive(false);
    }

    private void ChestTest()
    {
        int x = 0;
        int y = 0;
        int z = 0;
        int q = 0;

        for(int i=0; i < 10000; i++)
        {
            // reward mockup
            switch (ChestManager.GetBattleChestSkillReward(ChestManager.GetRandomBattleChest().ToString()).ToString())
            {
                case "COMMON":
                    x++;
                    break;
                case "UNCOMMON":
                    y++;
                    break;
                case "RARE":
                    z++;
                    break;
                case "EPIC":
                    q++;
                    break;
            }
        }

        Debug.Log("COMMON: " + x + "| UNCOMMON: " + y + " | RARE: " + z + " | EPIC: " + q);
    }
}