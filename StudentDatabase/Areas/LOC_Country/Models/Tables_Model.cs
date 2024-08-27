using System.Data;
using System.Reflection;

namespace StudentDatabase.Areas.LOC_Country.Models
{
    public interface Models_Interface
    {
        #region variables
        public List<String> dataNames { get; set; }
        public List<String> publicDataNames { get; set; }
        public List<String> dropDownNamesList { get; set; }
        public List<String> noRelationDropDownNamesList { get; set; }
        #endregion
        #region Methods
        /*private void setInterfacevariables();*/
        #endregion

    }

    public static class TableNames
    {
        public static readonly List<String> tables = new List<String> { "Country", "State", "City", "Branch", "Student" };
    }
    public class Tables_Model
    {
        #region Variables
        public List<dynamic> data = new List<dynamic>();
        public List<String> dataNames, publicDataNames, dropDownNamesList, noRelationDropDownNamesList;
        public dynamic dataModel;
        public string type;
        int id;
        #endregion
        #region Constructor
        public Tables_Model() { }
        public Tables_Model(String name, dynamic? model = null)
        {
            type = name;
            if (model != null)
                dataModel = model;
            Type types = Type.GetType($"StudentDatabase.Areas.LOC_Country.Models.LOC_{type}_Model");
            var tempObj = createInstanceOfClass(types);
            publicDataNames = tempObj.publicDataNames;
            dataNames = tempObj.dataNames;
            dropDownNamesList = tempObj.dropDownNamesList;
            noRelationDropDownNamesList = tempObj.noRelationDropDownNamesList;
            if (model == null)
                dataModel = tempObj;
        }
        public Tables_Model(DataTable dataTable, String name)
        {
            type = name;
            Type types = Type.GetType($"StudentDatabase.Areas.LOC_Country.Models.LOC_{type}_Model");
            foreach (DataRow datas in dataTable.Rows)
            {
                data.Add(createInstanceOfClass(types, datas));
            }
            dynamic temp = createInstanceOfClass(types);
            dataNames = temp.dataNames;
            publicDataNames = temp.publicDataNames;
            dropDownNamesList = temp.dropDownNamesList;
            noRelationDropDownNamesList = temp.noRelationDropDownNamesList;
        }
        #endregion
        #region Instance Methods
        public static dynamic createInstanceOfClass(Type types) => Activator.CreateInstance(types);

        public static dynamic createInstanceOfClass(Type types, dynamic datas) => Activator.CreateInstance(types, datas);
        public List<dynamic> instanceVariableValue(List<String> lst, dynamic? data = null)
        {
            Type type = (data ?? dataModel).GetType();
            List<dynamic> values = new List<dynamic>();
            foreach (String variableName in lst)
                values.Add((type.GetProperty(variableName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)).GetValue(data ?? dataModel));
            return values;
        }
        public dynamic instanceVariableValue(String variableName, dynamic? data = null) => ((data ?? dataModel).GetType().GetProperty(variableName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)).GetValue(data ?? dataModel);

        #endregion
    }
}
