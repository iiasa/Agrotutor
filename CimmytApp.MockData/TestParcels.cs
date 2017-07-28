using System;
using System.Collections.Generic;
using CimmytApp.DTO;
using CimmytApp.DTO.Parcel;

namespace CimmytApp.MockData
{
    public class TestParcels : List<Parcel>
    {
        public TestParcels()
        {
            this.Add(new Parcel()
            {
                ID = 0,
                AgriculturalCycle = "Spring-Summer",
                Crop = "Maize",
                Cultivar = "Example Cultivar",
                EstimatedParcelArea = "10",
                GeoPosition = new GeoPosition()
                {
                    Accuracy = 10,
                    Latitude = 48,
                    Longitude = 17
                },
                Irrigation = "Irrigated",
                ParcelName = "Testparcel 1",
                Performance = "1.2",
                PesticidesApplied = new List<PesticideApplication>(){
                    new PesticideApplication(){
                        ProductName = "Pesticide Product",
                        ApplicationDate = "2017-01-14"
                    },
                    new PesticideApplication(){
                        ProductName = "Pesticide Product 2",
                        ApplicationDate = "2017-01-15"
                    }
                },
                PlantingDate = new DateTime(),
                TechnologiesUsed = new List<string>(),
                Year = "2017"
            });

            this.Add(new Parcel()
            {
                ID = 1,
                AgriculturalCycle = "Spring-Summer",
                Crop = "Maize",
                Cultivar = "Example Cultivar 2",
                EstimatedParcelArea = "10",
                GeoPosition = new GeoPosition()
                {
                    Accuracy = 10,
                    Latitude = 48.01,
                    Longitude = 17.02
                },
                Irrigation = "Irrigated",
                ParcelName = "Testparcel 2",
                Performance = "1.2",
                PesticidesApplied = new List<PesticideApplication>(){
                    new PesticideApplication(){
                        ProductName = "Pesticide Product",
                        ApplicationDate = "2017-01-14"
                    },
                    new PesticideApplication(){
                        ProductName = "Pesticide Product 2",
                        ApplicationDate = "2017-01-15"
                    }
                },
                PlantingDate = new DateTime(),
                TechnologiesUsed = new List<string>(),
                Year = "2017"
            });

        }
    }
}
