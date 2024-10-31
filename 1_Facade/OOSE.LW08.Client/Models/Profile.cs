using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOSE.LW08.Client.Models
{
    public class Profile
    {
        public string Username { get; set; }
        public ColorTheme ColorTheme { get; set; }

        public override string ToString()
        {
            return $"{Username}: Color Theme {ColorTheme}";
        }
    }
}
