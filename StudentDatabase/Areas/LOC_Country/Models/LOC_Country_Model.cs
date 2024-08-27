using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentDatabase.Areas.LOC_Country.Models
{
    public class LOC_Country_Model : Models_Interface
    {
        #region Constructor
        public LOC_Country_Model() => setInterfacevariables();
        public LOC_Country_Model(dynamic dataRow)
        {
            if (int.TryParse(dataRow["CountryID"].ToString(), out int parsedCountryID)) CountryID = parsedCountryID;
            CountryName = dataRow["CountryName"] ?? "";
            CountryCode = dataRow["CountryCode"] ?? "";
            if (DateTime.TryParse(dataRow["Created"].ToString(), out DateTime parsedCreated)) Created = parsedCreated;
            if (DateTime.TryParse(dataRow["Modified"].ToString(), out DateTime parsedModified)) Modified = parsedModified;
            /*            CountryID = (int) dataRow["CountryID"];
                        CountryName =(String) dataRow["CountryName"];
                        CountryCode=(String) dataRow["CountryCode"];
                        Created=(DateTime) dataRow["Created"];
                        Modified= (DateTime) dataRow["Modified"];*/
            setInterfacevariables();
        }
        #endregion
        #region Interface
        private void setInterfacevariables()
        {
            dataNames = new List<String> { "CountryID", "CountryName", "CountryCode", "Created", "Modified" };
            publicDataNames = new List<string> { "CountryName", "CountryCode" };
            dropDownNamesList = new List<String> { };
            noRelationDropDownNamesList = new List<String>();
        }
        public List<String> dataNames { get; set; }
        public List<String> publicDataNames { get; set; }
        public List<String> dropDownNamesList { get; set; }
        public List<String> noRelationDropDownNamesList { get; set; }
        #endregion
        public int CountryID { get; set; }
        [Required, MinLength(6), DisplayName("Country Name")]
        public string CountryName { get; set; } = string.Empty;
        [Required, MinLength(2), DisplayName("Country Code")]
        public string CountryCode { get; set; } = string.Empty;
        private DateTime Created { get; set; }
        private DateTime Modified { get; set; }
    }
}
