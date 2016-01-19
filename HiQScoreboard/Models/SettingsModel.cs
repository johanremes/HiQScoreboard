using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HiQScoreboard.Models
{
    public class SettingsModel
    {
        public const string PushBtnLabel = "Tillåt";

        [Display(Name = "Push")]
        public bool AllowPush { get; set; }

        [Display(Name = "Kontor")]
        public Offices Office { get; set; }
    }
}