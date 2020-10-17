using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoderStruct
{
    class Program
    {
        static List<string> fread1 = new List<string>() {
            "/var",
            "/var/a",
            "/var/www",
            "/etc/pw",
            "/var/b",
            "/var/b/c",
            "/var/www/abc",
            "/var/asd/g",
            "/var/www/cba",
            "/var/b/c/d",
            "/var/b/c/d/e",
            "/var/b/c/d/f",
            "/var/b/c/d/e/g",
            "/var/b/c/d/e/k",
            "/var/sec/vis",
            "/var/sec/moresec/secread",
            "/var/er1",
            "/var/er1/er2",
            "/var/er1/er2/er3",
        };

        static List<string> fwrite1 = new List<string>() {
            "/var/a",
            "/var/b/c",
            "/var/www/abc",
            "/var/b/c/d/e/g",
            "/var/b/c/d/e/k",
            "/var/sec/vis",
        };

        static void Main(string[] args)
        {
            Tree tree = new Tree();
            TreeItem root = tree.GetWritableFolderStructure(fread1, fwrite1);
            tree.ShowStructure(root);
            ;
        }
    }
}
