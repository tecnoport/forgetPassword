using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Data.OleDb;

namespace ForgetPassword
{
    public partial class frmForgetPassword : Form
    {
        Random rnd = new Random();
      public int code ;
        
        //Connect database, copy and change the location of database blow
      OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\ForgetPassword-master\ForgetPassword\ForgetPassword.mdb");

        public frmForgetPassword()
        {
            InitializeComponent();
        }


        private void mail()
        {

            code = rnd.Next(123123, 999999);
            const string p = "sent password";


            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress("Sent email");

            //Enter your email blow and also change in database too

            message.To.Add(new MailAddress("recive email"));
            message.Subject = "change password";
            message.Body = "Write this given code on text box\n" + code + "\nThank you!";

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sent email", p);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            MessageBox.Show("Email has been sent");


        }


        private void btnForgetPasswordChange_Click(object sender, EventArgs e)
        {
            
        }

        private void btnForgetPassordNextStep_Click_1(object sender, EventArgs e)
        {
            if (code.ToString() == txtForgrtPassswordCode.Text)
            {
                lblNextStep2.Visible = true;
                imgNextStep2.Visible = true;

                txtForgetPasswordConformPassword.Enabled = true;
                txtForgetPasswordNewPassword.Enabled = true;
                btnForgetPasswordChange.Enabled = true;
                btnForgetPassordNextStep.Enabled = false;
                btnForgetPassordSendAgain.Enabled = false;
            }

            else
            {
                MessageBox.Show("Code doesent Match");
            }
      
        }

        private void btnForgetPassordSendAgain_Click_1(object sender, EventArgs e)
        {
            mail();
        }

        private void btnForgetPasswordChange_Click_1(object sender, EventArgs e)
        {
            if (txtForgetPasswordNewPassword.Text == txtForgetPasswordConformPassword.Text)
            {
                string query = "update ownerInfo set [password]='" + txtForgetPasswordConformPassword.Text + "' where ID=1";
                OleDbCommand cmd = new OleDbCommand(query, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                MessageBox.Show("Pasword changes succussfully");
                this.Dispose();
            }
            else
            {
                MessageBox.Show("password dosent match");
            }
        }

        private void btnForgetPasswordSendMail_Click(object sender, EventArgs e)
        {

            string query = "select Email from ownerInfo";
            OleDbCommand cmd = new OleDbCommand(query, con);

            DataTable dt = new DataTable();
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            string txtboxEmail = txtForgetPasswordEmail.Text;
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    if (txtboxEmail == dr[dc].ToString())
                    {
                        lblNextStep1.Visible = true;
                        imgNextStep1.Visible = true;

                        txtForgrtPassswordCode.Enabled = true;
                        btnForgetPassordNextStep.Enabled = true;
                        btnForgetPassordSendAgain.Enabled = true;
                        btnForgetPasswordSendMail.Enabled = false;

                        mail();
                    }
                    else
                    {
                        MessageBox.Show("This email is not exisit in the given data");

                    }


                }
            }
            

        }


        }

}
