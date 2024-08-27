using System.ComponentModel.DataAnnotations;

namespace StudentDatabase.Areas.LOC_Country.Models
{
    public class LOC_State_Model : Models_Interface
    {
        #region Constructor
        public LOC_State_Model(dynamic dataRow)
        {
            if (int.TryParse(dataRow["StateID"].ToString(), out int parsedStateID)) StateID = parsedStateID;
            if (int.TryParse(dataRow["CountryID"].ToString(), out int parsedCountryID)) CountryID = parsedCountryID;
            StateName = (String)dataRow["StateName"] ?? "";
            StateCode = (String)dataRow["StateCode"] ?? "";
            CountryName = (String)dataRow["CountryName"] ?? "";
            if (DateTime.TryParse(dataRow["Created"].ToString(), out DateTime parsedCreated)) Created = parsedCreated;
            if (DateTime.TryParse(dataRow["Modified"].ToString(), out DateTime parsedModified)) Modified = parsedModified;
            /*            StateID = (int)dataRow["StateID"];
                        StateName = (String)dataRow["StateName"];
                        StateCode = (String)dataRow["StateCode"];
                        CountryName= (String)dataRow["CountryName"];
                        CountryID = (int)dataRow["CountryID"];
                        Created = (DateTime)dataRow["Created"];
                        Modified = (DateTime)dataRow["Modified"];*/
            setInterfacevariables();
        }
        public LOC_State_Model() => setInterfacevariables();
        #endregion
        #region Interface
        private void setInterfacevariables()
        {
            dataNames = new List<String> { "StateID", "CountryID", "StateName", "StateCode", "Created", "Modified" };
            publicDataNames = new List<String> { "StateName", "StateCode" };
            dropDownNamesList = new List<String> { "CountryName" };
            noRelationDropDownNamesList = new List<String>();
        }
        public List<String> dataNames { get; set; }
        public List<String> publicDataNames { get; set; }
        public List<String> dropDownNamesList { get; set; }
        public List<String> noRelationDropDownNamesList { get; set; }
        #endregion
        public int StateID { get; set; }
        [Required]
        public String StateName { get; set; } = String.Empty;
        public String CountryName { get; set; } = String.Empty;
        [Required]
        public String StateCode { get; set; } = String.Empty;
        [Required(ErrorMessage = "Country is required")]
        public int? CountryID { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
