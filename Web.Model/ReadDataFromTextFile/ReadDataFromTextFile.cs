﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Model.ReadDataFromTextFile
{
    public class DisplayDropDownMenuViewModel
    {
        public string County        { get; set; }
        public IEnumerable<SelectListItem> ListOfCountys    { get; set; }
    }
    public class DropDownListOfCountysDataModel
    {
        public string Counties { get; set; }
        public IEnumerable<SelectListItem> ListOfCounty => GetCounties();

        private IEnumerable<SelectListItem> GetCounties()
        {
            const string directory  = @"ListOfCounties";
            const string fileName   = "UnitedKingdom.txt";
            string pathToFile       = File.ReadAllText(HttpContext.Current.Server.MapPath($"~/{directory}/{fileName}"));

            string[] counties = pathToFile.Split(',');

            var retrieveCounties = counties.Select(r => new SelectListItem { Text = r.Trim(), Value = r.Trim() }).ToList();

            return retrieveCounties;
        }
    }
}