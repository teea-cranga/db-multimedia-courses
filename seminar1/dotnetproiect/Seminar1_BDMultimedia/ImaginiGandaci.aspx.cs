using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
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

        // functia ia ca input un id si returneaza imaginea (prin procedura pafisare din SQLDEVELOPER) cu id-ul respectiv
        protected void afiseaza_imagine_Click(object sender, EventArgs e) {
            BugApp.Text = "";
            try {
                connectionToBugDatabase.Open();
            }
            catch (OracleException er) 
            {
                BugApp.Text +=  er.Message; 
            }
            OracleCommand cmd = new OracleCommand("pafisare", connectionToBugDatabase);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("p_id", OracleDbType.Int32);
            cmd.Parameters.Add("p_img", OracleDbType.Blob);
            cmd.Parameters[0].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[0].Value = Convert.ToInt32(tb_afis.Text);
            try {
                cmd.ExecuteScalar();
                Byte[] blob = new byte[((OracleBlob)cmd.Parameters[1].Value).Length];
                ((OracleBlob)cmd.Parameters[1].Value).Read(blob, 0, blob.Length);
                string myimg = Convert.ToBase64String(blob, 0, blob.Length);
                afis_img.ImageUrl = "data:image/gif;base64," + myimg;
            }
            catch (OracleException ex) { BugApp.Text += ex.Message; }
        }

        protected void gen_semn_Click(object sender, EventArgs e) {
            try {
                connectionToBugDatabase.Open();
            }
            catch(OracleException ex) {
                BugApp.Text += ex.Message;
            }
            OracleCommand cmd = new OracleCommand("psgen_semn", connectionToBugDatabase);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try {
                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex) {
                BugApp.Text += ex.Message;
            }

            connectionToBugDatabase.Close();
            BugApp.Text += "Semnaturi generate cu succes";
        }
    }
}