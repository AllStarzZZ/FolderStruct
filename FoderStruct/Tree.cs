using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoderStruct
{
    class Tree
    {
        static readonly char Separator = '/';

        TreeItem root;
        Dictionary<string, TreeItem> folderDictionary;

        public Tree()
        {
            folderDictionary = new Dictionary<string, TreeItem>();
        }

        public TreeItem GetWritableFolderStructure(
            List<string> readableFolders,
            List<string> writableFolders)
        {
            ValidateInputList(readableFolders);
            ValidateInputList(writableFolders);

            BuildDictionary(readableFolders, writableFolders);
            CalculateWeightForEachNode(root);
            RemoveNodesWithZeroWeight();

            return root;
        }

        // build a dictionary where each node can be access via it's absolute path
        // this speed up the seach when we try to find the parent directory
        private void BuildDictionary(
            List<string> readableFolders,
            List<string> writableFolders)
        {
            int lastCount = 0;
            readableFolders.Sort();

            do
            {
                lastCount = readableFolders.Count;
                List<string> registeredDirectories = new List<string>();

                foreach (string path in readableFolders)
                {
                    // select the only one root directory
                    // should only run once
                    if (path.LastIndexOf(Separator) == 0)
                    {
                        TreeItem tmp = new TreeItem(path);
                        folderDictionary.Add(path, tmp);
                        root = tmp;
                        registeredDirectories.Add(path);
                    }
                    else
                    {
                        string parent = GetParentDir(path);
                        if (folderDictionary.ContainsKey(parent))
                        {
                            try
                            {
                                TreeItem tmp = new TreeItem(path);
                                folderDictionary.Add(path, tmp);
                                folderDictionary[parent].AddChild(tmp);
                                registeredDirectories.Add(path);
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine($"Path: ({path}) is already in the dictionary, this instance ignored.");
                            }
                        }
                    }
                }

                readableFolders = readableFolders.Except(registeredDirectories).ToList();
            } while (lastCount != readableFolders.Count && lastCount != 0);

            foreach (string path in writableFolders)
            {
                try
                {
                    folderDictionary[path].State = State.Writeable;
                }catch (KeyNotFoundException)
                {
                    Console.WriteLine($"Writeable, but has non readable parent, path: {path}");
                }
            }
        }

        // using depth first search implemented by a recursive method
        private int CalculateWeightForEachNode(TreeItem node)
        {
            int currentWeight = (int)node.State;
            foreach (TreeItem child in node.Children)
            {
                currentWeight += CalculateWeightForEachNode(child);
            }
            node.Weight = currentWeight;
            
            return currentWeight;
        }

        private void RemoveNodesWithZeroWeight()
        {
            Queue<TreeItem> treeItemQueue = new Queue<TreeItem>();

            treeItemQueue.Enqueue(root);
            BreadthFirstSearch(treeItemQueue, (TreeItem item) =>
            {
                if(item.Weight == 0)
                {
                    folderDictionary[GetParentDir(item.Name)].Children.Remove(item);
                }
            });
        }

        private void BreadthFirstSearch(Queue<TreeItem> treeItemQueue, Action<TreeItem> process)
        {
            while(treeItemQueue.Count != 0)
            {
                TreeItem currentNode = treeItemQueue.Dequeue();
                process(currentNode);
                foreach (TreeItem child in currentNode.Children)
                {
                    treeItemQueue.Enqueue(child);
                }
            }
        }

        private void ValidateInputList(List<string> input)
        {
            foreach (string path in input)
            {
                if(!path.StartsWith("/") ||
                    path.EndsWith("/") ||
                    path.Contains("\\"))
                {
                    throw new BadInputFormatException(path);
                }
            }
        }

        public void ShowStructure(TreeItem root)
        {
            Queue<TreeItem> treeItemQueue = new Queue<TreeItem>();
            treeItemQueue.Enqueue(root);
            BreadthFirstSearch(treeItemQueue, (TreeItem item) => Console.WriteLine(item.Name));
        }

        private string GetParentDir(string path) => path.Substring(0, path.Length - (path.Length - path.LastIndexOf('/')));
    }
}
