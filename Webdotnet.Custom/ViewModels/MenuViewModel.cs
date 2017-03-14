using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webdotnet.Custom.ViewModels
{
    public class MenuViewModel
    {
        public List<NavElement> Pages { get; set; }
        public List<NavSocials> SocialItems { get; set; }
    }

    public class NavElement
    {
        public string Name { get; set; }
        public bool IsVisibleInMenu { get; set; }
        public bool IsActive { get; set; }
        public string Url { get; set; }

    }
    public class NavSocials
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string FontAwesomeClass { get; set; }
        
    }
}
