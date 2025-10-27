using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seminar1_BDMultimedia {
    public partial class ImaginiGandaci : System.Web.UI.Page {

        OracleConnection connectionToBugDatabase;


        protected void Page_Load(object sender, EventArgs e) {

            string connectionString = "User ID=STUD_CRANGAA; Password=student; Data Source=(DESCRIPTION=" +

            "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=37.120.249.41)(PORT=1521)))" +

            "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcls)));"; ;

            connectionToBugDatabase = new OracleConnection(connectionString);
        }

        protected void ButtonUpload_Click(object sender, EventArgs e) {
            BugApp.Text = "";
            if (FileUpload1.HasFile) {

                FileUpload1.SaveAs(@"T:\BD Multimedia" + FileUpload1.FileName);

                BugApp.Text = "Fisier incarcat: " + FileUpload1.FileName;

                using (var image = System.IO.File.OpenRead(@"T:\BD Multimedia\" + FileUpload1.FileName)) {

                    var ImageBytes = new byte[image.Length];
                    image.Read(ImageBytes, 0, (int)image.Length);
                    BugApp.Text = "Imaginea are dimensiunea: " + image.Length.ToString();

                    try {
                        connectionToBugDatabase.Open();
                    }
                    catch (OracleException exceptionOrcl) {
                        BugApp.Text += exceptionOrcl.Message;
                    }

                    // adaugam parametrii pentru procedura SQL
                    OracleCommand oracleCommand = new OracleCommand("PS_INSERARE_IMG", connectionToBugDatabase);
                    oracleCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add("v_id", OracleDbType.Int64);
                    oracleCommand.Parameters.Add("v_denumire", OracleDbType.Varchar2);
                    oracleCommand.Parameters.Add("v_denumire_stiintifica", OracleDbType.Varchar2);
                    oracleCommand.Parameters.Add("v_fisier", OracleDbType.Blob);

                    // ne folosim de parametri sa adaugam valorile din form
                    oracleCommand.Parameters[0].Value = Convert.ToInt64(TextBoxID.Text);
                    oracleCommand.Parameters[1].Value = TextBoxDen.Text;
                    oracleCommand.Parameters[2].Value = TextBoxDenSt.Text;
                    oracleCommand.Parameters[3].Value = ImageBytes;

                    try {
                        oracleCommand.ExecuteNonQuery();
                    } catch (OracleException exceptionOrcl) {
                        connectionToBugDatabase.Close();
                    }
                }
            }
        }
    }
}