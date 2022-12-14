namespace Day_7
{
    public abstract class DirectoryNode
    {
        public string Name;

        public DirectoryNode(string name)
        {
            this.Name = name;
        }
    }

    public class Directory_ : DirectoryNode
    {
        public Directory_(string name) : base(name) {}

        public static Directory_ ParseDirectory(string directory) => new(directory.Split(" ").Last());
    }

    public class File_ : DirectoryNode
    {
        public int Size;

        private File_(string name, int size) : base(name)
        {
            this.Size = size;
        }

        public static File_ ParseFile(string file) => ParseFile(file.Split(" "));

        private static File_ ParseFile(string[] parts) => new(parts[1], int.Parse(parts[0]));
    }
}
