using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FoderStruct;

namespace FoderStruct.UnitTests
{
    public class StructureBuildTest
    {
        private readonly Tree tree;

        public StructureBuildTest()
        {
            tree = new Tree();
        }

        [Fact]
        public void NReadbleNWriteable_ReturnNElement()
        {
            List<string> r = new List<string>() { "/var", "/var/www", "/var/asd" };
            List<string> w = new List<string>() { "/var", "/var/www", "/var/asd" };

            tree.GetWritableFolderStructure(r, w);

            Assert.Equal(3, tree.ValidTreeItems);
        }

        [Fact]
        public void NReadbleNMinusOneWriteable_ReturnNMinusOneElement()
        {
            List<string> r = new List<string>() { "/var", "/var/www", "/var/asd" };
            List<string> w = new List<string>() { "/var", "/var/www" };

            tree.GetWritableFolderStructure(r, w);

            Assert.Equal(2, tree.ValidTreeItems);
        }

        [Fact]
        public void HasWriteableViaSecretFolder_IgnoreTheSubTree()
        {
            List<string> r = new List<string>() { "/var", "/var/www" };
            List<string> w = new List<string>() { "/var", "/var/www", "/var/secret/asd" };

            tree.GetWritableFolderStructure(r, w);

            Assert.Equal(2, tree.ValidTreeItems);
        }

        [Fact]
        public void HasReadableViaSecretFolder_IgnoreTheSubTree()
        {
            List<string> r = new List<string>() { "/var", "/var/www", "/var/www/secret/asd" };
            List<string> w = new List<string>() { "/var", "/var/www" };

            tree.GetWritableFolderStructure(r, w);

            Assert.Equal(2, tree.ValidTreeItems);
        }

        [Fact]
        public void HasWriteableLeafsViaReadableFolder_ReserveTheSubTree()
        {
            List<string> r = new List<string>() { "/var", "/var/www", "/var/www/a", "/var/www/b" };
            List<string> w = new List<string>() { "/var", "/var/www/a", "/var/www/b" };

            tree.GetWritableFolderStructure(r, w);

            Assert.Equal(4, tree.ValidTreeItems);
        }
    }
}
