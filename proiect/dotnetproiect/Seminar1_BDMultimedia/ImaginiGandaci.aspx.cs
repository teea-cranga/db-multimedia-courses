using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
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

                FileUpload1.SaveAs(@"C:\Users\Teea\Desktop\" + FileUpload1.FileName);

                BugApp.Text = "Fisier incarcat: " + FileUpload1.FileName;

                using (var image = System.IO.File.OpenRead(@"C:\Users\Teea\Desktop\" + FileUpload1.FileName)) {

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
                    oracleCommand.Parameters.Add("v_denumire", OracleDbType.Varchar2);
                    oracleCommand.Parameters.Add("v_denumire_stiintifica", OracleDbType.Varchar2);
                    oracleCommand.Parameters.Add("v_fisier", OracleDbType.Blob);

                    // ne folosim de parametri sa adaugam valorile din form
                    oracleCommand.Parameters[0].Value = TextBoxDen.Text;
                    oracleCommand.Parameters[1].Value = TextBoxDenSt.Text;
                    oracleCommand.Parameters[2].Value = ImageBytes;

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
                cmd.ExecuteNonQuery();
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdaugaVideo.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("AfisareGandaci.aspx");
        }

        protected void Unnamed5_Click(object sender, EventArgs e)
        {
            if (FileUploadS.HasFile)
            {
                FileUploadS.SaveAs(@"C:\Users\Teea\Desktop\" + FileUploadS.FileName);
                using (var img = System.IO.File.OpenRead(@"C:\Users\Teea\Desktop\" + FileUploadS.FileName))
                {
                    Byte[] imageByte = new Byte[img.Length];
                    img.Read(imageByte, 0, (int)img.Length);
                    try
                    {
                        connectionToBugDatabase.Open();
                    }
                    catch (Exception ex)
                    {
                        BugApp.Text = "Eroare " + ex.Message;
                    }
                    OracleCommand cmd = new OracleCommand("PSREGASIRE", connectionToBugDatabase);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("fis", OracleDbType.Blob);
                    cmd.Parameters.Add("vdetalii", OracleDbType.Varchar2, 255);
                    cmd.Parameters[0].Direction = ParameterDirection.Input;
                    cmd.Parameters[1].Direction = ParameterDirection.Output;
                    cmd.Parameters[0].Value = imageByte;

                    try
                    {
                        cmd.ExecuteScalar();
                    }
                    catch (OracleException ex)
                    {
                        BugApp.Text = "Eroare " + ex.Message;
                    }
                    LabelDescriere.Text = cmd.Parameters[1].Value.ToString();
                    string[] den = LabelDescriere.Text.Split(',');
                    connectionToBugDatabase.Close();
                    TextBoxDen.Text = den[0];
                }
            }
        }
    }
}