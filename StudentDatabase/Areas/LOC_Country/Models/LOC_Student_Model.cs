using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentDatabase.Areas.LOC_Country.Models
{
    public class LOC_Student_Model : Models_Interface
    {
        #region Constructor
        public LOC_Student_Model(dynamic dataRow)
        {
            if (int.TryParse(dataRow["StudentID"].ToString(), out int parsedStudentID)) StudentID = parsedStudentID;
            if (int.TryParse(dataRow["BranchID"].ToString(), out int parsedBranchID)) BranchID = parsedBranchID;
            if (int.TryParse(dataRow["CityID"].ToString(), out int parsedCityID)) CityID = parsedCityID;
            if (int.TryParse(dataRow["Age"].ToString(), out int parsedAge)) Age = parsedAge;
            if (Boolean.TryParse(dataRow["IsActive"].ToString(), out bool parsedIsActive)) IsActive = parsedIsActive;
            if (DateTime.TryParse(dataRow["BirthDate"].ToString(), out DateTime parsedBirthDate)) BirthDate = parsedBirthDate;
            StudentName = (string)dataRow["StudentName"] ?? "";
            BranchName = (string)dataRow["BranchName"] ?? "";
            CityName = (string)dataRow["CityName"] ?? "";
            MobileNoStudent = (string)dataRow["MobileNoStudent"] ?? "";
            Email = (string)dataRow["Email"] ?? "";
            MobileNoFather = (string)dataRow["MobileNoFather"] ?? "";
            Address = (string)dataRow["Address"] ?? "";
            Gender = (string)dataRow["Gender"] ?? "";
            Password = (string)dataRow["Password"] ?? "";
            /*            StudentID = (int)dataRow["StudentID"];
                        BranchID = (int)dataRow["BranchID"];
                        CityID = (int)dataRow["CityID"];
                        StudentName = (string)dataRow["StudentName"];
                        BranchName = (string)dataRow["BranchName"];
                        CityName = (string)dataRow["CityName"];
                        MobileNoStudent = (string)dataRow["MobileNoStudent"];
                        Email = (string)dataRow["Email"];
                        MobileNoFather = (string)dataRow["MobileNoFather"];
                        Address = (string)dataRow["Address"];
                        BirthDate = (DateTime)dataRow["BirthDate"];
                        Age = (int)dataRow["Age"];
                        IsActive = (bool)dataRow["IsActive"];
                        Gender = (string)dataRow["Gender"];
                        Password = (string)dataRow["Password"];*/
            setInterfacevariables();
        }
        public LOC_Student_Model() => setInterfacevariables();
        #endregion
        #region interface
        private void setInterfacevariables()
        {
            dataNames = new List<string> { "StudentID", "BranchID", "CityID", "StudentName", "MobileNoStudent", "Email", "MobileNoFather", "Address", "BirthDate", "Age", "IsActive", "Gender", "Password" };
            publicDataNames = new List<string> { "StudentName", "MobileNoStudent", "Email", "MobileNoFather", "Address", "BirthDate", "Age", "IsActive", "Gender", "Password" };
            dropDownNamesList = new List<String>();
            noRelationDropDownNamesList = new List<String> { "BranchName", "CityName" };
        }
        public List<String> dataNames { get; set; }
        public List<String> publicDataNames { get; set; }
        public List<String> dropDownNamesList { get; set; }
        public List<String> noRelationDropDownNamesList { get; set; }

        #endregion
        public int StudentID { get; set; }
        [Required(ErrorMessage = "Branch is required")]
        public int? BranchID { get; set; }
        [Required(ErrorMessage = "City is required")]
        public int? CityID { get; set; }
        public String BranchName { get; set; } = String.Empty;
        public String StudentName { get; set; } = String.Empty;

        [DisplayName("Student Name")]
        public string CityName { get; set; } = string.Empty;

        [DisplayName("Student Mobile"), Required, StringLength(maximumLength: 10, MinimumLength = 10)]
        public string MobileNoStudent { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [DisplayName("Father's Mobile")]
        public string MobileNoFather { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        [Range(typeof(DateTime), "1/1/1950", "01/01/2021",
        ErrorMessage = "Date must be between 1950 and 2020.")]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}