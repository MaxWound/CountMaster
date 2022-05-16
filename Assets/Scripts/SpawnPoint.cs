using UnityEngine;

public class SpawnPoint
{
    public ParticleSystem enemyParticles = VfxManager.Instance.enemyDieVfx;
   public ParticleSystem soldierParticles = VfxManager.Instance.dieVfx;
    public GameObject soldierParticlesGo = VfxManager.Instance.vfxGO;
   
    public Vector3 Position { get; set; }
    public int Ring { get; set; }
    public bool isEmpty => empty; 
private bool empty;
    
    public SpawnPoint(Vector3 position, int ring)
    {
        
        Position = position;
        Ring = ring;
        empty = true;
    }
    
    public void PlayDieAnim()
    {
        soldierParticles.Play();
    }
    public void PlayEnemyDieAnim()
    {
        enemyParticles.Play();
    }
    
    public void Despawn()
    {
        empty = true;
    }
    
    public void SetEmpty(bool state)
    {
        empty = state;
    }

    public Ally Spawn(Ally original, Transform parent)
    {
      
        empty = false;
        Ally ally = PoolManager.Instance.SpawnFromPool(parent);
        ally.transform.position = AlliesGroup.Instance.transform.position + Position;
        ally.transform.rotation = original.transform.rotation;
                    //Object.Instantiate(original, AlliesGroup.Instance.transform.position + Position, original.transform.rotation, parent);
          
        ally.transform.position = new Vector3(ally.transform.position.x, original.transform.position.y, ally.transform.position.z);
        Debug.Log(AlliesGroup.Instance.transform.position - ally.transform.position);
        //soldierParticles = GameObject.Instantiate(soldierParticlesGo, ally.transform.position, Quaternion.LookRotation(new Vector3(0,0,0), new Vector3(0,1,0)) , parent).GetComponent<ParticleSystem>();
        return ally;

    }

    public Enemy Spawn(Enemy original, Transform parent)
    {
        empty = false;
        Enemy enemy = Object.Instantiate(original, Position, original.transform.rotation, parent);
        enemyParticles = GameObject.Instantiate(soldierParticlesGo, parent.position, Quaternion.LookRotation(new Vector3(0, 0, 0), new Vector3(0, 1, 0)), parent).GetComponent<ParticleSystem>();
        return enemy;
    }

}
