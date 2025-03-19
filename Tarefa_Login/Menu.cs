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

namespace Tarefa_Login
{
    public partial class Menu: Form
    {
        public Menu()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Start();
        }

        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now; // Obtém a hora atual
            MessageBox.Show($"Hora atual: {currentTime.ToString("HH:mm:ss")}");
        }

        private void Menu_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";
            openFileDialog.Title = "Abrir Arquivo";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string caminhoArquivo = openFileDialog.FileName;

                using (StreamReader reader = new StreamReader(caminhoArquivo))
                {
                    txtBox.Text = reader.ReadToEnd();
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";
            saveFileDialog.Title = "Salvar Arquivo";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.FileName = "arquivo";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string caminhoArquivo = saveFileDialog.FileName;

                using (StreamWriter writer = new StreamWriter(caminhoArquivo))
                {
                    writer.WriteLine(txtBox.Text); 
                }

                MessageBox.Show("Arquivo salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
