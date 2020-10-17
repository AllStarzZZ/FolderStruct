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
        Writeable = 1,
        None = 2
    }

    class TreeItem
    {
        string name;
        List<TreeItem> children;

        public State State { get; set; }

        public TreeItem(string name)
        {
            this.name = name;            
            children = new List<TreeItem>();
            State = State.Readable;
        }

        public void AddChild(TreeItem c) => children.Add(c);

        public override string ToString() => $"name: {name}, direct subfolders: {children.Count}, state: {State.ToString()}";
    }
}
