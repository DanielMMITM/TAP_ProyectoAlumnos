using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TAP_ProyectoAlumnos.MisClases;

namespace TAP_ProyectoAlumnos
{
    public partial class LoginDocente : Form
    {
        private String addMaestroWS = "http://localhost/WSProyecto/agregaM.php";

        String user;
        string idmateria;

        String usuarioMaestro;
        String password;

        public LoginDocente()
        {
            InitializeComponent();
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.Font = new System.Drawing.Font(label3.Font, FontStyle.Underline);
            label3.ForeColor = SystemColors.MenuHighlight;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.Font = new System.Drawing.Font(label3.Font, FontStyle.Regular);
            label3.ForeColor = SystemColors.ActiveCaptionText;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox1.Visible = false;
            label2.Visible = false;
            textBox2.Visible = false;
            btnEntrar.Visible = false;
            label3.Visible = false;
            label9.Visible = false;

            label4.Visible = true;
            textNombre.Visible = true;
            label5.Visible = true;
            textUsuario.Visible = true;
            label6.Visible = true;
            textMateria.Visible = true;
            label7.Visible = true;
            textPassword.Visible = true;
            btnCancelar.Visible = true;
            btnRegistrar.Visible = true;
        }

        private void alumnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void LoginDocente_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            textNombre.Visible = false;
            textNombre.Clear();
            label5.Visible = false;
            textUsuario.Visible = false;
            textUsuario.Clear();
            label6.Visible = false;
            textMateria.Visible = false;
            textMateria.Clear();
            label7.Visible = false;
            textPassword.Visible = false;
            textPassword.Clear();
            label8.Visible = false;
            btnCancelar.Visible = false;
            btnRegistrar.Visible = false;
            
            label1.Visible = true;
            textBox1.Clear();
            textBox1.Visible = true;
            label2.Visible = true;
            textBox2.Clear();
            textBox2.Visible = true;
            btnEntrar.Visible = true;
            label3.Visible = true;
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (textNombre.Text == "" || textUsuario.Text == "" || textMateria.Text == "" ||
                textPassword.Text == "")
            {
                label8.Visible = true;
            }
            else
            {
                String materia = textMateria.Text;

                HttpClient client = new HttpClient();
                String content = await client.GetStringAsync("http://localhost/WSProyecto/consultaIDMateria.php?materia="+materia);

                try
                {
                    JObject jsonO = JObject.Parse(content);
                    JArray output = (JArray)jsonO.GetValue("output");
                    foreach(JObject json in output)
                    {
                        idmateria = json.GetValue("id_materia").ToString();
                    }

                    WebClient client2 = new WebClient();

                    string nombre = textNombre.Text;
                    string usuario = textUsuario.Text;
                    string password = textPassword.Text;

                    NameValueCollection postData = new NameValueCollection()
                    {
                        { "nombre", nombre },
                        { "usuario", usuario },
                        { "password", password },
                        { "id_materia", idmateria.ToString() }
                    };

                    string pageSource = Encoding.UTF8.GetString(client2.UploadValues(addMaestroWS, postData));

                    MessageBox.Show("Registrado Correctamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                    MessageBox.Show("Error");
                }

                label4.Visible = false;
                textNombre.Visible = false;
                textNombre.Clear();
                label5.Visible = false;
                textUsuario.Visible = false;
                textUsuario.Clear();
                label6.Visible = false;
                textMateria.Visible = false;
                textMateria.Clear();
                label7.Visible = false;
                textPassword.Visible = false;
                textPassword.Clear();
                label8.Visible = false;
                btnCancelar.Visible = false;
                btnRegistrar.Visible = false;

                label1.Visible = true;
                textBox1.Clear();
                textBox1.Visible = true;
                label2.Visible = true;
                textBox2.Clear();
                textBox2.Visible = true;
                btnEntrar.Visible = true;
                label3.Visible = true;

            }
        }

        private async void btnEntrar_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Llene todos los campos para iniciar sesión");
            }
            else
            {
                String varUsuario = textBox1.Text;
                HttpClient client = new HttpClient();
                String content = await client.GetStringAsync("http://localhost/WSProyecto/findUsuarioMaestro.php?usuario=" + varUsuario);

                try
                {
                    JObject jsonO = JObject.Parse(content);
                    JArray output = (JArray)jsonO.GetValue("output");

                    foreach (JObject json in output)
                    {
                        usuarioMaestro = json.GetValue("usuario").ToString();
                        password = json.GetValue("password").ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                    MessageBox.Show("Error");
                }

                if (usuarioMaestro == textBox1.Text && password == textBox2.Text)
                {
                    user = textBox1.Text;
                    new MenuDocente(user).Show();
                    label9.Visible = false;
                    textBox1.Clear();
                    textBox2.Clear();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña invalida. \nInténtelo de nuevo.");
                }
            }
        }
    }
}
