using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentDatabase.Areas.LOC_Country.Models
{
    public class LOC_Branch_Model : Models_Interface
    {
        #region Constructor
        public LOC_Branch_Model(dynamic dataRow)
        {
            if (int.TryParse(dataRow["BranchID"].ToString(), out int parsedBranchID)) BranchID = parsedBranchID;
            BranchName = (String)dataRow["BranchName"] ?? "";
            BranchCode = (String)dataRow["BranchCode"] ?? "";
            if (DateTime.TryParse(dataRow["Created"].ToString(), out DateTime parsedCreated)) Created = parsedCreated;
            if (DateTime.TryParse(dataRow["Modified"].ToString(), out DateTime parsedModified)) Modified = parsedModified;
            /*            BranchID = (int)dataRow["BranchID"];
                        BranchName = (String)dataRow["BranchName"];
                        BranchCode = (String)dataRow["BranchCode"];
                        Created = (DateTime)dataRow["Created"];
                        Modified = (DateTime)dataRow["Modified"];*/
            setInterfacevariables();
        }
        public LOC_Branch_Model() => setInterfacevariables();
        #endregion
        #region Interface
        private void setInterfacevariables()
        {
            dataNames = new List<String> { "BranchID", "BranchName", "BranchCode", "Created", "Modified" };
            publicDataNames = new List<string> { "BranchName", "BranchCode" };
            dropDownNamesList = new List<String>();
            noRelationDropDownNamesList = new List<String>();
        }
        public List<String> dataNames { get; set; }
        public List<String> publicDataNames { get; set; }
        public List<String> dropDownNamesList { get; set; }
        public List<String> noRelationDropDownNamesList { get; set; }
        #endregion
        public int BranchID { get; set; }
        [Required, MinLength(6), DisplayName("Branch Name")]
        public string BranchName { get; set; } = string.Empty;
        [Required, MinLength(2), DisplayName("Branch Code")]
        public string BranchCode { get; set; } = string.Empty;
        private DateTime Created { get; set; }
        private DateTime Modified { get; set; }
    }
}
