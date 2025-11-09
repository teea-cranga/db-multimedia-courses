using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seminar1_BDMultimedia {
    public partial class AdaugaVideo : System.Web.UI.Page {

        OracleConnection connectionToBugDatabase;


        protected void Page_Load(object sender, EventArgs e) {

            string connectionString = "User ID=STUD_CRANGAA; Password=student; Data Source=(DESCRIPTION=" +

            "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=37.120.249.41)(PORT=1521)))" +

            "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcls)));"; ;

            connectionToBugDatabase = new OracleConnection(connectionString);
        }

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            addVideoLbl.Text = "";
            if (fileupload.HasFile)
            {

                fileupload.SaveAs(@"C:\Users\Teea\Desktop\" + fileupload.FileName);

                addVideoLbl.Text = "Fisier incarcat: " + fileupload.FileName;

                using (var video = System.IO.File.OpenRead(@"C:\Users\Teea\Desktop\" + fileupload.FileName))
                {

                    var VideoBytes = new byte[video.Length];
                    video.Read(VideoBytes, 0, (int)video.Length);
                    addVideoLbl.Text = "Video are dimensiunea: " + video.Length.ToString();

                    try
                    {
                        connectionToBugDatabase.Open();
                    }
                    catch (OracleException exceptionOrcl)
                    {
                        addVideoLbl.Text += exceptionOrcl.Message;
                    }

                    // adaugam parametrii pentru procedura SQL
                    OracleCommand oracleCommand = new OracleCommand("PINSERAREVIDEOGANDAC", connectionToBugDatabase);
                    oracleCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add("vid", OracleDbType.Int64);
                    oracleCommand.Parameters.Add("fis", OracleDbType.Blob);


                    // ne folosim de parametri sa adaugam valorile din form
                    oracleCommand.Parameters[0].Value = Convert.ToInt64(id_vid.Text);
                    oracleCommand.Parameters[1].Value = VideoBytes;

                    try
                    {
                        oracleCommand.ExecuteNonQuery();
                        addVideoLbl.Text += " Video adaugat la imaginea cu id-ul: " + id_vid.Text;
                    }
                    catch (OracleException exceptionOrcl)
                    {
                        addVideoLbl.Text += exceptionOrcl.Message;
                    }

                    connectionToBugDatabase.Close();
                }
            }
            else
            {
                addVideoLbl.Text = "Nu a fost selectat niciun fisier.";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImaginiGandaci.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("AfisareGandaci.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                connectionToBugDatabase.Open();
            }
            catch (OracleException er)
            {
                addVideoLbl.Text += er.Message;
            }
            OracleCommand cmd = new OracleCommand("pafisarevideo", connectionToBugDatabase);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("vid", OracleDbType.Int32);
            cmd.Parameters.Add("flux", OracleDbType.Blob);
            cmd.Parameters[0].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[0].Value = Convert.ToInt32(id_vid.Text);
            try
            {
                cmd.ExecuteNonQuery();

                OracleBlob blobData = (OracleBlob)cmd.Parameters["flux"].Value;

                byte[] blob = new byte[blobData.Length];
                blobData.Read(blob, 0, blob.Length);

                string myimg = Convert.ToBase64String(blob);
                video.Src = "data:video/mp4;base64," + myimg;
            }
            catch (OracleException ex) { 
                addVideoLbl.Text += ex.Message; 
            }

            connectionToBugDatabase.Close();
        }
    }
}