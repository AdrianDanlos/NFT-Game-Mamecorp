using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System;
using UnityEngine.UI;
using TMPro;

public class Combat : MonoBehaviour
{
    // Data Objects
    public static Fighter player;
    public static Fighter bot;
    public static int botElo;

    // GameObjects
    public static GameObject playerGameObject;
    public GameObject playerWrapper;
    public static GameObject botGameObject;
    public Canvas results;
    public SpriteRenderer arena;
    public GameObject combatUI;
    public GameObject combatLoadingScreenUI;
    public GameObject combatLoadingScreenSprites;
    private TextMeshProUGUI levelTextBot;

    // Script references
    public static Movement movementScript;
    public static FightersUIData fightersUIDataScript;
    SkillsLogicInCombat skillsLogicScript;
    Attack attackScript;
    LoadingScreen loadingScreen;
    CupManager cupManager;

    // Positions data
    static Vector3 playerStartingPosition = new Vector3(-6, -0.7f, 0);
    static Vector3 botStartingPosition = new Vector3(6, -0.7f, 0);
    public const float DefaultDistanceFromEachotherOnAttack = 2.3f;

    // Game status data
    public static bool isGameOver;
    List<Fighter> fightersOrderOfAttack = new List<Fighter> { };
    public static float playerMaxHp;
    public static float botMaxHp;

    //Balance constants
    private const int ProbabilityOfUsingSkillEachTurn = 50;

    private void Awake()
    {
        SetupUI();

        isGameOver = false;

        FindGameObjects();
        GetComponentReferences();

        // cup mode
        if (Cup.Instance.isActive && !CombatMode.isSoloqEnabled)
            MatchMaking.GenerateCupBotData(player, bot);
        if (CombatMode.isSoloqEnabled)
            MatchMaking.GenerateBotData(player, bot);

        SetMaxHpValues();

        // LoadingScreen
        loadingScreen.SetPlayerLoadingScreenData(player);
        loadingScreen.DisplayLoaderForEnemy();
        ToggleLoadingScreenVisibility(true);

        //Load everything needed for the combat
        GenerateSkillsFixturesForPlayer();
        BoostFightersStatsBasedOnPassiveSkills();
        SetVisibilityOfGameObjects();
        SetFighterPositions();
        SetOrderOfAttacks();
        GetRandomArena();
        FighterSkin.SetFightersSkin(player, bot);
        FighterAnimations.ResetToDefaultAnimation(player);
        fightersUIDataScript.SetFightersUIInfo(player, bot, botElo);
        fightersUIDataScript.HidePortraitsUI();
        StopExplosionParticlesAutoPlay();
    }

    private void StopExplosionParticlesAutoPlay(){
        playerGameObject.transform.Find("VFX/Explosion_VFX").GetComponent<ParticleSystem>().Stop();
        botGameObject.transform.Find("VFX/Explosion_VFX").GetComponent<ParticleSystem>().Stop();
    }

    IEnumerator Start()
    {
        // StartCoroutine(SceneManagerScript.instance.FadeIn());
        // yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));

        // --- Enable this for loading effect ---
        //yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(2f));
        // loadingScreen.SetBotLoadingScreenData(bot);
        // levelTextBot.enabled = true;
        //yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(3f));
        yield return null; //remove

        // UI
        fightersUIDataScript.ShowPortraitsUI();

        ToggleLoadingScreenVisibility(false);
        StartCoroutine(InitiateCombat());

        SceneFlag.sceneName = SceneNames.Combat.ToString();
    }

    private void SetupUI()
    {
        levelTextBot = GameObject.Find("LevelTextBot").GetComponent<TextMeshProUGUI>();
        levelTextBot.enabled = false;
    }

    private void BoostFightersStatsBasedOnPassiveSkills()
    {
        skillsLogicScript.BoostStatsBasedOnPassiveSkills(player);
        skillsLogicScript.BoostStatsBasedOnPassiveSkills(bot);
    }

    private void GetComponentReferences()
    {
        movementScript = GetComponent<Movement>();
        fightersUIDataScript = GetComponent<FightersUIData>();
        skillsLogicScript = GetComponent<SkillsLogicInCombat>();
        attackScript = GetComponent<Attack>();
        loadingScreen = GetComponent<LoadingScreen>();
        player = playerGameObject.GetComponent<Fighter>();
        bot = botGameObject.GetComponent<Fighter>();
        cupManager = GameObject.Find("CupManager").GetComponent<CupManager>();
    }

    private void ToggleLoadingScreenVisibility(bool displayLoadingScreen)
    {
        combatLoadingScreenUI.SetActive(displayLoadingScreen);
        combatLoadingScreenSprites.SetActive(displayLoadingScreen);
    }

    private void SetMaxHpValues()
    {
        playerMaxHp = player.hp;
        botMaxHp = bot.hp;
    }
    private void GetRandomArena()
    {
        Sprite[] arenas = Resources.LoadAll<Sprite>("Arenas/");
        int chosenArena = UnityEngine.Random.Range(0, arenas.Length);
        arena.sprite = arenas[chosenArena];
    }

    private void SetVisibilityOfGameObjects()
    {
        playerGameObject.SetActive(true);
    }
    private void FindGameObjects()
    {
        playerWrapper = GameObject.Find("FighterWrapper");
        playerGameObject = playerWrapper.transform.Find("Fighter").gameObject;
        botGameObject = GameObject.FindGameObjectWithTag("FighterBot");
        results = GameObject.FindGameObjectWithTag("Results").GetComponent<Canvas>();
        arena = GameObject.FindGameObjectWithTag("Arena").GetComponent<SpriteRenderer>();
        combatUI = GameObject.FindGameObjectWithTag("CombatUI");
        combatLoadingScreenUI = GameObject.FindGameObjectWithTag("CombatLoadingScreenUI");
        combatLoadingScreenSprites = GameObject.FindGameObjectWithTag("CombatLoadingScreenSprites");
    }

    private void SetFighterPositions()
    {
        //Set GameObjects
        playerGameObject.transform.position = playerStartingPosition;
        botGameObject.transform.position = botStartingPosition;

        //Set Objects
        player.initialPosition = playerStartingPosition;
        bot.initialPosition = botStartingPosition;
    }

    public void SetFightersPortrait(GameObject playerPortrait, GameObject botPortrait)
    {
        playerPortrait.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterProfilePicture/" + player.species);
        botPortrait.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterProfilePicture/" + bot.species);
    }

    IEnumerator InitiateCombat()
    {
        Fighter firstAttacker = fightersOrderOfAttack[0];
        Fighter secondAttacker = fightersOrderOfAttack[1];

        //1 loop = 1 turn (both players attacking)
        while (!isGameOver)
        {
            // The StartTurn method should handle all the actions of a player for that turn. E.G. Move, Attack, Throw skill....
            yield return StartCoroutine(StartTurn(firstAttacker, secondAttacker));
            while (!isGameOver && attackScript.IsExtraTurn(firstAttacker)) yield return StartCoroutine(StartTurn(firstAttacker, secondAttacker));
            if (isGameOver) break;
            yield return StartCoroutine(StartTurn(secondAttacker, firstAttacker));
            while (!isGameOver && attackScript.IsExtraTurn(secondAttacker)) yield return StartCoroutine(StartTurn(secondAttacker, firstAttacker));
        }
        StartPostGameActions();
    }

    //TODO: Remove this on production
    private void GenerateSkillsFixturesForPlayer()
    {
        //GIVE ALL SKILLS TO THE PLAYER FOR THE COMBAT
        foreach (OrderedDictionary skill in SkillCollection.skills)
        {
            Skill skillInstance = new Skill(skill["name"].ToString(), skill["description"].ToString(),
                skill["skillRarity"].ToString(), skill["category"].ToString(), skill["icon"].ToString());

            player.skills.Add(skillInstance);
        }
    }

    IEnumerator StartTurn(Fighter attacker, Fighter defender)
    {
        if (WillUseSkillThisTurn(attacker))
        {
            yield return StartCoroutine(UseRandomSkill(attacker, defender, attacker));
            yield break;
        }
        yield return skillsLogicScript.AttackWithoutSkills(attacker, defender);
        FighterAnimations.ChangeAnimation(GetAttackerIfAlive(attacker, defender), FighterAnimations.AnimationNames.IDLE);
    }

    // This check is important as the attacker might have lost due to a counter or reversal attack
    private Fighter GetAttackerIfAlive(Fighter attacker, Fighter defender)
    {
        return attacker.hp > 0 ? attacker : defender;
    }

    //We create the fighterWeTakeTheSkillFrom param for the ViciousTheft skill as we take a skill from the opponent instead.
    IEnumerator UseRandomSkill(Fighter attacker, Fighter defender, Fighter fighterWeTakeTheSkillFrom)
    {
        //TODO v2: Each skill should have each own class with its own skill implementation. (methods, attributes, etc...)
        // Then we can instantiate a random class here to use a random SUPER skill this turn

        List<string> skillNamesList = fighterWeTakeTheSkillFrom.skills.
            Where(skill => skill.category == SkillCollection.SkillType.SUPER.ToString()).
            Select(skill => skill.skillName).ToList();

        // Can be 0 in some edge cases e.g. when ViciousTheft is used and the opponent does not have skills left.
        if (skillNamesList.Count() > 0)
        {
            int randomSkillIndex = UnityEngine.Random.Range(0, skillNamesList.Count());

            switch (skillNamesList[randomSkillIndex])
            {
                case SkillNames.JumpStrike:
                    yield return skillsLogicScript.JumpStrike(attacker, defender);
                    fighterWeTakeTheSkillFrom.removeUsedSkill(SkillNames.JumpStrike);
                    break;
                case SkillNames.CosmicKicks:
                    yield return skillsLogicScript.CosmicKicks(attacker, defender);
                    fighterWeTakeTheSkillFrom.removeUsedSkill(SkillNames.CosmicKicks);
                    break;
                case SkillNames.ShurikenFury:
                    yield return skillsLogicScript.ShurikenFury(attacker, defender);
                    fighterWeTakeTheSkillFrom.removeUsedSkill(SkillNames.ShurikenFury);
                    break;
                case SkillNames.LowBlow:
                    yield return skillsLogicScript.LowBlow(attacker, defender);
                    fighterWeTakeTheSkillFrom.removeUsedSkill(SkillNames.LowBlow);
                    break;
                case SkillNames.ExplosiveBomb:
                    yield return skillsLogicScript.ExplosiveBomb(attacker, defender);
                    fighterWeTakeTheSkillFrom.removeUsedSkill(SkillNames.ExplosiveBomb);
                    break;
                case SkillNames.ShadowTravel:
                    yield return skillsLogicScript.ShadowTravel(attacker, defender);
                    fighterWeTakeTheSkillFrom.removeUsedSkill(SkillNames.ShadowTravel);
                    break;
                case SkillNames.HealingPotion:
                    yield return skillsLogicScript.HealingPotion(attacker);
                    fighterWeTakeTheSkillFrom.removeUsedSkill(SkillNames.HealingPotion);
                    break;
                case SkillNames.ViciousTheft:
                    fighterWeTakeTheSkillFrom.removeUsedSkill(SkillNames.ViciousTheft);
                    yield return UseRandomSkill(attacker, defender, defender);
                    break;
            }
        }
        FighterAnimations.ChangeAnimation(GetAttackerIfAlive(attacker, defender), FighterAnimations.AnimationNames.IDLE);
    }

    public static Func<Fighter, bool> WillUseSkillThisTurn = attacker =>
        attacker.skills.Count() > 0 && Probabilities.IsHappening(ProbabilityOfUsingSkillEachTurn);

    public IEnumerator MoveForwardHandler(Fighter attacker, Fighter defender, float distanceFromEachOtherOnAttack = DefaultDistanceFromEachotherOnAttack)
    {
        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.RUN);
        yield return StartCoroutine(movementScript.MoveForward(attacker, GetAttackerDestinationPosition(defender, distanceFromEachOtherOnAttack)));
    }

    public static Vector3 GetAttackerDestinationPosition(Fighter defender, float distanceFromEachOtherOnAttack)
    {
        Vector3 attackerDestinationPosition = defender.transform.position;
        attackerDestinationPosition.x = attackerDestinationPosition.x + (player == defender ? +distanceFromEachOtherOnAttack : -distanceFromEachOtherOnAttack);
        return attackerDestinationPosition;
    }

    public IEnumerator MoveBackHandler(Fighter attacker)
    {
        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.RUN);
        FighterSkin.SwitchFighterOrientation(attacker.GetComponent<SpriteRenderer>());
        yield return StartCoroutine(movementScript.MoveBack(attacker, attacker.initialPosition));
        FighterSkin.SwitchFighterOrientation(attacker.GetComponent<SpriteRenderer>());
    }

    //The attack order is determined by the Initiator skill. If no players have it it is determined by the speed.
    private void SetOrderOfAttacks()
    {
        if (player.HasSkill(SkillNames.Initiator))
        {
            fightersOrderOfAttack.Add(player);
            fightersOrderOfAttack.Add(bot);
            return;
        }

        // Creates a dictionary with the Fighter class objects sorted by their speeds to get the order of attack.
        // Higher speeds will get sorted first
        OrderedDictionary fighterDictWithSpeed = new OrderedDictionary
        {
            {player, player.speed},
            {bot, bot.speed},
        };

        var fighterDictSortedBySpeed = fighterDictWithSpeed.Cast<DictionaryEntry>()
                       .OrderByDescending(r => r.Value)
                       .ToDictionary(c => c.Key, d => d.Value);

        foreach (var fighter in fighterDictSortedBySpeed)
        {
            fightersOrderOfAttack.Add((Fighter)fighter.Key);
        }
    }

    private void StartPostGameActions()
    {
        bool isPlayerWinner = PostGameActions.HasPlayerWon(player);
        int eloChange = MatchMaking.CalculateEloChange(User.Instance.elo, botElo, isPlayerWinner);
        int playerUpdatedExperience = player.experiencePoints + Levels.GetXpGain(isPlayerWinner);
        bool isLevelUp = Levels.IsLevelUp(player.level, playerUpdatedExperience);
        int goldReward = PostGameActions.GoldReward(isPlayerWinner);
        int gemsReward = PostGameActions.GemsReward();

        //Reset fighter values that were modified in combat e.g. hp
        ResetPlayerObject();

        //PlayerData
        PostGameActions.SetElo(eloChange);
        PostGameActions.SetWinLoseCounter(isPlayerWinner);
        PostGameActions.SetExperience(player, isPlayerWinner);
        if (isLevelUp) PostGameActions.SetLevelUpSideEffects(player);
        if (CombatMode.isSoloqEnabled) EnergyManager.SubtractOneEnergyPoint(); // tournament doesn't cost energy

        //Rewards
        PostGameActions.SetCurrencies(goldReward, gemsReward);

        //UI
        fightersUIDataScript.ShowPostCombatInfo(player, isPlayerWinner, eloChange, isLevelUp, goldReward, gemsReward, results);

        //Save
        PostGameActions.Save(player);

        //Profile
        ProfileData.SaveFights();
        ProfileData.SavePeakElo(User.Instance.elo);

        if (Cup.Instance.isActive && !CombatMode.isSoloqEnabled)
        {
            switch (Cup.Instance.round)
            {
                case "QUARTERS":
                    cupManager.SimulateQuarters(isPlayerWinner);
                    break;
                case "SEMIS":
                    cupManager.SimulateSemis(isPlayerWinner);
                    break;
                case "FINALS":
                    cupManager.SimulateFinals(isPlayerWinner);
                    break;
            }

            if (!isPlayerWinner)
            {
                // enable rewards button on cup menu
                // disable battle button
                Cup.Instance.isActive = false;
                Cup.Instance.SaveCup();
            }
        }
    }

    //During the combat the player object experiences a lot of changes so we need to set it back to its default state after the combat.
    private Action ResetPlayerObject = () => player = JsonDataManager.ReadFighterFile();
}
