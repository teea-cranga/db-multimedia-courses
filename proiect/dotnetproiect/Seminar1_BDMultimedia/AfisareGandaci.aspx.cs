using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seminar1_BDMultimedia {
    public partial class AfisareGandaci : System.Web.UI.Page {

        OracleConnection connectionToBugDatabase;


        protected void Page_Load(object sender, EventArgs e)
        {

            string connectionString = "User ID=STUD_CRANGAA; Password=student; Data Source=(DESCRIPTION=" +

            "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=37.120.249.41)(PORT=1521)))" +

            "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcls)));"; ;

            connectionToBugDatabase = new OracleConnection(connectionString);

            Image1.ImageUrl = "";
            showPicsLbl.Text = "";
            try
            {
                connectionToBugDatabase.Open();
            }
            catch (Exception ex)
            {
                showPicsLbl.Text = "Eroare " + ex.Message;
            }
            OracleCommand command1 = new OracleCommand("pnumargandaci", connectionToBugDatabase);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.Add("numar", OracleDbType.Int32);
            command1.Parameters[0].Direction = ParameterDirection.Output;
            try
            {
                command1.ExecuteScalar();
            }
            catch (Exception ex)
            {
                showPicsLbl.Text = "Eroare " + ex.Message;
            }
            string rez = command1.Parameters[0].Value.ToString();
            int nr = Convert.ToInt32(rez);

            showPicsLbl.Text = "Numar postari: " + nr;

            for (int i = 1; i <= nr; i++)
            {
                OracleCommand command = new OracleCommand("pafisarepostari", connectionToBugDatabase);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("vid", OracleDbType.Int32);
                command.Parameters.Add("flux", OracleDbType.Blob);
                command.Parameters[0].Direction = ParameterDirection.Input;
                command.Parameters[1].Direction = ParameterDirection.Output;
                command.Parameters[0].Value = i;
                Image img = new Image();
                this.Controls.Add(img);
                try
                {
                    command.ExecuteScalar();
                    Byte[] blob = new Byte[((OracleBlob)command.Parameters[1].Value).Length];
                    try
                    {
                        ((OracleBlob)command.Parameters[1].Value).Read(blob, 0, blob.Length);
                    }
                    catch (Exception ex)
                    {
                        showPicsLbl.Text = "Eroare " + ex.Message;
                    }
                    string myimg = Convert.ToBase64String(blob, 0, blob.Length);
                    img.ImageUrl = "data:image/gif;base64," + myimg;
                }
                catch (Exception ex)
                {
                    showPicsLbl.Text = "Eroare " + ex.Message;
                }
            }
            connectionToBugDatabase.Close();
        }

        protected void ImaginiGandaci_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImaginiGandaci.aspx");
        }

        protected void AdaugaVideo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdaugaVideo.aspx");
        }
    }
}