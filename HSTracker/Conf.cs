using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utility.ModifyRegistry;

namespace HSTracker
{   
    public class Conf
    {
        ModifyRegistry uninstallRegistry = new ModifyRegistry();
        string         uninstallSubKey   = @"SOFTWARE\\Microsoft\Windows\CurrentVersion\Uninstall\Hearthstone";

        public Conf()
        {
            uninstallRegistry.ShowError = true;
            uninstallRegistry.SubKey = uninstallSubKey;
        }

        public string LogPath()
        {
            return uninstallRegistry.Read("InstallLocation") + @"\Hearthstone_Data\output.log";
        }
    }
}
