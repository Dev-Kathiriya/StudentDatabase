using System.ComponentModel.DataAnnotations;

namespace StudentDatabase.Areas.LOC_Country.Models
{
    public class LOC_City_Model : Models_Interface
    {
        #region Interface
        private void setInterfacevariables()
        {
            dataNames = new List<String> { "CityID", "StateID", "CountryID", "CityName", "Citycode", "CreationDate", "Modified" };
            publicDataNames = new List<String> { "CityName", "Citycode" };
            dropDownNamesList = new List<String> { "CountryName", "StateName" };
            noRelationDropDownNamesList = new List<String>();
        }
        public List<String> dataNames { get; set; }
        public List<String> publicDataNames { get; set; }
        public List<String> dropDownNamesList { get; set; }
        public List<String> noRelationDropDownNamesList { get; set; }
        #endregion
        #region Constructor
        public LOC_City_Model(dynamic dataRow)
        {
            if (int.TryParse(dataRow["CityID"].ToString(), out int parsedCityID)) CityID = parsedCityID;
            if (int.TryParse(dataRow["StateID"].ToString(), out int parsedStateID)) StateID = parsedStateID;
            if (int.TryParse(dataRow["CountryID"].ToString(), out int parsedCountryID)) CountryID = parsedCountryID;
            CityName = (String)dataRow["CityName"] ?? "";
            Citycode = (String)dataRow["Citycode"] ?? "";
            CountryName = (String)dataRow["CountryName"] ?? "";
            StateName = (String)dataRow["StateName"] ?? "";
            if (DateTime.TryParse(dataRow["CreationDate"].ToString(), out DateTime parsedCreated)) CreationDate = parsedCreated;
            if (DateTime.TryParse(dataRow["Modified"].ToString(), out DateTime parsedModified)) Modified = parsedModified;
            /*            CityID = (int)dataRow["CityID"];
                        CityName = (String)dataRow["CityName"];
                        Citycode = (String)dataRow["Citycode"];
                        StateID = (int)dataRow["StateID"];
                        CountryName= (String)dataRow["CountryName"];
                        StateName = (String)dataRow["StateName"];
                        CountryID = (int)dataRow["CountryID"];
                        CreationDate = (DateTime)dataRow["CreationDate"];
                        Modified = (DateTime)dataRow["Modified"];*/
            setInterfacevariables();
        }
        public LOC_City_Model() => setInterfacevariables();
        #endregion
        public int CityID { get; set; }
        [Required, StringLength(maximumLength: 50, MinimumLength = 2)]
        public String CityName { get; set; } = String.Empty;
        public String CountryName { get; set; } = String.Empty;
        public String StateName { get; set; } = String.Empty;
        [Required, StringLength(maximumLength: 50, MinimumLength = 2)]
        public String Citycode { get; set; } = String.Empty;
        [Required(ErrorMessage = "State is required")]
        public int? StateID { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public int? CountryID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Modified { get; set; }
    }
}
