using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Tarefa_Login.Form1;

namespace Tarefa_Login
{
    public partial class Conta : Form
    {
        public Conta()
        {
            InitializeComponent();
            btnEntrar2.Enabled = false;
            txtUsuario2.TextChanged += ValidarCampos;
            txtSenha2.TextChanged += ValidarCampos;
            txtSenha3.TextChanged += ValidarCampos;

        }

        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private void btnEntrar2_Click(object sender, EventArgs e)
        {
            string user2 = txtUsuario2.Text;
            string email = txtEmail.Text;
            string pass2 = txtSenha2.Text;
            string pass3 = txtSenha3.Text;

            if (string.IsNullOrWhiteSpace(user2) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass2))
            {
                MessageBox.Show("Preencha todos os campos antes de continuar.");
                return;
            }

            if (pass2 != pass3)
            {
                MessageBox.Show("As senhas não coincidem.");
                return;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains(".com"))
            {
                MessageBox.Show("E-mail inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveUser(user2, email, pass2);
            MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            UserSession.RegisteredUsername = user2;

            this.Close();

            Form1 form2 = new Form1();
            form2.Show();
            this.Hide();
        }
        private void SaveUser(string user3, string email, string pass4)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("users.txt", true))
                {
                    sw.WriteLine($"{user3},{email},{pass4}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar usuário: " + ex.Message);
            }
        }
        private void ValidarCampos(object sender, EventArgs e)
        {
            btnEntrar2.Enabled = !string.IsNullOrWhiteSpace(txtUsuario2.Text) && !string.IsNullOrWhiteSpace(txtSenha2.Text) && !string.IsNullOrWhiteSpace(txtSenha3.Text) && !string.IsNullOrWhiteSpace(txtEmail.Text);
        }

        private void chbSenha2_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha2.UseSystemPasswordChar = !chbSenha2.Checked;
        }

        private void chbSenha3_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha3.UseSystemPasswordChar = !chbSenha3.Checked;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Conta_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
    }
}