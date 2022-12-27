using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using OpenMcdf;

namespace SettingsHelper
{
    public class CFFolder
    {
        private CFStorage _parent;

        private CFStorage _storage;

        public List<CFFolder> _folders;

        public List<CFStream> _files;

        public CFFolder(CFStorage parent, CFStorage storage, CompoundFile compoundFile)
        {
            _parent = parent;
            _storage = storage;

            _folders = new List<CFFolder>();
            _files = new List<CFStream>();

            initializeFolder(compoundFile);
        }

        private void initializeFolder(CompoundFile cf)
        {
            int num = cf.GetNumDirectories();
            for(int i  = 0; i < num; i++)
            {
                string entryName = cf.GetNameDirEntry(i);
                if(cf.GetStorageType(i) == StgType.StgStorage)
                {
                    _storage.TryGetStorage(entryName, out CFStorage cfStorage);
                    if(cfStorage != null)
                    {
                        CFFolder folder = new CFFolder(_storage, cfStorage, cf);
                        _folders.Add(folder);
                    }
                }
                else if(cf.GetStorageType(i) == StgType.StgStream)
                {
                    _storage.TryGetStream(entryName, out CFStream cfStream);
                    if(cfStream != null)
                    {
                        _files.Add(cfStream);
                    }
                }
            }
        }

        public CFStorage Parent { get { return _parent; } }

        public List<CFFolder> Folders
        {
            get
            {
                return _folders;
            }
        }
        public List<CFStream> Files
        {
            get
            {
                return _files;
            }
        }

        public void ExtractTo(string path)
        {
            string pathName = path;
            if(!_storage.IsRoot)
            {
                pathName = Path.Combine(path, _storage.Name);
                Console.WriteLine("Extracting folder: " + pathName);
            }
            if (!Directory.Exists(pathName))
            {
                Directory.CreateDirectory(pathName);
            }

            foreach (CFStream stream in _files)
            {
                string fileName = Path.Combine(pathName, stream.Name);
                Console.WriteLine(" Extracting file: " + fileName);
                byte[] bytes = stream.GetData();
                File.WriteAllBytes(fileName, bytes);
            }

            foreach(CFFolder folder in _folders)
            {
                folder.ExtractTo(pathName);
            }
        }

        public CFStream FindStream(string name, bool recursive)
        {
            CFStream stream = null;
            foreach(CFStream file in _files)
            {
                if(file.Name.Equals(name))
                {
                    stream = file;
                    break;
                }
            }

            if(stream != null)
            {
                return stream;
            }

            if(!recursive)
            {
                return stream;
            }

            foreach(CFFolder folder in _folders)
            {
                stream = folder.FindStream(name, recursive);
                if(stream != null)
                {
                    return stream;
                }
            }

            return stream;
        }
    }
}
