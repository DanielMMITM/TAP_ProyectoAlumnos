using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TAP_ProyectoAlumnos
{
    public partial class MenuDocente : Form
    {
        String userActual;

        String idmateria;
        String comentarios = "";
        String alumnoevaluacion = "";
        String id_maestro;

        float promediorubro1, promediorubro2, promediorubro3, promediorubro4, promediorubro5, promediorubro6, 
            promediorubro7, promediorubro8, promediorubro9;

        float promtotal = 0;

        int contA;


        public MenuDocente(String user)
        {
            userActual = user;
            InitializeComponent();
        }

        private async void MenuDocente_Load(object sender, EventArgs e)
        {
            label2.Text = userActual;

            HttpClient client = new HttpClient();
            String content = await client.GetStringAsync("http://localhost/WSProyecto/findUsuarioMaestro.php?usuario=" + userActual.ToString());

            try
            {
                JObject jsonObject = JObject.Parse(content);
                JArray jOutput = (JArray)jsonObject.GetValue("output");

                foreach (JObject json in jOutput)
                {
                    id_maestro = json.GetValue("id_maestro").ToString();
                    idmateria = json.GetValue("id_materia").ToString();

                    HttpClient client3 = new HttpClient();
                    String content3 = await client.GetStringAsync("http://localhost/WSProyecto/consultarEvaluaciones.php?id_maestro=" + id_maestro);

                    JObject jsonObject3 = JObject.Parse(content3);
                    JArray jOutput3 = (JArray)jsonObject3.GetValue("output");

                    foreach (JObject jsonE in jOutput3)
                    {
                        switch ((int)jsonE.GetValue("id_rubro"))
                        {
                            case 1:
                                {
                                    promediorubro1 += (float)jsonE.GetValue("calific");
                                    break;
                                }
                            case 2:
                                {
                                    promediorubro2 += (float)jsonE.GetValue("calific");
                                    break;
                                }
                            case 3:
                                {
                                    promediorubro3 += (float)jsonE.GetValue("calific");
                                    break;
                                }
                            case 4:
                                {
                                    promediorubro4 += (float)jsonE.GetValue("calific");
                                    break;
                                }
                            case 5:
                                {
                                    promediorubro5 += (float)jsonE.GetValue("calific");
                                    break;
                                }
                            case 6:
                                {
                                    promediorubro6 += (float)jsonE.GetValue("calific");
                                    break;
                                }
                            case 7:
                                {
                                    promediorubro7 += (float)jsonE.GetValue("calific");
                                    break;
                                }
                            case 8:
                                {
                                    promediorubro8 += (float)jsonE.GetValue("calific");
                                    break;
                                }
                            case 9:
                                {
                                    promediorubro9 += (float)jsonE.GetValue("calific");
                                    break;
                                }
                            case 10:
                                {
                                    comentarios += jsonE.GetValue("coment") + ", ";
                                    contA++;
                                    string id_alumno = jsonE.GetValue("id_alumno").ToString();

                                    HttpClient client4 = new HttpClient();
                                    String content4 = await client4.GetStringAsync("http://localhost/WSProyecto/consultarNoControl.php?id_alumno=" + id_alumno);

                                    JObject jsonObject4 = JObject.Parse(content4);
                                    JArray jOutput4 = (JArray)jsonObject4.GetValue("output");

                                    foreach (JObject jsonN in jOutput4)
                                    {
                                        alumnoevaluacion += jsonN.GetValue("noControl").ToString() +", ";
                                    }
                                    break;
                                }
                            default:
                                {
                                    break;
                                }

                        }
                    }

                }
                promediorubro1 = promediorubro1 / (float)contA;
                promediorubro2 = promediorubro2 / (float)contA;
                promediorubro3 = promediorubro3 / (float)contA;
                promediorubro4 = promediorubro4 / (float)contA;
                promediorubro5 = promediorubro5 / (float)contA;
                promediorubro6 = promediorubro6 / (float)contA;
                promediorubro7 = promediorubro7 / (float)contA;
                promediorubro8 = promediorubro8 / (float)contA;
                promediorubro9 = promediorubro9 / (float)contA;

                label28.Text = contA.ToString();
                label29.Text = alumnoevaluacion;

                label15.Text = promediorubro1.ToString();
                Series serie1 = chart1.Series.Add("Rubros");
                DataPoint dp1 = new DataPoint(1, promediorubro1);
                dp1.Label = promediorubro1.ToString();
                serie1.Points.Add(dp1);

                label16.Text = promediorubro2.ToString();
                DataPoint dp2 = new DataPoint(2, promediorubro2);
                dp2.Label = promediorubro2.ToString();
                serie1.Points.Add(dp2);

                label17.Text = promediorubro3.ToString();
                DataPoint dp3 = new DataPoint(3, promediorubro3);
                dp3.Label = promediorubro3.ToString();
                serie1.Points.Add(dp3);

                label18.Text = promediorubro4.ToString();
                DataPoint dp4 = new DataPoint(4, promediorubro4);
                dp4.Label = promediorubro4.ToString();
                serie1.Points.Add(dp4);

                label19.Text = promediorubro5.ToString();
                DataPoint dp5 = new DataPoint(5, promediorubro5);
                dp5.Label = promediorubro5.ToString();
                serie1.Points.Add(dp5);

                label20.Text = promediorubro6.ToString();
                DataPoint dp6 = new DataPoint(6, promediorubro6);
                dp6.Label = promediorubro6.ToString();
                serie1.Points.Add(dp6);

                label21.Text = promediorubro7.ToString();
                DataPoint dp7 = new DataPoint(7, promediorubro7);
                dp7.Label = promediorubro7.ToString();
                serie1.Points.Add(dp7);

                label22.Text = promediorubro8.ToString();
                DataPoint dp8 = new DataPoint(8, promediorubro8);
                dp8.Label = promediorubro8.ToString();
                serie1.Points.Add(dp8);

                label23.Text = promediorubro9.ToString();
                DataPoint dp9 = new DataPoint(9, promediorubro9);
                dp9.Label = promediorubro9.ToString();
                serie1.Points.Add(dp9);

                serie1.IsVisibleInLegend = false;

                label25.Text = comentarios;

                promtotal = (promediorubro1 + promediorubro2 + promediorubro3 + promediorubro4 + promediorubro5 +
                    promediorubro6 + promediorubro7 + promediorubro8 + promediorubro9) / 9;

                label27.Text = promtotal.ToString();
                chart1.Visible = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                MessageBox.Show("Error");
            }

            HttpClient client2 = new HttpClient();
            String content2 = await client.GetStringAsync("http://localhost/WSProyecto/findNombreMateria.php?materia=" + idmateria);

            try
            {
                JObject jsonObject = JObject.Parse(content2);
                JArray jOutput = (JArray)jsonObject.GetValue("output");

                foreach (JObject json in jOutput)
                {
                    label30.Text = json.GetValue("nombre_materia").ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                MessageBox.Show("Error");
            }
        }

        private void MenuDocente_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            new LoginDocente().Show();
        }
    }
}
