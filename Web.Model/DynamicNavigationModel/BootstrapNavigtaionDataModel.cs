using System;
using System.Collections.Generic;

namespace Web.Model.DynamicNavigationModel
{
    public class BootstrapNavigtaionDataModel
    {
        public List<BootstrapNavigtaionViewModel> RetrieveMenuData()
        {
            //This data model would pull the data from a database
            //The data is just hard coded here for demo purposes
            try
            {
                List<BootstrapNavigtaionViewModel> model = new List<BootstrapNavigtaionViewModel>
            {
                new BootstrapNavigtaionViewModel(manufacturer: "BMW", carType:"Petrol",model: "X5", pageUrl: "#"),
                new BootstrapNavigtaionViewModel(manufacturer: "BMW", carType:"Petrol",model: "X3", pageUrl: "#"),
                new BootstrapNavigtaionViewModel(manufacturer: "BMW", carType:"Diesel",model: "i8", pageUrl: "#"),
                new BootstrapNavigtaionViewModel(manufacturer: "BMW", carType:"Diesel",model: "Z4", pageUrl: "#"),
                new BootstrapNavigtaionViewModel(manufacturer: "Audi", carType:"Electric",model: "A4", pageUrl: "#"),
                new BootstrapNavigtaionViewModel(manufacturer: "Audi", carType:"Electric",model: "A6", pageUrl: "#"),
                new BootstrapNavigtaionViewModel(manufacturer: "Audi", carType:"Hybrid",model: "Q5", pageUrl: "#"),
                new BootstrapNavigtaionViewModel(manufacturer: "Audi", carType:"Hybrid",model: "A8", pageUrl: "#"),
                new BootstrapNavigtaionViewModel(manufacturer: "Porsche", carType:"Petrol",model: "911", pageUrl: "#"),
                new BootstrapNavigtaionViewModel(manufacturer: "Porsche", carType:"Petrol",model: "Cayenne", pageUrl: "#"),
                new BootstrapNavigtaionViewModel(manufacturer: "Porsche", carType:"Diesel",model: "Taycan", pageUrl: "#"),
                new BootstrapNavigtaionViewModel(manufacturer: "Porsche", carType:"Diesel",model: "718", pageUrl: "#")
            };

                return model;
            }
            catch (Exception e)
            {
                //Do whatever if error occurs
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
