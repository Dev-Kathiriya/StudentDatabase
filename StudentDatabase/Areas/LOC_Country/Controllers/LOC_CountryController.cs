using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using StudentDatabase.Areas.LOC_Country.Models;
using StudentDatabase.BAL;
using System.Data;
using System.Data.SqlClient;

namespace StudentDatabase.Areas.LOC_Country.Controllers
{

    [CheckAccess, Area("LOC_Country"), Route("Tables/{controller=LOC_Country}/{action=Index}")]
    public class LOC_CountryController : Controller
    {
        #region Basic Construction
        //procType is this way because it makes easy to call procedure directly
        private static String procType = "Country";
        private IConfiguration _configuration;
        public LOC_CountryController(IConfiguration configuration) => this._configuration = configuration;
        #endregion
        #region CRUD
        #region private _command, insert, selectByPK, selectAll
        private (SqlConnection, SqlCommand) _command(bool passID = false)
        {
            String connStr = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            if (passID) objCmd.Parameters.AddWithValue("UserID", UserSessonCV.UserId());
            return (conn, objCmd);
        }
        private ActionResult _insertUpdateDatabase(Models_Interface model, int id)
        {
            if (!TryValidateModel(model)) return _WrongFilled(model);
            var (conn, objCmd) = _command(id == -1);
            if (id == -1)
                objCmd.CommandText = $"[dbo].[PR_{procType}_Insert]";
            else
            {
                objCmd.CommandText = $"[dbo].[PR_{procType}_Update]";
                objCmd.Parameters.AddWithValue($"{procType}ID", id);
            }
            List<String> dataNames = model.publicDataNames.Concat(model.dropDownNamesList.Select(name => name.Replace("Name", "ID")).ToList()).Concat(model.noRelationDropDownNamesList.Select(name => name.Replace("Name", "ID")).ToList()).ToList();
            List<dynamic> data = new Tables_Model().instanceVariableValue(lst: dataNames, data: model);
            for (int i = 0; i < data.Count; i++) objCmd.Parameters.AddWithValue(dataNames[i], data[i]);
            bool isNotExicuted = objCmd.ExecuteNonQuery() < 1;
            conn.Close();
            if (isNotExicuted) ViewBag.isNotExicuted = isNotExicuted;
            return isNotExicuted ? _WrongFilled(model) : RedirectToAction("Table", new { procType = procType });
        }
        private Tables_Model _selectByPk(int id, string type = null)
        {
            DataTable dt = new DataTable();
            var (conn, objCmd) = (_command());
            objCmd.CommandText = $"[dbo].[PR_{type ?? procType}_SelectByPK]";
            objCmd.Parameters.AddWithValue($"{type ?? procType}ID", id);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            conn.Close();
            Tables_Model table = new Tables_Model(dt, type ?? procType);
            table.dataModel = table.data[0];
            return table;
        }
        private Tables_Model _selectAll(String name = null, String data = null, Dictionary<string, int> dict = null)
        {
            DataTable dt = new DataTable();
            var (conn, objCmd) = (_command(true));
            objCmd.CommandText = $"[dbo].[PR_{name ?? procType}_SelectAll]";
            if (data != null) objCmd.Parameters.AddWithValue("data", data);
            if (dict != null)
                foreach (string key in dict.Keys)
                    if (dict[key] != -1 && name != key) objCmd.Parameters.AddWithValue($"{key}ID", dict[key]);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            conn.Close();
            return new Tables_Model(dt, name ?? procType);
        }
        #endregion
        #region public LOC_Country_Delete, _WrongFilled
        public ActionResult LOC_Country_Delete(string classType, int id)
        {
            Country_Delete(classType, id);
            return RedirectToAction("Table", new { procType = classType });
        }
        public bool Country_Delete(string classType, int id)
        {
            try
            {
                var (conn, objCmd) = (_command());
                objCmd.CommandText = $"[dbo].[PR_{classType}_Delete]";
                objCmd.Parameters.AddWithValue(classType + "ID", id);
                Console.WriteLine(objCmd.ExecuteNonQuery());
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [Route("{id?}")]
        private ActionResult _WrongFilled(Models_Interface model)
        {
            _setDropDown(form: _setFormOfDropDown(model), procType: procType);
            return View("AddEdit", model: new Tables_Model(name: procType, model: model));
        }
        #endregion
        #endregion
        #region New Table Adds
        [HttpPost]
        public ActionResult Adds(IFormCollection form, int id = -1)
        {
            Models_Interface model = Tables_Model.createInstanceOfClass(Type.GetType($"StudentDatabase.Areas.LOC_Country.Models.LOC_{procType}_Model"), form);
            return _insertUpdateDatabase(model, id);
        }
        #endregion
        #region DropDown
        private IFormCollection _setFormOfDropDown(Models_Interface model)
        {
            Dictionary<string, StringValues> dict = new Dictionary<string, StringValues>();
            List<string> lst = model.dropDownNamesList.Select(name => name.Replace("Name", "ID")).Concat(model.noRelationDropDownNamesList.Select(name => name.Replace("Name", "ID"))).ToList();
            List<dynamic> fields = new Tables_Model().instanceVariableValue(lst: lst, data: model);
            int length = fields.Count;
            for (int i = 0; i < fields.Count; i++)
                dict.Add(lst[i].Substring(0, lst[i].Length - 2), new StringValues(fields[i] == null ? "-1" : fields[i].ToString()));
            return new FormCollection(dict);
        }
        private void _setDropDown(IFormCollection form, String procType = "Country")
        {
            string key = form["key"];
            Tables_Model model = new Tables_Model(name: procType);
            ViewBag.keys = new List<string>();
            List<String> lst = model.dropDownNamesList.Concat(model.noRelationDropDownNamesList).ToList();
            int dropDown = int.TryParse(form["dropDown"], out dropDown) ? dropDown : -1, relationalDropDownCount = model.dropDownNamesList.Count;
            String? oldType = null;
            for (int i = 0; i < lst.Count; i++)
            {
                String type = lst[i].Substring(0, lst[i].Length - 4);
                ViewBag.keys.Add(type);
                if (TempData[type] == null) TempData[type] = int.TryParse(form[type], out int parsedValue) ? parsedValue : -1;
                if (key != null && i != 0 && ViewBag.keys[i - 1] == key && dropDown != -1 && i < relationalDropDownCount)
                {
                    ViewData[type] = _selectAll(type, dict: new Dictionary<string, int>() { { key, dropDown } });
                    continue;
                }
                else if (oldType != null && (int)TempData[oldType] != -1 && i < relationalDropDownCount)
                {
                    ViewData[type] = _selectAll(name: type, dict: new Dictionary<string, int>() { { oldType, (int)TempData[oldType] } });
                    continue;
                }
                ViewData[type] = _selectAll(name: type, data: i < relationalDropDownCount ? (string)ViewData[type + "data"] : null);
                oldType = type;
            }
            if (key != null) TempData[key] = dropDown;
        }
        #endregion
        [Route("{procType?}")]
        public ActionResult Table(IFormCollection form, string procType = "Country", String data = "")
        {
            _setDropDown(form: form, procType: procType);
            Dictionary<string, int> dict = TempData.Keys.ToDictionary(key => key, key => int.TryParse(form[key], out int id) ? id : -1);
            if (form["key"].ToString() != "") dict[form["key"]] = int.TryParse(form["dropDown"], out int dropDown) ? dropDown : -1;
            return View(model: _selectAll(procType, dict: dict, data: data));
        }
        public ActionResult AddEdit(IFormCollection form, string? procTypes, int id = -1)
        {
            if (procTypes != null)
                procType = procTypes;
            ViewBag.procType = procType;
            if (id != -1)
            {
                ViewBag.id = id;
                Tables_Model model = _selectByPk(id);
                _setDropDown(form: _setFormOfDropDown(model.dataModel), procType: procType);
                return View(model: model);
            }
            _setDropDown(form: form, procType: procType);
            return View(model: new Tables_Model(procType));

        }
        public ActionResult DeleteCheckBox(List<string> selectedIds)
        {
            foreach (string ids in selectedIds)
                Country_Delete(procType, int.Parse(ids));
            return RedirectToAction("Tables/Table", new { procType = procType });
        }

    }
}
/*private void setDropDownList()
{
    var module = Tables_Model.createInstanceOfClass(Type.GetType($"StudentDatabase.Areas.LOC_Country.Models.LOC_{procType}_Model"));
    foreach (string name in module.dropDownNamesList)
    {
        Tables_Model table = _selectAll(name.Substring(0, name.Length - 4));
        ViewData[name] = table.data;
    }
    foreach (string name in module.noRelationDropDownNamesList)
    {
        Tables_Model table = _selectAll(name.Substring(0, name.Length - 4));
        ViewData[name] = table.data;
    }

}*/ 