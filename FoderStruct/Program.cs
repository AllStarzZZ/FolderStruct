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
            "/var/www/cba"
        };

        static List<string> fwrite1 = new List<string>() {
            "/var/a",
            "/var/b/c",
            "/var/www/abc",
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
