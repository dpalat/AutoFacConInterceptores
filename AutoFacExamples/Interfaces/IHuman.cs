namespace TestIoC
{
    public interface IHuman
    {
        string Name { get; set; }

        [TraceResponseSize]
        void Breathe();
    }
}
