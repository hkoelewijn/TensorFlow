namespace WindowsApplicationTester
{
    public class ProcessInfo
    {
        public ProcessInfo(uint id, string name, string path)
        {
            Id = id;
            Name = name;
            Path = path;
        }

        public uint Id { get; }

        public string Name { get; }

        public string Path { get; }
    }
}