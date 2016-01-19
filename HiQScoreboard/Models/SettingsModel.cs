using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiQScoreboard.Models
{
    public class SettingsModel
    {
        public SettingsModel()
        {

        }
        public bool AllowPush { get; set; }
        public Offices Office { get; set; }
    }
}