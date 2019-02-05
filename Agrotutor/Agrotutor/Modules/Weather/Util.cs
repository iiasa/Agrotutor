namespace Agrotutor.Modules.Weather
{
    using System;
    using Xamarin.Essentials;

    public static class Util
    {
        public static bool ShouldRefresh(Xamarin.Essentials.Location oldLocation, Xamarin.Essentials.Location newLocation)
        {
            if (newLocation == null) return false;
            if (oldLocation == null) return true;
            if (newLocation.Timestamp.Subtract(oldLocation.Timestamp)
                > TimeSpan.FromMinutes(Constants.MainMapWeatherRefreshDuration))
            {
                return true;
            }

            if (newLocation.CalculateDistance(oldLocation, DistanceUnits.Kilometers)
                > Constants.MainMapWeatherRefreshDistance)
            {
                return true;
            }

            return false;
        }

        public static string GetIconSrcForWx(string wxCode)
        {
            switch (wxCode)
            {
                case "3":
                    return "blowing_dust.png";

                case "5":
                    return "haze.png";

                case "7":
                    return "blowing_dust.png";

                case "10":
                    return "patchy_fog.png";

                case "18":
                    return "cloudy.png";

                case "31":
                    return "cloudy.png";

                case "34":
                    return "cloudy.png";

                case "36":
                    return "blowing_snow.png";

                case "38":
                    return "blowing_snow.png";

                case "41":
                    return "patchy_fog.png";

                case "45":
                    return "patchy_fog.png";

                case "49":
                    return "patchy_fog.png";

                case "51":
                    return "drizzle.png";

                case "53":
                    return "drizzle.png";

                case "55":
                    return "drizzle.png";

                case "56":
                    return "light freezing drizzle";

                case "57":
                    return "freezing_drizzle.png";

                case "61":
                    return "light_rain.png";

                case "63":
                    return "rain.png";

                case "65":
                    return "rain_showers.png";

                case "66":
                    return "freezing_rain.png";

                case "67":
                    return "freezing_rain.png";

                case "68":
                    return "sleet.png";

                case "69":
                    return "sleet.png";

                case "71":
                    return "snow.png";

                case "73":
                    return "snow.png";

                case "75":
                    return "heavy_snow.png";

                case "79":
                    return "sleet.png";

                case "80":
                    return "light_rain.png";

                case "81":
                    return "rain_showers.png";

                case "83":
                    return "sleet.png";

                case "84":
                    return "sleet.png";

                case "85":
                    return "light_snow.png";

                case "86":
                    return "snow.png";

                case "89":
                    return "light_hail.png";

                case "90":
                    return "hail.png";

                case "95":
                    return "thunderstorms.png";

                case "97":
                    return "thunderstorms.png";

                case "100":
                    return "clear.png";

                case "101":
                    return "mostly_clear.png";

                case "102":
                    return "partly_cloudy.png";

                case "103":
                    return "mostly_cloudy.png";

                case "104":
                    return "partly_cloudy.png";

                default:
                    return "question.png";
            }
        }

        public static string GetTinyIconSrcForWx(string wxCode)
        {
            return "tiny_" + GetIconSrcForWx(wxCode);
        }

        /// <summary>
        ///     The GetTextForWx
        /// </summary>
        /// <param name="wxCode">The <see cref="string" /></param>
        /// <returns>The <see cref="string" /></returns>
        public static string GetTextForWx(string wxCode)
        {
            switch (wxCode)
            {
                case "3":
                    return "fumar";

                case "5":
                    return "calina";

                case "7":
                    return "soplando polvo";

                case "10":
                    return "niebla ligera";

                case "18":
                    return "tempestad";

                case "31":
                    return "tormenta de polvo";

                case "34":
                    return "tormenta de polvo severo";

                case "36":
                    return "nieve que deriva";

                case "38":
                    return "nieve que sopla";

                case "41":
                    return "niebla irregular";

                case "45":
                    return "niebla";

                case "49":
                    return "niebla helada";

                case "51":
                    return "llovizna ligera";

                case "53":
                    return "llovizna";

                case "55":
                    return "fuerte llovizna";

                case "56":
                    return "llovizna helada ligera";

                case "57":
                    return "llovizna helada";

                case "61":
                    return "lluvia ligera";

                case "63":
                    return "lluvia";

                case "65":
                    return "lluvia pesada";

                case "66":
                    return "lluvia helada ligera";

                case "67":
                    return "lluvia helada";

                case "68":
                    return "lluvia ligera y nieve";

                case "69":
                    return "lluvia y nieve";

                case "71":
                    return "nieve ligera";

                case "73":
                    return "nieve";

                case "75":
                    return "fuertes nevadas";

                case "79":
                    return "aguanieve";

                case "80":
                    return "ducha de lluvia ligera";

                case "81":
                    return "ducha de lluvia";

                case "83":
                    return "lluvia ligera y nieve";

                case "84":
                    return "lluvia y nieve";

                case "85":
                    return "ducha de nieve ligera";

                case "86":
                    return "ducha de nieve";

                case "89":
                    return "granizo ligero";

                case "90":
                    return "granizo";

                case "95":
                    return "tormenta";

                case "97":
                    return "fuerte tormenta";

                case "100":
                    return "despejado";

                case "101":
                    return "mayormente despejado";

                case "102":
                    return "parcialmente despejado";

                case "103":
                    return "mayormente despejado";

                case "104":
                    return "nublado";

                default:
                    return "desconocido";
            }
        }
    }
}
