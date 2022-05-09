using UnityEngine;

public class SpawnPoint
{
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
        Ally ally = Object.Instantiate(original, AlliesGroup.Instance.transform.position + Position, original.transform.rotation, parent);
        ally.transform.position = new Vector3(ally.transform.position.x, original.transform.position.y, ally.transform.position.z);
        return ally;
    }

    public Enemy Spawn(Enemy original, Transform parent)
    {
        empty = false;
        Enemy enemy = Object.Instantiate(original, Position, original.transform.rotation, parent);

        return enemy;
    }

}
