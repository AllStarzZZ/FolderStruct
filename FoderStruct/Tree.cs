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
        Dictionary<string, TreeItem> folderDictionary = new Dictionary<string, TreeItem>();

        public TreeItem GetWritableFolderStructure(
            List<string> readableFolders,
            List<string> writableFolders)
        {
            BuildDictionaries(readableFolders, writableFolders);
            CalculateWeightForEachNode();
            return root;
        }

        void BuildDictionaries(
            List<string> readableFolders,
            List<string> writableFolders)
        {
            int lastCount = 0;
            do
            {
                lastCount = readableFolders.Count;
                List<string> shouldExcept = new List<string>();

                foreach (string path in readableFolders)
                {
                    if (path.LastIndexOf(Separator) == 0)
                    {
                        TreeItem tmp = new TreeItem(path);
                        folderDictionary.Add(path, tmp);
                        root = tmp;
                        shouldExcept.Add(path);
                    }
                    else
                    {
                        string parent = GetPartentDir(path);
                        if (folderDictionary.ContainsKey(parent))
                        {
                            TreeItem tmp = new TreeItem(path);
                            folderDictionary[parent].AddChild(tmp);
                            folderDictionary.Add(path, tmp);
                            shouldExcept.Add(path);
                        }
                    }
                }

                readableFolders = readableFolders.Except(shouldExcept).ToList();
            } while (lastCount != readableFolders.Count && lastCount != 0);

            foreach (string path in writableFolders)
            {
                try
                {
                    folderDictionary[path].State = State.Writeable;
                }catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        void CalculateWeightForEachNode()
        {
            DepthFirstSearch(root);
        }

        int DepthFirstSearch(TreeItem node)
        {
            int currentWeight = (int)node.State;
            foreach (TreeItem child in node.Children)
            {
                currentWeight += DepthFirstSearch(child);
            }
            node.Weight = currentWeight;
            
            return currentWeight;
        }

        string GetPartentDir(string path) => path.Substring(0, path.Length - (path.Length - path.LastIndexOf('/')));
    }
}
