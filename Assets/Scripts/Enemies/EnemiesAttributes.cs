public class EnemyAttributes
{
    public float attackDamage;
    public int health;
    public float chaseDistance;
    public float importancia;


    public EnemyAttributes()
    {
        this.attackDamage = 0;    
        this.health = 0;
        this.chaseDistance = 0;
        this.importancia = 0;
    }

    public EnemyAttributes(float attackDamage, float duracion, int health,  float chaseDistance, float doneDamage)
    {
        this.attackDamage = attackDamage;
        this.health = health;
        this.chaseDistance = chaseDistance;
        this.importancia = 0.3f * duracion + 0.7f * doneDamage;
    }

    public float[] getAtributos()
    {
        return new float[]
       {
            attackDamage,
            health, // Convertir a float expl�citamente
            chaseDistance,
            importancia
       };
    }
}