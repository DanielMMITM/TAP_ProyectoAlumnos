using Newtonsoft.Json.Linq;
using System;
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
    public partial class Menu : Form
    {
        String usuario;
        int id;
        String semest;

        int opc = 10;
        int cont = 0;
        int cont2 = 0;
        int contm = 0;

        bool abierto = false;
        bool abierto2 = false;
        bool abierto3 = false;
        bool abierto4 = false;
        bool abierto5 = false;

        int maestro;
        int maestro2;

        public Menu(String user)
        {
            InitializeComponent();
            usuario = user;
        }
        

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            labelDPersonal.BackColor = SystemColors.ActiveCaption;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            labelDPersonal.BackColor = SystemColors.AppWorkspace;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            labelHorario.BackColor = SystemColors.ActiveCaption;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            labelHorario.BackColor = SystemColors.AppWorkspace;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            labelCalif.BackColor = SystemColors.ActiveCaption;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            labelCalif.BackColor = SystemColors.AppWorkspace;
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            labelReticula.BackColor = SystemColors.ActiveCaption;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            labelReticula.BackColor = SystemColors.AppWorkspace;
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            labelLogOut.Font = new System.Drawing.Font(labelLogOut.Font, FontStyle.Bold);
            labelLogOut.BackColor = SystemColors.ActiveCaption;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            labelLogOut.Font = new System.Drawing.Font(labelLogOut.Font, FontStyle.Regular);
            labelLogOut.BackColor = SystemColors.AppWorkspace;
        }

        private async void labelDPersonal_Click(object sender, EventArgs e)
        {
            while (abierto == false)
            {
                Limpiar();
                abierto = true;

                labelDPersonal.BackColor = SystemColors.ActiveCaption;
                opc = 1;

                label2_MouseEnter(sender, e);
                label2_MouseLeave(sender, e);
                label3_MouseEnter(sender, e);
                label3_MouseLeave(sender, e);
                label4_MouseEnter(sender, e);
                label4_MouseLeave(sender, e);
                labelEvaluacion_MouseEnter(sender, e);
                labelEvaluacion_MouseLeave(sender, e);

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                textBox1.Visible = true;
                btnsaveName.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                textBox2.Visible = true;
                label12.Visible = true;
                textBox3.Visible = true;
                btnsavePass.Visible = true;
                label18.Visible = true;
                label19.Visible = true;

                HttpClient client = new HttpClient();
                String content = await client.GetStringAsync("http://localhost/WSProyecto/findUsuarioAlumno.php?noControl=" + usuario.ToString());

                try
                {
                    JObject jsonObject = JObject.Parse(content);
                    JArray jOutput = (JArray)jsonObject.GetValue("output");

                    foreach (JObject json in jOutput)
                    {
                        label2.Text = json.GetValue("nombre").ToString();
                        label5.Text = json.GetValue("noControl").ToString();
                        label7.Text = json.GetValue("carrera").ToString();
                        label9.Text = json.GetValue("id_semestre").ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                    MessageBox.Show("Error");
                }
            }
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void labelLogOut_Click(object sender, EventArgs e)
        {
            this.Dispose();
            new Form1().Show();
        }

        private async void labelHorario_Click(object sender, EventArgs e)
        {
            while (abierto2 == false)
            {
                Limpiar();
                abierto2 = true;

                label1_MouseEnter(sender, e);
                label1_MouseLeave(sender, e);
                label3_MouseEnter(sender, e);
                label3_MouseLeave(sender, e);
                label4_MouseEnter(sender, e);
                label4_MouseLeave(sender, e);
                labelEvaluacion_MouseEnter(sender, e);
                labelEvaluacion_MouseLeave(sender, e);

                labelHorario.BackColor = SystemColors.ActiveCaption;
                opc = 2;

                dataGridView1.Visible = true;
                dataGridView2.Visible = true;
                label20.Visible = true;
                label21.Visible = true;
                JArray arrMaterias = new JArray();

                if (cont == 0)
                {
                    HttpClient client = new HttpClient();
                    String content = await client.GetStringAsync("http://localhost/WSProyecto/findUsuarioAlumno.php?noControl=" + usuario.ToString());

                    try
                    {
                        JObject jsonObject = JObject.Parse(content);
                        JArray jOutput = (JArray)jsonObject.GetValue("output");

                        foreach(JObject jsonA in jOutput)
                        {
                            HttpClient client2 = new HttpClient();
                            String content2 = await client2.GetStringAsync("http://localhost/WSProyecto/consultarMaterias.php");

                            JObject jsonObject2 = JObject.Parse(content2);
                            JArray jOutput2 = (JArray)jsonObject2.GetValue("output");

                            foreach (JObject json in jOutput2)
                            {

                                if ((int)json.GetValue("id_semestre") == (int)jsonA.GetValue("id_semestre"))
                                {
                                    arrMaterias.Add(json.GetValue("nombre_materia").ToString());
                                }
                                else
                                {
                                    if ((int)json.GetValue("id_semestre") > (int)jsonA.GetValue("id_semestre"))
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        for (int j = 0; j < arrMaterias.Count; j++)
                        {
                            String[] rowArray = { $"{arrMaterias[j]}", $"{arrMaterias[j]}",
                                        $"{arrMaterias[j]}", $"{arrMaterias[j]}", $"{arrMaterias[j]}"};
                            dataGridView1.Rows.Insert(j, rowArray);
                        }
                        String rowArray2 = "7:00 - 8:00 am";
                        dataGridView2.Rows.Insert(0, rowArray2);
                        rowArray2 = "8:00 - 9:00 am";
                        dataGridView2.Rows.Insert(1, rowArray2);
                        rowArray2 = "9:00 - 10:00 am";
                        dataGridView2.Rows.Insert(2, rowArray2);
                        rowArray2 = "10:00 - 11:00 am";
                        dataGridView2.Rows.Insert(3, rowArray2);
                        rowArray2 = "11:00 - 12:00 pm";
                        dataGridView2.Rows.Insert(4, rowArray2);
                        rowArray2 = "12:00 - 1:00 pm";
                        dataGridView2.Rows.Insert(5, rowArray2);
                        rowArray2 = "1:00 - 2:00 pm";
                        dataGridView2.Rows.Insert(6, rowArray2);
                        rowArray2 = "2:00 - 3:00 pm";
                        dataGridView2.Rows.Insert(7, rowArray2);

                        cont = 1;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        private void Limpiar()
        {
            labelDPersonal.BackColor = SystemColors.AppWorkspace;
            labelHorario.BackColor = SystemColors.AppWorkspace;
            labelCalif.BackColor = SystemColors.AppWorkspace;
            labelReticula.BackColor = SystemColors.AppWorkspace;
            labelEvaluacion.BackColor = SystemColors.AppWorkspace;
            opc = 0;

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox1.Visible = false;
            btnsaveName.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            textBox2.Visible = false;
            label12.Visible = false;
            textBox3.Visible = false;
            btnsavePass.Visible = false;
            label18.Visible = false;
            label19.Visible = false;

            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            label20.Visible = false;
            label21.Visible = false;

            label17.Visible = false;
            comboBox1.Visible = false;
            dataGridView4.Visible = false;
            label22.Visible = false;
            label23.Visible = false;

            dataGridView3.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;

            label29.Visible = false;
            labelDia.Visible = false;
            labelDiag.Visible = false;
            labelMes.Visible = false;
            labelanio.Visible = false;
            btnRealizar.Visible = false;
            label30.Visible = false;
            tabControl1.Visible = false;

            abierto = abierto2 = abierto3 = abierto4 = abierto5 = false;

            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;
            label28.Visible = false;

        }

        private void labelHorario_BackColorChanged(object sender, EventArgs e)
        {

            if (opc == 2)
            {
                labelHorario.BackColor = SystemColors.ActiveCaption;
            }
        }

        private async void btnsaveName_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                HttpClient client = new HttpClient();
                String content = await client.GetStringAsync("http://localhost/WSProyecto/findUsuarioAlumno.php?noControl=" + usuario.ToString());
                try
                {
                    JObject jsonObject = JObject.Parse(content);
                    JArray jOutput = (JArray)jsonObject.GetValue("output");

                    foreach (JObject json in jOutput)
                    {
                        HttpClient client2 = new HttpClient();

                        string id_alumno = json.GetValue("id_alumno").ToString();
                        string nombre = textBox1.Text;

                        String content2 = await client2.GetStringAsync("http://localhost/WSProyecto/actualizarName.php?id_alumno=" + id_alumno + "&nombre=" + nombre);

                        MessageBox.Show("Guardado correctamente");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                    MessageBox.Show("Error");
                }
                label2.Text = textBox1.Text;
                textBox1.Clear();
            }
        }

        private void labelDPersonal_BackColorChanged(object sender, EventArgs e)
        {
            if (opc == 1)
            {
                labelDPersonal.BackColor = SystemColors.ActiveCaption;
            }
        }

        private async void btnsavePass_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (textBox3.Text != "")
                {
                    HttpClient client = new HttpClient();
                    String content = await client.GetStringAsync("http://localhost/WSProyecto/findUsuarioAlumno.php?noControl=" + usuario.ToString());
                    try
                    {
                        JObject jsonObject = JObject.Parse(content);
                        JArray jOutput = (JArray)jsonObject.GetValue("output");

                        foreach (JObject json in jOutput)
                        {
                            if (json.GetValue("password").ToString() == textBox2.Text)
                            {
                                HttpClient client2 = new HttpClient();

                                string id_alumno = json.GetValue("id_alumno").ToString();
                                string password = textBox3.Text;
                                Console.WriteLine( id_alumno + password);

                                String content2 = await client2.GetStringAsync("http://localhost/WSProyecto/actualizarPass.php?id_alumno="+id_alumno+"&password="+password);

                                label13.Visible = false;
                                textBox2.Clear();
                                textBox3.Clear();
                                MessageBox.Show("Guardado correctamente");
                            }
                            else
                            {
                                label13.Visible = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        private async void labelReticula_Click(object sender, EventArgs e)
        {
            while (abierto4 == false)
            {
                Limpiar();
                abierto4 = true;

                label1_MouseEnter(sender, e);
                label1_MouseLeave(sender, e);
                label2_MouseEnter(sender, e);
                label2_MouseLeave(sender, e);
                label3_MouseEnter(sender, e);
                label3_MouseLeave(sender, e);
                labelEvaluacion_MouseEnter(sender, e);
                labelEvaluacion_MouseLeave(sender, e);

                labelReticula.BackColor = SystemColors.ActiveCaption;
                opc = 4;

                dataGridView3.Visible = true;
                panel2.Visible = true;
                panel3.Visible = true;
                panel4.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label16.Visible = true;

                if (cont2 == 0)
                {
                    for (int r = 0; r < 8; r++)
                    {
                        dataGridView3.Rows.Add();
                    }
                    HttpClient client = new HttpClient();
                    String content = await client.GetStringAsync("http://localhost/WSProyecto/findUsuarioAlumno.php?noControl=" + usuario.ToString());

                    JObject jsonObject = JObject.Parse(content);
                    JArray jOutput = (JArray)jsonObject.GetValue("output");

                    foreach (JObject jsonAl in jOutput)
                    {
                        
                        HttpClient client2 = new HttpClient();
                        String content2 = await client2.GetStringAsync("http://localhost/WSProyecto/consultarMaterias.php");

                        JObject jsonObject2 = JObject.Parse(content2);
                        JArray jOutput2 = (JArray)jsonObject2.GetValue("output");

                        int semestreActual = (int)jsonAl.GetValue("id_semestre");
                        int contm = 0;
                        int semAnt = 0;

                        foreach (JObject json2 in jOutput2)
                        {
                            if ((int)json2.GetValue("id_semestre") == semestreActual)
                            {
                                if (semAnt != (int)json2.GetValue("id_semestre"))
                                {
                                    contm = 0;
                                    String row = json2.GetValue("nombre_materia").ToString();
                                    dataGridView3.Rows[contm].Cells[semestreActual - 1].Value = row;
                                    dataGridView3.Rows[contm].Cells[semestreActual - 1].Style.BackColor = Color.MediumOrchid;
                                    contm++;
                                }
                                else
                                {
                                    String row = json2.GetValue("nombre_materia").ToString();
                                    dataGridView3.Rows[contm].Cells[semestreActual - 1].Value = row;
                                    dataGridView3.Rows[contm].Cells[semestreActual - 1].Style.BackColor = Color.MediumOrchid;
                                    contm++;
                                }
                            }
                            else
                            {
                                if ((int)json2.GetValue("id_semestre") < semestreActual)
                                {
                                    int semestre_anteriores = (int)json2.GetValue("id_semestre");
                                    if (semAnt != (int)json2.GetValue("id_semestre"))
                                    {
                                        contm = 0;
                                        String row = json2.GetValue("nombre_materia").ToString();
                                        dataGridView3.Rows[contm].Cells[semestre_anteriores - 1].Value = row;
                                        dataGridView3.Rows[contm].Cells[semestre_anteriores - 1].Style.BackColor = Color.SpringGreen;
                                        contm++;
                                    }
                                    else
                                    {
                                        String row = json2.GetValue("nombre_materia").ToString();
                                        dataGridView3.Rows[contm].Cells[semestre_anteriores - 1].Value = row;
                                        dataGridView3.Rows[contm].Cells[semestre_anteriores - 1].Style.BackColor = Color.SpringGreen;
                                        contm++;
                                    }
                                }
                                else
                                {
                                    if ((int)json2.GetValue("id_semestre") > semestreActual)
                                    {
                                        int semestre_next = (int)json2.GetValue("id_semestre");
                                        if (semAnt != (int)json2.GetValue("id_semestre"))
                                        {
                                            contm = 0;
                                            String row = json2.GetValue("nombre_materia").ToString();
                                            dataGridView3.Rows[contm].Cells[semestre_next - 1].Value = row;
                                            dataGridView3.Rows[contm].Cells[semestre_next - 1].Style.BackColor = Color.Silver;
                                            contm++;
                                        }
                                        else
                                        {
                                            String row = json2.GetValue("nombre_materia").ToString();
                                            dataGridView3.Rows[contm].Cells[semestre_next - 1].Value = row;
                                            dataGridView3.Rows[contm].Cells[semestre_next - 1].Style.BackColor = Color.Silver;
                                            contm++;
                                        }
                                    }
                                }
                            }
                            semAnt = (int)json2.GetValue("id_semestre");
                        }
                    }
                    cont2 = 1;
                }
            }
        }

        private void labelReticula_BackColorChanged(object sender, EventArgs e)
        {
            if (opc == 4)
            {
                labelReticula.BackColor = SystemColors.ActiveCaption;
            }
        }

        private async void labelCalif_Click(object sender, EventArgs e)
        {
            while (abierto3 == false)
            {
                Limpiar();
                abierto3 = true;

                label1_MouseEnter(sender, e);
                label1_MouseLeave(sender, e);
                label2_MouseEnter(sender, e);
                label2_MouseLeave(sender, e);
                label4_MouseEnter(sender, e);
                label4_MouseLeave(sender, e);
                labelEvaluacion_MouseEnter(sender, e);
                labelEvaluacion_MouseLeave(sender, e);

                labelCalif.BackColor = SystemColors.ActiveCaption;
                opc = 3;

                label17.Visible = true;
                comboBox1.Visible = true;
                label22.Visible = true;
                label23.Visible = true;

                comboBox1.Items.Clear();

                HttpClient client = new HttpClient();
                String content = await client.GetStringAsync("http://localhost/WSProyecto/findUsuarioAlumno.php?noControl=" + usuario.ToString());
                try
                {
                    JObject jo = JObject.Parse(content);
                    JArray ja = (JArray)jo.GetValue("output");
                    foreach(JObject json in ja)
                    {
                        int semestre = (int) json.GetValue("id_semestre");
                        for (int i = 1; i < semestre; i++)
                        {
                            String semItem = i.ToString();
                            comboBox1.Items.Add(semItem);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                    MessageBox.Show("Error");
                }
            }
        }

        private void labelCalif_BackColorChanged(object sender, EventArgs e)
        {
            if (opc == 3)
            {
                labelCalif.BackColor = SystemColors.ActiveCaption;
            }
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView4.Visible = true;
            dataGridView4.Rows.Clear();
            String mostrar = comboBox1.GetItemText(comboBox1.SelectedItem);
            String[] array = new String[53];
            JObject json2;

            switch (mostrar)
            {
                case "1":
                    dataGridView4.Rows.Insert(0, 8);    

                    HttpClient client = new HttpClient();
                    String content = await client.GetStringAsync("http://localhost/WSProyecto/consultarCalificaciones.php?primera=1&ultima=8&id_alumno=" + usuario.ToString());
                    JObject jo = JObject.Parse(content);
                    JArray ja = (JArray)jo.GetValue("output");

                    for (int i = 0; i < 8; i++)
                    {
                        json2 = (JObject)ja[i];
                        dataGridView4.Rows[i].Cells[1].Value = json2.GetValue("calif").ToString();
                    }

                    dataGridView4.Rows[0].Cells[0].Value = "Calculo diferencial";
                    dataGridView4.Rows[1].Cells[0].Value = "Fundamentos de programacion";
                    dataGridView4.Rows[2].Cells[0].Value = "Taller de etica";
                    dataGridView4.Rows[3].Cells[0].Value = "Matematicas discretas";
                    dataGridView4.Rows[4].Cells[0].Value = "Taller de administracion";
                    dataGridView4.Rows[5].Cells[0].Value = "Fundamentos de investigacion";
                    dataGridView4.Rows[6].Cells[0].Value = "Tutoria";
                    dataGridView4.Rows[7].Cells[0].Value = "Actividades fisicas";

                    break;

                case "2":
                    dataGridView4.Rows.Insert(0, 7);

                    HttpClient client2 = new HttpClient();
                    String content2 = await client2.GetStringAsync("http://localhost/WSProyecto/consultarCalificaciones.php?primera=9&ultima=15&id_alumno=" + usuario.ToString());
                    JObject jo2 = JObject.Parse(content2);
                    JArray ja2 = (JArray)jo2.GetValue("output");

                    for (int i = 0; i < 7; i++)
                    {
                        json2 = (JObject)ja2[i];
                        dataGridView4.Rows[i].Cells[1].Value = json2.GetValue("calif").ToString();
                    }

                    dataGridView4.Rows[0].Cells[0].Value = "Calculo integral";
                    dataGridView4.Rows[1].Cells[0].Value = "Programacion orientada a objetos";
                    dataGridView4.Rows[2].Cells[0].Value = "Contabilidad financiera";
                    dataGridView4.Rows[3].Cells[0].Value = "Quimica";
                    dataGridView4.Rows[4].Cells[0].Value = "Algebra lineal";
                    dataGridView4.Rows[5].Cells[0].Value = "Probabilidad y estadistica";
                    dataGridView4.Rows[6].Cells[0].Value = "Musica y artes";

                    break;

                case "3":
                    dataGridView4.Rows.Insert(0, 6);

                    HttpClient client3 = new HttpClient();
                    String content3 = await client3.GetStringAsync("http://localhost/WSProyecto/consultarCalificaciones.php?primera=16&ultima=21&id_alumno=" + usuario.ToString());
                    JObject jo3 = JObject.Parse(content3);
                    JArray ja3 = (JArray)jo3.GetValue("output");

                    for (int i = 0; i < 6; i++)
                    {
                        json2 = (JObject)ja3[i];
                        dataGridView4.Rows[i].Cells[1].Value = json2.GetValue("calif").ToString();
                    }

                    dataGridView4.Rows[0].Cells[0].Value = "Calculo vectorial";
                    dataGridView4.Rows[1].Cells[0].Value = "Estructura de datos";
                    dataGridView4.Rows[2].Cells[0].Value = "Cultura empresarial";
                    dataGridView4.Rows[3].Cells[0].Value = "Investigacion de operaciones";
                    dataGridView4.Rows[4].Cells[0].Value = "Desarrollo sustentable";
                    dataGridView4.Rows[5].Cells[0].Value = "Fisica general";

                    break;

                case "4":
                    dataGridView4.Rows.Insert(0, 6);

                    HttpClient client4 = new HttpClient();
                    String content4 = await client4.GetStringAsync("http://localhost/WSProyecto/consultarCalificaciones.php?primera=22&ultima=27&id_alumno=" + usuario.ToString());
                    JObject jo4 = JObject.Parse(content4);
                    JArray ja4 = (JArray)jo4.GetValue("output");

                    for (int i = 0; i < 6; i++)
                    {
                        json2 = (JObject)ja4[i];
                        dataGridView4.Rows[i].Cells[1].Value = json2.GetValue("calif").ToString();
                    }

                    dataGridView4.Rows[0].Cells[0].Value = "Ecuaciones diferenciales";
                    dataGridView4.Rows[1].Cells[0].Value = "Metodos numericos";
                    dataGridView4.Rows[2].Cells[0].Value = "Topicos avanzados de programacion";
                    dataGridView4.Rows[3].Cells[0].Value = "Fundamentos de base de datos";
                    dataGridView4.Rows[4].Cells[0].Value = "Simulacion";
                    dataGridView4.Rows[5].Cells[0].Value = "Principios electricos";

                    break;

                case "5":
                    dataGridView4.Rows.Insert(0, 6);

                    HttpClient client5 = new HttpClient();
                    String content5 = await client5.GetStringAsync("http://localhost/WSProyecto/consultarCalificaciones.php?primera=28&ultima=33&id_alumno=" + usuario.ToString());
                    JObject jo5 = JObject.Parse(content5);
                    JArray ja5 = (JArray)jo5.GetValue("output");

                    for (int i = 0; i < 6; i++)
                    {
                        json2 = (JObject)ja5[i];
                        dataGridView4.Rows[i].Cells[1].Value = json2.GetValue("calif").ToString();
                    }

                    dataGridView4.Rows[0].Cells[0].Value = "Graficacion";
                    dataGridView4.Rows[1].Cells[0].Value = "Fundamentos de telecomunicaciones";
                    dataGridView4.Rows[2].Cells[0].Value = "Sistemas operativos";
                    dataGridView4.Rows[3].Cells[0].Value = "Taller de bases de datos";
                    dataGridView4.Rows[4].Cells[0].Value = "Fundamentos de ingenieria de software";
                    dataGridView4.Rows[5].Cells[0].Value = "Sistemas programables";

                    break;

                case "6":
                    dataGridView4.Rows.Insert(0, 6);

                    HttpClient client6 = new HttpClient();
                    String content6 = await client6.GetStringAsync("http://localhost/WSProyecto/consultarCalificaciones.php?primera=34&ultima=39&id_alumno=" + usuario.ToString());
                    JObject jo6 = JObject.Parse(content6);
                    JArray ja6 = (JArray)jo6.GetValue("output");

                    for (int i = 0; i < 6; i++)
                    {
                        json2 = (JObject)ja6[i];
                        dataGridView4.Rows[i].Cells[1].Value = json2.GetValue("calif").ToString();
                    }

                    dataGridView4.Rows[0].Cells[0].Value = "Lenguajes y automatas I";
                    dataGridView4.Rows[1].Cells[0].Value = "Redes de computadoras";
                    dataGridView4.Rows[2].Cells[0].Value = "Taller de sistemas operativos";
                    dataGridView4.Rows[3].Cells[0].Value = "Administracion de bases de datos";
                    dataGridView4.Rows[4].Cells[0].Value = "Ingenieria de software";
                    dataGridView4.Rows[5].Cells[0].Value = "Programacion web";

                    break;

                case "7":
                    dataGridView4.Rows.Insert(0, 6);

                    HttpClient client7 = new HttpClient();
                    String content7 = await client7.GetStringAsync("http://localhost/WSProyecto/consultarCalificaciones.php?primera=40&ultima=45&id_alumno=" + usuario.ToString());
                    JObject jo7 = JObject.Parse(content7);
                    JArray ja7 = (JArray)jo7.GetValue("output");

                    for (int i = 0; i < 6; i++)
                    {
                        json2 = (JObject)ja7[i];
                        dataGridView4.Rows[i].Cells[1].Value = json2.GetValue("calif").ToString();
                    }

                    dataGridView4.Rows[0].Cells[0].Value = "Lenguajes y automatas II";
                    dataGridView4.Rows[1].Cells[0].Value = "Conmutacion y Enrutamiento en Redes de Datos";
                    dataGridView4.Rows[2].Cells[0].Value = "Taller de Investigacion I";
                    dataGridView4.Rows[3].Cells[0].Value = "Gestion de Proyectos de Software";
                    dataGridView4.Rows[4].Cells[0].Value = "Arquitectura de Computadoras";
                    dataGridView4.Rows[5].Cells[0].Value = "Actividades Complementarias";

                    break;

                case "8":
                    dataGridView4.Rows.Insert(0, 6);

                    HttpClient client8 = new HttpClient();
                    String content8 = await client8.GetStringAsync("http://localhost/WSProyecto/consultarCalificaciones.php?primera=46&ultima=51&id_alumno=" + usuario.ToString());
                    JObject jo8 = JObject.Parse(content8);
                    JArray ja8 = (JArray)jo8.GetValue("output");

                    for (int i = 0; i < 6; i++)
                    {
                        json2 = (JObject)ja8[i];
                        dataGridView4.Rows[i].Cells[1].Value = json2.GetValue("calif").ToString();
                    }

                    dataGridView4.Rows[0].Cells[0].Value = "Programacion Logica y Funcional";
                    dataGridView4.Rows[1].Cells[0].Value = "Administracion de Redes";
                    dataGridView4.Rows[2].Cells[0].Value = "Taller de Investigacion II";
                    dataGridView4.Rows[3].Cells[0].Value = "Lenguajes de Interfaz";
                    dataGridView4.Rows[4].Cells[0].Value = "Servicio Social";
                    dataGridView4.Rows[5].Cells[0].Value = "Lab Lenguajes de Interfaz";

                    break;

                case "9":
                    dataGridView4.Rows.Insert(0, 2);

                    HttpClient client9 = new HttpClient();
                    String content9 = await client9.GetStringAsync("http://localhost/WSProyecto/consultarCalificaciones.php?primera=52&ultima=53&id_alumno=" + usuario.ToString());
                    JObject jo9 = JObject.Parse(content9);
                    JArray ja9 = (JArray)jo9.GetValue("output");

                    for (int i = 0; i < 2; i++)
                    {
                        json2 = (JObject)ja9[i];
                        dataGridView4.Rows[i].Cells[1].Value = json2.GetValue("calif").ToString();
                    }

                    dataGridView4.Rows[0].Cells[0].Value = "Inteligencia Artificial";
                    dataGridView4.Rows[1].Cells[0].Value = "Residencia Profesional";

                    break;
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            label24.Visible = true;
            label25.Text = usuario;
            label25.Visible = true;
            label26.Visible = true;
            label27.Visible = true;
            label28.Visible = true;
        }

        private void labelEvaluacion_Click(object sender, EventArgs e)
        {
            while (abierto5 == false)
            {
                Limpiar();
                abierto5 = true;

                labelEvaluacion.BackColor = SystemColors.ActiveCaption;
                opc = 5;

                label1_MouseEnter(sender, e);
                label1_MouseLeave(sender, e);
                label2_MouseEnter(sender, e);
                label2_MouseLeave(sender, e);
                label3_MouseEnter(sender, e);
                label3_MouseLeave(sender, e);
                label4_MouseEnter(sender, e);
                label4_MouseLeave(sender, e);

                label29.Visible = true;
                labelDia.Visible = true;
                labelDiag.Visible = true;
                labelMes.Visible = true;
                labelanio.Visible = true;
                btnRealizar.Visible = true;

            }
        }

        private void labelEvaluacion_BackColorChanged(object sender, EventArgs e)
        {
            if (opc == 5)
            {
                labelEvaluacion.BackColor = SystemColors.ActiveCaption;
            }
        }

        private void labelEvaluacion_MouseEnter(object sender, EventArgs e)
        {
            labelEvaluacion.BackColor = SystemColors.ActiveCaption;
        }

        private void labelEvaluacion_MouseLeave(object sender, EventArgs e)
        {
            labelEvaluacion.BackColor = SystemColors.AppWorkspace;
        }

        private async void btnRealizar_Click(object sender, EventArgs e)
        {
            String day = DateTime.Today.Day.ToString();
            String month = DateTime.Today.Month.ToString();

            int cont = 0;
            int dia = int.Parse(day);
            int diaLabel = int.Parse(labelDia.Text.ToString());
            int mes = int.Parse(month);
            int mesLabel = int.Parse(labelMes.Text.ToString());

            if (mes <= mesLabel)
            {
                if (dia <= diaLabel)
                {
                    HttpClient client = new HttpClient();
                    String content = await client.GetStringAsync("http://localhost/WSProyecto/findUsuarioAlumno.php?noControl=" + usuario.ToString());
                    try
                    {
                        JObject jsonObject = JObject.Parse(content);
                        JArray jOutput = (JArray)jsonObject.GetValue("output");

                        foreach (JObject json in jOutput)
                        {
                            if ((int)json.GetValue("evaluacionDoc") == 0)
                            {
                                semest = json.GetValue("id_semestre").ToString();
                                id = (int)json.GetValue("id_alumno");

                                HttpClient client2 = new HttpClient();
                                String content2 = await client2.GetStringAsync("http://localhost/WSProyecto/consultarMaestros.php" );
                                
                                JObject jsonObject2 = JObject.Parse(content2);
                                JArray jOutput2 = (JArray)jsonObject2.GetValue("output");
                                
                                foreach (JObject jsonP in jOutput2)
                                {
                                    int idmaestro_materia = (int)jsonP.GetValue("id_materia");

                                    HttpClient client3 = new HttpClient();
                                    String content3 = await client3.GetStringAsync("http://localhost/WSProyecto/consultarMaterias.php");

                                    JObject jsonObject3 = JObject.Parse(content3);
                                    JArray jOutput3 = (JArray)jsonObject3.GetValue("output");

                                    foreach (JObject jsonMaterias in jOutput3)
                                    {
                                        if (idmaestro_materia == (int)jsonMaterias.GetValue("id_materia"))
                                        {
                                            if (semest == jsonMaterias.GetValue("id_semestre").ToString())
                                            {
                                                if (cont == 0)
                                                {
                                                    maestro = (int)jsonP.GetValue("id_maestro");
                                                    label51.Text = label52.Text = label53.Text = label54.Text
                                                    = label55.Text = label56.Text = label57.Text = label58.Text
                                                    = label59.Text = label60.Text = jsonP.GetValue("nombre").ToString();
                                                }
                                                if (cont == 1)
                                                {
                                                    maestro2 = (int)jsonP.GetValue("id_maestro");
                                                    label61.Text = label62.Text = label63.Text = label64.Text
                                                    = label65.Text = label66.Text = label67.Text = label68.Text
                                                    = label69.Text = label70.Text = jsonP.GetValue("nombre").ToString();
                                                }
                                                cont++;
                                                break;
                                            }
                                        }
                                    }
                                    if (cont == 2)
                                    {
                                        break;
                                    }

                                }

                                if (cont == 0)
                                {
                                    MessageBox.Show("No existen maestros para realizar la evaluacion docente");
                                }
                                else
                                {
                                    label29.Visible = false;
                                    labelDia.Visible = false;
                                    labelDiag.Visible = false;
                                    labelMes.Visible = false;
                                    labelanio.Visible = false;
                                    btnRealizar.Visible = false;

                                    tabControl1.Visible = true;
                                    tabPage2.Parent = tabPage3.Parent = tabPage4.Parent = tabPage5.Parent
                                       = tabPage6.Parent = tabPage7.Parent = tabPage8.Parent = tabPage9.Parent
                                       = tabPage10.Parent = null;
                                }

                            }
                            else
                            {
                                label30.Visible = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                        MessageBox.Show("Error");
                    }
                }
                else
                {
                    label30.Visible = true;
                }
            }
            else
            {
                label30.Visible = true;
            }
        }


        public void mostrarRubro(int r)
        {
            int rubro = r;
            switch (rubro)
            {
                case 1:
                    {
                        tabPage1.Parent = null;
                        tabPage2.Parent = tabControl1;
                        break;
                    }
                case 2:
                    {
                        tabPage2.Parent = null;
                        tabPage3.Parent = tabControl1;
                        break;
                    }
                case 3:
                    {
                        tabPage3.Parent = null;
                        tabPage4.Parent = tabControl1;
                        break;
                    }
                case 4:
                    {
                        tabPage4.Parent = null;
                        tabPage5.Parent = tabControl1;
                        break;
                    }
                case 5:
                    {
                        tabPage5.Parent = null;
                        tabPage6.Parent = tabControl1;
                        break;
                    }
                case 6:
                    {
                        tabPage6.Parent = null;
                        tabPage7.Parent = tabControl1;
                        break;
                    }
                case 7:
                    {
                        tabPage7.Parent = null;
                        tabPage8.Parent = tabControl1;
                        break;
                    }
                case 8:
                    {
                        tabPage8.Parent = null;
                        tabPage9.Parent = tabControl1;
                        break;
                    }
                case 9:
                    {
                        tabPage9.Parent = null;
                        tabPage10.Parent = tabControl1;
                        break;
                    }
                case 10:
                    {
                        tabPage10.Parent = null;
                        tabPage1.Parent = tabControl1;
                        tabControl1.Visible = false;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void btnRubro1_Click(object sender, EventArgs e)
        {
            mostrarRubro(1);
        }

        private void btnRubro2_Click(object sender, EventArgs e)
        {
            mostrarRubro(2);
        }

        private void btnRubro3_Click(object sender, EventArgs e)
        {
            mostrarRubro(3);
        }

        private void btnRubro4_Click(object sender, EventArgs e)
        {
            mostrarRubro(4);
        }

        private void btnRubro5_Click(object sender, EventArgs e)
        {
            mostrarRubro(5);
        }

        private void btnRubro6_Click(object sender, EventArgs e)
        {
            mostrarRubro(6);
        }

        private void btnRubro7_Click(object sender, EventArgs e)
        {
            mostrarRubro(7);
        }

        private void btnRubro8_Click(object sender, EventArgs e)
        {
            mostrarRubro(8);
        }

        private void btnRubro9_Click(object sender, EventArgs e)
        {
            mostrarRubro(9);
        }

        private async void btnRubro10_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();

            NameValueCollection postData = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro.ToString() },
                { "calific", numericUpDown1.Value.ToString() },
                { "coment", null },
                { "id_rubro", "1"},
            };

            string pageSource = Encoding.UTF8.GetString(client.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postData));

            WebClient client2 = new WebClient();

            NameValueCollection postData2 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro.ToString() },
                { "calific", numericUpDown3.Value.ToString() },
                { "coment", null },
                { "id_rubro", "2"},
            };

            string pageSource2 = Encoding.UTF8.GetString(client2.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postData2));

            WebClient client3 = new WebClient();

            NameValueCollection postData3 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro.ToString() },
                { "calific", numericUpDown5.Value.ToString() },
                { "coment", null },
                { "id_rubro", "3"},
            };

            string pageSource3 = Encoding.UTF8.GetString(client3.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postData3));

            WebClient client4 = new WebClient();

            NameValueCollection postData4 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro.ToString() },
                { "calific", numericUpDown7.Value.ToString() },
                { "coment", null },
                { "id_rubro", "4"},
            };

            string pageSource4 = Encoding.UTF8.GetString(client4.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postData4));

            WebClient client5 = new WebClient();

            NameValueCollection postData5 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro.ToString() },
                { "calific", numericUpDown9.Value.ToString() },
                { "coment", null },
                { "id_rubro", "5"},
            };

            string pageSource5 = Encoding.UTF8.GetString(client5.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postData5));

            WebClient client6 = new WebClient();

            NameValueCollection postData6 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro.ToString() },
                { "calific", numericUpDown11.Value.ToString() },
                { "coment", null },
                { "id_rubro", "6"},
            };

            string pageSource6 = Encoding.UTF8.GetString(client6.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postData6));

            WebClient client7 = new WebClient();

            NameValueCollection postData7 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro.ToString() },
                { "calific", numericUpDown13.Value.ToString() },
                { "coment", null },
                { "id_rubro", "7"},
            };

            string pageSource7 = Encoding.UTF8.GetString(client7.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postData7));

            WebClient client8 = new WebClient();

            NameValueCollection postData8 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro.ToString() },
                { "calific", numericUpDown15.Value.ToString() },
                { "coment", null },
                { "id_rubro", "8"},
            };

            string pageSource8 = Encoding.UTF8.GetString(client8.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postData8));

            WebClient client9 = new WebClient();

            NameValueCollection postData9 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro.ToString() },
                { "calific", numericUpDown17.Value.ToString() },
                { "coment", null },
                { "id_rubro", "9"},
            };

            string pageSource9 = Encoding.UTF8.GetString(client9.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postData9));

            WebClient client10 = new WebClient();

            NameValueCollection postData10 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro.ToString() },
                { "calific", null},
                { "coment", textBox4.Text },
                { "id_rubro", "10"},
            };

            string pageSource10 = Encoding.UTF8.GetString(client10.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postData10));


            WebClient clientt = new WebClient();

            NameValueCollection postDataa = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro2.ToString() },
                { "calific", numericUpDown2.Value.ToString() },
                { "coment", null },
                { "id_rubro", "1"},
            };

            string pageSourcee = Encoding.UTF8.GetString(clientt.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postDataa));

            WebClient clientt2 = new WebClient();

            NameValueCollection postDataa2 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro2.ToString() },
                { "calific", numericUpDown4.Value.ToString() },
                { "coment", null },
                { "id_rubro", "2"},
            };

            string pageSourcee2 = Encoding.UTF8.GetString(clientt2.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postDataa2));

            WebClient clientt3 = new WebClient();

            NameValueCollection postDataa3 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro2.ToString() },
                { "calific", numericUpDown6.Value.ToString() },
                { "coment", null },
                { "id_rubro", "3"},
            };

            string pageSourcee3 = Encoding.UTF8.GetString(clientt3.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postDataa3));

            WebClient clientt4 = new WebClient();

            NameValueCollection postDataa4 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro2.ToString() },
                { "calific", numericUpDown8.Value.ToString() },
                { "coment", null },
                { "id_rubro", "4"},
            };

            string pageSourcee4 = Encoding.UTF8.GetString(clientt4.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postDataa4));

            WebClient clientt5 = new WebClient();

            NameValueCollection postDataa5 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro2.ToString() },
                { "calific", numericUpDown10.Value.ToString() },
                { "coment", null },
                { "id_rubro", "5"},
            };

            string pageSourcee5 = Encoding.UTF8.GetString(clientt5.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postDataa5));

            WebClient clientt6 = new WebClient();

            NameValueCollection postDataa6 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro2.ToString() },
                { "calific", numericUpDown12.Value.ToString() },
                { "coment", null },
                { "id_rubro", "6"},
            };

            string pageSourcee6 = Encoding.UTF8.GetString(clientt6.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postDataa6));

            WebClient clientt7 = new WebClient();

            NameValueCollection postDataa7 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro2.ToString() },
                { "calific", numericUpDown14.Value.ToString() },
                { "coment", null },
                { "id_rubro", "7"},
            };

            string pageSourcee7 = Encoding.UTF8.GetString(clientt7.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postDataa7));

            WebClient clientt8 = new WebClient();

            NameValueCollection postDataa8 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro2.ToString() },
                { "calific", numericUpDown16.Value.ToString() },
                { "coment", null },
                { "id_rubro", "8"},
            };

            string pageSourcee8 = Encoding.UTF8.GetString(clientt8.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postDataa8));

            WebClient clientt9 = new WebClient();

            NameValueCollection postDataa9 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro2.ToString() },
                { "calific", numericUpDown18.Value.ToString() },
                { "coment", null },
                { "id_rubro", "9"},
            };

            string pageSourcee9 = Encoding.UTF8.GetString(clientt9.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postDataa9));

            WebClient clientt10 = new WebClient();

            NameValueCollection postDataa10 = new NameValueCollection()
            {
                { "id_alumno", id.ToString() },
                { "id_maestro", maestro2.ToString() },
                { "calific", null},
                { "coment", textBox5.Text },
                { "id_rubro", "10"},
            };

            string pageSourcee10 = Encoding.UTF8.GetString(clientt10.UploadValues("http://localhost/WSProyecto/agregarEvalDoc.php", postDataa10));

            HttpClient cliente = new HttpClient();
            String content = await cliente.GetStringAsync("http://localhost/WSProyecto/findUsuarioAlumno.php?noControl=" + usuario.ToString());
            try
            {
                JObject jsonObject = JObject.Parse(content);
                JArray jOutput = (JArray)jsonObject.GetValue("output");

                foreach (JObject json in jOutput)
                {
                    HttpClient cliente2 = new HttpClient();

                    string id_alumno = json.GetValue("id_alumno").ToString();
                    
                    String content2 = await cliente2.GetStringAsync("http://localhost/WSProyecto/actualizarEvaluacionDoc.php?id_alumno=" + id_alumno + "&evaluacionDoc=" + "1");
                }
                MessageBox.Show("Tu evaluacion docente se registro exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                MessageBox.Show("Error");
            }

            mostrarRubro(10);
            label29.Visible = true;
            labelDia.Visible = true;
            labelDiag.Visible = true;
            labelMes.Visible = true;
            labelanio.Visible = true;
            btnRealizar.Visible = true;

            numericUpDown1.Value = numericUpDown2.Value = numericUpDown3.Value = numericUpDown4.Value =
                numericUpDown5.Value = numericUpDown6.Value = numericUpDown7.Value = numericUpDown8.Value =
                numericUpDown9.Value = numericUpDown10.Value = numericUpDown11.Value = numericUpDown12.Value =
                numericUpDown13.Value = numericUpDown14.Value = numericUpDown15.Value = numericUpDown16.Value =
                numericUpDown17.Value = numericUpDown18.Value = 1;

            textBox4.Clear();
            textBox5.Clear();
        }
    }
}