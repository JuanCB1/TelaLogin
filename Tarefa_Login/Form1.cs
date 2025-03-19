using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tarefa_Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnEntrar.Enabled = false;
            txtUsuario.TextChanged += ValidarCampos;
            txtSenha.TextChanged += ValidarCampos;
            txtSenha.UseSystemPasswordChar = true;
        }

        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text;
            string pass = txtSenha.Text;


            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Os campos não podem estar vazios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AuthenticateUser(user, pass))
            {
                MessageBox.Show("Login bem-sucedido!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Menu menu = new Menu();
                menu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            if (File.Exists("users.txt"))
            {
                string[] lines = File.ReadAllLines("users.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3 && parts[0] == username && parts[2] == password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void lblCriar_Click(object sender, EventArgs e)
        {
            Conta conta = new Conta();
            conta.Show();
            this.Hide();

        }
        private void ValidarCampos(object sender, EventArgs e)
        {
            btnEntrar.Enabled = !string.IsNullOrWhiteSpace(txtUsuario.Text) && !string.IsNullOrWhiteSpace(txtSenha.Text);
        }

        public static class UserSession
        {
            public static string RegisteredUsername { get; set; }
        }

        private void chbSenha_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = !chbSenha.Checked;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
    }
}