namespace SevenTamTest
{
    public interface IDamegable
    {
        public int Health {  get; }

        public void TakeDamage(int damage);
    }
}
