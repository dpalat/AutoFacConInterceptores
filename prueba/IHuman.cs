namespace TestIoC
{
    public interface IHuman
    {
        string Name { get; set; }

        [MedirConsumo]
        void Breathe();
    }
}
