using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public abstract class Enemy : Mover
{
    protected int _health;
    protected GameObject _enemy;
    protected virtual void Kill() {}
    protected virtual void DropDiamonds(){}

    //public EnemySpawnController enemySpawnController;
    //public GameData data;
    //public EnemyStats _enemyData;
    public float turningDelayRate = 1;
    public ContactFilter2D filter;
    private bool collidingWithPlayer;//for animating
    private Transform playerTransform;
    private Vector3 startingPosition;
    private Collider2D[] hits = new Collider2D[10];
    private Vector3 motion;
    public float chaseSpeed = 0.5f; //the less the value the slower it is
    private bool startAnim = true;// just to show animespawn  anim;
    private float lastAudioPlay;
    private bool isDeath;
    private Transform enemyHealthBar;
    private Vector3 playerLastTrackedPos;
    private float lastFollowTime;
    private float turningTimeDelay = 1f;

    public int enemyType;
    public int enemySpawner;

    protected override void Start()
    {
        base.Start();
        //_enemyData = GameManager.instance.enemyStats;
        LoadData(enemyType);
        //playerTransform = GameManager.instance.player.transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //enemySpawnController = GameManager.instance.enemySpawnController;
        startingPosition = transform.position;
        //enemyHealthBar = transform.GetChild(1).GetChild(0).transform;
        lastFollowTime = Time.time;
        playerLastTrackedPos = playerTransform.position;
        turningTimeDelay = ((float)1f - (float)xSpeed);
        turningTimeDelay += 1f * turningDelayRate;
        //data = GameManager.instance.data;
        AssignRecipes(enemySpawner);
        //Debug.Log(turningTimeDelay);
    }
    public void LoadData(int enemyValue)
    {
        /*health = _enemyData.enemys[enemyValue].HP;
        maxHealth = _enemyData.enemys[enemyValue].HP;
        recoveryPerSecond = _enemyData.enemys[enemyValue].Hps;
        attackDamage = _enemyData.enemys[enemyValue].ADamage;
        attackSpeed = _enemyData.enemys[enemyValue].ASpeed;
        attackRange = _enemyData.enemys[enemyValue].ARange;
        xSpeed = _enemyData.enemys[enemyValue].MSpeed;
        ySpeed = _enemyData.enemys[enemyValue].MSpeed;
        chaseSpeed = _enemyData.enemys[enemyValue].MSpeed;*/
        //armor = data.armor;
    }
    private void FixedUpdate()
    {

        if (isDeath)
            return;
        if (startAnim)
        {
           // sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime * 10);
          //  if (sr.color == Color.white)
                startAnim = false;
            return;
        }
        //is the player in range?
        //if (hasEnemyTarget) {

        if (!collidingWithPlayer)
        {
            ChasePlayer();
        }

        motion = startingPosition - playerTransform.position;
        collidingWithPlayer = false;
       // boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            if (hits[i].tag == "Player")
            {
                collidingWithPlayer = true;
            }
            hits[i] = null;
        }
        if (collidingWithPlayer)
            UpdateMotor(Vector3.zero);
        //else UpdateMotor(motion);
    }
    void AssignRecipes(int spawner)
    {
        /*SpawnerMods spawnMods = new SpawnerMods();
        spawnMods.Damage_up = GameManager.instance.modParams.modParams[0].value;
        spawnMods.Speed_up = GameManager.instance.modParams.modParams[1].value;
        spawnMods.Health_up = GameManager.instance.modParams.modParams[2].value;
        spawnMods.Power_up = GameManager.instance.modParams.modParams[3].value;
        spawnMods.Value_up = GameManager.instance.modParams.modParams[4].value;
        spawnMods.Casino_up = GameManager.instance.modParams.modParams[5].value;
        spawnMods.Crowds_up = GameManager.instance.modParams.modParams[6].value;
        spawnMods.Profit_up = GameManager.instance.modParams.modParams[7].value;*/
        float healtMultiplicator = 0;
        float atackMultiplicator = 0;
        float speedMultiplicator = 0;
        float powerMultiplicator = 0;
        //string multiplicators = "";
        /*for (int i = 0; i < GameManager.instance.recipeController.spawnActivRecipeId[spawner].Count; i++)
        {
            multiplicators = GameManager.instance.recipeController.spawnActivRecipeId[spawner][i].spawner_mod;
            if (multiplicators.Contains("Damage_up")) { int sameTimes = Regex.Matches(multiplicators, "Damage_up").Count; atackMultiplicator += GameManager.instance.modParams.modParams[0].value * sameTimes; }
            if (multiplicators.Contains("Speed_up")) { int sameTimes = Regex.Matches(multiplicators, "Speed_up").Count; speedMultiplicator += GameManager.instance.modParams.modParams[1].value * sameTimes; }
            if (multiplicators.Contains("Health_up")) { int sameTimes = Regex.Matches(multiplicators, "Health_up").Count; healtMultiplicator += GameManager.instance.modParams.modParams[2].value * sameTimes; }
            if (multiplicators.Contains("Power_up")) { int sameTimes = Regex.Matches(multiplicators, "Power_up").Count; powerMultiplicator += GameManager.instance.modParams.modParams[3].value * sameTimes; }
        }*/
        atackMultiplicator = (atackMultiplicator / 100) * attackDamage;
        attackDamage += atackMultiplicator;
        healtMultiplicator = (healtMultiplicator / 100) * health;
        health += healtMultiplicator;
        speedMultiplicator = (speedMultiplicator / 100) * xSpeed;
        //xSpeed += speedMultiplicator;
        //ySpeed += speedMultiplicator;
        chaseSpeed += speedMultiplicator;
        float powerMultiplicatorDamage = (powerMultiplicator / 100) * attackDamage;
        float powerMultiplicatorSpeed = (powerMultiplicator / 100) * xSpeed;
        attackDamage += powerMultiplicatorDamage;
        //xSpeed += powerMultiplicatorSpeed;
        //ySpeed += powerMultiplicatorSpeed;
        chaseSpeed += powerMultiplicatorSpeed;

    }
    protected override void ReceiveDamage(Damage dmg)
    {
        if (isDeath || startAnim)
            return;
        base.ReceiveDamage(dmg);
        UpdateHealthBar();
        Hurt();
    }

    protected override void Death()
    {
        isDeath = true;
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetTrigger("EnemyDeath");
        //enemySpawnController.KillAndReset(gameObject);
        Destroy(gameObject, 0.7f);
    }

    private void Hurt()
    {
        Invoke("RestoreColor", 0.5f);
        if (audioS.isActiveAndEnabled && Time.time - lastAudioPlay > 0.5f)
        {
            lastAudioPlay = Time.time;
            audioS.Play();
        }
    }
    private void UpdateHealthBar()
    {
        if (enemyHealthBar == null)
            return;
        float localScaleX = (float)health / (float)maxHealth;
        enemyHealthBar.localScale = new Vector3(localScaleX, enemyHealthBar.localScale.y, enemyHealthBar.localScale.z);
        if (localScaleX == 0)
            enemyHealthBar.parent.localScale = Vector3.zero;
    }
    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, chaseSpeed * Time.deltaTime);
        anim.SetBool("Run", true);
        if (transform.position == playerTransform.position)
        {
            anim.SetBool("Run", false);
        }
    }
}
