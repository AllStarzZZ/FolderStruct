using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoderStruct
{
    public enum State
    {
        Readable = 0,
        Writeable = 1
    }

    class TreeItem
    {
        string name;
        List<TreeItem> children;

        public string Name { get { return name; }}

        public List<TreeItem> Children { get { return children; } }

        public int Weight { get; set; }
        
        public State State { get; set; }

        public TreeItem(string name)
        {
            this.name = name;            
            children = new List<TreeItem>();
            State = State.Readable;
        }

        public void AddChild(TreeItem c) => Children.Add(c);

        public override string ToString() => $"name: {name}, direct subfolders: {Children.Count}, state: {State.ToString()}";
    }
}
