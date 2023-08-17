using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using TAP_ProyectoAlumnos.MisClases;

namespace TAP_ProyectoAlumnos
{
    public partial class Form1 : Form
    {

        private String addAlumnoWS = "http://localhost/WSProyecto/agrega.php";


        String userActual;
        String numc;

        String usuarioAlumno;
        String usuarioPass;

        Random calificacion = new Random();
        int sem;

        public Form1()
        {
            InitializeComponent();
        }

        private void labelSign_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label8.Visible = false;
            labelSign.Visible = false;
            btnEntrar.Visible = false;
            textNoCtrl.Visible = false;
            textPass.Visible = false;

            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            textCarrera.Visible = true;
            numericUpDown1.Visible = true;
            textSignPass.Visible = true;
            textNombre.Visible = true;
            textSignNo.Visible = true;
            btnRegistrar.Visible = true;
            btnBack.Visible = true;

        }

        private void labelSign_MouseEnter(object sender, EventArgs e)
        {
            labelSign.Font = new System.Drawing.Font(labelSign.Font, FontStyle.Underline);
            labelSign.ForeColor = SystemColors.MenuHighlight;
        }

        private void labelSign_MouseLeave(object sender, EventArgs e)
        {
            labelSign.Font = new System.Drawing.Font(labelSign.Font, FontStyle.Regular);
            labelSign.ForeColor = SystemColors.ActiveCaptionText;
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (textNombre.Text == "" || textSignNo.Text == "" || textCarrera.Text == "" ||
                numericUpDown1.Value.ToString() == "" || textSignPass.Text == "")
            {
                label9.Visible = true;
            }
            else
            {
                numc = textSignNo.Text;
                sem = (int)numericUpDown1.Value;

                WebClient client = new WebClient();

                string nombre = textNombre.Text.ToString();
                string nocontrol = textSignNo.Text.ToString();
                string carrera = textCarrera.Text.ToString();
                string semestre = numericUpDown1.Value.ToString();
                string password = textSignPass.Text.ToString();
                string evaluacion = "0";

                NameValueCollection postData = new NameValueCollection()
                {
                        { "nombre", nombre },
                        { "noControl", nocontrol },
                        { "carrera", carrera },
                        { "semestre", semestre },
                        { "password", password },
                        { "evaluacionDoc", evaluacion }
                };

                string pageSource = Encoding.UTF8.GetString(client.UploadValues(addAlumnoWS, postData));

                HttpClient client2 = new HttpClient();
                String content = await client2.GetStringAsync("http://localhost/WSProyecto/consultarMaterias.php");

                try
                    {
                    JObject jsonObject = JObject.Parse(content);
                    JArray jOutput = (JArray)jsonObject.GetValue("output");

                    foreach (JObject json in jOutput){

                        if ((int)json.GetValue("id_semestre") < sem){

                            int calif = calificacion.Next(70, 101);
                            NameValueCollection postData2 = new NameValueCollection()
                            {
                                { "calif", calif.ToString()},
                                { "id_alumno", numc.ToString() },
                                { "id_materia", json.GetValue("id_materia").ToString() }
                            };

                            string pageSource2 = Encoding.UTF8.GetString(client.UploadValues("http://localhost/WSProyecto/asignarCalif.php", postData2));
                        }
                        else
                        {
                            break;
                        }
                    }
                    MessageBox.Show("Registrado Correctamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                    MessageBox.Show("Error");
                }

                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                textCarrera.Clear();
                textCarrera.Visible = false;
                numericUpDown1.Value = 1;
                numericUpDown1.Visible = false;
                textSignPass.Clear();
                textSignPass.Visible = false;
                textNombre.Clear();
                textNombre.Visible = false;
                textSignNo.Clear();
                textSignNo.Visible = false;
                btnRegistrar.Visible = false;
                label9.Visible = false;
                btnBack.Visible = false;

                label1.Visible = true;
                label2.Visible = true;
                labelSign.Visible = true;
                btnEntrar.Visible = true;
                textNoCtrl.Visible = true;
                textPass.Visible = true;

                textNoCtrl.Clear();
                textPass.Clear();
            }

        }

        private async void btnEntrar_Click(object sender, EventArgs e)
        {
            if (textNoCtrl.Text == "" || textPass.Text == "")
            {
                MessageBox.Show("Llene todos los campos para iniciar sesión");
            }
            else
            {
                String varUsuarioAlumno = textNoCtrl.Text;
                HttpClient client = new HttpClient();
                String content = await client.GetStringAsync("http://localhost/WSProyecto/findUsuarioAlumno.php?noControl=" + varUsuarioAlumno);

                try
                {
                    JObject jsonUsuarioAlumno = JObject.Parse(content);
                    JArray output = (JArray)jsonUsuarioAlumno.GetValue("output");

                    foreach(JObject json in output)
                    {
                        usuarioAlumno = json.GetValue("noControl").ToString();
                        usuarioPass = json.GetValue("password").ToString();
                        
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                    MessageBox.Show("Error");
                }

                if(usuarioAlumno == textNoCtrl.Text && usuarioPass == textPass.Text)
                {
                    userActual = textNoCtrl.Text;
                    new Menu(userActual).Show();
                    label8.Visible = false;
                    textNoCtrl.Clear();
                    textPass.Clear();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña invalida. \nInténtelo de nuevo.");
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            textCarrera.Clear();
            textCarrera.Visible = false;
            numericUpDown1.Value = 1;
            numericUpDown1.Visible = false;
            textSignPass.Clear();
            textSignPass.Visible = false;
            textNombre.Clear();
            textNombre.Visible = false;
            textSignNo.Clear();
            textSignNo.Visible = false;
            btnRegistrar.Visible = false;
            btnBack.Visible = false;
            label9.Visible = false;

            label1.Visible = true;
            label2.Visible = true;
            labelSign.Visible = true;
            btnEntrar.Visible = true;
            textNoCtrl.Visible = true;
            textPass.Visible = true;

            textNoCtrl.Clear();
            textPass.Clear();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void maestroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new LoginDocente().Show();
            this.Hide();
        }
    }
}
