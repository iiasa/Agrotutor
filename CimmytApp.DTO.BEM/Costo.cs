namespace CimmytApp.DTO.BEM
{
    using Newtonsoft.Json;
    using SQLite.Net.Attributes;

    [Table("BEM-Costo")]
    public class Costo : BemDataset
    {
        [JsonProperty("id_bitacora")]
        public string BitacoraId { get; set; }

        [JsonProperty("id_tipo_bitacora")]
        public string BitacoraTypeId { get; set; }

        [JsonProperty("anio")]
        public string Year { get; set; }

        [JsonProperty("ciclo")]
        public string AgriculturalCycle { get; set; }

        [JsonProperty("tipo_produccion")]
        public string ProductionType { get; set; }

        [JsonProperty("tipo_parcela")]
        public string PlotType { get; set; }

        [JsonProperty("id_hub")]
        public string HubId { get; set; }

        [JsonProperty("hub")]
        public string Hub { get; set; }

        [JsonProperty("id_estado")]
        public string StateId { get; set; }

        [JsonProperty("estado")]
        public string State { get; set; }

        [JsonProperty("id_municipio")]
        public string MunicipalityId { get; set; }

        [JsonProperty("municipio")]
        public string Municipality { get; set; }

        [JsonProperty("id_localidad")]
        public string LocationId { get; set; }

        [JsonProperty("localidad")]
        public string Location { get; set; }

        [JsonProperty("bi_preparacion_del_suelo_con_herbicida_ha")]
        public string HerbicidesForSoilPreparation { get; set; }

        [JsonProperty("bii_preparacion_mecanica_del_suelo_ha")]
        public string MechanicalSoilPreparation { get; set; }

        [JsonProperty("c_siembra_ha")]
        public string Sowing { get; set; }

        [JsonProperty("ci_resiembra_ha")]
        public string Resowing { get; set; }

        [JsonProperty("d_analisis_de_suelo_ha")]
        public string SoilAnalysis { get; set; }

        [JsonProperty("fi_fertilizacion_quimica_al_suelo_ha")]
        public string ChemicFertilizerSoil { get; set; }

        [JsonProperty("fii_fertilizacion_quimica_foliar_ha")]
        public string ChemicFertilizerLeaves { get; set; }

        [JsonProperty("fii_fertilizacion_organica_abono_ha")]
        public string OrganicalFertilizer { get; set; }

        [JsonProperty("g_uso_de_biofertilizantes_ha")]
        public string UseOfBiofertilizers { get; set; }

        [JsonProperty("h_uso_de_mejoradores_ha")]
        public string UseOfEnhancers { get; set; }

        [JsonProperty("i_riego_ha")]
        public string Irrigation { get; set; }

        [JsonProperty("ji_control_quimico_de_malezas_despues_de_la_siembra_ha")]
        public string ChemicalControlOfWeedsAfterSowing { get; set; }

        [JsonProperty("jii_labores_culturales_y_control_fisico_de_malezas_ha")]
        public string CulturalPracticesPhysicalWeedControl { get; set; }

        [JsonProperty("k_insecticida_ha")]
        public string Insecticides { get; set; }

        [JsonProperty("l_fungicidas_ha")]
        public string Fungicides { get; set; }

        [JsonProperty("mi_cosecha_manual_ha")]
        public string ManualHarvesting { get; set; }

        [JsonProperty("mii_cosecha_mecanica_ha")]
        public string MechanicalHarvest { get; set; }

        [JsonProperty("o_comercializacion_ha")]
        public string Commercialization { get; set; }

        [JsonProperty("r_gastos_indirectos_ha")]
        public string IndirectExpenses { get; set; }

        [JsonProperty("costos_produccion_ha")]
        public string ProductionCost { get; set; }
    }
}