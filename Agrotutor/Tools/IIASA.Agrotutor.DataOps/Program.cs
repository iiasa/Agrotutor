using System;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace IIASA.Agrotutor.DataOps
{
    // generate geojsons from hubcontacts.csv and investigationPlatforms.csv.
    class Program
    {
        static void Main()
        {
            Console.WriteLine("1. Prepare Hubs geojson.");
            Console.WriteLine("2. Prepare investigation_platforms geojson.");
            var option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    ProcessHubsDataCsv();
                    break;

                case 2:
                    ProcessInvestigationPlatformsDataCsv();
                    break;

                default:
                    Console.WriteLine($"Invalid input {option}");
                    Environment.Exit(-1);
                    break;
            }
        }

        private static void ProcessInvestigationPlatformsDataCsv()
        {
            Console.WriteLine("Enter path to investigation csv file. You can find sample file in solution folder.");

            var inputCsv = Console.ReadLine();
            var featureCollection = new FeatureCollection();
            var reader = GetCsvReader(inputCsv);
            reader.Read(); // read header english
            reader.Read(); // read header spanish

            while (reader.Read())
            {
                var id = reader.GetField<int>(0);
                var platform = reader.GetField<string>(1);
                var hub = reader.GetField<string>(2);
                var abbreviation = reader.GetField<string>(3);
                var initYear = reader.GetField<string>(4);
                var state = reader.GetField<string>(5);
                var municipality = reader.GetField<string>(6);
                var locality = reader.GetField<string>(7);
                var address = reader.GetField<string>(8);
                var latitude = reader.GetField<double>(9);
                var longitude = reader.GetField<double>(10);
                var asnm = reader.GetField<string>(11);
                var instituteColab = reader.GetField<string>(12);
                var campus = reader.GetField<string>(13);
                var invResp = reader.GetField<string>(14);
                var teleIr = reader.GetField<string>(15);
                var emailIr = reader.GetField<string>(16);
                var tecResp = reader.GetField<string>(17);
                var telTr = reader.GetField<string>(18);
                var emailTr = reader.GetField<string>(19);
                var culPrinc = reader.GetField<string>(20);
                var cicloAgri = reader.GetField<string>(21);
                var regHumPv = reader.GetField<string>(22);
                var regHumOi = reader.GetField<string>(23);

                var attributes = new AttributesTable
                {
                    {"NUM_ID", id},
                    {"NOM_PLAT", platform},
                    {"HUB", hub},
                    {"ABRVIACION", abbreviation},
                    {"ANIO_INST", initYear},
                    {"ESTADO", state},
                    {"MUNICIPIO", municipality},
                    {"LOCALIDAD", locality},
                    {"DIRECCION", address},
                    {"LATITUD", latitude},
                    {"LONGITUD", longitude},
                    {"ASNM", asnm},
                    {"INST_COLAB", instituteColab},
                    {"CAMPUS", campus},
                    {"INV_RESP", invResp},
                    {"TEL_IR", teleIr},
                    {"EMAIL_IR", emailIr},
                    {"TEC_RESP", tecResp},
                    {"TEL_TR", telTr},
                    {"EMAIL_TR", emailTr},
                    {"CUL_PRINC", culPrinc},
                    {"CICLO_AGRI", cicloAgri},
                    {"REG_HUM_PV", regHumPv},
                    {"REG_HUM_OI", regHumOi}
                };

                var feature = new Feature(new Point(longitude, latitude), attributes);
                featureCollection.Add(feature);
            }
            SaveCollectionToFile(featureCollection, ".\\investigation_platforms.geojson");
            reader.Dispose();
        }

        private static void ProcessHubsDataCsv()
        {
            Console.WriteLine("Enter path to Hubs csv file. You can find sample file in solution folder.");
            var inputCsv = Console.ReadLine();

            var featureCollection = new FeatureCollection();
            var reader = GetCsvReader(inputCsv);
            reader.Read(); // read header english
            reader.Read(); // read header spanish

            while (reader.Read())
            {
                var hub = reader.GetField<string>(0);
                var manager = reader.GetField<string>(1);
                var managerMail = reader.GetField<string>(2);
                var assistant = reader.GetField<string>(3);
                var assistantMail = reader.GetField<string>(4);
                var telephone = reader.GetField<string>(5);
                var latitude = reader.GetField<double>(6);
                var longitude = reader.GetField<double>(7);

                var attributes = new AttributesTable
                {
                    {"HUB", hub},
                    {"Gerente", manager},
                    {"Email_Gte", managerMail},
                    {"Asistente", assistant},
                    {"Email_Asis", assistantMail},
                    {"Telefono", telephone},
                    {"Latitud", latitude},
                    {"Longitud", longitude}
                };

                var feature = new Feature(new Point(longitude, latitude), attributes);
                featureCollection.Add(feature);
            }
            SaveCollectionToFile(featureCollection, ".\\hubs_contact.geojson");
            reader.Dispose();
        }

        private static CsvReader GetCsvReader(string inputCsv)
        {
            CsvReader reader = new CsvReader(new StreamReader(inputCsv, Encoding.GetEncoding("iso-8859-1")),
                new CsvConfiguration(CultureInfo.InvariantCulture) {Delimiter = ",", BadDataFound = BadDataFunc});
            return reader;
        }

        private static void SaveCollectionToFile(FeatureCollection featureCollection, string fileName)
        {
            var stringBuilder = new StringBuilder();
            var writer = new StringWriter(stringBuilder);
            var serializer = GeoJsonSerializer.CreateDefault();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Serialize(writer, featureCollection);
            writer.Flush();
            File.WriteAllText(fileName, stringBuilder.ToString());
        }

        private static void BadDataFunc(ReadingContext obj)
        {
            Console.WriteLine($"Error - {obj.Field}");
        }
    }
}
