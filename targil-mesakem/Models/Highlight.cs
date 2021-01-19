namespace targil_mesakem.Models
{
    public class Highlight
    {
        int id;
        string name;

        public Highlight(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Highlight() { }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}