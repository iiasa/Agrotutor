namespace Agrotutor.Dev
{
    using System;
    using System.Collections.Generic;

    using Core.Entities;

    public class Helper
    {
        public static List<Plot> GetTestData()
        {
            List<Plot> plots = new List<Plot>
            {
                new Plot
                {
                    Activities = new List<Activity>
                    {
                        new Activity
                        {
                            ActivityType = ActivityType.Sowing,
                            Name = "Sowing",
                            Date = DateTime.Today.AddDays((double)-30)
                        },
                        new Activity
                        {
                            ActivityType = ActivityType.Harvest,
                            Name = "Harvest",
                            Date = DateTime.Today.AddDays((double)-2)
                        }
                    },
                    Position = new Position
                    {
                        Latitude = 21.0,
                        Longitude = -105.5
                    },
                    ClimateType = ClimateType.Hybrid,
                    CropType = CropType.Bean,
                    Delineation = new List<Position>
                    {
                        new Position
                        {
                            Latitude = 20.5,
                            Longitude = -105.0
                        },
                        new Position
                        {
                            Latitude = 21.5,
                            Longitude = -105.0
                        },
                        new Position
                        {
                            Latitude = 21.5,
                            Longitude = -106.0
                        },
                        new Position
                        {
                            Latitude = 20.5,
                            Longitude = -106.0
                        },
                        new Position
                        {
                            Latitude = 20.5,
                            Longitude = -105.0
                        },
                    },
                    MaturityType = MaturityType.Late,
                    Name = "Beans 1"
                },
                new Plot
                {
                    Activities = new List<Activity>
                    {
                        new Activity
                        {
                            ActivityType = ActivityType.Sowing,
                            Name = "Sowing",
                            Date = DateTime.Today.AddDays((double)-10)
                        },
                        new Activity
                        {
                            ActivityType = ActivityType.Harvest,
                            Name = "Harvest",
                            Date = DateTime.Today.AddDays((double)-4)
                        }
                    },
                    Position = new Position
                    {
                        Latitude = 23.0,
                        Longitude = -105.5
                    },
                    ClimateType = ClimateType.Hybrid,
                    CropType = CropType.Corn,
                    Delineation = new List<Position>
                    {
                        new Position
                        {
                            Latitude = 22.5,
                            Longitude = -105.0
                        },
                        new Position
                        {
                            Latitude = 23.5,
                            Longitude = -105.0
                        },
                        new Position
                        {
                            Latitude = 23.5,
                            Longitude = -106.0
                        },
                        new Position
                        {
                            Latitude = 22.5,
                            Longitude = -106.0
                        },
                        new Position
                        {
                            Latitude = 22.5,
                            Longitude = -105.0
                        },
                    },
                    MaturityType = MaturityType.Late,
                    Name = "Corn 1"
                }
            };
            return plots;
        }
    }
}
