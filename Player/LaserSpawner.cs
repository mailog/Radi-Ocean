using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public GameObject bubbleBurst;
    public GameObject laser;

    public Vector2 laserOffset;

    public float projSpeed, spawnDist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator MakeLaser(float addedDirection = 0, float addedXOffset = 0, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        GameObject tmp = ObjectPooler.laserPooler.GetEnemyLaser();
        tmp.transform.position = (Vector2)transform.position + laserOffset + new Vector2(addedXOffset,0);
        tmp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, addedDirection));
        tmp.GetComponent<Rigidbody2D>().velocity = tmp.transform.up * projSpeed;

        Instantiate(bubbleBurst, (Vector2)transform.position + laserOffset, Quaternion.identity);
    }

    public void ShootAngle(float angle)
    {
        GameObject tmp = ObjectPooler.laserPooler.GetEnemyLaser();
        tmp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
        tmp.transform.position = (Vector2)transform.position + (Vector2)tmp.transform.up * spawnDist;
        tmp.GetComponent<Rigidbody2D>().velocity = tmp.transform.up * projSpeed;

        Instantiate(bubbleBurst, (Vector2)transform.position + laserOffset, Quaternion.identity);
    }

    public IEnumerator MakeLaserAtPos(Vector2 pos, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject tmp = ObjectPooler.laserPooler.GetEnemyLaser();
        tmp.transform.position = pos;
        tmp.GetComponent<Rigidbody2D>().velocity = tmp.transform.up * projSpeed;

        Instantiate(bubbleBurst, pos, Quaternion.identity);
    }

}
