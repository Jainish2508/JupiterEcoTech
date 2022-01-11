using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JupiterEcoTech.Models
{
    public class HomeViewModels
    {
        public string ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductFile { get; set; }

        public List<HomeViewModels> RandomProducts { get; set; }
    }
}