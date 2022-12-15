namespace Day_7
{
    public class DirectoryTree
    {
        private DirectoryNode Value { get; set; }

        private List<DirectoryTree> Children { get; set; }

        public DirectoryTree? Parent { get; set; }

        public DirectoryTree(DirectoryNode value)
        {
            this.Value = value;
            this.Children = new List<DirectoryTree>();
        }

        public DirectoryTree(DirectoryNode value, DirectoryTree parent)
        {
            this.Value = value;
            this.Parent = parent;
            this.Children = new List<DirectoryTree>();
        }

        public void AddChild(DirectoryNode child) => this.Children.Add(new DirectoryTree(child, this));

        public DirectoryTree GetChild(string name) => this.Children.First(child => child.Value.Name == name);

        public int GetSize() => this.Value switch
        {
            Directory_ => this.Children.Select(child => child.GetSize()).Sum(),
            File_ file => file.Size,
            _          => throw new Exception()
        };

        public List<int> GetDirectorySizes() => this.GetDirectorySizes(new List<int>());

        public static DirectoryTree ParseDirectoryTree(string inputFile)
        {
            string[] lines = File.ReadAllLines(inputFile);
            var root = new DirectoryTree(new Directory_("/"));
            var currentNode = root;

            foreach (var line in lines)
            {
                if (line.StartsWith("$ cd"))
                {
                    currentNode = ChangeDirectory(line, root, currentNode);
                }
                else if (line.StartsWith("dir"))
                {
                    currentNode?.AddChild(Directory_.ParseDirectory(line));
                }
                else if (!line.StartsWith("$ ls"))
                {
                    currentNode?.AddChild(File_.ParseFile(line));
                }
            }

            return root;
        }

        private static DirectoryTree? ChangeDirectory(string cd, DirectoryTree root, DirectoryTree? currentNode)
        {
            var directory = Directory_.ParseDirectory(cd);

            return directory.Name switch
            {
                "/"  => root,
                ".." => currentNode?.Parent,
                _    => currentNode?.GetChild(directory.Name)
            };
        }

        private List<int> GetDirectorySizes(List<int> sizes)
        {
            if (this.Value is Directory_)
            {
                sizes.Add(this.GetSize());
                this.Children.ForEach(child => child.GetDirectorySizes(sizes));
            }

            return sizes;
        }
    }
}
