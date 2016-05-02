using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Skunkworks.Ics.Web.Models
{
    public class SnippetModel
    {
        public int Id { get; set; }

        [Display(Name="User-Name")]
        public string UserName { get; set; }

        public string Title { get; set; }

        public string Language { get; set; }

        public string Code { get; set; }

    }
}