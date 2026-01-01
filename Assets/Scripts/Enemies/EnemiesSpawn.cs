using SystemMath = System.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
    public List<GameObject> TypeEnemies;
    public List<Transform> randomPoints;
    public int EnemyCount;
    public EnemiesCounter ec;
   
    public List<GameObject> Enemigos;
    // Variable para controlar si se est� esperando para iniciar una nueva oleada
    private bool esperandoInicio = false;
    public int CrearEnemigos = 4;
    // Start is called before the first frame update
  
    void Start()
    {
        // Comenzar a generar enemigos continuamente
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) // Ejecutar continuamente
        {
            // Verificar si la lista Enemigos es nula o est� vac�a
            if (Enemigos == null || Enemigos.Count < CrearEnemigos)
            {
                // Verificar si ec es igual a 0 para reiniciar la generaci�n de enemigos
                if (ec.enemiesCounter == 0)
                {
                    if (!esperandoInicio)
                    {
                        // Indicar que se est� esperando para iniciar una nueva oleada
                        esperandoInicio = true;
                        ec.Oleada++;
                        Debug.Log("Oleada " + ec.Oleada);
                        CrearEnemigos+= ec.Oleada;
                        ec.textoOleada();

                        // Esperar 30 segundos antes de comenzar la pr�xima oleada
                        yield return new WaitForSeconds(15f);
                        // Reiniciar el contador de enemigos
                        EnemyCount = 0;
                        esperandoInicio = false;
                        DestruirAliens();
                    }

                    // Volver a generar la oleada de enemigos
                    while (EnemyCount < CrearEnemigos)
                    {          
                        CrearEnemigo(Mutacion(new float[] {5f,100f,5f,0f }));

                        // Esperar un breve per�odo antes de spawnear otro enemigo
                        yield return new WaitForSeconds(0.5f);
                    }
                }

                // Esperar un tiempo antes de verificar de nuevo el valor de ec
                yield return new WaitForSeconds(1f);
            }
            else
            {
                if (ec.enemiesCounter == 0)
                {

                    if (!esperandoInicio)
                    {
                        // Indicar que se est� esperando para iniciar una nueva oleada
                        esperandoInicio = true;
                        ec.Oleada++;
                        //Debug.Log("Oleada " + ec.Oleada);
                        ec.textoOleada();
                        // Esperar 30 segundos antes de comenzar la pr�xima oleada
                        yield return new WaitForSeconds(15f);
                        // Reiniciar el contador de enemigos
                        EnemyCount = 0;
                        esperandoInicio = false;
                        DestruirAliens();
                        CrearEnemigos+=ec.Oleada;
                    }

                    Debug.Log("Haciendo Algoritmo Genetico");
                    OrdenarEnemigos();

                    float[] padre1 = Enemigos[0].GetComponent<EnemyController>().attributes.getAtributos();
                    float[] padre2 = Enemigos[1].GetComponent<EnemyController>().attributes.getAtributos();
                    float[] peor = Enemigos[Enemigos.Count - 1].GetComponent<EnemyController>().attributes.getAtributos();
                    Enemigos.Clear();
                    //Debug.Log("Padre 1:" + padre1[0] + " " + padre1[1] + " " + padre1[2] + " " + padre1[3] );
                    //Debug.Log("Padre 2:" + padre2[0] + " " + padre2[1] + " " + padre2[2] + " " + padre2[3] );
                    CrearEnemigo(padre1);
                    CrearEnemigo(padre2);
                    CrearEnemigo(peor);
                    while (EnemyCount < CrearEnemigos)
                    {
                        //Debug.Log(EnemyCount);
                        // Ordenar la lista de enemigos

                        int inicio = Random.Range(0, padre1.Length);
                        int final = Random.Range(0, padre1.Length);

                        while (inicio == final)
                        {
                            final = Random.Range(0, padre1.Length);
                        }

                        if (inicio > final)
                        {
                            Swap(ref inicio, ref final);
                        }

                        List<float> hijo = new List<float>(padre1.Length);

                        for (int i = 0; i < padre1.Length; i++)
                        {
                            if (i >= inicio && i <= final)
                            {
                                hijo.Add(padre1[i]);
                            }
                            else
                            {
                                hijo.Add(padre2[i]);
                            }
                        }

                        
                        // Crear un nuevo enemigo con los atributos generados
                        CrearEnemigo(Mutacion(hijo.ToArray()));

                        // Esperar un tiempo antes de la pr�xima iteraci�n
                        yield return new WaitForSeconds(0.5f);
                    }
                    
                    
                }
                yield return new WaitForSeconds(1f);
                
            }
           
        }
        
    }

    public void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    private void OrdenarEnemigos()
    {
        Enemigos.Sort((a, b) =>
        {
            float[] enemyA = a.GetComponent<EnemyController>().attributes.getAtributos();
            float[] enemyB = b.GetComponent<EnemyController>().attributes.getAtributos();

            return enemyB[3].CompareTo(enemyA[3]);
        });
    }

    private void CrearEnemigo(float[] atributos)
    {
        // Elegir un punto de spawn aleatorio
        int randomIndex = Random.Range(0, randomPoints.Count);
        Vector3 spawnPosition = randomPoints[randomIndex].position;

        // Elegir un enemigo aleatorio de la lista de enemigos
        GameObject enemyPrefab = TypeEnemies[Random.Range(0, TypeEnemies.Count)];

        // Instanciar el enemigo en la posici�n aleatoria
        GameObject nuevoEnemigo = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Configurar los atributos del nuevo enemigo
        EnemyController enemyController = nuevoEnemigo.GetComponent<EnemyController>();
        enemyController.attackDamage = atributos[0];
        enemyController.duracion = 0f;
        enemyController.health = (int)atributos[1];
        enemyController.chaseDistance = atributos[2];
        enemyController.doneDamage = 0f;
        EnemyCount++;
        //Debug.Log("Hijo:" + atributos[0] + " " + atributos[1] + " " + atributos[2] + " " + atributos[3] + " " + atributos[4] + " " + atributos[5]);
    }

    public void Enemigo(GameObject enemigo)
    {
        Enemigos.Add(enemigo);
    }

    public float[] Mutacion(float[] atributos)
    {
        for (int i = 0; i < atributos.Length - 1; i++)
        {
            // Probabilidad de mutación
            if (Random.Range(0, 10) < 1)
            {
                // Incremento gradual basado en la oleada
                float incremento = 0.5f * ec.Oleada;  // Puedes ajustar este valor según sea necesario

                switch (i)
                {
                    case 0: // Ataque
                        Debug.Log("Ataque");
                        atributos[i] += incremento * 10 + Random.Range(-4, 10);  // Ajuste según sea necesario
                        if (atributos[i] > 100) atributos[i] = 100;
                        else if (atributos[i] < 5) atributos[i] = 5;
                        break;

                    case 1: // Vida
                        Debug.Log("Vida " + atributos[i]);
                        atributos[i] += incremento * 50 + Random.Range(-50, 50);  // Ajuste según sea necesario
                        if (atributos[i] > 10000) atributos[i] = 10000;
                        else if (atributos[i] < 100) atributos[i] = 100;
                        Debug.Log("Vida " + atributos[i]);
                        break;

                    case 2: // chaseDistance
                        Debug.Log("chaseDistance " + atributos[i]);
                        atributos[i] += incremento * 5 + Random.Range(-5, 10);  // Ajuste según sea necesario
                        if (atributos[i] > 50) atributos[i] = 50;
                        else if (atributos[i] < 10) atributos[i] = 10;
                        Debug.Log("chaseDistance " + atributos[i]);
                        break;

                    default:
                        break;
                }
            }
        }
        return atributos;
    }

    private void DestruirAliens()
    {
        GameObject[] aliens = GameObject.FindGameObjectsWithTag("ALIEN");
        foreach (GameObject alien in aliens)
        {
            Destroy(alien);
        }
    }   

}
